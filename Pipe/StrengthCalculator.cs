#nullable disable
using System;
using System.Windows.Forms;

namespace Pipe
{
    public static class StrengthCalculator
    {
        public static (double allowablePressure, double erf, string severity) Calculate(Pipeline pipe, double depthPercent, double lengthMm, double widthMm)
        {
            try
            {
                if (pipe == null)
                    throw new ArgumentNullException(nameof(pipe), "Данные трубопровода не загружены");

                double t = pipe.WallThicknessMm;
                double d = t * depthPercent / 100.0;
                double D = pipe.DiameterMm;
                double sigma_t = pipe.YieldStrengthMpa;
                double L = lengthMm;
                double P_des = pipe.DesignPressureMpa;

                if (t <= 0 || D <= 0 || sigma_t <= 0 || P_des <= 0)
                    throw new InvalidOperationException("Некорректные параметры трубопровода (толщина, диаметр, прочность или давление)");

                // Формула СТО Газпром 2-2.3-112-2007 для одиночного коррозионного дефекта
                double term1 = (2.0 * sigma_t * (t - d)) / D;
                double term2 = (1 - L / (Math.PI * D));
                double term3 = (1 - (L / (Math.PI * D)) * (1 - d / t));
                double P_allow = term1 * term2 / term3;
                if (P_allow < 0) P_allow = 0;

                double erf = P_allow > 0 ? P_des / P_allow : 999;

                string severity;
                if (erf <= 0.7 && depthPercent < 10)
                    severity = "Low";
                else if (erf <= 0.9 || depthPercent < 30)
                    severity = "Medium";
                else if (erf <= 1.0 || depthPercent < 50)
                    severity = "High";
                else
                    severity = "Critical";

                return (P_allow, erf, severity);
            }
            catch (Exception ex)
            {
                string errorMsg = $"Ошибка при расчёте остаточной прочности дефекта.\n\n" +
                                  $"Инструкция:\n" +
                                  $"Проверьте параметры трубопровода:\n" +
                                  $"• Диаметр (D) должен быть > 0\n" +
                                  $"• Толщина стенки (t) должна быть > 0\n" +
                                  $"• Предел текучести должен быть > 0\n" +
                                  $"• Проектное давление должно быть > 0\n\n" +
                                  $"Техническая ошибка:\n{ex.Message}\n\n" +
                                  $"Стек вызовов:\n{ex.StackTrace}";

                MessageBox.Show(errorMsg, "Ошибка расчёта", MessageBoxButtons.OK, MessageBoxIcon.Error);
                AuditLogger.LogError(Program.CurrentUser ?? "system", "StrengthCalculator.Calculate", ex, $"depthPercent={depthPercent}, lengthMm={lengthMm}");
                return (0, 999, "Critical");
            }
        }

        /// <summary>
        /// Проверка периодичности инспекций (не реже 1 раза в 3 года)
        /// </summary>
        public static bool CheckInspectionPeriodicity(DateTime lastInspectionDate)
        {
            return (DateTime.Now - lastInspectionDate).TotalDays <= 365 * 3;
        }
    }
}