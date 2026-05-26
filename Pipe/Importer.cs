#nullable disable
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using System.Windows.Forms;

namespace Pipe
{
    public static class Importer
    {
        public struct ImportResult
        {
            public int TotalRows;
            public int Added;
            public int Skipped;
        }

        public static ImportResult ImportFromCsv(string filePath, int inspectionId, Pipeline pipe, IProgress<string> progress)
        {
            var result = new ImportResult();
            try
            {
                var encoding = DetectEncoding(filePath);
                var lines = File.ReadAllLines(filePath, encoding);
                if (lines.Length < 2) return result;

                var headers = lines[0].Split(GetSeparator(lines[0]));
                int idxDistance = FindHeader(headers, new[] { "distance_m", "distance", "m", "дистанция" });
                int idxAngle = FindHeader(headers, new[] { "clock", "angle_deg", "угол", "положение" });
                int idxType = FindHeader(headers, new[] { "defect_type", "type", "тип" });
                int idxDepth = FindHeader(headers, new[] { "depth_percent", "depth", "глубина" });
                int idxLength = FindHeader(headers, new[] { "length_mm", "length", "длина" });
                int idxWidth = FindHeader(headers, new[] { "width_mm", "width", "ширина" });

                if (idxDistance == -1 || idxDepth == -1)
                    throw new Exception("Файл не содержит обязательных столбцов: дистанция, глубина");

                var defectsToInsert = new List<Defect>();
                int skipped = 0;

                for (int i = 1; i < lines.Length; i++)
                {
                    string line = lines[i].Trim();
                    if (string.IsNullOrEmpty(line)) continue;
                    var parts = line.Split(GetSeparator(line));
                    if (parts.Length < Math.Max(idxDistance, idxDepth) + 1)
                    {
                        skipped++;
                        continue;
                    }

                    try
                    {
                        double distance = double.Parse(parts[idxDistance].Trim());
                        string angleStr = (idxAngle >= 0 && idxAngle < parts.Length) ? parts[idxAngle].Trim() : "0";
                        int angle = ParseAngle(angleStr);
                        string defectType = (idxType >= 0 && idxType < parts.Length) ? parts[idxType].Trim() : "CORR";
                        double depthPercent = double.Parse(parts[idxDepth].Trim());
                        double length = (idxLength >= 0 && idxLength < parts.Length) ? double.Parse(parts[idxLength].Trim()) : 0;
                        double width = (idxWidth >= 0 && idxWidth < parts.Length) ? double.Parse(parts[idxWidth].Trim()) : 0;

                        var calc = StrengthCalculator.Calculate(pipe, depthPercent, length, width);
                        var defect = new Defect
                        {
                            InspectionId = inspectionId,
                            DistanceM = distance,
                            AngleDeg = angle,
                            DefectType = defectType,
                            DepthPercent = depthPercent,
                            DepthMm = pipe.WallThicknessMm * depthPercent / 100.0,
                            LengthMm = length,
                            WidthMm = width,
                            AllowablePressureMpa = calc.allowablePressure,
                            Erf = calc.erf,
                            Severity = calc.severity
                        };
                        defectsToInsert.Add(defect);
                    }
                    catch
                    {
                        skipped++;
                    }

                    if (i % 100 == 0) progress?.Report($"Обработано {i} строк...");
                }

                int added = 0;
                using (var conn = new MySqlConnection(DatabaseHelper.GetConnectionString()))
                {
                    conn.Open();
                    string sql = @"INSERT INTO defects 
                        (inspection_id, distance_m, angle_deg, defect_type, depth_percent, depth_mm, length_mm, width_mm, 
                         allowable_pressure_mpa, erf, severity)
                        VALUES (@insp, @dist, @ang, @type, @depthPct, @depthMm, @len, @wid, @allow, @erf, @sev)";

                    foreach (var def in defectsToInsert)
                    {
                        using (var cmd = new MySqlCommand(sql, conn))
                        {
                            cmd.Parameters.AddWithValue("@insp", def.InspectionId);
                            cmd.Parameters.AddWithValue("@dist", def.DistanceM);
                            cmd.Parameters.AddWithValue("@ang", def.AngleDeg);
                            cmd.Parameters.AddWithValue("@type", def.DefectType);
                            cmd.Parameters.AddWithValue("@depthPct", def.DepthPercent);
                            cmd.Parameters.AddWithValue("@depthMm", def.DepthMm);
                            cmd.Parameters.AddWithValue("@len", def.LengthMm);
                            cmd.Parameters.AddWithValue("@wid", def.WidthMm);
                            cmd.Parameters.AddWithValue("@allow", def.AllowablePressureMpa);
                            cmd.Parameters.AddWithValue("@erf", def.Erf);
                            cmd.Parameters.AddWithValue("@sev", def.Severity);
                            cmd.ExecuteNonQuery();
                            added++;
                        }
                    }
                }

                result.TotalRows = lines.Length - 1;
                result.Added = added;
                result.Skipped = skipped;
                return result;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Не удалось импортировать файл с дефектами.\n\n" +
                    "Проверьте:\n" +
                    "• Формат файла (CSV с разделителями , ; или табуляция)\n" +
                    "• Наличие обязательных столбцов: дистанция, глубина\n" +
                    "• Корректность числовых значений\n\n" +
                    $"Техническая ошибка: {ex.Message}",
                    "Ошибка импорта",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return result;
            }
        }

        private static int FindHeader(string[] headers, string[] possibleNames)
        {
            for (int i = 0; i < headers.Length; i++)
                if (possibleNames.Contains(headers[i].ToLower()))
                    return i;
            return -1;
        }

        private static char GetSeparator(string line)
        {
            if (line.Contains(';')) return ';';
            if (line.Contains(',')) return ',';
            return '\t';
        }

        private static Encoding DetectEncoding(string file)
        {
            byte[] bom = new byte[4];
            using (var fs = new FileStream(file, FileMode.Open, FileAccess.Read))
            {
                fs.Read(bom, 0, 4);
            }
            if (bom[0] == 0x2b && bom[1] == 0x2f && bom[2] == 0x76) return Encoding.UTF8;
            if (bom[0] == 0xef && bom[1] == 0xbb && bom[2] == 0xbf) return Encoding.UTF8;
            if (bom[0] == 0xff && bom[1] == 0xfe) return Encoding.Unicode;
            return Encoding.GetEncoding(1251);
        }

        private static int ParseAngle(string s)
        {
            if (s.Contains(':'))
            {
                var parts = s.Split(':');
                if (int.TryParse(parts[0], out int hour))
                    return (hour % 12) * 30;
            }
            if (int.TryParse(s, out int deg))
                return deg % 360;
            return 0;
        }
    }
}