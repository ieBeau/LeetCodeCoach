using LeetCodeCoach.Components.Buttons;

namespace LeetCodeCoach.Components.Panels
{
    public class Settings : ComponentBase
    {
        private Panel SettingsPanel { get; set; }

        public Settings()
        {
            Size PanelSize = new Size(300, _form.Height - 45);

            SettingsPanel = new Panel
            {
                Size = PanelSize,
                BackColor = Color.FromArgb(255, 10, 10, 10),
                Location = new Point(_form.Width - PanelSize.Width, 45),
                Dock = DockStyle.Bottom,
                Anchor = AnchorStyles.Right | AnchorStyles.Top,
                Visible = false
            };

            Label TitleLabel = new Label
            {
                Text = "Settings",
                Font = new Font("Segoe UI", 15, FontStyle.Bold),
                AutoSize = true,
                ForeColor = Color.White,
                BackColor = Color.Transparent,
                Location = new Point(10, 10)
            };

            SettingsPanel.Controls.Add(TitleLabel);

            Label UserLabel = new Label
            {
                Text = "Delete User Data:",
                Font = new Font("Segoe UI", 12),
                AutoSize = true,
                ForeColor = Color.White,
                BackColor = Color.Transparent,
                Location = new Point(10, 80)
            };

            SettingsPanel.Controls.Add(UserLabel);

            Button DeleteUserButton = new DeleteButton("user").Build();
            DeleteUserButton.Location = new Point(PanelSize.Width - 80, 80);
            SettingsPanel.Controls.Add(DeleteUserButton);

            Label AllUserLabel = new Label
            {
                Text = "Delete All Data:",
                Font = new Font("Segoe UI", 12),
                AutoSize = true,
                ForeColor = Color.White,
                BackColor = Color.Transparent,
                Location = new Point(10, 130)
            };

            SettingsPanel.Controls.Add(AllUserLabel);

            Button DeleteAllUserButton = new DeleteButton("all").Build();
            DeleteAllUserButton.Location = new Point(PanelSize.Width - 80, 130);
            SettingsPanel.Controls.Add(DeleteAllUserButton);
        }

        public Panel Build()
        {
            return SettingsPanel;
        }
    }
}