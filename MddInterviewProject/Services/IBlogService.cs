using System.Collections.Generic;
using System.Threading.Tasks;
using MddInterviewProject.Models;

namespace MddInterviewProject.Services
{
    public interface IBlogService
    {
        Task<List<Post>> GetPostsAndCommentsAsync();
    }
}