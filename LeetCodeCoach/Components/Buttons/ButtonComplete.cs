using System.Text.Json;
using LeetCodeCoach.Models;

namespace LeetCodeCoach.Components.Buttons
{
    public class CompleteButton : ComponentBase
    {
        private Button Button { get; set; } = new Button();

        public CompleteButton()
        {
            Button.Text = "Submit";
            Button.Name = "CompleteButton";
            Button.Size = new Size(150, 50);
            Button.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point);
            Button.ForeColor = Color.Black;
            Button.BackColor = Color.White;
            Button.FlatAppearance.BorderColor = Color.Black;
            Button.Cursor = Cursors.Hand;
            Button.FlatStyle = FlatStyle.Flat;
            Button.FlatAppearance.BorderSize = 1;
            Button.MouseEnter += (sender, e) => Button.BackColor = Color.LightGray;
            Button.MouseLeave += (sender, e) => Button.BackColor = Color.White;
            Button.Click += Button_Click;
        }

        public Button Build()
        {
            return Button;
        }

        private async void Button_Click(object? sender, EventArgs e)
        {
            Timer.Pause();

            JsonDocument previousCompletion = await LeetCode.FetchCompletionAsync(); // Fix: Await the Task<string> and assign the result to a string variable
            var previousQuestion = previousCompletion.RootElement.GetProperty("data").GetProperty("recentAcSubmissionList")[0];

            Question? CurrentQuestion = _form.CurrentQuestion;

            bool isCompleted = previousQuestion.GetProperty("titleSlug").GetString() == GetPathFromUrl(CurrentQuestion?.url ?? string.Empty);

            long currentTimestamp = new DateTimeOffset(_form.CurrentQuestionTime).ToUnixTimeSeconds();
            long previousTimestamp = long.Parse(previousQuestion.GetProperty("timestamp").GetString() ?? "0");

            // Time difference between LeetCode and the start of the question being clicked.
            TimeSpan timeDifference = Timer.GetTimeDifference(currentTimestamp, previousTimestamp);
            string timeSpent = timeDifference.ToString();

            // If timer was paused, use timer for timestamp.
            if (Timer.HasPaused()) timeSpent = Timer.GetTime();

            if (isCompleted && timeDifference.TotalSeconds > 0)
            {
                Timer.Stop();

                int topicId = int.Parse(_form.CurrentTopicId);
                string currentDate = DateTime.Now.ToString("yyyy-MM-dd");

                if (CurrentQuestion == null) return;
                
                DB.SetQuestion(User.Id, CurrentQuestion.id, topicId, string.Join("|", CurrentQuestion.tags), CurrentQuestion.title ?? string.Empty, CurrentQuestion.difficulty ?? string.Empty, CurrentQuestion.url ?? string.Empty, timeSpent, currentDate);

                QuestionContainer CompletedQuestions = _form.CompletedQuestions;

                Question newQuestion = new Question
                {
                    id = CompletedQuestions.GetAll().Count + 1,
                    user_id = User.Id,
                    question_id = CurrentQuestion.id,
                    topic_id = topicId,
                    tags = CurrentQuestion.tags,
                    title = CurrentQuestion.title,
                    difficulty = CurrentQuestion.difficulty,
                    url = CurrentQuestion.url,
                    time = timeSpent,
                    date = currentDate
                };

                Form1.QuestionsService.Add(newQuestion);

                _form.UpdateComplete(newQuestion);

                MessageBox.Show($"Congratulations! Question completed in: {timeSpent}");
            }
            else
            {
                Timer.Continue();
                MessageBox.Show("Question has not been completed!");
            }
        }
        
        private string GetPathFromUrl(string url)
        {
            var parts = url.TrimEnd('/').Split('/');
            return parts.Length >= 2 ? parts[^1] : string.Empty;
        }
    }
}