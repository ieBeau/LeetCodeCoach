using System.Text.Json;

namespace LeetCodeCoach.Controllers.LeetCode
{
    public class LeetCode : ControllerBase
    {
        string queryUrl = "https://leetcode.com/graphql";

        public async Task<JsonDocument> FetchCompletionAsync()
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Get, queryUrl);

            request.Headers.Add("Cookie", "csrftoken=1N9zcmWxGEZqQeD7gVQaVHUaRydbOmbJO9fi2qn5iVGIzez7SYVuqBQ6BsbKfwwX");

            var content = new StringContent("{\"query\":\"" +
                "query \\r\\n" +
                    "recentAcSubmissions($username: String!, $limit: Int!) {\\r\\n    " +
                        "recentAcSubmissionList(username: $username, limit: $limit) {\\r\\n            " +
                            "id    \\r\\n            " +
                            "title    \\r\\n            " +
                            "titleSlug    \\r\\n            " +
                            "timestamp  \\r\\n            " +
                        "}\\r\\n            " +
                    "}\",\"" +
                    "variables\":" +
                    "{\"" +
                        "username\":\"iebeau\",\"" +
                        "limit\":25" +
                    "}" +
                "}",
                null,
                "application/json"
             );

            request.Content = content;

            var response = await client.SendAsync(request);

            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsStringAsync();
            var json = JsonDocument.Parse(responseBody);

            return json;
        }

        public async Task<JsonDocument> FetchUserAsync(string username)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Get, queryUrl);

            request.Headers.Add("Cookie", "csrftoken=1N9zcmWxGEZqQeD7gVQaVHUaRydbOmbJO9fi2qn5iVGIzez7SYVuqBQ6BsbKfwwX");

            var content = new StringContent(string.Format("{{\"query\":\"query userProblemsSolved($username: String!) {{\\r\\n" +
            "    allQuestionsCount {{    \\r\\n" +
            "        difficulty    \\r\\n" +
            "        count  \\r\\n" +
            "        }}\\r\\n" +
            "        matchedUser(username: $username) {{\\r\\n" +
            "            problemsSolvedBeatsStats {{ \\r\\n" +
            "                difficulty\\r\\n" +
            "                percentage    \\r\\n" +
            "                }}\\r\\n" +
            "        submitStatsGlobal {{\\r\\n" +
            "            acSubmissionNum {{        \\r\\n" +
            "                difficulty        \\r\\n" +
            "                count      \\r\\n" +
            "                    }}    \\r\\n" +
            "                }}  \\r\\n" +
            "            }}             \\r\\n" +
            "        }}\\r\\n\",\"variables\":{{\"username\":\"{0}\"}}}}", username),
            null, "application/json");

            request.Content = content;

            var response = await client.SendAsync(request);

            response.EnsureSuccessStatusCode();

            var responseBody = await response.Content.ReadAsStringAsync();
            var json = JsonDocument.Parse(responseBody);

            return json;
        }
    };
}