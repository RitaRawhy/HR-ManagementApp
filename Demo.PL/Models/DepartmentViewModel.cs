using System.ComponentModel.DataAnnotations;

namespace Demo.PL.Models
{
    public class DepartmentViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Department Code is Required")]
        public string Code { get; set; }
        [Required(ErrorMessage = "Department Name is Required")]
        [MinLength(3, ErrorMessage = "MinLength is 3 characters")]
        public string Name { get; set; }
    }
}
