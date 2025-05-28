using LeetCodeCoach.Components.Panels;

namespace LeetCodeCoach.Components.Buttons
{
    public class VideoButton : ComponentBase
    {
        public Panel VideoPanel { get; set; }

        private Button Button { get; set; }

        private Videos Videos { get; set; }
        
        public Image Image { get; set; }

        public Bitmap Bitmap { get; set; }

        public Color Color { get; set; } = Color.White;

        public VideoButton()
        {
            string imagePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets", "video.png");

            Image = Image.FromFile(imagePath);
            Bitmap = new Bitmap(Image);

            Button = new Button
            {
                Name = "VideoButton",
                Image = Image,
                Size = new Size(30, 30),
                BackColor = Color.Transparent,
                Cursor = Cursors.Hand,
                FlatStyle = FlatStyle.Flat,
                FlatAppearance = {
                    BorderSize = 0,
                    MouseOverBackColor = Color.Transparent,
                    MouseDownBackColor = Color.Transparent
                },
            };

            Videos = new Videos();
            VideoPanel = Videos.Build();
            _form.Controls.Add(VideoPanel);

            Button.Click += VideoButton_Click;
            Button.MouseEnter += (sender, e) => Button.Image = EditImage.ChangeColor(Bitmap, Color.FromArgb(255, Math.Max(0, Color.R - 60), Math.Max(0, Color.G - 60), Math.Max(0, Color.B - 60)));
            Button.MouseLeave += (sender, e) => Button.Image = EditImage.ChangeColor(Bitmap, Color);
        }

        public Button Build()
        {
            return Button;
        }

        private void VideoButton_Click(object? sender, EventArgs e)
        {
            if (VideoPanel.Visible)
            {
                VideoPanel.Visible = false;
            }
            else
            {
                Videos.Update();
                VideoPanel.Visible = true;
                VideoPanel.BringToFront();
            }
        }
    }
}