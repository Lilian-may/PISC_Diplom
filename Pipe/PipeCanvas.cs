#nullable disable
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Pipe
{
    public class PipeCanvas : Panel
    {
        private List<DefectDraw> defects = new List<DefectDraw>();
        private double maxDistance = 0;
        private double minDistance = 0;
        private float scaleX = 1, scaleY = 1;
        private float offsetX = 0, offsetY = 0;
        private Point lastMouse;
        private ToolTip toolTip;
        private DefectDraw hoveredDefect = null;
        private bool showGrid = true;
        private float baseFontSize = 8f;

        // Цвета
        private static readonly Color ColorCritical = Color.FromArgb(220, 53, 69);
        private static readonly Color ColorHigh = Color.FromArgb(255, 193, 7);
        private static readonly Color ColorMedium = Color.FromArgb(255, 235, 59);
        private static readonly Color ColorLow = Color.FromArgb(40, 167, 69);
        private static readonly Color ColorGrid = Color.FromArgb(200, 200, 200);
        private static readonly Color ColorAxis = Color.FromArgb(80, 80, 80);
        private static readonly Color ColorBackground = Color.FromArgb(248, 249, 250);

        public PipeCanvas()
        {
            this.DoubleBuffered = true;
            this.BackColor = ColorBackground;
            this.MouseWheel += OnMouseWheel;
            this.MouseDown += OnMouseDown;
            this.MouseMove += OnMouseMove;
            this.MouseLeave += OnMouseLeave;

            toolTip = new ToolTip();
            toolTip.SetToolTip(this, "");
        }

        public void SetDefects(List<Defect> defectList, double diameterMm)
        {
            defects.Clear();
            minDistance = double.MaxValue;
            maxDistance = 0;

            foreach (var d in defectList)
            {
                var draw = new DefectDraw
                {
                    X = d.DistanceM,
                    Y = d.AngleDeg,
                    Severity = d.Severity,
                    Width = 6,
                    Height = 6,
                    DefectType = d.DefectType,
                    DepthPercent = d.DepthPercent,
                    LengthMm = d.LengthMm,
                    WidthMm = d.WidthMm,
                    Erf = d.Erf ?? 0,
                    AllowablePressure = d.AllowablePressureMpa ?? 0
                };
                defects.Add(draw);

                if (d.DistanceM < minDistance) minDistance = d.DistanceM;
                if (d.DistanceM > maxDistance) maxDistance = d.DistanceM;
            }

            if (minDistance == double.MaxValue) minDistance = 0;
            if (maxDistance == 0) maxDistance = 100;

            // Добавляем 5% отступа
            double range = maxDistance - minDistance;
            if (range < 10) range = 100;
            minDistance = Math.Max(0, minDistance - range * 0.05);
            maxDistance = maxDistance + range * 0.05;

            ResetView();
            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

            if (defects.Count == 0)
            {
                using (var font = new Font("Segoe UI", 12f))
                using (var brush = new SolidBrush(Color.Gray))
                {
                    string msg = "Нет дефектов для отображения";
                    SizeF msgSize = g.MeasureString(msg, font);
                    g.DrawString(msg, font, brush, (Width - msgSize.Width) / 2, (Height - msgSize.Height) / 2);
                }
                return;
            }

            // Сохраняем трансформацию
            g.TranslateTransform(offsetX, offsetY);
            g.ScaleTransform(scaleX, scaleY);

            float w = Width / scaleX;
            float h = Height / scaleY;
            double range = maxDistance - minDistance;

            // Рисуем сетку (масштабируется вместе с содержимым)
            if (showGrid)
                DrawGrid(g, w, h, range);

            // Рисуем дефекты
            DrawDefects(g, w, h, range);

            g.ResetTransform();

            // Рисуем легенду (не масштабируется)
            DrawLegend(g);

            // Рисуем рамку
            using (var pen = new Pen(Color.FromArgb(0, 70, 128), 2))
            {
                g.DrawRectangle(pen, 0, 0, Width - 1, Height - 1);
            }
        }

        private void DrawGrid(Graphics g, float w, float h, double range)
        {
            float currentFontSize = baseFontSize / scaleX;
            if (currentFontSize < 5) currentFontSize = 5;
            if (currentFontSize > 12) currentFontSize = 12;

            using (var gridPen = new Pen(ColorGrid, 1f / scaleX) { DashStyle = DashStyle.Dash })
            using (var axisPen = new Pen(ColorAxis, 2f / scaleX))
            using (var font = new Font("Segoe UI", currentFontSize))
            using (var brush = new SolidBrush(ColorAxis))
            {
                // Вертикальные линии (дистанция)
                int divisions = Math.Max(5, (int)(10 / scaleX));
                for (int i = 0; i <= divisions; i++)
                {
                    double dist = minDistance + (range * i / divisions);
                    float x = (float)((dist - minDistance) / range * w);

                    if (x >= 0 && x <= w)
                    {
                        g.DrawLine(gridPen, x, 0, x, h);

                        string label;
                        if (dist >= 1000)
                            label = $"{dist / 1000:F1} км";
                        else
                            label = $"{dist:F0} м";

                        SizeF size = g.MeasureString(label, font);
                        g.DrawString(label, font, brush, x - size.Width / 2, h - size.Height - 5);
                    }
                }

                // Горизонтальные линии (угол)
                for (int angle = 0; angle <= 360; angle += 90)
                {
                    float y = angle / 360f * h;
                    if (y >= 0 && y <= h)
                    {
                        g.DrawLine(gridPen, 0, y, w, y);

                        string label = angle == 0 ? "0° (верх)" :
                                      angle == 90 ? "90° (правый)" :
                                      angle == 180 ? "180° (низ)" : "270° (левый)";

                        SizeF size = g.MeasureString(label, font);
                        g.DrawString(label, font, brush, 5, y - size.Height / 2);
                    }
                }

                // Оси
                g.DrawLine(axisPen, 0, 0, w, 0);
                g.DrawLine(axisPen, 0, h, w, h);
                g.DrawLine(axisPen, 0, 0, 0, h);
                g.DrawLine(axisPen, w, 0, w, h);
            }
        }

        private void DrawDefects(Graphics g, float w, float h, double range)
        {
            foreach (var d in defects)
            {
                Color color = GetColorBySeverity(d.Severity);
                float x = (float)((d.X - minDistance) / range * w);
                float y = (float)(d.Y / 360f * h);
                float size = d.Width / scaleX;
                if (size < 3) size = 3;
                if (size > 12) size = 12;

                using (var brush = new SolidBrush(color))
                using (var pen = new Pen(Color.Black, 1f / scaleX))
                {
                    g.FillEllipse(brush, x - size / 2, y - size / 2, size, size);
                    g.DrawEllipse(pen, x - size / 2, y - size / 2, size, size);
                }

                // Сохраняем область для хит-теста (в координатах canvas)
                d.Bounds = new RectangleF(x - size / 2, y - size / 2, size, size);
            }
        }

        private void DrawLegend(Graphics g)
        {
            int x = Width - 160;
            int y = 10;
            int height = 110;
            int width = 150;

            using (var bgBrush = new SolidBrush(Color.FromArgb(240, 240, 240)))
            using (var borderPen = new Pen(Color.Gray, 1))
            using (var font = new Font("Segoe UI", 9, FontStyle.Bold))
            using (var textBrush = new SolidBrush(Color.Black))
            {
                g.FillRectangle(bgBrush, x, y, width, height);
                g.DrawRectangle(borderPen, x, y, width, height);
                g.DrawString("Категория опасности", font, textBrush, x + 10, y + 5);

                DrawLegendItem(g, x + 10, y + 28, ColorCritical, "Critical (аварийный)");
                DrawLegendItem(g, x + 10, y + 48, ColorHigh, "High (ближайший ремонт)");
                DrawLegendItem(g, x + 10, y + 68, ColorMedium, "Medium (плановый ремонт)");
                DrawLegendItem(g, x + 10, y + 88, ColorLow, "Low (наблюдение)");
            }
        }

        private void DrawLegendItem(Graphics g, int x, int y, Color color, string text)
        {
            using (var brush = new SolidBrush(color))
            using (var pen = new Pen(Color.Black, 1))
            using (var font = new Font("Segoe UI", 8))
            using (var textBrush = new SolidBrush(Color.Black))
            {
                g.FillEllipse(brush, x, y, 12, 12);
                g.DrawEllipse(pen, x, y, 12, 12);
                g.DrawString(text, font, textBrush, x + 18, y + 2);
            }
        }

        private Color GetColorBySeverity(string severity)
        {
            return severity switch
            {
                "Critical" => ColorCritical,
                "High" => ColorHigh,
                "Medium" => ColorMedium,
                _ => ColorLow
            };
        }

        private void OnMouseWheel(object sender, MouseEventArgs e)
        {
            float delta = e.Delta > 0 ? 1.1f : 0.9f;
            float oldScaleX = scaleX;
            float oldScaleY = scaleY;

            // Масштабируем
            scaleX *= delta;
            scaleY *= delta;

            // Ограничения масштаба
            if (scaleX < 0.3f) scaleX = 0.3f;
            if (scaleX > 10f) scaleX = 10f;
            scaleY = scaleX;

            // Корректируем смещение относительно курсора
            float mouseX = (e.X - offsetX) / oldScaleX;
            float mouseY = (e.Y - offsetY) / oldScaleY;
            offsetX = e.X - mouseX * scaleX;
            offsetY = e.Y - mouseY * scaleY;

            Invalidate();
        }

        private void OnMouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                lastMouse = e.Location;
        }

        private void OnMouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                offsetX += e.X - lastMouse.X;
                offsetY += e.Y - lastMouse.Y;
                lastMouse = e.Location;
                Invalidate();
            }
            else
            {
                CheckHover(e.Location);
            }
        }

        private void CheckHover(Point mousePos)
        {
            float canvasX = (mousePos.X - offsetX) / scaleX;
            float canvasY = (mousePos.Y - offsetY) / scaleY;

            DefectDraw newHovered = null;
            foreach (var d in defects)
            {
                if (d.Bounds.Contains(canvasX, canvasY))
                {
                    newHovered = d;
                    break;
                }
            }

            if (newHovered != hoveredDefect)
            {
                hoveredDefect = newHovered;
                UpdateTooltip();
            }
        }

        private void UpdateTooltip()
        {
            if (hoveredDefect != null)
            {
                string tip = $"Дефект: {hoveredDefect.DefectType}\n" +
                            $"Дистанция: {hoveredDefect.X:F1} м\n" +
                            $"Угол: {hoveredDefect.Y:F0}°\n" +
                            $"Глубина: {hoveredDefect.DepthPercent:F1}%\n" +
                            $"Длина: {hoveredDefect.LengthMm:F0} мм\n" +
                            $"ERF: {hoveredDefect.Erf:F4}\n" +
                            $"Категория: {hoveredDefect.Severity}";
                toolTip.SetToolTip(this, tip);
            }
            else
            {
                toolTip.SetToolTip(this, "");
            }
        }

        private void OnMouseLeave(object sender, EventArgs e)
        {
            hoveredDefect = null;
            toolTip.SetToolTip(this, "");
        }

        public void ResetView()
        {
            scaleX = 1;
            scaleY = 1;

            // Центрируем содержимое
            float w = Width;
            float h = Height;
            double range = maxDistance - minDistance;
            float contentWidth = (float)(range > 0 ? w / range * (maxDistance - minDistance) : w);
            float contentHeight = 360f;

            offsetX = (w - contentWidth) / 2;
            offsetY = (h - contentHeight) / 2;

            Invalidate();
        }

        public void ToggleGrid()
        {
            showGrid = !showGrid;
            Invalidate();
        }

        public bool IsGridVisible()
        {
            return showGrid;
        }

        private class DefectDraw
        {
            public double X, Y;
            public string Severity;
            public float Width, Height;
            public string DefectType;
            public double DepthPercent;
            public double LengthMm;
            public double WidthMm;
            public double Erf;
            public double AllowablePressure;
            public RectangleF Bounds;
        }
    }
}