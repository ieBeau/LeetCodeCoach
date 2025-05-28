namespace LeetCodeCoach.Components.Indicators.DifficultyCounters
{
    public class DifficultyCounter : ComponentBase
    {
        private string Difficulty { get; set; } = string.Empty;

        private Panel Panel { get; set; } = new Panel();
        
        private Label Text { get; set; } = new Label();

        private Label Counter { get; set; } = new Label();

        public DifficultyCounter()
        {
            Panel.Size = new Size(100, 30);
            Panel.AutoSize = true;

            Text.Font = new Font("Segoe UI", 10);
            Text.Location = new Point(0, 0);
            Text.BackColor = Color.Transparent;
            Text.AutoSize = true;

            Counter.Text = "0/0";
            Counter.Font = new Font("Segoe UI", 10);
            Counter.ForeColor = Color.White;
            Counter.BackColor = Color.Transparent;
            Counter.AutoSize = true;

            Panel.Controls.Add(Text);
            Panel.Controls.Add(Counter);
        }

        public Panel Easy()
        {
            Difficulty = "Easy";

            Text.Text = Difficulty;
            Text.ForeColor = Color.Green;
            Counter.Location = new Point(38, 0);

            Counter.Text = $"{QuestionsService.QuestionDifficultyComplete["Easy"].Count}/{QuestionsService.QuestionDifficultyTotal["Easy"]}";

            return Panel;
        }

        public Panel Medium()
        {
            Difficulty = "Medium";
            
            Text.Text = Difficulty;
            Text.ForeColor = Color.Orange;
            Counter.Location = new Point(65, 0);
            
            Counter.Text = $"{QuestionsService.QuestionDifficultyComplete["Medium"].Count}/{QuestionsService.QuestionDifficultyTotal["Medium"]}";

            return Panel;
        }

        public Panel Hard()
        {
            Difficulty = "Hard";

            Text.Text = Difficulty;
            Text.ForeColor = Color.Red;
            Counter.Location = new Point(43, 0);

            Counter.Text = $"{QuestionsService.QuestionDifficultyComplete["Hard"].Count}/{QuestionsService.QuestionDifficultyTotal["Hard"]}";

            return Panel;
        }

        public void UpdateCounter()
        {
            Counter.Text = $"{QuestionsService.QuestionDifficultyComplete[Difficulty].Count}/{QuestionsService.QuestionDifficultyTotal[Difficulty]}";
        }
    }
}