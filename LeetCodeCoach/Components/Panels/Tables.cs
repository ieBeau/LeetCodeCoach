using LeetCodeCoach.Components.Tables;

namespace LeetCodeCoach.Components.Panels
{
    public class Tables : ComponentBase
    {
        private static Panel Panel { get; set; } = new Panel();

        private static DataGridView? DataGridSystem { get; set; }

        private static DataGridView? DataGridUser { get; set; }
        
        public Tables()
        {
            Panel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Panel.Size = new Size(3000, 3000);
            Panel.BackColor = Color.FromArgb(255, 15, 15, 15);
            Panel.Location = new Point(0, 0);
            Panel.AutoScroll = false;
            Panel.Visible = false;
            
            DataGridSystem = new TableSystem().Build();
            DataGridSystem.Location = new Point(11, 55);
            Panel.Controls.Add(DataGridSystem);
            
            DataGridUser = new TableUser().Build();
            DataGridUser.Location = new Point(617, 55);
            Panel.Controls.Add(DataGridUser);
        }

        public Panel Build()
        {
            return Panel;
        }
    }

}