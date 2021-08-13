using System.Web;
using Microsoft.AspNetCore.Http;

namespace dotnet_demos2.Models
{
    public class Post
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public IFormFile File { get; set; }
    }
}