using LeetCodeCoach.Models;
using LeetCodeCoach.Components.Panels;
using LeetCodeCoach.Controllers.Data;
using LeetCodeCoach.Controllers.LeetCode;
using LeetCodeCoach.Controllers.SQLite;
using LeetCodeCoach.Services;

namespace LeetCodeCoach
{
    public partial class Form1 : Form
    {
        public static Form1? Instance { get; private set; }

        public static SQLite DB { get; private set; } = new SQLite();

        public static LeetCode LeetCode { get; private set; } = new LeetCode();

        public static User User { get; set; } = new User();

        public Data Data { get; private set; } = Data.Instance;

        public DateTime CurrentQuestionTime = DateTime.UtcNow;

        public string CurrentTopicName { get; set; } = string.Empty;

        public string CurrentTopicId { get; set; } = string.Empty;

        public Question? CurrentQuestion { get; set; }

        public QuestionContainer CompletedQuestions { get; private set; } = new QuestionContainer();

        public static QuestionsService QuestionsService { get; private set; } = QuestionsService.Instance;

        public List<Question> CompletedTopicQuestions { get; private set; } = new List<Question>();

        public Dictionary<string, Dictionary<string, float>> UserScores { get; private set; } = new Dictionary<string, Dictionary<string, float>>();

        private static Dictionary<int, int> QuestionCompleteCount = Roadmap.QuestionCompleteCount;

        public Roadmap? Roadmap { get; private set; }

        public Tables? Tables { get; private set; }

        public Topbar? Topbar { get; private set; }

        public Settings? Settings { get; private set; }

        public Panel? RoadmapPanel { get; private set; }

        public Panel? TablesPanel { get; private set; }

        public Panel? TopPanel { get; private set; }

        public Panel? SettingsPanel { get; private set; }

        public Form1()
        {
            InitializeComponent();
            InitializeData();
            InitializeLayout();
        }

        private void InitializeData()
        {
            Instance = this;

            UpdateData();
        }

        public void InitializeLayout()
        {
            Topbar = new Topbar();
            Settings = new Settings();
            Tables = new Tables();
            Roadmap = new Roadmap();

            TopPanel = Topbar.Build();
            SettingsPanel = Settings.Build();
            TablesPanel = Tables.Build();
            RoadmapPanel = Roadmap.Build();

            this.Controls.Add(TopPanel);
            this.Controls.Add(SettingsPanel);
            this.Controls.Add(TablesPanel);
            this.Controls.Add(RoadmapPanel);
        }

        public void UpdateData()
        {
            User = DB.GetCurrentUser();
            CompletedQuestions = DB.FetchQuestions(User.Id);
            QuestionsService.Initialize(CompletedQuestions);
        }

        public void UpdateDeleteUser()
        {
            QuestionsService.Initialize(CompletedQuestions);
        }

        public void UpdateDeleteQuestion(int questionId)
        {
            DB.DeleteQuestion(User.Id, questionId);

            Question? question = CompletedQuestions.Get(User.Id, questionId);
            if (question == null) return;

            CompletedQuestions.Remove(User.Id, questionId);

            float score = QuestionsService.RemoveScore(question, CompletedTopicQuestions);

            QuestionsService.Initialize(CompletedQuestions);

            UpdateSystemTable(question, -1);

            Topbar?.UpdateIndicators();

            Roadmap?.RemoveCardValue(question);

            Roadmap?.UpdateValues(question, score);
        }

        private void UpdateSystemTable(Question question, int value)
        {
            DataGridView? DataViewSystem = TablesPanel?.Controls.Find("DataGridSystem", true)[0] as DataGridView;

            if (Data.Instance.Topics == null || DataViewSystem == null) return;

            Topic topics = Data.Instance.Topics[CurrentTopicId];
            for (int i = 0; i < topics.questions.Count; i++)
            {
                if (topics.questions[i].id == question.question_id)
                {
                    string? bestTime = QuestionsService.BestQuestions.ContainsKey(question.question_id.ToString()) ? QuestionsService.BestQuestions[question.question_id.ToString()].time : "--:--:--";

                    DataViewSystem.Rows[i].Cells[3].Value = (int)DataViewSystem.Rows[i].Cells[3].Value + value;
                    DataViewSystem.Rows[i].Cells[4].Value = bestTime;
                    break;
                }
            }
        }

        private void UserTableAdd(Question question)
        {

            DataGridView? DataViewUser = TablesPanel?.Controls.Find("DataGridUser", true)[0] as DataGridView;

            if (DataViewUser == null) return;

            DataGridViewLinkCell linkCell = new DataGridViewLinkCell
            {
                Value = question.title,
                Tag = question.url
            };

            linkCell.LinkColor = Color.White;
            linkCell.ActiveLinkColor = Color.White;
            linkCell.VisitedLinkColor = Color.White;

            // Update User Table
            DataViewUser.Rows.Insert(0, new DataGridViewRow());
            DataViewUser.Rows[0].Cells[0].Value = question.id;
            DataViewUser.Rows[0].Cells[1].Value = question.question_id;
            DataViewUser.Rows[0].Cells[2] = linkCell;
            DataViewUser.Rows[0].Cells[3].Value = question.difficulty;
            DataViewUser.Rows[0].Cells[4].Value = question.time;
            DataViewUser.Rows[0].Cells[5].Value = question.date;
            DataViewUser.Rows[0].Cells[6].Value = "🚫";

            if (question.difficulty == "Easy") DataViewUser.Rows[0].Cells[3].Style.ForeColor = Color.Green;
            else if (question.difficulty == "Medium") DataViewUser.Rows[0].Cells[3].Style.ForeColor = Color.Orange;
            else if (question.difficulty == "Hard") DataViewUser.Rows[0].Cells[3].Style.ForeColor = Color.Red;
        }

        public void UpdateComplete(Question question)
        {
            UpdateSystemTable(question, 1);

            UserTableAdd(question);

            float score = QuestionsService.AddScore(question);

            CompletedQuestions.Add(question);

            Topbar?.UpdateIndicators();

            Roadmap?.AddCardValue(question);

            Roadmap?.UpdateValues(question, score);
        }

        public void BuildTables()
        {
            DataGridView? DataViewSystem = TablesPanel?.Controls.Find("DataGridSystem", true)[0] as DataGridView;
            if (DataViewSystem != null) DataViewSystem.Rows.Clear(); // Clear existing rows in the DataGridView

            DataGridView? DataViewUser = TablesPanel?.Controls.Find("DataGridUser", true)[0] as DataGridView;
            if (DataViewUser != null) DataViewUser.Rows.Clear(); // Clear existing rows in the DataGridView

            // Change Title to Topic Name
            if (Topbar != null && Topbar.TopicName != null) Topbar.TopicName.Text = CurrentTopicName;

            if (Data.Instance.Topics == null || DataViewSystem == null) return;
            Topic topics = Data.Instance.Topics[CurrentTopicId];

            DataViewSystem.Rows.Clear(); // Clear existing rows in the DataGridView

            foreach (Question question in topics.questions)
            {
                string bestTime = QuestionsService.BestQuestions.ContainsKey(question.id.ToString()) && QuestionsService.BestQuestions[question.id.ToString()].time != null
                    ? QuestionsService.BestQuestions[question.id.ToString()].time!
                    : "--:--:--";

                DataGridViewLinkCell linkCell = new DataGridViewLinkCell
                {
                    Value = question.title,
                    Tag = question.url
                };

                linkCell.LinkColor = Color.White;
                linkCell.ActiveLinkColor = Color.White;
                linkCell.VisitedLinkColor = Color.White;

                int rowIndexSystem = DataViewSystem.Rows.Add();
                DataViewSystem.Rows[rowIndexSystem].Cells[0].Value = question.id;
                DataViewSystem.Rows[rowIndexSystem].Cells[1] = linkCell;
                DataViewSystem.Rows[rowIndexSystem].Cells[2].Value = question.difficulty;
                DataViewSystem.Rows[rowIndexSystem].Cells[3].Value = QuestionCompleteCount.ContainsKey(question.id) ? QuestionCompleteCount[question.id] : 0;
                DataViewSystem.Rows[rowIndexSystem].Cells[4].Value = bestTime;

                if (question.difficulty == "Easy") DataViewSystem.Rows[rowIndexSystem].Cells[2].Style.ForeColor = Color.Green;
                else if (question.difficulty == "Medium") DataViewSystem.Rows[rowIndexSystem].Cells[2].Style.ForeColor = Color.Orange;
                else if (question.difficulty == "Hard") DataViewSystem.Rows[rowIndexSystem].Cells[2].Style.ForeColor = Color.Red;
            }

            for (int i = CompletedQuestions.questions.Count - 1; i >= 0; i--)
            {
                Question question = CompletedQuestions.questions[i];

                if (question.topic_id == int.Parse(CurrentTopicId))
                {
                    DataGridViewLinkCell linkCell = new DataGridViewLinkCell
                    {
                        Value = question.title,
                        Tag = question.url
                    };

                    linkCell.LinkColor = Color.White;
                    linkCell.ActiveLinkColor = Color.White;
                    linkCell.VisitedLinkColor = Color.White;

                    if (DataViewUser != null)
                    {
                        int rowIndexUser = DataViewUser.Rows.Add();

                        DataViewUser.Rows[rowIndexUser].Cells[0].Value = question.id;
                        DataViewUser.Rows[rowIndexUser].Cells[1].Value = question.question_id;
                        DataViewUser.Rows[rowIndexUser].Cells[2] = linkCell;
                        DataViewUser.Rows[rowIndexUser].Cells[3].Value = question.difficulty;
                        DataViewUser.Rows[rowIndexUser].Cells[4].Value = question.time;
                        DataViewUser.Rows[rowIndexUser].Cells[5].Value = question.date;
                        DataViewUser.Rows[rowIndexUser].Cells[6].Value = "🚫";

                        if (question.difficulty == "Easy") DataViewUser.Rows[rowIndexUser].Cells[3].Style.ForeColor = Color.Green;
                        else if (question.difficulty == "Medium") DataViewUser.Rows[rowIndexUser].Cells[3].Style.ForeColor = Color.Orange;
                        else if (question.difficulty == "Hard") DataViewUser.Rows[rowIndexUser].Cells[3].Style.ForeColor = Color.Red;
                    }
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
