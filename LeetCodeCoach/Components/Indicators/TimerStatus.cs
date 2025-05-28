namespace LeetCodeCoach.Components.Indicators
{
    public class TimerStatus : ComponentBase
    {
        private Panel Panel { get; set; }

        private Color Color { get; set; } = Color.Red;

        public TimerStatus()
        {
            Panel = new Panel
            {
                Size = new Size(18, 18)
            };

            Panel.Paint += onPaint;
        }

        public Panel Build()
        {
            return Panel;
        }

        public void SetColor(Color color)
        {
            Color = color;

            Panel.Paint += onPaint;

            Panel.Invalidate();
        }

        private void onPaint(object? sender, PaintEventArgs e)
        {
            var g = e.Graphics;

            TimerStatusConstructors.DrawCircle(g, new Pen(Color.Transparent, 2), Panel.Width / 2, Panel.Height / 2, Panel.Width / 2 - 1);
            TimerStatusConstructors.FillCircle(g, new SolidBrush(Color), Panel.Width / 2, Panel.Height / 2, Panel.Width / 2 - 1);
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
        }
    }

    public static class TimerStatusConstructors
    {
        public static void DrawCircle(this Graphics g, Pen pen, float centerX, float centerY, float radius)
        {
            g.DrawEllipse(pen, centerX - radius, centerY - radius, radius + radius, radius + radius);
        }

        public static void FillCircle(this Graphics g, Brush brush, float centerX, float centerY, float radius)
        {
            g.FillEllipse(brush, centerX - radius, centerY - radius, radius + radius, radius + radius);
        }
    }
}