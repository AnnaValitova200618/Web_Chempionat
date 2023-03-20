using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication1.Tools;

namespace WebApplication1.Pages
{
    public class SelectTypeModel : PageModel
    {
        public bool Auth { get; set; }
        private readonly ILogger<PrivacyModel> _logger;
        private readonly Auth auth;
        public string Session { get => session; }
        string session;
        public SelectTypeModel(ILogger<PrivacyModel> logger, Auth auth)
        {
            _logger = logger;
            this.auth = auth;
        }
        public void OnGet(string handler)
        {
            session = handler;
            Auth = auth.Get(session) != null;
        }
    }
}
