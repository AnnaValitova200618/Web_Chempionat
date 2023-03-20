using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebApplication1.DB;
using WebApplication1.Model;
using WebApplication1.Tools;

namespace WebApplication1.Pages
{
    public class PrivacyModel : PageModel
    {
        public bool Auth { get; set; }
        public List<Request> Requests { get; set; }


        private readonly ILogger<PrivacyModel> _logger;
        private readonly Auth auth;
        private readonly User501Context db;
        User currentUser;
        public string Session { get => session; }
        string session;
        public PrivacyModel(ILogger<PrivacyModel> logger, Auth auth, User501Context db)
        {
            _logger = logger;
            this.auth = auth;
            this.db = db;
        }

        public void OnGet(string handler)
        {
            session = handler;
            currentUser = auth.Get(session);
            Auth = currentUser != null;
            Requests = db.CrossUserRequests?.
                Include(s => s.IdRequestNavigation).
                Include(s => s.IdRequestNavigation.CrossUserRequests).
                Include(s => s.IdRequestNavigation.IdWorkerNavigation).
                Include(s=>s.IdRequestNavigation.IdStatusNavigation).
                Include(s => s.IdRequestNavigation.IdWorkerNavigation.IdSubdivisionNavigation).
                Where(s => s.IdUser == currentUser.Id).
                Select(s => s.IdRequestNavigation).
                ToList();
        }
    }
}