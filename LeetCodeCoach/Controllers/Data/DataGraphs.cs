using System.Text.Json;
using LeetCodeCoach.Models;

namespace LeetCodeCoach.Controllers.Data
{
    public class DataGraphs : ControllerBase
    {
        private static DataGraphs? _instance;

        public static DataGraphs Instance => _instance ??= new DataGraphs();

        private string FilePath { get; } = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "graphs.json");

        private Dictionary<string, Dictionary<string, Dictionary<string, List<string>>>> Data { get; set; }
        
        public Dictionary<string, string> TagsToName { get; set; }

        public Dictionary<string, string> NameToGraph { get; set; }

        public Dictionary<string, GraphData> DataStructures { get; set; }

        public Dictionary<string, GraphData> Algorithms { get; set; }

        private DataGraphs()
        {
            TagsToName = new Dictionary<string, string>();
            NameToGraph = new Dictionary<string, string>();
            
            string jsonData = File.ReadAllText(FilePath);

            Data = JsonSerializer.Deserialize<Dictionary<string, Dictionary<string, Dictionary<string, List<string>>>>>(jsonData) 
                   ?? new Dictionary<string, Dictionary<string, Dictionary<string, List<string>>>>();

            foreach (var kvp in Data.Keys)
            {
                foreach (var name in Data[kvp].Keys)
                {
                    if (!NameToGraph.ContainsKey(name)) NameToGraph[name] = kvp;
                    foreach (var tag in Data[kvp][name]["Tags"])
                    {
                        if (!TagsToName.ContainsKey(tag)) TagsToName[tag] = name;
                    }
                }
            }

            DataStructures = new Dictionary<string, GraphData>();
            DataStructures = Data["Data Structures"].ToDictionary(
                kvp => kvp.Key,
                kvp => new GraphData
                {
                    Name = kvp.Key,
                    Tags = kvp.Value["Tags"]
                }
            );

            Algorithms = new Dictionary<string, GraphData>();
            Algorithms = Data["Algorithms"].ToDictionary(
                kvp => kvp.Key,
                kvp => new GraphData
                {
                    Name = kvp.Key,
                    Tags = kvp.Value["Tags"]
                }
            );
        }
    }
}