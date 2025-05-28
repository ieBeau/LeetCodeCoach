using System.Drawing.Drawing2D;
using LeetCodeCoach.Models;
using LeetCodeCoach.Components.Cards;
using LeetCodeCoach.Controllers.Data;
using LeetCodeCoach.Components.Graphs;

namespace LeetCodeCoach.Components.Panels
{
    public class Roadmap : ComponentBase
    {
        private static Panel Panel { get; set; } = new Panel();

        public static Panel RoadmapPanel { get; set; } = new Panel();
        
        public static QuestionContainer? CompletedQuestions { get; set; }

        private static TopicContainer? Topics { get; set; }

        public static Dictionary<string, TopicCard> Nodes { get; set; } = new Dictionary<string, TopicCard>();

        public static Dictionary<string, Point> OriginalNodePositions { get; set; } = new Dictionary<string, Point>();

        public static Dictionary<int, int> topicCount { get; set; } = new Dictionary<int, int>();

        public static Dictionary<int, HashSet<int>> TopicCompleteCount { get; set; } = new Dictionary<int, HashSet<int>>();

        public static Dictionary<string, int> QuestionCount { get; set; } = new Dictionary<string, int>();

        public static Dictionary<int, int> QuestionCompleteCount { get; set; } = new Dictionary<int, int>();

        private static List<(string from, string to)> Edges { get; set; } = new List<(string from, string to)>();

        public static Dictionary<string, float> DataStructureScores { get; private set; } = new Dictionary<string, float>();

        public static Dictionary<string, float> AlgorithmScores { get; private set; } = new Dictionary<string, float>();

        private Hexagon? DataStructureGraph { get; set; }
        
        private Hexagon? AlgorithmGraph { get; set; }

        public static Point canvasOffset = new Point(0, 0);

        private int Radius { get; set; } = 100;

        private static float zoom { get; set; } = 1.0f;
        
        // private static Point dragStart { get; set; }

        // private static bool dragging { get; set; } = false;
        
        public Roadmap()
        {
            CompletedQuestions = _form.CompletedQuestions ?? new QuestionContainer();

            Topics = Form1.QuestionsService.Topics;
            Edges = Form1.QuestionsService.TopicPointers;

            Panel.Size = new Size(3000, 3000);
            Panel.BackColor = Color.FromArgb(255, 15, 15, 15);
            Panel.Location = new Point(0, 0);

            RoadmapPanel.Size = new Size(3000, 3000);
            RoadmapPanel.BackColor = Color.FromArgb(255, 15, 15, 15);
            RoadmapPanel.Location = new Point(25, 25);
            Panel.Controls.Add(RoadmapPanel);

            InitializeRoadmap();
        }

        public Panel Build()
        {
            return Panel;
        }

        public void InitializeRoadmap()
        {
            InitializeQuestions();
            CreateNodes();

            RoadmapPanel.Paint += RoadmapPanel_Paint;

            // // Enable zooming
            // Panel.MouseWheel += Roadmap_MouseWheel;

            // // Enable panning
            // Panel.MouseDown += RoadmapPanel_MouseDown;
            // Panel.MouseMove += RoadmapPanel_MouseMove;
            // Panel.MouseUp += RoadmapPanel_MouseUp;

            UpdateNodeLayouts();
            
            BuildGraphs();
        }

        public void CreateNodes()
        {
            if(Topics == null) return;

            foreach (Topic topic in Topics.Get())
            {
                if (string.IsNullOrEmpty(topic.name)) continue;
                TopicCard card = new TopicCard(OriginalNodePositions, topic.name, new Point(topic.location[0], topic.location[1]), topic.id);
                Nodes[topic.name] = card;
            }
        }

        private void RoadmapPanel_Paint(object? sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            Pen basePen = new Pen(Color.WhiteSmoke, 2);

            var groupedByChild = Edges.GroupBy(x => x.to);

            foreach (var group in groupedByChild)
            {
                string childKey = group.Key;
                if (!Nodes.ContainsKey(childKey)) continue;

                Label child = Nodes[childKey].GetLabel();
                PointF childTop = new PointF(child.Left + child.Width / 2, child.Top);

                // Choose a merge point slightly above the child
                PointF mergePoint = new PointF(childTop.X, childTop.Y - 10);

                // For each parent, draw a line to the merge point
                foreach (var edge in group)
                {
                    if (!Nodes.ContainsKey(edge.from)) continue;
                    Label parent = Nodes[edge.from].GetLabel();

                    PointF start = new PointF(parent.Left + parent.Width / 2, parent.Top + parent.Height);
                    PointF cp1 = new PointF(start.X, start.Y + 30);
                    PointF cp2 = new PointF(mergePoint.X, mergePoint.Y - 30);

                    // These curves converge to the merge point, no arrow
                    g.DrawBezier(basePen, start, cp1, cp2, mergePoint);
                }

                // Now draw ONE arrow from merge point to child
                using (Pen arrowPen = (Pen)basePen.Clone())
                {
                    arrowPen.CustomEndCap = new AdjustableArrowCap(5, 5);
                    g.DrawLine(arrowPen, mergePoint, childTop);
                }
            }
        }

        private void InitializeQuestions()
        {
            QuestionCompleteCount.Clear();
            TopicCompleteCount.Clear();

            if (CompletedQuestions != null)
            {
                List<Question> userQuestions = CompletedQuestions.GetAll();
                foreach (Question question in userQuestions)
                {
                    AddQuestion(question);
                }
            }

            if (Topics != null)
            {
                foreach (Topic topic in Topics.Get())
                {
                    int key = int.Parse(topic.id.ToString());
                    var defaultQuestions = topic.questions;

                    foreach (Question question in defaultQuestions)
                    {
                        // Count the number of questions per difficulty
                        if (!string.IsNullOrEmpty(question.difficulty))
                        {
                            if (QuestionCount.ContainsKey(question.difficulty)) QuestionCount[question.difficulty]++;
                            else QuestionCount[question.difficulty] = 1;
                        }

                        // Count the number of questions per topic
                        if (topicCount.ContainsKey(key)) topicCount[key]++;
                        else topicCount[key] = 1;
                    }
                }
            }
        }

        private void AddQuestion(Question question)
        {
            // Count the number of completed questions
            if (QuestionCompleteCount.ContainsKey(question.question_id)) QuestionCompleteCount[question.question_id]++;
            else QuestionCompleteCount[question.question_id] = 1;

            // Count the number of completed questions per topic
            if (!TopicCompleteCount.ContainsKey(question.topic_id)) TopicCompleteCount[question.topic_id] = [];
            TopicCompleteCount[question.topic_id].Add(question.question_id);
        }

        private void UpdateRoadmap(Question question)
        {
            string? topicName = Data.Instance.Topics?[question.topic_id.ToString()].name;
            if (topicName != null) Nodes[topicName].UpdateProgressBar();
        }

        public void AddCardValue(Question question)
        {
            AddQuestion(question);
            UpdateRoadmap(question);
        }

        public void RemoveCardValue(Question question)
        {
            QuestionCompleteCount[question.question_id]--;
            if (QuestionCompleteCount[question.question_id] == 0) TopicCompleteCount[question.topic_id].Remove(question.question_id);

            UpdateRoadmap(question);
        }

        public void ClearRoadmap()
        {
            QuestionCompleteCount.Clear();
            TopicCompleteCount.Clear();

            foreach (var node in Nodes)
            {
                Nodes[node.Key].UpdateProgressBar();
            }
        }

        public void UpdateValues(Question question, float score)
        {
            if (question == null) return;

            Dictionary<string, string> TagNames = DataGraphs.Instance.TagsToName;

            foreach (var tag in question.tags)
            {
                if (TagNames.ContainsKey(tag))
                {
                    string topic = TagNames[tag];

                    if (DataStructureScores.Keys.Contains(topic)) 
                    {
                        DataStructureScores[topic] += score;
                    }

                    if (AlgorithmScores.Keys.Contains(topic)) 
                    {
                        AlgorithmScores[topic] += score;
                    }
                }
            }

            // Remove old graphs
            foreach (Control control in RoadmapPanel.Controls.OfType<Panel>().ToList())
            {
                if (control.Name == "Data Structures" || control.Name == "Algorithms")
                {
                    RoadmapPanel.Controls.Remove(control);
                    control.Dispose();
                }
            }
            
            BuildGraphs();
        }

        private void BuildGraphs()
        {
            IntitializeScores();

            DataStructureGraph = new Hexagon("Data Structures", DataStructureScores, Radius);
            Panel dataStructurePanel = DataStructureGraph.Build();
            dataStructurePanel.Name = "Data Structures";
            dataStructurePanel.Location = new Point(_form.Width - 250 - Radius * 4, 35);
            RoadmapPanel.Controls.Add(dataStructurePanel);

            AlgorithmGraph = new Hexagon("Algorithms", AlgorithmScores, Radius);
            Panel algorithmPanel = AlgorithmGraph.Build();
            algorithmPanel.Name = "Algorithms";
            algorithmPanel.Location = new Point(_form.Width - 125 - Radius * 2, 35);
            RoadmapPanel.Controls.Add(algorithmPanel);
        }

        private void IntitializeScores()
        {
            DataStructureScores = new Dictionary<string, float>();
            foreach (var kvp in DataGraphs.Instance.DataStructures)
            {
                float total = QuestionsService.GraphTotalCount[kvp.Key];
                float complete = QuestionsService.GraphCompleteCount.Keys.Contains(kvp.Key) ? QuestionsService.GraphCompleteCount[kvp.Key] : 0;
                float percentage = complete / total;

                DataStructureScores[kvp.Key] = percentage;
            }
            
            AlgorithmScores = new Dictionary<string, float>();
            foreach (var kvp in DataGraphs.Instance.Algorithms)
            {
                float total = QuestionsService.GraphTotalCount[kvp.Key];
                float complete = QuestionsService.GraphCompleteCount.Keys.Contains(kvp.Key) ? QuestionsService.GraphCompleteCount[kvp.Key] : 0;
                float percentage = complete / total;

                AlgorithmScores[kvp.Key] = percentage;
            }
        }

        // private void Roadmap_MouseWheel(object sender, MouseEventArgs e)
        // {
        //     float oldZoom = zoom;
        //     if (e.Delta > 0) zoom += 0.1f;
        //     else zoom -= 0.1f;

        //     if (zoom < 0.3f) zoom = 0.3f;
        //     if (zoom > 2.5f) zoom = 2.5f;

        //     UpdateNodeLayouts();
        // }

        // // Panning
        // private void RoadmapPanel_MouseDown(object sender, MouseEventArgs e)
        // {
        //     dragging = true;
        //     dragStart = e.Location;
        //     RoadmapPanel.Cursor = Cursors.Hand;
        // }

        // private void RoadmapPanel_MouseMove(object sender, MouseEventArgs e)
        // {
        //     if (dragging)
        //     {
        //         Point delta = new Point(e.X - dragStart.X, e.Y - dragStart.Y);
        //         canvasOffset.X += delta.X;
        //         canvasOffset.Y += delta.Y;
        //         dragStart = e.Location;

        //         UpdateNodeLayouts();
        //     }
        // }

        // private void RoadmapPanel_MouseUp(object sender, MouseEventArgs e)
        // {
        //     dragging = false;
        //     RoadmapPanel.Cursor = Cursors.Default;
        // }

        public void UpdateNodeLayouts()
        {
            foreach (var node in Nodes)
            {
                string key = node.Key;
                Label Node = node.Value.GetLabel();

                Point original = OriginalNodePositions[key];

                int x = (int)(original.X * zoom) + canvasOffset.X;
                int y = (int)(original.Y * zoom) + canvasOffset.Y;

                int width = (int)(140 * zoom);
                int height = (int)(40 * zoom);

                Node.Location = new Point(x, y);
                Node.Size = new Size(width, height);

                if (Node.Controls.Count > 0)
                {
                    Panel progressBar = (Panel)Node.Controls[0];
                    progressBar.Location = new Point(0, height - 5);
                    progressBar.Size = new Size(Node.Width, 5);
                }
            }

            RoadmapPanel.Invalidate();
        }
    }

}