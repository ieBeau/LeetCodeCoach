using LeetCodeCoach.Controllers.Data;
using LeetCodeCoach.Models;

namespace LeetCodeCoach.Services
{
    public class QuestionsService
    {
        private static QuestionsService? _instance;

        public static QuestionsService Instance => _instance ??= new QuestionsService();

        private readonly DataGraphs GraphData = DataGraphs.Instance;
        private readonly Dictionary<string, Topic> TopicData = Data.Instance.Topics!;

        public TopicContainer Topics { get; private set; }

        public QuestionContainer Questions { get; private set; }

        public List<(string from, string to)> TopicPointers { get; private set; }

        public Dictionary<string, float> GraphTotalCount { get; private set; } = new Dictionary<string, float>();
        public Dictionary<string, float> GraphCompleteCount { get; private set; } = new Dictionary<string, float>();

        public Dictionary<int, int> TopicTotal { get; private set; }
        public Dictionary<int, HashSet<string>> TopicComplete { get; private set; }

        public Dictionary<string, int> QuestionDifficultyTotal { get; private set; }
        public Dictionary<string, HashSet<string>> QuestionDifficultyComplete { get; private set; }

        public Dictionary<int, Dictionary<string, int>> TopicDifficultyTotal { get; private set; }
        public Dictionary<int, Dictionary<string, HashSet<string>>> TopicDifficultyComplete { get; private set; }

        public Dictionary<string, Question> BestQuestions { get; private set; }

        private QuestionsService()
        {
            Topics = new TopicContainer();
            Questions = new QuestionContainer();

            TopicPointers = new List<(string from, string to)>();

            TopicTotal = new Dictionary<int, int>();
            TopicComplete = new Dictionary<int, HashSet<string>>();

            QuestionDifficultyTotal = new Dictionary<string, int>();
            QuestionDifficultyComplete = new Dictionary<string, HashSet<string>>();

            TopicDifficultyTotal = new Dictionary<int, Dictionary<string, int>>();
            TopicDifficultyComplete = new Dictionary<int, Dictionary<string, HashSet<string>>>();

            BestQuestions = new Dictionary<string, Question>();
        }

        public void Initialize(QuestionContainer userContainer)
        {
            QuestionDifficultyTotal["Easy"] = 0;
            QuestionDifficultyTotal["Medium"] = 0;
            QuestionDifficultyTotal["Hard"] = 0;

            QuestionDifficultyComplete["Easy"] = new HashSet<string>();
            QuestionDifficultyComplete["Medium"] = new HashSet<string>();
            QuestionDifficultyComplete["Hard"] = new HashSet<string>();

            // Initialize the question container
            foreach (var data in TopicData)
            {
                Topic topic = data.Value;
                topic.id = int.Parse(data.Key);
                
                Topics.Add(topic);
                
                // Initialize pointers for Roadmap
                foreach (var pointer in topic.pointers)
                {
                    TopicPointers.Add((topic.name!, pointer));
                }

                TopicDifficultyTotal[topic.id] = new Dictionary<string, int>();
                TopicDifficultyTotal[topic.id]["Easy"] = 0;
                TopicDifficultyTotal[topic.id]["Medium"] = 0;
                TopicDifficultyTotal[topic.id]["Hard"] = 0;

                TopicDifficultyComplete[topic.id] = new Dictionary<string, HashSet<string>>();
                TopicDifficultyComplete[topic.id]["Easy"] = new HashSet<string>();
                TopicDifficultyComplete[topic.id]["Medium"] = new HashSet<string>();
                TopicDifficultyComplete[topic.id]["Hard"] = new HashSet<string>();

                foreach (Question question in topic.questions)
                {
                    Questions.Add(question);

                    // Add the total count for each graph name
                    foreach (string tag in question.tags)
                    {
                        string GraphName = GraphData.TagsToName[tag];

                        if (!GraphTotalCount.ContainsKey(GraphName)) GraphTotalCount[GraphName] = 0;
                        if (question.difficulty == "Easy") GraphTotalCount[GraphName] += 1;
                        else if (question.difficulty == "Medium") GraphTotalCount[GraphName] += 2;
                        else if (question.difficulty == "Hard") GraphTotalCount[GraphName] += 3;
                    }

                    // Count the number of questions per topic
                    if (TopicTotal.ContainsKey(topic.id)) TopicTotal[topic.id]++;
                    else TopicTotal[topic.id] = 1;

                    // Initialize the difficulty total counts
                    if (!string.IsNullOrEmpty(question.difficulty))
                    {
                        if (QuestionDifficultyTotal.ContainsKey(question.difficulty)) QuestionDifficultyTotal[question.difficulty]++;
                        else QuestionDifficultyTotal[question.difficulty] = 1;
                        
                        // Initialize the topic total counts
                        TopicDifficultyTotal[topic.id][question.difficulty]++;
                        TopicDifficultyComplete[topic.id][question.difficulty].Add(question.id.ToString());
                    }
                    
                }
            }

            // Initialize the best questions dictionary
            foreach (Question question in userContainer.GetAll())
            {
                // Initialize the difficulty complete counts
                if (!string.IsNullOrEmpty(question.difficulty)) QuestionDifficultyComplete[question.difficulty].Add(question.question_id.ToString());

                if (!TopicComplete.ContainsKey(question.topic_id)) TopicComplete[question.topic_id] = new HashSet<string>();
                TopicComplete[question.topic_id].Add(question.question_id.ToString());
                
                UpdateBestQuestion(question);
            }
        }

        public void Add(Question question)
        {
            UpdateBestQuestion(question);

            string Id = question.question_id.ToString();

            // Update the complete count for the difficulty level
            if (!string.IsNullOrEmpty(question.difficulty))
            {
                QuestionDifficultyComplete[question.difficulty].Add(Id);
                TopicDifficultyComplete[question.topic_id][question.difficulty].Add(Id);
            }

            // Update the complete count for the topic level
            if (!TopicComplete.ContainsKey(question.topic_id)) TopicComplete[question.topic_id] = new HashSet<string>();
            TopicComplete[question.topic_id].Add(Id);
        }

        public float AddScore(Question question)
        {
            float score = 0f;

            string Id = question.question_id.ToString();

            // question time
            int timeMs = QuestionTimeMs(question.time ?? string.Empty);

            // best question time
            int bestTimeMs = BestQuestions.ContainsKey(Id) ? QuestionTimeMs(BestQuestions[Id].time ?? string.Empty) : 0;
            
            // Add the question to the best questions dictionary
            if (bestTimeMs == 0 || timeMs < bestTimeMs) 
            {
                BestQuestions[Id] = question;
                
                float multiplier = 0;
                if (timeMs > 45 * 60 * 1000) multiplier = 0.25f;
                else if (timeMs > 25 * 60 * 1000) multiplier = 0.5f;
                else if (timeMs > 0 * 60 * 1000) multiplier = 1f;
                
                if (question.difficulty == "Easy") score = 1 * multiplier;
                else if (question.difficulty == "Medium") score = 2 * multiplier;
                else if (question.difficulty == "Hard") score = 3 * multiplier;

                // Add the complete count for each graph name
                foreach (string tag in question.tags)
                {
                    // Add the total count for each graph name
                    string GraphName = GraphData.TagsToName[tag];
                    if (!GraphCompleteCount.ContainsKey(GraphName)) GraphCompleteCount[GraphName] = 0;
                    GraphCompleteCount[GraphName] += score;
                }
            }

            return score;
        }

        public float RemoveScore(Question question, List<Question> questions)
        {
            float score = 0f;

            string Id = question.question_id.ToString();

            // question time
            int timeMs = QuestionTimeMs(question.time ?? string.Empty);

            // best question time
            Question bestQuestion = BestQuestions[Id];
            
            // Add the question to the best questions dictionary
            if (bestQuestion.user_id == question.user_id && bestQuestion.id == question.id)
            {
                BestQuestions.Remove(Id);
                
                float multiplier = 0;
                if (timeMs > 45 * 60 * 1000) multiplier = 0.25f;
                else if (timeMs > 25 * 60 * 1000) multiplier = 0.5f;
                else if (timeMs > 0 * 60 * 1000) multiplier = 1f;
                
                if (question.difficulty == "Easy") score = 1 * multiplier;
                else if (question.difficulty == "Medium") score = 2 * multiplier;
                else if (question.difficulty == "Hard") score = 3 * multiplier;

                score = -score;

                // Add the complete count for each graph name
                foreach (string tag in question.tags)
                {
                    // Add the total count for each graph name
                    string GraphName = GraphData.TagsToName[tag];
                    if (!GraphCompleteCount.ContainsKey(GraphName)) GraphCompleteCount[GraphName] = 0;
                    GraphCompleteCount[GraphName] += score;
                }

                // Get the best question time
                int bestTimeMs = 0;
                Question? newBestQuestion = null;
                foreach (Question q in questions)
                {
                    if (q.question_id == question.question_id)
                    {
                        int questionTimeMs = QuestionTimeMs(q.time ?? string.Empty);
                        if (questionTimeMs < bestTimeMs || bestTimeMs == 0)
                        {
                            bestTimeMs = questionTimeMs;
                            newBestQuestion = q;
                        }
                    }
                }

                if (bestTimeMs == 0) return score;

                if (newBestQuestion is Question) BestQuestions[Id] = newBestQuestion;
            }

            return score;
        }

        private void UpdateBestQuestion(Question question)
        {
            AddScore(question);
        }

        private int QuestionTimeMs(string time = "")
        {
            if (string.IsNullOrEmpty(time)) return 0;
            
            // Convert the time string to milliseconds
            List<int> list = time.Split(":").Select(int.Parse).ToList();

            if (list.Count < 3) return 0;
            
            int timeMs = list[0] * 60 * 1000 + list[1] * 1000 + list[2] * 1000 / 1000;
            return timeMs;
        }
    }
}