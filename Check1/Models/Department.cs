using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Check1.Data.Base;

namespace Check1.Models
{
    public class Department:IEntityBase
    {
        [Key]
        
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Employee> Employees { get; set; }

    }
}
