using LeetCodeCoach.Components.Panels;

namespace LeetCodeCoach.Components.Cards
{
    public class TopicCard : ComponentBase
    {
        private Label Label { get; set; } = new Label();

        private static Panel RoadmapPanel { get; set; } = Roadmap.RoadmapPanel;

        private int Id { get; set; }

        private static Dictionary<int, int> TopicCount { get; set; } = Roadmap.topicCount;

        private static Dictionary<int, HashSet<int>> TopicCompleteCount { get; set; } = Roadmap.TopicCompleteCount;

        private float Progress { get; set; } = 0;
        
        private Panel progressBar = new Panel();

        private Panel innerProgressBar = new Panel();

        private Color Color { get; set; } = Color.FromArgb(255, 94, 86, 56);

        public TopicCard(Dictionary<string, Point> originalNodePositions, string text, Point location, int id)
        {
            Id = id;

            int total = TopicCount.ContainsKey(Id) ? TopicCount[Id] : 0;
            int completed = TopicCompleteCount.ContainsKey(Id) ? TopicCompleteCount[Id].Count() : 0;

            Progress = (float)completed / total;

            if (Progress == 1) Color = Color.DarkGreen;

            Label.Text = text;
            Label.ForeColor = Color.White;
            Label.BackColor = Color;
            Label.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            Label.TextAlign = ContentAlignment.MiddleCenter;
            Label.Size = new Size(140, 40);
            Label.BorderStyle = BorderStyle.FixedSingle;
            Label.Cursor = Cursors.Hand;
            Label.AccessibleName = text;
            Label.AccessibleDescription = Id.ToString();

            Label.MouseEnter += (s, e) => Label.BackColor = Color.FromArgb(255, Math.Max(0, Color.R - 40), Math.Max(0, Color.G - 40), Math.Max(0, Color.B - 40));
            Label.MouseLeave += (s, e) => Label.BackColor = Color;

            ProgressBar();

            Label.Click += Progress_Click;

            RoadmapPanel.Controls.Add(Label);
            originalNodePositions[text] = location;
        }

        public Label GetLabel()
        {
            return Label;
        }

        private void ProgressBar()
        {
            progressBar.BackColor = Color.White;
            progressBar.Size = new Size(Label.Width, 5);
            progressBar.Location = new Point(0, Label.Height - 5);

            innerProgressBar.BackColor = Color.LimeGreen;
            UpdateProgressBar();

            progressBar.Controls.Add(innerProgressBar);
            Label.Controls.Add(progressBar);
        }

        public void UpdateProgressBar()
        {
            int total = TopicCount.ContainsKey(Id) ? TopicCount[Id] : 0;
            int completed = TopicCompleteCount.ContainsKey(Id) ? TopicCompleteCount[Id].Count() : 0;
            
            Progress = (float)completed / total;
            if (Progress == 1) 
            {
                Color = Color.DarkGreen;
                Label.BackColor = Color;
            }

            int barWidth = (int)(Progress * Label.Width);
            innerProgressBar.Size = new Size(barWidth, 5);
            
        }

        private void Progress_Click(object? sender, EventArgs e)
        {
            if (sender is Control control)
            {
                Control? searchButton = _form.TopPanel?.Controls.Find("SearchUser", true)[0].Controls.Find("Username", true)[0];
                if (searchButton is not null && searchButton.Enabled)
                {
                    searchButton.BackColor = Color.FromArgb(255, 255, 128, 128);
                    if (MessageBox.Show("Please lock-in a valid LeetCode username.\n\nWould you like to go to LeetCode to create an account?", "Invalid User", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk) == DialogResult.Yes)
                    {
                        System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                        {
                            FileName = "https://leetcode.com/accounts/login/",
                            UseShellExecute = true
                        });
                    }
                    return;
                }

                if (_form.Topbar is not null)
                {
                    _form.Topbar.BackButton.Enable();
                    _form.Topbar.VideoButton.Visible = true;
                }

                _form.CurrentTopicId = control.AccessibleDescription ?? string.Empty;
                _form.CurrentTopicName = control.AccessibleName ?? string.Empty;

                _form.BuildTables();

                if (_form.TablesPanel is not null) _form.TablesPanel.Visible = true;
            }
        }
    }
}
