﻿using System.ComponentModel.DataAnnotations;

namespace EventAddis.Entity
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<Event> Events { get; set; }
    }
}
