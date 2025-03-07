using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2
{
    public class Exam : IDateAndCopy
    {
        public string Subject { get; set; }
        public int Grade { get; set; }
        public DateTime ExamDate { get; set; }

        public Exam(string subject, int grade, DateTime examDate)
        {
            Subject = subject;
            Grade = grade;
            ExamDate = examDate;
        }

        public Exam()
        {
            Subject = "Unknown";
            Grade = 0;
            ExamDate = DateTime.MinValue;
        }

        public override string ToString()
        {
            return $"Предмет: {Subject}, Оценка: {Grade}, Дата экзамена: {ExamDate.ToShortDateString()}";
        }

        public object DeepCopy()
        {
            return new Exam(Subject, Grade, ExamDate);
        }

        public DateTime Date
        {
            get { return ExamDate; }
            set { ExamDate = value; }
        }
    }
}
