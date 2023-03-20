namespace WebApplication1.DB
{
    public class DBInstance
    {
        private static User501Context instance;
        public static User501Context GetInstance()
        {
            if (instance == null)
                instance = new User501Context();
            return instance;
        }
    }
}
