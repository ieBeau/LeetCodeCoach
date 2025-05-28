
namespace LeetCodeCoach.Components.Buttons
{
    public class BackButton : ComponentBase
    {
        public Button Button { get; set; } = new Button();
        
        public Image Image { get; set; }

        public Bitmap Bitmap { get; set; }

        public Color Color { get; set; } = Color.FromArgb(155, 30, 30, 30);

        public BackButton()
        {
            string imagePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets", "back_arrow.png");

            Image = Image.FromFile(imagePath);
            Bitmap = new Bitmap(Image);

            Button.Image = EditImage.ChangeColor(Bitmap, Color);
            Button.Name = "BackButton";
            Button.Enabled = false;
            Button.Size = new Size(20, 25);
            Button.Cursor = Cursors.Hand;
            Button.FlatStyle = FlatStyle.Flat;
            Button.TextAlign = ContentAlignment.MiddleCenter;
            Button.UseCompatibleTextRendering = true;
            Button.FlatAppearance.BorderSize = 0;
            Button.FlatAppearance.BorderColor = Color.FromArgb(255, 10, 10, 10);
            Button.FlatAppearance.CheckedBackColor = Color.Transparent;
            Button.FlatAppearance.MouseOverBackColor = Color.Transparent;
            Button.FlatAppearance.MouseDownBackColor = Color.Transparent;
            Button.Click += Button_Click;
            Button.MouseEnter += (sender, e) => Button.Image = EditImage.ChangeColor(Bitmap, Color.FromArgb(255, Math.Max(0, Color.R - 60), Math.Max(0, Color.G - 60), Math.Max(0, Color.B - 60)));
            Button.MouseLeave += (sender, e) => Button.Image = EditImage.ChangeColor(Bitmap, Color);
        }

        public void Enable()
        {
            Button.Enabled = true;
            Color = Color.White;
            Button.Image = EditImage.ChangeColor(Bitmap, Color);
        }

        public void Disable()
        {
            Button.Enabled = false;
            Color = Color.FromArgb(155, 30, 30, 30);
            Button.Image = EditImage.ChangeColor(Bitmap, Color);
        }

        private void Button_Click(object? sender, EventArgs e)
        {
            Button.Enabled = false;
            Color = Color.FromArgb(155, 30, 30, 30);

            _form.Controls.Find("VideoContainer", true)[0].Visible = false;

            
            if (_form.Topbar != null)
            {
                _form.Topbar.TopicName.Text = "";
                _form.Topbar.VideoButton.Visible = false;
            }

            if (_form.TablesPanel != null) _form.TablesPanel.Visible = false;
        } 
    }
}
