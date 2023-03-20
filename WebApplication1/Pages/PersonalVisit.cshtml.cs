using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Net.Mail;
using WebApplication1.DB;
using WebApplication1.Model;
using WebApplication1.Tools;

namespace WebApplication1.Pages
{
    public class PersonalVisitModel : PageModel
    {
        [BindProperty]
        public User User { get; set; } = new();
        [BindProperty]
        public Request Request { get; set; }
        [BindProperty]
        public string Doc { get; set; }
        public User AuthUser { get; set; }
        public bool Auth { get; set; }
        private readonly ILogger<PrivacyModel> _logger;
        private readonly Auth auth;
        private readonly User501Context db;

        public List<VisitPurpose> VisitPurposes { get; set; }
        [BindProperty]
        public int VisitPurposesID { get; set; }
        public SelectList VisPurList { get; set; }
        public List<Worker> Workers { get; set; }
        [BindProperty]
        public int WorkerID { get; set; }
        public SelectList WorkerList { get; set; }
        public SelectList SubList { get; set; }
        [BindProperty]
        public int SubdivisionID { get; set; }
        public List<Subdivision> Subdivisions { get; set; }
        public string Session { get => session; }
        string session;

        public string Message { get; set; }

        public PersonalVisitModel(ILogger<PrivacyModel> logger, Auth auth, User501Context db)
        {
            _logger = logger;
            this.auth = auth;
            this.db = db;
            
            Workers = db.Workers.Include(s => s.IdSubdivisionNavigation).ToList();
            Subdivisions = db.Subdivisions.ToList();
            VisitPurposes = db.VisitPurposes.ToList();

            VisPurList = new SelectList(VisitPurposes, "Id", "Title");
            WorkerList = new SelectList(Workers, "Id", "LastName");
            SubList = new SelectList(Subdivisions, "Id", "Title");
            
        }
        public void OnGet(string handler)
        {
            session = handler;
            Request = new Request { DateStart = DateTime.Now, DateEnd = DateTime.Now };
            AuthUser = auth.Get(session);
            Auth = AuthUser != null;
            User = AuthUser;
            

            
        }
        public IActionResult OnPost(string handler, User user, string ggg, string fillData = null)
        {
            session = handler;
            AuthUser = auth.Get(session);
            Auth = AuthUser != null;

            if (fillData == null)
                return null; // работает очистка lol
            MailAddress addr;
            byte[] data = null;    
            var shits = base.Request.Form.Files;
            if(shits != null && shits.Count > 0)    
            {
                using (var fs = shits.First().OpenReadStream())
                {
                    // можно проверить длину через fs.Length
                    data = new byte[fs.Length];
                    using (var mc = new MemoryStream(data))
                        fs.CopyTo(mc);
                    Doc = shits.First().FileName;
                }
            }
            
            user.Login = AuthUser.Login;
            user.Password = AuthUser.Password;

            if(string.IsNullOrEmpty(user.Email) ||
                string.IsNullOrEmpty(user.FirstName) ||
                string.IsNullOrEmpty(user.LastName) ||
                string.IsNullOrEmpty(user.Patronymic) ||
                string.IsNullOrEmpty(user.PassportNumber) ||
                string.IsNullOrEmpty(user.PassportSeries) ||
                string.IsNullOrEmpty(user.Note) || data == null 
                
                )
            {
                Message = "Необходимо заполнить поля :(";
                return null;
            }
            try
            {
                addr = new MailAddress(user.Email);

            }
            catch
            {
                Message = "Неправильная почта";
                return null;
            }
            user.NameScan = Doc;
            user.PassportScan = data;

            if (user.PassportNumber.Count() != 6)
                Message = "В номере паспорта должно быть шесть символов";
            if(user.PassportSeries.Count() != 4)
                Message = "В серии паспорта должно быть четыре символа";
            if (user.Dob == null || DateTime.Now.Subtract(user.Dob.Value).Days / 365 < 16)
                Message = "Ты еще маленький(ЛООООХ) ";
            if (user.FirstName == "Эдик")
                Message = "Доступ запрещён.";
            if ((Request.DateEnd - Request.DateStart).Days > 15 || Request.DateStart == Request.DateEnd)
            {
                Message = "Выберете дату заявки не более 15 дней";
            }
            if (Request.DateStart == DateTime.Today)
                Message = "Дата начала заявки должно быть больше на один день";
            if((Request.DateEnd).Day < (Request.DateStart).Day)
                Message = "Дата конца заявки не может быть меньше даты начала";




            if (Message != null)
                return null;

            Request.IdVisitPurpose = VisitPurposesID;
            Request.IdWorker = WorkerID;
            Request.IdStatus = 1;

            DBInstance.GetInstance().Requests.Add(Request);
            DBInstance.GetInstance().SaveChanges();
            Request = DBInstance.GetInstance().Requests.ToList().Last();
            Request.CrossUserRequests.Add(new CrossUserRequest { IdRequest = Request.Id, IdUser = user.Id});

            
            DBInstance.GetInstance().Entry<User>(AuthUser).CurrentValues.SetValues(user);
            
            DBInstance.GetInstance().SaveChanges();
            return RedirectToPage("Privacy", session);
        }
    }
}
