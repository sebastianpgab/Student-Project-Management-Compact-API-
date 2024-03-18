using System.ComponentModel.DataAnnotations;
using System_do_zarządzania_projektami.Services;

namespace System_do_zarządzania_projektami.Entites
{
    public class TaskItem
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public Status Status { get; set; }
        public int ProjectId { get; set; }
        public Project? Project { get; set; }
    }
}
