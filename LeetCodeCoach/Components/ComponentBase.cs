using System.Net;
using LeetCodeCoach.Components.Timer;
using LeetCodeCoach.Controllers.LeetCode;
using LeetCodeCoach.Controllers.SQLite;
using LeetCodeCoach.Models;
using LeetCodeCoach.Services;
using LeetCodeCoach.Utility;

namespace LeetCodeCoach.Components
{
    public class ComponentBase
    {
        public static EditImage EditImage { get; private set; } = new EditImage();
        
        public static Form1 _form { get; private set; } = Form1.Instance!;

        public static SQLite DB { get; private set; } = Form1.DB;

        public static LeetCode LeetCode { get; private set; } = Form1.LeetCode;

        public static User User { get; private set; } = Form1.User;

        public static TimerCard Timer { get; private set; } = new TimerCard();

        public static QuestionsService QuestionsService { get; private set; } = Form1.QuestionsService;
    }
}
