using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Comments
{
    public class UpdateRequestDto
    {   
        [Required]
        [MinLength(5, ErrorMessage = "Title must be more than 5 character")]
        [MaxLength(280, ErrorMessage = "Title must be less than 280 character")]
        public string Title { get; set; } = string.Empty;
        [Required]
        [MinLength(5, ErrorMessage = "Context must be more than 5 character")]
        [MaxLength(280, ErrorMessage = "Context must be less than 280 character")]
        public string Context { get; set; } = string.Empty;
    }
}