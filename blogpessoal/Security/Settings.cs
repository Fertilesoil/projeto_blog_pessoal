namespace blogpessoal.Security
{
    public class Settings
    {
        private static string secret = "574bfa1ca3f349f0751d8c3628872593f568b9b56db6fe473133639cba4e698a";

        public static string Secret { get => secret; set => secret = value; }
    }
}
