﻿namespace LMS.DB.Entities
{
    public class QuizExam
    {
        public Guid Id { get; set; }
        public string Name { get; set; }    
        public DateTime CreatedDate { get; set; }
        public string Description {  get; set; }
        public DateTime Duration { get; set; }

        //navigation property
        public ICollection<Subject> Subjects { get; set; } 
        public ICollection<StudentAttempts> StudentAttempts { get; set; }
        public ICollection<Subject_Quiz> SubjectQuizzes { get; set; }
        public ICollection<Subject_quiz_question> SubjectQuizQuestions { get; set; }

    }
}
