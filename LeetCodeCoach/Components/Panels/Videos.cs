using LeetCodeCoach.Models;

namespace LeetCodeCoach.Components.Panels
{
    public class Videos : ComponentBase
    {
        private Size ImageSize { get; set; } = new Size(320, 180);

        private Size PanelSize { get; set; }

        private Panel Panel { get; set; }

        private Panel VideoPanel { get; set; }

        private List<Video> VideosList { get; set; } = new List<Video>();

        public Videos()
        {
            PanelSize = new Size(ImageSize.Width + 40, ImageSize.Height * 3 + 40);

            Panel = new Panel
            {
                Name = "VideoContainer",
                Size = PanelSize,
                BackColor = Color.FromArgb(255, 10, 10, 10),
                BorderStyle = BorderStyle.FixedSingle,
                Location = new Point((_form.Width / 2) - (PanelSize.Width / 2), (_form.Height / 2) - (PanelSize.Height / 2)),
                Visible = false
            };

            Panel.MouseDown += (sender, e) =>
            {
                if (e.Button == MouseButtons.Left)
                {
                    Panel.Tag = new Point(e.X, e.Y);
                }
            };

            Panel.MouseMove += (sender, e) =>
            {
                if (e.Button == MouseButtons.Left && Panel.Tag is Point start)
                {
                    Panel.Left += e.X - start.X;
                    Panel.Top += e.Y - start.Y;
                }
            };

            Panel.MouseUp += (sender, e) =>
            {
                Panel.Tag = null;
            };

            Button closeButton = new Button
            {
                Text = "✕",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                Size = new Size(30, 30),
                Location = new Point(Panel.Width - 30, 0),
                FlatAppearance = { BorderSize = 0, MouseOverBackColor = Color.FromArgb(255, 10, 10, 10) },
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.Transparent,
                ForeColor = Color.White,
                TabStop = false,
                Cursor = Cursors.Hand,
            };

            closeButton.MouseEnter += (sender, e) => closeButton.ForeColor = Color.Gray;
            closeButton.MouseLeave += (sender, e) => closeButton.ForeColor = Color.White;

            closeButton.Click += (sender, e) =>
            {
                Panel.Visible = false;
            };

            Panel.Controls.Add(closeButton);

            Label TitleLabel = new Label
            {
                Text = "Video Lessons",
                Font = new Font("Segoe UI", 15, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleCenter,
                ForeColor = Color.White,
                BackColor = Color.Transparent,
                Size = new Size(PanelSize.Width, 30),
            };

            Panel.Controls.Add(TitleLabel);

            VideoPanel = new Panel
            {
                Name = "VideoPanel",
                Size = new Size(PanelSize.Width, PanelSize.Height - 30),
                BackColor = Color.Transparent,
                Location = new Point(0, 30),
                AutoScroll = true,
            };

            Panel.Controls.Add(VideoPanel);
        }

        public Panel Build()
        {
            return Panel;
        }

        public void Update()
        {
            VideoPanel.Controls.Clear();
            
            if (_form.Data.Topics != null && _form.Data.Topics.ContainsKey(_form.CurrentTopicId)) VideosList = _form.Data.Topics[_form.CurrentTopicId].videos;

            for (int i = 0; i < VideosList.Count; i++)
            {
                Video video = VideosList[i];

                if (video == null || string.IsNullOrEmpty(video.url)) continue;
                
                PictureBox videoBox = CreateVideo(video, i);

                VideoPanel.Controls.Add(videoBox);
            }
        }

        private PictureBox CreateVideo(Video video, int i)
        {
            string? video_id = video.url?.Split('=')[1];
            string thumbnail_type = "hqdefault"; // or "mqdefault", "sddefault", "maxresdefault"
            string imageUrl = $"https://img.youtube.com/vi/{video_id}/{thumbnail_type}.jpg";

            Image image;
            using (var httpClient = new HttpClient())
            {
                var imageBytesTask = httpClient.GetByteArrayAsync(imageUrl);
                imageBytesTask.Wait();
                byte[] imageBytes = imageBytesTask.Result;
                using (MemoryStream ms = new MemoryStream(imageBytes))
                {
                    image = Image.FromStream(ms);
                }
            }

            PictureBox videoPictureBox = new PictureBox
            {
                Image = image,
                SizeMode = PictureBoxSizeMode.StretchImage,
                Size = ImageSize,
                Location = new Point(20, 30 + (i * ImageSize.Height) + (i * 10)),
                BackColor = Color.Transparent,
                BorderStyle = BorderStyle.FixedSingle,
                Cursor = Cursors.Hand,
            };

            Button playButton = new Button
            {
                Size = new Size(60, 60),
                Location = new Point((videoPictureBox.Width - 60) / 2, (videoPictureBox.Height - 60) / 2),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(180, 0, 0, 0),
                ForeColor = Color.White,
                Text = "▶",
                Font = new Font("Segoe UI", 24, FontStyle.Bold),
                TabStop = false,
                Cursor = Cursors.Hand,
                Parent = videoPictureBox,
                FlatAppearance = { BorderSize = 0 },
            };
            playButton.BringToFront();
            videoPictureBox.Controls.Add(playButton);
            
            playButton.Click += (sender, e) =>
            {
                System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                {
                    FileName = video.url,
                    UseShellExecute = true
                });
            };

            videoPictureBox.Click += (sender, e) =>
            {
                System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                    {
                        FileName = video.url,
                        UseShellExecute = true
                    });
            };

            videoPictureBox.MouseEnter += (sender, e) => playButton.BackColor = Color.FromArgb(255, 0, 0, 0);
            videoPictureBox.MouseLeave += (sender, e) => playButton.BackColor = Color.FromArgb(180, 0, 0, 0);

            return videoPictureBox;
        }
    }
}