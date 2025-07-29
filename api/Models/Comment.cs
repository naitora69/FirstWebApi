using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    [Table("Comments")]
    public class Comment
    {
        public int Id { get; set; }//собственный id primary key
        public string Title { get; set; } = string.Empty;
        public string Context { get; set; } = string.Empty;
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        public int? StockId { get; set; }

        public Stock? Stock { get; set; } // свойство навигации 
    }
}