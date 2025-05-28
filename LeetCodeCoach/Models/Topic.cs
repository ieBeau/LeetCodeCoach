
namespace LeetCodeCoach.Models
{
    public class Topic
    {
        public int id { get; set; }

        public string? name { get; set; }

        public List<int> location { get; set; } = new List<int>();

        public List<string> pointers { get; set; } = new List<string>();

        public List<Video> videos { get; set; } = new List<Video>();
        
        public List<Question> questions { get; set; } = new List<Question>();
    }

    public class TopicContainer
    {
        private static List<Topic> topics { get; set; } = new List<Topic>();

        public TopicContainer() { }

        public void Add(Topic topic)
        {
            topics.Add(topic);
        }

        public List<Topic> Get()
        {
            return topics;
        }
    }
}
