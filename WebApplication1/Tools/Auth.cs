using WebApplication1.Model;

namespace WebApplication1.Tools
{
    public class Auth
    {
        Dictionary<string, User> auth = new Dictionary<string, User>();

        public string Create(User user)
        {
            string session = Guid.NewGuid().ToString();
            auth[session] = user;
            return session;
        }
        public User Get(string session)
        {
            if (!string.IsNullOrEmpty(session) && auth.ContainsKey(session))
                return auth[session];
            else
                return null;
        }
    }
}
