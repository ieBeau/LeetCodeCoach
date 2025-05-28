using System.Text.Json;
using LeetCodeCoach.Models;

namespace LeetCodeCoach.Controllers.Data
{
    public class Data : ControllerBase
    {
        private static Data? _instance;

        public static Data Instance => _instance ??= new Data();

        private string FilePath { get; } = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "topics.json");

        public Dictionary<string, Topic>? Topics { get; private set; } = new Dictionary<string, Topic>();
        private Data()
        {
            string jsonData = File.ReadAllText(FilePath);

            Topics = JsonSerializer.Deserialize<Dictionary<string, Topic>>(jsonData);
        }
    }
}