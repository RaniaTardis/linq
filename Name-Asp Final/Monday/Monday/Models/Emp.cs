using System.ComponentModel.DataAnnotations;

namespace Monday.Models
{
    public class Emp
    {
        public int Id { get; set; }
        [Display(Name ="Name")]
        [Required(ErrorMessage ="Required")]
        public string Ename { get; set; }
        [Display(Name = "Department")]
        public string Dept { get; set; }

        [Display(Name = "Gross Salary")]
        [DataType(DataType.Currency)]
        
        public int  GSalary { get; set; }

        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Required")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Required")]
        [StringLength(5,  ErrorMessage ="Length 5")]
        public string Password { get; set; }
        public string EImage { get; set; }
    }
}
