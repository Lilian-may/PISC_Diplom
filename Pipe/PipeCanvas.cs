#nullable disable
using System;
using System.Collections.Generic;
using System.Drawing;
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

        public PipeCanvas()
        {
            try
            {
                this.DoubleBuffered = true;
                this.BackColor = Color.White;
                this.MouseWheel += OnMouseWheel;
                this.MouseDown += OnMouseDown;
                this.MouseMove += OnMouseMove;
                this.MouseLeave += OnMouseLeave;
                toolTip = new ToolTip();
                toolTip.SetToolTip(this, "");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"PipeCanvas constructor error: {ex.Message}");
            }
        }

        public void SetDefects(List<Defect> defectList, double diameterMm)
        {
            try
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
                        Width = 8,
                        Height = 8,
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
                double range = maxDistance - minDistance;
                if (range < 10) range = 100;
                minDistance = 0;
                maxDistance = range + range * 0.1;
                Invalidate();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"SetDefects error: {ex.Message}");
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            try
            {
                base.OnPaint(e);
                Graphics g = e.Graphics;
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                if (defects.Count == 0)
                {
                    g.DrawString("Нет дефектов для отображения", new Font("Arial", 12), Brushes.Gray, Width / 2 - 100, Height / 2);
                    return;
                }
                g.TranslateTransform(offsetX, offsetY);
                g.ScaleTransform(scaleX, scaleY);
                float w = Width / scaleX;
                float h = Height / scaleY;
                double range = maxDistance - minDistance;
                DrawGrid(g, w, h, range);
                DrawDefects(g, w, h, range);
                g.ResetTransform();
                DrawLegend(g);
                using (var pen = new Pen(Color.Navy, 2))
                {
                    g.DrawRectangle(pen, 0, 0, Width - 1, Height - 1);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"OnPaint error: {ex.Message}");
            }
        }

        private void DrawGrid(Graphics g, float w, float h, double range)
        {
            try
            {
                using (var gridPen = new Pen(Color.LightGray, 1) { DashStyle = System.Drawing.Drawing2D.DashStyle.Dash })
                using (var font = new Font("Arial", 8))
                using (var brush = new SolidBrush(Color.DarkGray))
                {
                    int divisions = 10;
                    for (int i = 0; i <= divisions; i++)
                    {
                        double dist = minDistance + (range * i / divisions);
                        float x = (float)((dist - minDistance) / range * w);
                        g.DrawLine(gridPen, x, 0, x, h);
                        string label = dist >= 1000 ? $"{dist / 1000:F1} км" : $"{dist:F0} м";
                        SizeF size = g.MeasureString(label, font);
                        g.DrawString(label, font, brush, x - size.Width / 2, h - 18);
                    }
                    for (int angle = 0; angle <= 360; angle += 90)
                    {
                        float y = angle / 360f * h;
                        g.DrawLine(gridPen, 0, y, w, y);
                        string label = angle == 0 ? "0° (верх)" :
                                      angle == 90 ? "90° (правый)" :
                                      angle == 180 ? "180° (низ)" : "270° (левый)";
                        g.DrawString(label, font, brush, 5, y - 8);
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"DrawGrid error: {ex.Message}");
            }
        }

        private void DrawDefects(Graphics g, float w, float h, double range)
        {
            try
            {
                foreach (var d in defects)
                {
                    Color color = GetColorBySeverity(d.Severity);
                    float x = (float)((d.X - minDistance) / range * w);
                    float y = (float)(d.Y / 360f * h);
                    float size = d.Width;
                    using (var brush = new SolidBrush(color))
                    using (var pen = new Pen(Color.Black, 1))
                    {
                        g.FillEllipse(brush, x - size / 2, y - size / 2, size, size);
                        g.DrawEllipse(pen, x - size / 2, y - size / 2, size, size);
                    }
                    d.Bounds = new RectangleF(x - size / 2, y - size / 2, size, size);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"DrawDefects error: {ex.Message}");
            }
        }

        private void DrawLegend(Graphics g)
        {
            try
            {
                int x = Width - 145;
                int y = 10;
                int height = 105;
                int width = 135;
                using (var bgBrush = new SolidBrush(Color.FromArgb(240, 240, 240)))
                using (var borderPen = new Pen(Color.Gray, 1))
                using (var font = new Font("Arial", 8, FontStyle.Bold))
                using (var textBrush = new SolidBrush(Color.Black))
                {
                    g.FillRectangle(bgBrush, x, y, width, height);
                    g.DrawRectangle(borderPen, x, y, width, height);
                    g.DrawString("Категория опасности", font, textBrush, x + 5, y + 3);
                    DrawLegendItem(g, x + 5, y + 20, "Critical", Color.Red);
                    DrawLegendItem(g, x + 5, y + 40, "High", Color.Orange);
                    DrawLegendItem(g, x + 5, y + 60, "Medium", Color.Yellow);
                    DrawLegendItem(g, x + 5, y + 80, "Low", Color.Green);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"DrawLegend error: {ex.Message}");
            }
        }

        private void DrawLegendItem(Graphics g, int x, int y, string text, Color color)
        {
            try
            {
                using (var brush = new SolidBrush(color))
                using (var pen = new Pen(Color.Black, 1))
                using (var font = new Font("Arial", 7))
                using (var textBrush = new SolidBrush(Color.Black))
                {
                    g.FillEllipse(brush, x, y + 2, 10, 10);
                    g.DrawEllipse(pen, x, y + 2, 10, 10);
                    g.DrawString(text, font, textBrush, x + 15, y + 3);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"DrawLegendItem error: {ex.Message}");
            }
        }

        private Color GetColorBySeverity(string severity)
        {
            return severity switch
            {
                "Critical" => Color.Red,
                "High" => Color.Orange,
                "Medium" => Color.Yellow,
                _ => Color.Green
            };
        }

        private void OnMouseWheel(object sender, MouseEventArgs e)
        {
            try
            {
                float delta = e.Delta > 0 ? 1.1f : 0.9f;
                scaleX *= delta;
                scaleY *= delta;
                if (scaleX < 0.3f) scaleX = 0.3f;
                if (scaleX > 5f) scaleX = 5f;
                scaleY = scaleX;
                Invalidate();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"OnMouseWheel error: {ex.Message}");
            }
        }

        private void OnMouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button == MouseButtons.Left)
                    lastMouse = e.Location;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"OnMouseDown error: {ex.Message}");
            }
        }

        private void OnMouseMove(object sender, MouseEventArgs e)
        {
            try
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
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"OnMouseMove error: {ex.Message}");
            }
        }

        private void CheckHover(Point mousePos)
        {
            try
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
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"CheckHover error: {ex.Message}");
            }
        }

        private void UpdateTooltip()
        {
            try
            {
                if (hoveredDefect != null)
                {
                    string tip = $"Дефект: {hoveredDefect.DefectType}\n" +
                                $"Дистанция: {hoveredDefect.X:F0} м\n" +
                                $"Угол: {hoveredDefect.Y:F0}°\n" +
                                $"Глубина: {hoveredDefect.DepthPercent:F1}%\n" +
                                $"ERF: {hoveredDefect.Erf:F4}\n" +
                                $"Категория: {hoveredDefect.Severity}";
                    toolTip.SetToolTip(this, tip);
                }
                else
                {
                    toolTip.SetToolTip(this, "");
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"UpdateTooltip error: {ex.Message}");
            }
        }

        private void OnMouseLeave(object sender, EventArgs e)
        {
            try
            {
                hoveredDefect = null;
                toolTip.SetToolTip(this, "");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"OnMouseLeave error: {ex.Message}");
            }
        }

        public void ResetView()
        {
            try
            {
                scaleX = 1;
                scaleY = 1;
                offsetX = 0;
                offsetY = 0;
                Invalidate();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"ResetView error: {ex.Message}");
            }
        }

        public void ToggleGrid()
        {
            try
            {
                Invalidate();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"ToggleGrid error: {ex.Message}");
            }
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