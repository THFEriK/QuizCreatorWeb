namespace QuizCreatorWeb.Data.Dto
{
    public class JsonDto
    {
        public long Id { get; set; }
        public string UserName { get; set; }
        public bool IsSysAdmin { get; set; }
        public List<Object> FactionRecords { get; set; }
        public List<string> Permissions { get; set; }
    }
}
