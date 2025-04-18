using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace WebApp.Models
{
    public class Department
    {
        public int Id { get; set; }

        [Required]
        public string? Name { get; set; }

        [StringLength(500)]
        public string? Description { get; set; }
    }
}
