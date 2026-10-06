using System.ComponentModel.DataAnnotations;
namespace ToDoListMvc.Models

{
    public class TaskItem
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="{0} required")]
        [StringLength(100,ErrorMessage = "The {0} cannot exceed {1} characters.")]
        public string Title { get; set; }
        [StringLength(500, ErrorMessage = "The {0} cannot exceed {1} characters.")]
        public string Description { get; set; }

        public bool IsCompleted{ get; set; }


    }
}
