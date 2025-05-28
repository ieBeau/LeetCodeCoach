
namespace LeetCodeCoach.Components.Buttons
{
    public class TimerControlButton : ComponentBase
    {
        public Button Button { get; set; } = new Button();

        public Image Image { get; set; }

        public Bitmap Bitmap { get; set; }

        public Color Color { get; set; } = Color.White;

        public TimerControlButton(string name)
        {
            string imagePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets", $"{name.ToLower()}.png");

            Image = Image.FromFile(imagePath);
            Bitmap = new Bitmap(Image);

            Button.Name = $"{name}Timer";
            Button.Font = new Font("Segoe UI Symbol", 45, FontStyle.Bold);
            Button.Image = Image;
            Button.Size = new Size(75, 75);
            Button.BackColor = Color.Transparent;
            Button.Cursor = Cursors.Hand;
            Button.FlatStyle = FlatStyle.Flat;
            Button.TextAlign = ContentAlignment.MiddleCenter;
            Button.UseCompatibleTextRendering = true;
            Button.FlatAppearance.BorderSize = 0;
            Button.FlatAppearance.BorderColor = Color.FromArgb(255, 10, 10, 10);
            Button.FlatAppearance.CheckedBackColor = Color.Transparent;
            Button.FlatAppearance.MouseOverBackColor = Color.Transparent;
            Button.FlatAppearance.MouseDownBackColor = Color.Transparent;
            Button.MouseEnter += (sender, e) => Button.Image = EditImage.ChangeColor(Bitmap, Color.FromArgb(255, Math.Max(0, Color.R - 60), Math.Max(0, Color.G - 60), Math.Max(0, Color.B - 60)));
            Button.MouseLeave += (sender, e) => Button.Image = EditImage.ChangeColor(Bitmap, Color);
        }

        public Button Build()
        {
            return Button;
        }

        public void UpdateColor(Color color)
        {
            Color = color;
            Button.Image = EditImage.ChangeColor(Bitmap, Color);
        }
    }
}