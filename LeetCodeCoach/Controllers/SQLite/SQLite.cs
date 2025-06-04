using LeetCodeCoach.Models;
using Microsoft.Data.Sqlite;

namespace LeetCodeCoach.Controllers.SQLite
{
    public class SQLite : ControllerBase
    {
        public string DatabasePath { get; private set; }

        private bool UserTableExists { get; set; }
        private bool CurrentUserTableExists { get; set; }
        private bool QuestionsTableExists { get; set; }

        public SQLite()
        {
            string appDataFolder = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                "LeetCodeCoach"
            );

            Directory.CreateDirectory(appDataFolder);

            DatabasePath = Path.Combine(appDataFolder, "LeetCode.db");

            using (var connection = new SqliteConnection($"Data Source={DatabasePath}"))
            {
                connection.Open();

                SqliteCommand command = connection.CreateCommand();

                ExistUserTable(command);

                ExistCurrentUserTable(command);

                ExistQuestionsTable(command);

                CreateUserTable(command);

                CreateCurrentUserTable(command);

                CreateQuestionTable(command);
            }
        }

        private void ExistUserTable(SqliteCommand command)
        {
            // Check if the User table exists
            command.CommandText =
            @"
                SELECT name 
                FROM sqlite_master 
                WHERE type='table' AND name='User';
            ";
            UserTableExists = command.ExecuteScalar() != null;
        }
        
        private void ExistCurrentUserTable(SqliteCommand command)
        {
            // Check if the CurrentUser table exists
            command.CommandText =
            @"
                SELECT name 
                FROM sqlite_master 
                WHERE type='table' AND name='CurrentUser';
            ";
            CurrentUserTableExists = command.ExecuteScalar() != null;
        }
        
        private void ExistQuestionsTable(SqliteCommand command)
        {
            // Check if the Questions table exists
            command.CommandText =
            @"
                SELECT name 
                FROM sqlite_master 
                WHERE type='table' AND name='Questions';
            ";
            QuestionsTableExists = command.ExecuteScalar() != null;
        }

        private void CreateUserTable(SqliteCommand command)
        {
            // Create tables if they do not exist
            if (!UserTableExists)
            {
                command.CommandText =
                @"
                    CREATE TABLE User (
                        user_id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                        username TEXT NOT NULL
                    );
                ";
                command.ExecuteNonQuery();

                UserTableExists = true;
            }
        }
        
        private void CreateCurrentUserTable(SqliteCommand command)
        {
            // Create tables if they do not exist
            if (!CurrentUserTableExists)
            {
                command.CommandText =
                @"
                    CREATE TABLE CurrentUser (
                        user_id INTEGER NOT NULL,
                        username TEXT NOT NULL
                    );
                ";
                command.ExecuteNonQuery();

                CurrentUserTableExists = true;
            }
        }
        
        private void CreateQuestionTable(SqliteCommand command)
        {
            // Create tables if they do not exist
            if (!QuestionsTableExists)
            {
                command.CommandText =
                @"
                    CREATE TABLE Questions (
                        id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                        user_id INTEGER NOT NULL,
                        question_id INTEGER NOT NULL,
                        topic_id INTEGER NOT NULL,
                        tags TEXT NOT NULL,
                        title TEXT NOT NULL,
                        difficulty TEXT NOT NULL,
                        link TEXT NOT NULL,     
                        time TEXT NOT NULL,
                        date TEXT NOT NULL,
                        FOREIGN KEY (user_id) REFERENCES User(user_id) ON DELETE CASCADE
                    );
                ";
                command.ExecuteNonQuery();

                QuestionsTableExists = true;
            }
        }

        public bool UserExist(string username)
        {
            using (var connection = new SqliteConnection($"Data Source={DatabasePath}"))
            {
                connection.Open();

                var command = connection.CreateCommand();
                
                if (!UserTableExists) CreateUserTable(command);

                command.CommandText =
                $@"
                    SELECT username
                    FROM User
                    WHERE username = ""{username}""
                ";
                var name = command.ExecuteScalar() as string;

                if (!string.IsNullOrEmpty(name)) return true;
                else return false;
            }
        }

        public void SetUser(string username)
        {
            using (var connection = new SqliteConnection($"Data Source={DatabasePath}"))
            {
                connection.Open();

                var command = connection.CreateCommand();
                
                if (!UserTableExists) CreateUserTable(command);

                command.CommandText =
                $@"
                    INSERT INTO User (username)
                    VALUES (""{username}"")
                ";

                command.ExecuteNonQuery();
            }
        }

        public User GetUser(string username)
        {

            using (var connection = new SqliteConnection($"Data Source={DatabasePath}"))
            {
                connection.Open();

                var command = connection.CreateCommand();
                
                if (!UserTableExists) CreateUserTable(command);

                command.CommandText =
                $@"
                    SELECT user_id, username
                    FROM User
                    WHERE username = ""{username}""
                ";

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new User
                        {
                            Id = reader.GetInt32(0),
                            Username = reader.GetString(1)
                        };
                    }
                }

                return new User();
            }
        }

        public void DeleteUser(User user)
        {
            using (var connection = new SqliteConnection($"Data Source={DatabasePath}"))
            {
                connection.Open();

                var command = connection.CreateCommand();
                
                if (!UserTableExists) CreateUserTable(command);
                if (!QuestionsTableExists) CreateQuestionTable(command);
                if (!CurrentUserTableExists) CreateCurrentUserTable(command);

                command.CommandText =
                $@"
                    DELETE FROM Questions
                    WHERE user_id = ""{user.Id}"";

                    DELETE FROM CurrentUser;

                    DELETE FROM User
                    WHERE user_id = ""{user.Id}"";
                ";

                command.ExecuteNonQuery();
            }
        }

        public void DeleteAllData()
        {
            using (var connection = new SqliteConnection($"Data Source={DatabasePath}"))
            {
                connection.Open();

                var command = connection.CreateCommand();

                command.CommandText =
                @"
                    DROP TABLE IF EXISTS Questions;
                    DROP TABLE IF EXISTS CurrentUser;
                    DROP TABLE IF EXISTS User;
                ";

                command.ExecuteNonQuery();

                QuestionsTableExists = false;
                CurrentUserTableExists = false;
                UserTableExists = false;
            }
        }

        public void SetCurrent(int user_id, string username)
        {

            if (GetCurrentUser().Username != username) DeleteCurrent();

            using (var connection = new SqliteConnection($"Data Source={DatabasePath}"))
            {
                connection.Open();

                var command = connection.CreateCommand();
                
                if (!CurrentUserTableExists) CreateCurrentUserTable(command);

                command.CommandText =
                $@"
                    INSERT INTO CurrentUser (user_id, username)
                    VALUES (""{user_id}"", ""{username}"")
                ";

                command.ExecuteNonQuery();
            }
        }

        public User GetCurrentUser()
        {
            using (var connection = new SqliteConnection($"Data Source={DatabasePath}"))
            {
                connection.Open();

                var command = connection.CreateCommand();
                
                if (!CurrentUserTableExists) CreateCurrentUserTable(command);

                command.CommandText =
                @"
                    SELECT user_id, username
                    FROM CurrentUser
                ";

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new User
                        {
                            Id = reader.GetInt32(0),
                            Username = reader.GetString(1)
                        };
                    }
                }

                return new User(); // Return null if no current user is found
            }
        }

        public void DeleteCurrent()
        {
            using (var connection = new SqliteConnection($"Data Source={DatabasePath}"))
            {
                connection.Open();

                var command = connection.CreateCommand();
                
                if (!CurrentUserTableExists) CreateCurrentUserTable(command);

                command.CommandText =
                @"
                    DELETE FROM CurrentUser
                ";

                command.ExecuteNonQuery();
            }
        }

        public void SetQuestion(int user_id, int question_id, int topic_id, string tags, string title, string difficulty, string link, string time, string date)
        {
            using (var connection = new SqliteConnection($"Data Source={DatabasePath}"))
            {
                connection.Open();

                var command = connection.CreateCommand();
                
                if (!QuestionsTableExists) CreateQuestionTable(command);

                command.CommandText =
                $@"
                    INSERT INTO Questions (user_id, question_id, topic_id, tags, title, difficulty, link, time, date)
                    VALUES (""{user_id}"", ""{question_id}"", ""{topic_id}"", ""{tags}"", ""{title}"", ""{difficulty}"", ""{link}"", ""{time}"", ""{date}"")
                ";

                command.ExecuteNonQuery();
            }
        }

        public void DeleteQuestion(int user_id, int question_id)
        {
            using (var connection = new SqliteConnection($"Data Source={DatabasePath}"))
            {
                connection.Open();

                var command = connection.CreateCommand();
                
                if (!QuestionsTableExists) CreateQuestionTable(command);

                command.CommandText =
                $@"
                    DELETE FROM Questions
                    WHERE user_id = ""{user_id}"" AND id = ""{question_id}""
                ";

                command.ExecuteNonQuery();
            }
        }

        public QuestionContainer FetchQuestions(int user_id)
        {
            QuestionContainer questions = new QuestionContainer();

            using (SqliteConnection connection = new SqliteConnection($"Data Source={DatabasePath}"))
            {
                connection.Open();

                SqliteCommand command = connection.CreateCommand();

                if (!QuestionsTableExists) CreateQuestionTable(command);

                command.CommandText =
                $@"
                    SELECT *
                    FROM Questions
                    WHERE user_id = ""{user_id}""
                ";

                using (SqliteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        questions.Add(new Question
                        {
                            id = reader.GetInt32(0),
                            user_id = reader.GetInt32(1),
                            question_id = reader.GetInt32(2),
                            topic_id = reader.GetInt32(3),
                            tags = reader.GetString(4).Split('|').ToList(),
                            title = reader.GetString(5),
                            difficulty = reader.GetString(6),
                            url = reader.GetString(7),
                            time = reader.GetString(8),
                            date = reader.GetString(9)
                        });
                    }
                }
            }

            return questions;
        }
    }
}
