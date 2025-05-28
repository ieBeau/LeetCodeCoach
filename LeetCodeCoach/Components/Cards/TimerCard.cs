using LeetCodeCoach.Components.Buttons;
using LeetCodeCoach.Components.Indicators;

namespace LeetCodeCoach.Components.Timer
{
    public class TimerCard : ComponentBase
    {
        private new System.Windows.Forms.Timer Timer { get; set; } = new System.Windows.Forms.Timer();

        private Panel Panel { get; set; }

        public Size PanelSize { get; set; } = new Size(500, 400);

        private Button CompleteButton { get; set; } = new CompleteButton().Build();

        private TimerControlButton PlayTimer { get; set; } = new TimerControlButton("Play");
        private TimerControlButton PauseTimer { get; set; } = new TimerControlButton("Pause");
        private TimerControlButton StopTimer { get; set; } = new TimerControlButton("Stop");

        private Button PlayButton { get; set; }
        private Button PauseButton { get; set; }
        private Button StopButton { get; set; }

        private TimerStatus TimerStatus { get; set; } = new TimerStatus();

        private Panel Status => TimerStatus.Build();

        private Label TimerLabel { get; set; }

        private Label QuestionName { get; set; }

        private int ElapsedSeconds { get; set; } = 0;

        private bool Paused { get; set; } = false;

        public TimerCard()
        {
            PlayButton = PlayTimer.Build();
            PauseButton = PauseTimer.Build();
            StopButton = StopTimer.Build();

            Panel = new Panel
            {
                Name = "TimerCard",
                Size = PanelSize,
                BackColor = Color.FromArgb(255, 10, 10, 10),
                BorderStyle = BorderStyle.FixedSingle,
            };

            QuestionName = new Label
            {
                Text = "",
                Name = "QuestionName",
                Font = new Font("Segoe UI", 25F, FontStyle.Bold, GraphicsUnit.Point),
                AutoEllipsis = true,
                // MaximumSize = new Size(PanelSize.Width, 0),
                TextAlign = ContentAlignment.TopCenter,
                ForeColor = Color.White,
                Size = new Size(PanelSize.Width, 50),
                Location = new Point(0, 20),
            };
            Panel.Controls.Add(QuestionName);

            // Panel Timer = TimerStatus.Build();
            Status.Location = new Point(125, 108);
            Panel.Controls.Add(Status);

            TimerLabel = new Label
            {
                Name = "Timer",
                Text = "00:00:00",
                Font = new Font("Segoe UI", 33F, FontStyle.Bold, GraphicsUnit.Point),
                ForeColor = Color.White,
                Size = new Size(PanelSize.Width, 50),
                TextAlign = ContentAlignment.MiddleCenter,
                Location = new Point(0, 85),
            };
            Panel.Controls.Add(TimerLabel);


            PauseButton.Location = new Point(125, 180);
            PauseButton.Click += (s, e) =>
            {
                PauseTimer.UpdateColor(Color.Orange);
                PlayTimer.UpdateColor(Color.White);
                Pause();
            };


            PlayButton.Location = new Point(PanelSize.Width / 2 - 50, 180);
            // PlayButton.Location = new Point(210, 180);
            PlayTimer.UpdateColor(Color.Green);
            PlayButton.Click += (s, e) =>
            {
                PauseTimer.UpdateColor(Color.White);
                PlayTimer.UpdateColor(Color.Green);
                Continue();
            };

            StopButton.Location = new Point(295, 180);
            StopTimer.UpdateColor(Color.DarkRed);
            StopButton.Click += (s, e) =>
            {
                PauseTimer.UpdateColor(Color.White);
                PlayTimer.UpdateColor(Color.Green);
                Stop();
            };

            Panel.Controls.Add(PlayButton);
            Panel.Controls.Add(PauseButton);
            Panel.Controls.Add(StopButton);

            CompleteButton.Location = new Point((PanelSize.Width - CompleteButton.Width) / 2, 300);
            Panel.Controls.Add(CompleteButton);

            InitializeTimer();
        }

        public Panel Build()
        {
            return Panel;
        }

        public void SetPanelWidth(int width)
        {
            if (width < 350) width = 350; // Minimum width to ensure visibility

            PanelSize = new Size(width, PanelSize.Height);

            Panel.Size = PanelSize;
            TimerLabel.Size = new Size(width, 50);
            QuestionName.Size = new Size(width, 50);

            Status.Location = new Point((width - Status.Width) / 2 - 110, 108);

            PauseButton.Location = new Point((width / 2) - 120, 180);
            PlayButton.Location = new Point(width / 2 - 45, 180);
            StopButton.Location = new Point((width / 2) + 30, 180);

            CompleteButton.Location = new Point((width - CompleteButton.Width) / 2 - 5, 300);
        }

        private void InitializeTimer()
        {
            Timer.Interval = 1000;
            Timer.Tick += Timer_Tick;
        }

        private void Timer_Tick(object? sender, EventArgs e)
        {
            ElapsedSeconds++;
            TimerLabel.Text = $"{ElapsedSeconds / 3600:D2}:{ElapsedSeconds / 60 % 60:D2}:{ElapsedSeconds % 60:D2}";
        }

        public void Start()
        {
            ElapsedSeconds = 0;
            TimerLabel.Text = "00:00:00";

            Panel.Controls.Find("QuestionName", true)[0].Text = Form1.Instance?.CurrentQuestion?.title ?? "";

            Timer.Start();
            TimerStatus.SetColor(Color.Green);
        }

        public void Continue()
        {
            Timer.Start();
            TimerStatus.SetColor(Color.Green);
        }

        public void Pause()
        {
            Paused = true;

            Timer.Stop();
            TimerStatus.SetColor(Color.Orange);
        }

        public void Stop()
        {
            ElapsedSeconds = 0;
            TimerLabel.Text = "00:00:00";

            Timer.Stop();
            TimerStatus.SetColor(Color.Red);

            RemoveTimer();
        }

        public void RemoveTimer()
        {
            // Remove control with name "TimerCard" from Panel's parent, if it exists
            if (Panel.Parent is not null)
            {
                var timerCardControl = Panel.Parent.Controls.Find("TimerCard", false).FirstOrDefault();
                if (timerCardControl != null) Panel.Parent.Controls.Remove(timerCardControl);
            }
        }

        public string GetTime()
        {
            return TimerLabel.Text;
        }

        public TimeSpan GetTimeDifference(long timestamp1, long timestamp2)
        {
            var dateTime1 = DateTimeOffset.FromUnixTimeSeconds(timestamp1).UtcDateTime;
            var dateTime2 = DateTimeOffset.FromUnixTimeSeconds(timestamp2).UtcDateTime;

            return dateTime2 - dateTime1;
        }

        public bool HasPaused()
        {
            return Paused;
        }
    }
}
