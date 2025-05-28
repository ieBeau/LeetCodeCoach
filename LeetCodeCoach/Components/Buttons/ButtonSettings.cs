namespace LeetCodeCoach.Components.Buttons
{
    public class SettingsButton : ComponentBase
    {
        private Button Button { get; set; }
        
        public Image Image { get; set; }

        public Bitmap Bitmap { get; set; }

        public Color Color { get; set; } = Color.White;

        public SettingsButton()
        {
            string imagePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets", "settings.png");

            Image = Image.FromFile(imagePath);
            Bitmap = new Bitmap(Image);

            Button = new Button
            {
                Name = "SettingsButton",
                Image = Image.FromFile(imagePath),
                Size = new Size(25, 25),
                BackColor = Color.Transparent,
                Cursor = Cursors.Hand,
                FlatStyle = FlatStyle.Flat,
                FlatAppearance = {
                    BorderSize = 0,
                    MouseOverBackColor = Color.Transparent,
                    MouseDownBackColor = Color.Transparent
                },
            };

            Button.Click += SettingsButton_Click;
            Button.MouseEnter += (sender, e) => Button.Image = EditImage.ChangeColor(Bitmap, Color.FromArgb(255, Math.Max(0, Color.R - 60), Math.Max(0, Color.G - 60), Math.Max(0, Color.B - 60)));
            Button.MouseLeave += (sender, e) => Button.Image = EditImage.ChangeColor(Bitmap, Color);
        }

        public Button Build()
        {
            return Button;
        }

        private void SettingsButton_Click(object? sender, EventArgs e)
        {
            if (_form.SettingsPanel is not null) _form.SettingsPanel.Visible = !_form.SettingsPanel.Visible;
        }
    }
}