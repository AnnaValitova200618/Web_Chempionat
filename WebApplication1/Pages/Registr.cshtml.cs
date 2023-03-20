using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Mail;
using WebApplication1.DB;
using WebApplication1.Model;
using WebApplication1.Tools;

namespace WebApplication1.Pages
{
    public class RegistrModel : PageModel
    {
        private readonly Auth auth;
        public bool Auth { get; set; }
        public string SomeString { get; set; } = "–егистраци€";
        public string Message { get; set; }

        public RegistrModel(Auth auth)
        {
            this.auth = auth;
        }

        public IActionResult OnPost(string mail, string password)
        {
            if (string.IsNullOrEmpty(mail) || string.IsNullOrEmpty(password))
            {
                Message = "Ќеобходимо заполнить пол€ :(";
                return null;
            }
            MailAddress addr;
            try
            {
                addr = new System.Net.Mail.MailAddress(mail);
            }
            catch
            {
                Message = "неправильна€ почта";
                return null;

            }

            User user = DBInstance.GetInstance().Users.FirstOrDefault(s => s.Email == mail);

            if (user == null)
            {

                string login = mail.Split('@')[0];
                DBInstance.GetInstance().Users.Add(new User() { Email = mail, Password = HashPass.GetHash(password), Login = login });
                DBInstance.GetInstance().SaveChanges();
                Message = "авторизаци€ прошла успешно";


            }
            else
                Message = "такой пользователь уже существует!!!!!";

            string session = auth.Create(user);
            return RedirectToPage("Privacy", session);
        }
    }
}

