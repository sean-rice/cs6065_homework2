using System;
using System.ComponentModel.DataAnnotations;

namespace Cs6065_Homework2.Models
{
    public class NflTeam
    {
        [Required]
        [Key]
        public Guid Id { get; set; }

        [Required]
        [StringLength(48)]
        [Display(Name = "Team Name", ShortName = "Tm")]
        public string Name { get; set; }
    }
}
