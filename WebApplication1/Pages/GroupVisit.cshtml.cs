using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication1.DB;
using WebApplication1.Model;
using WebApplication1.Tools;

namespace WebApplication1.Pages
{
    public class GroupVisitModel : PageModel
    {
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
        public GroupVisitModel(ILogger<PrivacyModel> logger, Auth auth, User501Context db)
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
            Auth = auth.Get(session) != null;
        }
        public  void OnPost(string handler, string fillData)
        {
            session = handler;
            Auth = auth.Get(session) != null;

            if (fillData == null)
                return;
        }
    }
}
