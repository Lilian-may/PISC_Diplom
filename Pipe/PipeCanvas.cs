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
		private bool showLegend = true;

		private static readonly Color ColorCritical = Color.FromArgb(255, 0, 0); private static readonly Color ColorHigh = Color.FromArgb(255, 128, 0); private static readonly Color ColorMedium = Color.FromArgb(255, 255, 0); private static readonly Color ColorLow = Color.FromArgb(0, 176, 80); private static readonly Color ColorGrid = Color.FromArgb(200, 200, 200);
		private static readonly Color ColorAxis = Color.FromArgb(80, 80, 80);
		private static readonly Color ColorBackground = Color.FromArgb(245, 245, 245);

		public PipeCanvas()
		{
			this.DoubleBuffered = true;
			this.BackColor = ColorBackground;
			this.MouseWheel += OnMouseWheel;
			this.MouseDown += OnMouseDown;
			this.MouseMove += OnMouseMove;
			this.MouseLeave += OnMouseLeave;
			this.Resize += (s, e) => Invalidate();

			toolTip = new ToolTip();
			toolTip.SetToolTip(this, "");
			toolTip.BackColor = Color.FromArgb(240, 240, 240);
			toolTip.ForeColor = Color.Black;
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
					Width = (float)Math.Max(4, d.LengthMm / 500.0),
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

			double range = maxDistance - minDistance;
			minDistance -= range * 0.05;
			maxDistance += range * 0.05;
			if (minDistance < 0) minDistance = 0;

			Invalidate();
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
			Graphics g = e.Graphics;
			g.SmoothingMode = SmoothingMode.AntiAlias;
			g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

			g.TranslateTransform(offsetX, offsetY);
			g.ScaleTransform(scaleX, scaleY);

			using (var bgBrush = new SolidBrush(ColorBackground))
			{
				g.FillRectangle(bgBrush, 0, 0, Width / scaleX, Height / scaleY);
			}

			if (showGrid)
				DrawGrid(g);

			DrawDefects(g);

			g.ResetTransform();

			if (showLegend)
				DrawLegend(g);

			DrawBorder(g);
		}

		private void DrawGrid(Graphics g)
		{
			float w = Width / scaleX;
			float h = Height / scaleY;

			using (var gridPen = new Pen(ColorGrid, 1) { DashStyle = DashStyle.Dash })
			using (var axisPen = new Pen(ColorAxis, 2))
			using (var font = new Font("Arial", 8))
			using (var brush = new SolidBrush(ColorAxis))
			{
				double distanceRange = maxDistance - minDistance;
				double step = distanceRange / 10;
				if (step < 1) step = 1;
				else if (step > 100) step = Math.Ceiling(step / 100) * 100;

				double startDist = Math.Floor(minDistance / step) * step;
				for (double dist = startDist; dist <= maxDistance; dist += step)
				{
					float x = (float)((dist - minDistance) / distanceRange * w);
					if (x >= 0 && x <= w)
					{
						g.DrawLine(gridPen, x, 0, x, h);

						string label;
						if (dist >= 1000)
							label = $"{dist / 1000:F1} км";
						else
							label = $"{dist:F0} м";

						SizeF textSize = g.MeasureString(label, font);
						g.DrawString(label, font, brush, x - textSize.Width / 2, h - 20);
					}
				}

				for (int angle = 0; angle <= 360; angle += 90)
				{
					float y = angle / 360f * h;
					if (y >= 0 && y <= h)
					{
						g.DrawLine(gridPen, 0, y, w, y);

						string label = angle == 0 ? "0° (верх)" :
									  angle == 90 ? "90° (правый бок)" :
									  angle == 180 ? "180° (низ)" :
									  angle == 270 ? "270° (левый бок)" : $"{angle}°";

						g.DrawString(label, font, brush, 5, y - 8);
					}
				}

				g.DrawLine(axisPen, 0, 0, w, 0);
				g.DrawLine(axisPen, 0, h, w, h);
				g.DrawLine(axisPen, 0, 0, 0, h);
				g.DrawLine(axisPen, w, 0, w, h);
			}
		}

		private void DrawDefects(Graphics g)
		{
			float w = Width / scaleX;
			float h = Height / scaleY;
			double distanceRange = maxDistance - minDistance;

			foreach (var d in defects)
			{
				Color color = GetColorBySeverity(d.Severity);
				float x = (float)((d.X - minDistance) / distanceRange * w);
				float y = (float)(d.Y / 360f * h);
				float size = Math.Max(4, d.Width);

				using (var brush = new SolidBrush(color))
				using (var borderPen = new Pen(Color.Black, 1))
				{
					g.FillEllipse(brush, x - size / 2, y - size / 2, size, size);
					g.DrawEllipse(borderPen, x - size / 2, y - size / 2, size, size);
				}

				d.Bounds = new RectangleF(x - size / 2, y - size / 2, size, size);
			}
		}

		private void DrawLegend(Graphics g)
		{
			int x = Width - 150;
			int y = 10;
			int itemHeight = 25;
			int width = 140;
			int height = itemHeight * 4 + 10;

			using (var bgBrush = new SolidBrush(Color.FromArgb(240, 240, 240)))
			using (var borderPen = new Pen(Color.Gray, 1))
			using (var font = new Font("Arial", 9, FontStyle.Bold))
			using (var textBrush = new SolidBrush(Color.Black))
			{
				g.FillRectangle(bgBrush, x, y, width, height);
				g.DrawRectangle(borderPen, x, y, width, height);

				g.DrawString("Категория опасности", font, textBrush, x + 5, y + 3);

				DrawLegendItem(g, x + 5, y + 20, ColorCritical, "Critical (аварийный)");
				DrawLegendItem(g, x + 5, y + 45, ColorHigh, "High (ближайший ремонт)");
				DrawLegendItem(g, x + 5, y + 70, ColorMedium, "Medium (плановый ремонт)");
				DrawLegendItem(g, x + 5, y + 95, ColorLow, "Low (наблюдение)");
			}
		}

		private void DrawLegendItem(Graphics g, int x, int y, Color color, string text)
		{
			using (var brush = new SolidBrush(color))
			using (var pen = new Pen(Color.Black, 1))
			using (var font = new Font("Arial", 8))
			using (var textBrush = new SolidBrush(Color.Black))
			{
				g.FillEllipse(brush, x, y + 2, 12, 12);
				g.DrawEllipse(pen, x, y + 2, 12, 12);
				g.DrawString(text, font, textBrush, x + 18, y + 3);
			}
		}

		private void DrawBorder(Graphics g)
		{
			using (var borderPen = new Pen(Color.FromArgb(0, 51, 102), 2))
			{
				g.DrawRectangle(borderPen, 0, 0, Width - 1, Height - 1);
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

			scaleX *= delta;
			scaleY *= delta;

			if (scaleX < 0.3f) scaleX = 0.3f;
			if (scaleX > 8f) scaleX = 8f;
			scaleY = scaleX;

			float mouseX = e.X - offsetX;
			float mouseY = e.Y - offsetY;
			offsetX = e.X - mouseX * (scaleX / oldScaleX);
			offsetY = e.Y - mouseY * (scaleY / oldScaleY);

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
				string tip = $"📍 Дефект: {hoveredDefect.DefectType}\n" +
							$"📏 Дистанция: {hoveredDefect.X:F1} м\n" +
							$"🔄 Угол: {hoveredDefect.Y:F0}°\n" +
							$"📊 Глубина: {hoveredDefect.DepthPercent:F1}%\n" +
							$"📐 Длина: {hoveredDefect.LengthMm:F0} мм\n" +
							$"📏 Ширина: {hoveredDefect.WidthMm:F0} мм\n" +
							$"⚠️ ERF: {hoveredDefect.Erf:F4}\n" +
							$"🔧 Категория: {hoveredDefect.Severity}\n" +
							$"⚙️ Допустимое давление: {hoveredDefect.AllowablePressure:F2} МПа";

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

		public void ToggleGrid()
		{
			showGrid = !showGrid;
			Invalidate();
		}

		public void ToggleLegend()
		{
			showLegend = !showLegend;
			Invalidate();
		}

		public void ResetView()
		{
			scaleX = 1;
			scaleY = 1;
			offsetX = 0;
			offsetY = 0;
			Invalidate();
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