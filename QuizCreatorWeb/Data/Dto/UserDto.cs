namespace QuizCreatorWeb.Data.Dto
{
    public class UserDto
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public bool IsAdmin { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
