using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using MddInterviewProject.Dtos;
using MddInterviewProject.Models;
using Newtonsoft.Json;

namespace MddInterviewProject.Services
{
    public class BlogService : IBlogService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;
        private readonly ILogger _logger;

        public BlogService(HttpClient httpClient, ILogger logger)
        {
            _httpClient = httpClient;
            _baseUrl = ConfigurationManager.AppSettings["BlogApiUri"];
            _logger = logger;
        }


        public async Task<List<Post>> GetPostsAndCommentsAsync()
        {
            try
            {
                _logger.Info("Fetching posts and comments");
                var postsResponse = await _httpClient.GetStringAsync(_baseUrl + "/postss");
                var commentsResponse = await _httpClient.GetStringAsync(_baseUrl + "/comments");
                _logger.Info("Posts and comments fetched successfully");
                
                _logger.Info("Deserializing posts and comments");
                var posts = JsonConvert.DeserializeObject<List<PostDto>>(postsResponse);
                var comments = JsonConvert.DeserializeObject<List<CommentDto>>(commentsResponse);
                _logger.Info("Posts and comments deserialized successfully");
                
                return MergePostsAndComments(posts, comments);
            }
            catch (HttpRequestException e)
            {
                _logger.Error("Error while fetching posts and comments", e);
                throw;
            }
            catch (Exception e)
            {
                _logger.Error("Unexpected error", e);
                throw;
            }
        }

        private List<Post> MergePostsAndComments(List<PostDto> posts, List<CommentDto> comments)
        {
            return posts.Select(post => new Post
            {
                Id = post.Id,
                Title = post.Title,
                Body = post.Body,
                Comments = comments.FindAll(c => c.PostId == post.Id).Select(c => new Comment
                {
                    Id = c.Id,
                    PostId = c.PostId,
                    Name = c.Name,
                    Email = c.Email,
                    Body = c.Body
                }).ToList()
            }).ToList();
        }
    }
}