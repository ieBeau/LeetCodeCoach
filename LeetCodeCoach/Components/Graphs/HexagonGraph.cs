using System.Drawing.Drawing2D;

namespace LeetCodeCoach.Components.Graphs
{
    public class Hexagon
    {
        private int Radius { get; set; }

        private int Sides { get; set; }

        private Label Label { get; set; }

        private List<string> Names { get; set; } = new List<string>();

        private List<float> Percentages { get; set; } = new List<float>();

        Panel Panel { get; set; } = new Panel();

        public Hexagon(string label, Dictionary<string, float> data, int radius = 70)
        {
            Radius = radius;

            Label = new Label
            {
                Text = label,
                ForeColor = Color.White,
                Font = new Font("Arial", 12, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleCenter,
                BackColor = Color.Transparent,
                Size = new Size(3 * radius, 20),
            };
            Panel.Controls.Add(Label);
            
            Initialize(data);
        }

        public Panel Build()
        {
            return Panel;
        }

        public void Initialize(Dictionary<string, float> data)
        {
            Sides = Math.Max(1, data.Count);
            Names = data.Keys.ToList();
            Percentages = data.Values.ToList();

            PaintCanvas();
        }
        
        private void PaintCanvas()
        {
            if (Sides == 1) return;

            int width = 3 * Radius;
            int height = 3 * Radius;

            Panel.Size = new Size(width, height + 20);

            Panel.Paint += Panel_Paint;
            
            Panel.Invalidate();
        }

        private void Panel_Paint(object? sender, PaintEventArgs e)
        {
            Graphics graphics = e.Graphics;
            graphics.SmoothingMode = SmoothingMode.AntiAlias;

            // Get the middle of the Panel  
            Panel? Panel = sender as Panel;
            if (Panel == null) return;

            float x_0 = Panel.Width / 2;
            float y_0 = Panel.Height / 2;

            var shape = new PointF[Sides]; // Only 6 points for the hexagon  

            // Hexagon percentage indicators
            var percentage25 = new PointF[Sides];
            var percentage50 = new PointF[Sides];
            var percentage75 = new PointF[Sides];

            // Create 6 points  
            for (int a = 0; a < Sides; a++)
            {
                // Adjust angle to rotate points so the first point is at the top
                float angleOffset = -90; // Rotate 90 degrees counterclockwise
                float x = x_0 + Radius * (float)Math.Cos((a * (10 * Sides) + angleOffset) * Math.PI / 180f);
                float y = y_0 + Radius * (float)Math.Sin((a * (10 * Sides) + angleOffset) * Math.PI / 180f);

                shape[a] = new PointF(x, y);

                percentage25[a] = new PointF(x_0 + 0.25f * (x - x_0), y_0 + 0.25f * (y - y_0)); // 25%
                percentage50[a] = new PointF(x_0 + 0.50f * (x - x_0), y_0 + 0.50f * (y - y_0)); // 50%
                percentage75[a] = new PointF(x_0 + 0.75f * (x - x_0), y_0 + 0.75f * (y - y_0)); // 75%

                // Draw text in the middle of the hexagon
                var font = new Font("Arial", (int)(Radius * 0.08f), FontStyle.Bold);
                var textSize = graphics.MeasureString(Names[a], font);

                var textX = x_0 + 1.22f * (x - x_0) - textSize.Width / 2;
                var textY = y_0 + 1.22f * (y - y_0) - textSize.Height / 2;

                graphics.DrawString(Names[a], font, Brushes.White, textX, textY);
                graphics.FillEllipse(Brushes.Gray, x - 2, y - 2, 4, 4); // Draw points

            }

            graphics.DrawPolygon(Pens.Gray, shape); // Draw hexagon  

            graphics.FillPolygon(new SolidBrush(Color.FromArgb(95, Color.Gray)), percentage25);
            graphics.DrawPolygon(Pens.Gray, percentage25); // Draw hexagon
            graphics.FillPolygon(new SolidBrush(Color.FromArgb(95, Color.Gray)), percentage50);
            graphics.DrawPolygon(Pens.Gray, percentage50); // Draw hexagon
            graphics.FillPolygon(new SolidBrush(Color.FromArgb(95, Color.Gray)), percentage75);
            graphics.DrawPolygon(Pens.Gray, percentage75); // Draw hexagon

            // List of percentages  
            var percentagePoints = new PointF[6];

            // Draw lines from center to each point and add points on the lines  
            for (int a = 0; a < 6; a++)
            {
                graphics.DrawLine(Pens.Gray, x_0, y_0, shape[a].X, shape[a].Y);

                float x = x_0 + Percentages[a] * (shape[a].X - x_0);
                float y = y_0 + Percentages[a] * (shape[a].Y - y_0);

                percentagePoints[a] = new PointF(x, y);

                graphics.FillEllipse(Brushes.Green, x - 2, y - 2, 4, 4);
            }

            // Fill the space between percentagePoints with green
            graphics.FillPolygon(new SolidBrush(Color.FromArgb(180, Color.ForestGreen)), percentagePoints);
            graphics.DrawPolygon(Pens.Green, percentagePoints); // Draw hexagon  


            graphics.FillEllipse(Brushes.Gray, x_0 - 3, y_0 - 3, 6, 6); // Draw center circle  
        }
    }
}
