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

                double term1 = (2.0 * sigma_t * (t - d)) / D;
                double term2 = (1 - L / (Math.PI * D));
                double term3 = (1 - (L / (Math.PI * D)) * (1 - d / t));
                double P_allow = term1 * term2 / term3;
                if (P_allow < 0) P_allow = 0;

                double erf = P_des / P_allow;

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
                MessageBox.Show(
                    "Ошибка при расчёте остаточной прочности дефекта.\n\n" +
                    "Проверьте параметры трубопровода (диаметр, толщина, предел текучести, давление).\n\n" +
                    $"Техническая ошибка: {ex.Message}",
                    "Ошибка расчёта",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return (0, 999, "Critical");
            }
        }
    }
}