using System.ComponentModel.DataAnnotations;

namespace System_do_zarządzania_projektami.Entites
{
    public class Project
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public List<TaskItem>? Tasks { get; set; }
    }
}
