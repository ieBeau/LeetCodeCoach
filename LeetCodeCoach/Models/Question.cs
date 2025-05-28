namespace LeetCodeCoach.Models
{
    public class Question
    {
        public int id { get; set; }

        public int user_id { get; set; }

        public int question_id { get; set; }

        public int topic_id { get; set; }

        public List<string> tags { get; set; } = new List<string>();

        public string? title { get; set; }

        public string? difficulty { get; set; }

        public string? url { get; set; }

        public string? time { get; set; }

        public string? date { get; set; }
    }
    
    public class QuestionContainer
    {
        public List<Question> questions = new List<Question>();

        public void Add(Question question)
        {
            questions.Add(question);
        }

        public void Remove(int UserId, int questionId)
        {
            Question? question = questions.Find(q => q.user_id == UserId && q.id == questionId);
            if (question != null) questions.Remove(question);
        }

        public Question? Get(int UserId, int questionId)
        {
            return questions.Find(q => q.user_id == UserId && q.id == questionId);
        }

        public List<Question> GetAll()
        {
            return questions;
        }
    }
}