using System.Diagnostics;

namespace LeetCodeCoach.Components.Buttons
{
    public class DeleteButton : ComponentBase
    {
        private Button Button { get; set; }

        private string Type { get; set; }

        public DeleteButton(string type)
        {
            Type = type.ToLower();

            Button = new Button
            {
                Name = "DeleteUserDataButton",
                Text = "Delete",
                Size = new Size(50, 25),
                BackColor = Color.DarkRed,
                ForeColor = Color.White,
                Cursor = Cursors.Hand,
                FlatStyle = FlatStyle.Flat,
                FlatAppearance = { 
                    BorderSize = 0, 
                    MouseOverBackColor = Color.Transparent, 
                    MouseDownBackColor = Color.Transparent 
                },  
            };

            Button.Click += onPress;
        }

        public Button Build()
        {
            return Button;
        }

        private void onPress(object? sender, EventArgs e)
        {
            switch (Type)
            {
                case "user":
                    if (MessageBox.Show("Are you sure you want to delete your user data?", "Delete User Data", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                    {
                        DB.DeleteUser(User);
                        _form.UpdateDeleteUser();
                        _form.Topbar?.UpdateIndicators();
                        _form.Roadmap?.ClearRoadmap();
                        MessageBox.Show("User data deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    break;
                case "all":
                    if (MessageBox.Show("Are you sure you want to delete all user data?", "Delete All User Data", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                    {
                        DB.DeleteAllData();
                        _form.UpdateDeleteUser();
                        _form.Topbar?.UpdateIndicators();
                        _form.Roadmap?.ClearRoadmap();

                        MessageBox.Show("All data deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    break;
                default:
                    Debug.WriteLine($"Unknown type: {Type}");
                    break;
            }

            if (_form.TopPanel is Panel)
            {
                // Unlock user input
                _form.TopPanel.Controls[1].Controls[0].Enabled = true;
                _form.TopPanel.Controls[1].Controls[0].Text = string.Empty;

                // Unlock user lock button
                _form.TopPanel.Controls[1].Controls[1].Text = "Lock";
            }
        }
    }
}