using System.Text.Json;
using LeetCodeCoach.Components.Panels;
using LeetCodeCoach.Models;

namespace LeetCodeCoach.Components.Buttons
{
    public class SearchButton : ComponentBase
    {
        private readonly Topbar _topbar;

        private Button Button { get; set; } = new Button();

        private TextBox UsernameText { get; set; } = new TextBox();

        private Panel SearchPanel { get; set; } = new Panel();

        public SearchButton(Topbar topbar)
        {
            _topbar = topbar;

            UsernameText.Name = "Username";
            UsernameText.Size = new Size(150, 30);
            UsernameText.Margin = new Padding(10);
            UsernameText.Location = new Point(0, 2);
            UsernameText.Enabled = true;

            Button.Name = "SearchButton";
            Button.Text = "Lock";
            Button.Size = new Size(75, 27);
            Button.Location = new Point(160, 0);
            Button.Cursor = Cursors.Hand;
            Button.Click += Button_Click;

            SearchPanel.Size = new Size(350, 50);
            SearchPanel.Controls.Add(UsernameText);
            SearchPanel.Controls.Add(Button);
        }

        public Panel Build()
        {
            return SearchPanel;
        }

        private void Button_Click(object? sender, EventArgs e)
        {
            if (UsernameText.Enabled && UsernameText.Text == User.Username)
            {
                UsernameText.Enabled = false;
                UsernameText.BackColor = Color.LightGray;
                
                Button.Text = "Unlock";
            } else if (UsernameText.Enabled)
            {
                Search();
            }
            else
            {
                _form.Controls.Find("VideoContainer", true)[0].Visible = false;

                if (_form.Topbar is not null)
                {
                    _form.Topbar.BackButton.Disable();
                    _form.Topbar.TopicName.Text = "";
                    _form.Topbar.VideoButton.Visible = false;
                }

                if (_form.TablesPanel is not null) _form.TablesPanel.Visible = false;

                UsernameText.Enabled = true;
                Button.Text = "Lock";
                return;
            }
        }

        private async void Search()
        {
            JsonDocument data = await LeetCode.FetchUserAsync(UsernameText.Text);
            var userData = data.RootElement.GetProperty("data").GetProperty("matchedUser");

            if (userData.ValueKind != JsonValueKind.Null)
            {
                Button.Text = "Unlock";
                UsernameText.Enabled = false;
                UsernameText.BackColor = Color.LightGray;

                if (!DB.UserExist(UsernameText.Text)) DB.SetUser(UsernameText.Text);

                User newUser = DB.GetUser(UsernameText.Text);

                User.Id = newUser.Id;
                User.Username = newUser.Username;

                DB.SetCurrent(User.Id, User.Username);

                UpdateData();

                UsernameText.BackColor = Color.White;

                MessageBox.Show($"{UsernameText.Text} successfully added!");
            }
            else
            {
                MessageBox.Show("User not found.");
            }
        }

        private void UpdateData()
        {
            _form.UpdateData();
            _topbar.UpdateIndicators();
        }
    }
}