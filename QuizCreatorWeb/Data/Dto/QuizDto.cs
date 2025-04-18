﻿namespace QuizCreatorWeb.Data.Dto
{
    public class QuizDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public int UserId { get; set; }
        public List<QuestionDto>? Questions { get; set; }
    }
}
