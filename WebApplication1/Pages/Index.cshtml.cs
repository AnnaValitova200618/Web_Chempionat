using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication1.DB;
using WebApplication1.Model;
using WebApplication1.Tools;

namespace WebApplication1.Pages
{
    public class IndexModel : PageModel
    {
        public bool Auth { get; set; }
        public string Message { get; set; }
        public string SomeString { get; set; } = "shit";
        private readonly ILogger<IndexModel> _logger;
        private readonly Auth auth;

        public IndexModel(ILogger<IndexModel> logger, Auth auth)
        {
            _logger = logger;
            this.auth = auth;

        }

        public void OnGet()
        {

        }

        public IActionResult OnPost(string login, string password)
        {

            if (string.IsNullOrEmpty(login) ||
                string.IsNullOrEmpty(password))
            {
                Message = "Необходимо заполнить поля :(";
                return null;
            }
            User user = DBInstance.GetInstance().Users.FirstOrDefault(s => s.Login == login &&
                s.Password == HashPass.GetHash(password));
            if (user == null)
            {
                Message = "Пользователь не найден";
                return null;
            }

            string session = auth.Create(user);

            return RedirectToPage("Privacy", session);
        }
    }
}