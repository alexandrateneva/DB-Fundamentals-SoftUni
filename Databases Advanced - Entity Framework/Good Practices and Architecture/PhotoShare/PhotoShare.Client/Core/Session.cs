namespace PhotoShare.Client.Core
{
    using PhotoShare.Models;

    internal static class Session
    {
        private static User user;

        public static User User
        {
            get => user;
            set => user = value;
        }

        public static Log GetLogStateOfUser()
        {
            if (user != null)
            {
                return Log.Login;
            }

            return Log.Logout;
        }
    }
}
