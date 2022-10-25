namespace applicant_api.Model.Data.Helper
{
    public class Constants
    {
        public const string targetLocalHttps = "https://localhost:7104";
        public const string targetLocalHttp = "http://localhost:8080";
    }
    public class BlackList
    {
        public readonly List<string> MobileNumbers = new List<string> { "09999999999", "09888888888", "09777777777" };
    }
}
