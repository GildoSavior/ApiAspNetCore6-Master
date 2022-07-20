using System.ComponentModel.DataAnnotations;

namespace ApiBalta.ViweModels
{
    public class CreateTodoVM
    {
        [Required]
        public string Title { get; set; }
    }
}