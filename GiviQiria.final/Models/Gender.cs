using Microsoft.AspNetCore.Builder;
using System.ComponentModel.DataAnnotations;

namespace GiviQiria.final.Models
{
 public  enum Gender
    {
        [Display(Name = "Male")]
        Male,
        [Display(Name = "Female")]
        Female,
        [Display(Name = "No Sex")]
        NoSex
    }
}
