﻿using System;
using System.ComponentModel.DataAnnotations;

namespace NewsApi.Models
{
    public class News
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public string EditorFirstName { get; set; }
        public string EditorLastName { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
