using System.Threading.Tasks;
using System.Web.Mvc;
using MddInterviewProject.Services;

namespace MddInterviewProject.Controllers
{
    public class PostsController : Controller
    {
        private readonly IBlogService _blogService;
        private readonly ILogger _logger;
        
        public PostsController(IBlogService blogService, ILogger logger)
        {
            _blogService = blogService;
            _logger = logger;
        }
        
        public async Task<ActionResult> Index()
        {
            var posts = await _blogService.GetPostsAndCommentsAsync();
            return View(posts);
        }
    }
}