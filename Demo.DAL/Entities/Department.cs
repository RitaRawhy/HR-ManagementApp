using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DAL.Entities
{
   public class Department
    {
        public Department()
        {
            DateOfCreation = DateTime.Now;
        }
        public int Id { get; set; }
        [Required(ErrorMessage = "Department Code is Required")]
        public string Code { get; set; }
        [Required(ErrorMessage ="Department Name is Required")]
        [MinLength(3,ErrorMessage ="MinLength is 3 characters")]
        public string Name { get; set; }
        public DateTime DateOfCreation { get; set; }

        public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
    }
}
