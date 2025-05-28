using LeetCodeCoach.Models;
using LeetCodeCoach.Components.Buttons;
using LeetCodeCoach.Components.Indicators.DifficultyCounters;

namespace LeetCodeCoach.Components.Panels
{
    public class Topbar : ComponentBase
    {
        protected Panel TopBar { get; set; } = new Panel();

        protected Panel DifficultyCounters { get; set; } = new Panel();

        protected DifficultyCounter EasyCounter = new DifficultyCounter();

        protected DifficultyCounter MediumCounter = new DifficultyCounter();

        protected DifficultyCounter HardCounter = new DifficultyCounter();

        public Panel SearchUser { get; set; }

        public Label TopicName { get; set; }
        
        public Button VideoButton = new VideoButton().Build();

        public BackButton BackButton { get; set; } = new BackButton();
        
        public Topbar()
        {
            TopBar.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            TopBar.Size = new Size(_form.Width, 45);
            TopBar.BackColor = Color.FromArgb(255, 10, 10, 10);

            BackButton.Button.Location = new Point(15, 10);
            TopBar.Controls.Add(BackButton.Button);

            SearchUser = new SearchButton(this).Build();
            SearchUser.Name = "SearchUser";
            SearchUser.Location = new Point(40, 9);

            if (!string.IsNullOrEmpty(User.Username))
            {
                SearchUser.Controls[0].Text = User.Username;
                SearchUser.Controls[0].Enabled = false;

                SearchUser.Controls[1].Text = "Unlock";
            }

            TopBar.Controls.Add(SearchUser);

            TopicName = new Label();
            TopicName.Text = "";
            TopicName.Font = new Font("Segoe UI", 20, FontStyle.Bold);
            TopicName.ForeColor = Color.White;
            TopicName.BackColor = Color.Transparent;
            TopicName.TextAlign = ContentAlignment.MiddleCenter;
            TopicName.Size = new Size(400, 45);
            TopicName.Location = new Point(440, 0);
            TopBar.Controls.Add(TopicName);
            

            DifficultyCounters.Dock = DockStyle.Right;
            DifficultyCounters.Size = new Size(445, 45);

            BuildIndicators();
            TopBar.Controls.Add(DifficultyCounters);
        }

        public Panel Build()
        {
            return TopBar;
        }

        private void BuildIndicators()
        {
            VideoButton.Visible = false;
            VideoButton.Location = new Point(0, 8);
            DifficultyCounters.Controls.Add(VideoButton);

            Panel EasyPanel = EasyCounter.Easy();
            EasyPanel.Location = new Point(50, 13);
            DifficultyCounters.Controls.Add(EasyPanel);

            Panel MediumPanel = MediumCounter.Medium();
            MediumPanel.Location = new Point(155, 13);
            DifficultyCounters.Controls.Add(MediumPanel);

            Panel HardPanel = HardCounter.Hard();
            HardPanel.Location = new Point(285, 13);
            DifficultyCounters.Controls.Add(HardPanel);
            
            Button settingsButton = new SettingsButton().Build();
            settingsButton.Location = new Point(385, 10);
            DifficultyCounters.Controls.Add(settingsButton);
        }

        public void UpdateIndicators()
        {
            EasyCounter.UpdateCounter();
            MediumCounter.UpdateCounter();
            HardCounter.UpdateCounter();
        }
    }
}