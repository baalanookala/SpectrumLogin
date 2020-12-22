using System;
namespace SampleLogin.Helpers
{
    public static class RegexPatterns
    {
        public static string validPasswordRegex = "^(?=.*[0-9])(?=.*[A-Z])(?=.*[@#$%^&+=!])(?=\\S+$).{8,15}$";
        public static string validNameRegex = "[^A-Za-z0-9]";
    }
}
