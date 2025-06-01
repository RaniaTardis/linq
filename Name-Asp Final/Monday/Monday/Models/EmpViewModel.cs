using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Monday.Models
{
    public class EmpViewModel
    {
        public int Id { get; set; }
        [Display(Name = "Name")]
        [Required(ErrorMessage = "Required")]
        public string Ename { get; set; }
        [Display(Name = "Department")]
        public string Dept { get; set; }

        [Display(Name = "Gross Salary")]
        [DataType(DataType.Currency)]

        public int GSalary { get; set; }

        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Required")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Required")]
        [StringLength(5, ErrorMessage = "Length 5")]
        public string Password { get; set; }
        public IFormFile EImage { get; set; }

        [Required]
        [Range(0,3000 , ErrorMessage ="0-3000")]
        public int Salary { get; set; }

        [Required(ErrorMessage ="Required")]
        public int Deduction { get; set; }

        [Required]
        [Compare("Password")]
        public string Confirm { get; set; }


    }
}
