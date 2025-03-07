using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2
{
    public class Student : Person, IDateAndCopy
    {
        private Education education;
        private int groupNumber;
        private ArrayList tests;
        private ArrayList exams;

        public Student(Person person, Education education, int groupNumber)
        {
            this.name = person.Name;
            this.surname = person.Surname;
            this.birthDate = person.BirthDate;
            this.education = education;
            this.groupNumber = groupNumber;
            this.tests = new ArrayList();
            this.exams = new ArrayList();
        }

        public Student()
        {
            this.name = "Unknown";
            this.surname = "Unknown";
            this.birthDate = DateTime.MinValue;
            this.education = Education.Specialist;
            this.groupNumber = 0;
            this.tests = new ArrayList();
            this.exams = new ArrayList();
        }

        public Education Education
        {
            get { return education; }
            set { education = value; }
        }

        public int GroupNumber
        {
            get { return groupNumber; }
            set
            {
                if (value <= 100 || value > 599)
                    throw new ArgumentOutOfRangeException("Номер группы должен быть в диапазоне от 101 до 599.");
                groupNumber = value;
            }
        }

        public ArrayList Tests
        {
            get { return tests; }
            set { tests = value; }
        }

        public ArrayList Exams
        {
            get { return exams; }
            set { exams = value; }
        }

        public double AverageGrade
        {
            get
            {
                if (exams.Count == 0) return 0;
                int sum = 0;
                foreach (Exam exam in exams)
                {
                    sum += exam.Grade;
                }
                return (double)sum / exams.Count;
            }
        }

        public bool this[Education edu]
        {
            get { return education == edu; }
        }

        public void AddExams(params Exam[] newExams)
        {
            exams.AddRange(newExams);
        }

        public void AddTests(params Test[] newTests)
        {
            tests.AddRange(newTests);
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Студент: {base.ToString()}");
            sb.AppendLine($"Образование: {education}");
            sb.AppendLine($"Номер группы: {groupNumber}");
            sb.AppendLine("Зачеты:");
            foreach (Test test in tests)
            {
                sb.AppendLine(test.ToString());
            }
            sb.AppendLine("Экзамены:");
            foreach (Exam exam in exams)
            {
                sb.AppendLine(exam.ToString());
            }
            return sb.ToString();
        }

        public override string ToShortString()
        {
            return $"Студент: {base.ToShortString()}\nОбразование: {education}\nНомер группы: {groupNumber}\nСредний балл: {AverageGrade}";
        }

        public override object DeepCopy()
        {
            Student copiedStudent = new Student(new Person(name, surname, birthDate), education, groupNumber);
            foreach (Test test in tests)
            {
                copiedStudent.Tests.Add(new Test(test.Subject, test.IsPassed));
            }
            foreach (Exam exam in exams)
            {
                copiedStudent.Exams.Add((Exam)exam.DeepCopy());
            }
            return copiedStudent;
        }

        public DateTime Date
        {
            get { return birthDate; }
            set { birthDate = value; }
        }

        public IEnumerable GetAllExamsAndTests()
        {
            foreach (var item in tests)
            {
                yield return item;
            }
            foreach (var item in exams)
            {
                yield return item;
            }
        }

        public IEnumerable GetExamsWithGradeGreaterThan(int grade)
        {
            foreach (Exam exam in exams)
            {
                if (exam.Grade > grade)
                    yield return exam;
            }
        }
    }
}
