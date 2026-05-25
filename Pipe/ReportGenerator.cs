#nullable disable

using OfficeOpenXml;
using OfficeOpenXml.Style;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using System;
using System.Data;
using System.IO;
using static System.Net.Mime.MediaTypeNames;

namespace Pipe
{
    public static class ReportGenerator
    {
        public static void ExportToExcel(DataTable data, string filePath)
        {
            using (var package = new ExcelPackage())
            {
                var ws = package.Workbook.Worksheets.Add("Отчёт ВТД");
                ws.Cells["A1"].LoadFromDataTable(data, true);
                ws.Cells[1, 1, 1, data.Columns.Count].Style.Font.Bold = true;
                ws.Cells.AutoFitColumns();
                package.SaveAs(new FileInfo(filePath));
            }
        }

        public static void GenerateCriticalDefectsPdf(DataTable defects, Pipeline pipe, Inspection insp, string filePath)
        {
            using (PdfDocument doc = new PdfDocument())
            {
                PdfPage page = doc.AddPage();
                page.Width = 595;
                page.Height = 842;
                XGraphics gfx = XGraphics.FromPdfPage(page);
                XFont titleFont = new XFont("Arial", 16, XFontStyle.Bold);
                XFont headerFont = new XFont("Arial", 12, XFontStyle.Bold);
                XFont textFont = new XFont("Arial", 10, XFontStyle.Regular);

                gfx.DrawString("ПАО «Газпром»", titleFont, XBrushes.Black,
                    new XRect(0, 20, page.Width, 30), XStringFormats.TopCenter);
                gfx.DrawString("Акт анализа внутритрубной диагностики", titleFont, XBrushes.Black,
                    new XRect(0, 60, page.Width, 30), XStringFormats.TopCenter);
                gfx.DrawString($"Трубопровод: {pipe.Name}", textFont, XBrushes.Black, 40, 110);
                gfx.DrawString($"Инспекция от {insp.Date:dd.MM.yyyy}, прибор: {insp.ToolType}", textFont, XBrushes.Black, 40, 140);
                gfx.DrawString($"Проектное давление: {pipe.DesignPressureMpa} МПа, рабочее: {pipe.OperatingPressureMpa} МПа", textFont, XBrushes.Black, 40, 170);
                gfx.DrawString("Перечень критических дефектов (High / Critical)", headerFont, XBrushes.Black, 40, 210);

                int y = 250;
                int rowHeight = 20;
                string[] cols = { "Дист., м", "Угол,°", "Тип", "Глуб.,%", "ERF", "Категория" };
                for (int i = 0; i < cols.Length; i++)
                    gfx.DrawString(cols[i], headerFont, XBrushes.Black, 40 + i * 80, y);
                y += rowHeight;

                foreach (DataRow row in defects.Rows)
                {
                    if (y > page.Height - 50)
                    {
                        page = doc.AddPage();
                        gfx = XGraphics.FromPdfPage(page);
                        y = 40;
                    }
                    gfx.DrawString(row["distance_m"]?.ToString() ?? "", textFont, XBrushes.Black, 40, y);
                    gfx.DrawString(row["angle_deg"]?.ToString() ?? "", textFont, XBrushes.Black, 120, y);
                    gfx.DrawString(row["defect_type"]?.ToString() ?? "", textFont, XBrushes.Black, 200, y);
                    gfx.DrawString(row["depth_percent"]?.ToString() ?? "", textFont, XBrushes.Black, 280, y);
                    double erf = row["erf"] == DBNull.Value ? 0 : Convert.ToDouble(row["erf"]);
                    gfx.DrawString(erf.ToString("F4"), textFont, XBrushes.Black, 360, y);
                    gfx.DrawString(row["severity"]?.ToString() ?? "", textFont, XBrushes.Black, 440, y);
                    y += rowHeight;
                }
                doc.Save(filePath);
            }
        }
    }
}