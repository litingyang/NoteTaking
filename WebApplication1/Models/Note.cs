using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class Note
    {
        public int Id { get; set; }
        [Required]
        public string User { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public Note()
        {
            Title = string.Empty;
            Content = string.Empty;
            User = string.Empty;

        }

        public Note(int id, string user, string title, string content)
        {
            Id = id;
            Title = title;
            User = user;
            Content = content;
        }
    }
}
