using System;

namespace Worktop.Core.Domain.Entities
{
    public class Article
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.Now;
        public DateTime DateUpdated { get; set; } = DateTime.Now;

        public static Article Create(string title, string content) => new Article { Title = title, Content = content };
    }
}