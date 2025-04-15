using System.ComponentModel.DataAnnotations;

namespace QuizCreatorWeb.Data.Dto
{
    public class SignUpDto
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter Name")]
        public string UserName { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter Email")]
        public string Email { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter Password")]
        public string Password { get; set; }
    }
}
