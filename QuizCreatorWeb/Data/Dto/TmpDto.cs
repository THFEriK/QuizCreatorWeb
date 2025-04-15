namespace QuizCreatorWeb.Data.Dto
{
    public class TmpDto
    {
        public long userdiscordid { get; set; }

        public TmpDto() { }
        public TmpDto(long userdiscordid) 
        {
            this.userdiscordid = userdiscordid;
        }
    }
}
