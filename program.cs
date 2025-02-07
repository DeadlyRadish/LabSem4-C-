using System;
using System.Diagnostics;
using System.Text;

public enum Education
{
    Specialist,
    Bachelor,
    SecondEducation
}

public class Exam
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
}

public class Person
{
    public string Name { get; set; }
    public string Surname { get; set; }

    public Person(string name, string surname)
    {
        Name = name;
        Surname = surname;
    }

    public override string ToString()
    {
        return $"{Name} {Surname}";
    }
}

public class Student
{
    private Person person;
    private Education education;
    private int groupNumber;
    private Exam[] exams;

    public Student(Person person, Education education, int groupNumber)
    {
        this.person = person;
        this.education = education;
        this.groupNumber = groupNumber;
        this.exams = new Exam[0];
    }

    public Student()
    {
        this.person = new Person("Unknown", "Unknown");
        this.education = Education.Specialist;
        this.groupNumber = 0;
        this.exams = new Exam[0];
    }

    public Person Person
    {
        get { return person; }
        set { person = value; }
    }

    public Education Education
    {
        get { return education; }
        set { education = value; }
    }

    public int GroupNumber
    {
        get { return groupNumber; }
        set { groupNumber = value; }
    }

    public Exam[] Exams
    {
        get { return exams; }
        set { exams = value; }
    }

    public double AverageGrade
    {
        get
        {
            if (exams.Length == 0) return 0;
            int sum = 0;
            foreach (var exam in exams)
            {
                sum += exam.Grade;
            }
            return (double)sum / exams.Length;
        }
    }

    public bool this[Education edu]
    {
        get { return education == edu; }
    }

    public void AddExams(params Exam[] newExams)
    {
        var newExamList = new Exam[exams.Length + newExams.Length];
        exams.CopyTo(newExamList, 0);
        newExams.CopyTo(newExamList, exams.Length);
        exams = newExamList;
    }

    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine($"Студент: {person}");
        sb.AppendLine($"Образование: {education}");
        sb.AppendLine($"Номер группы: {groupNumber}");
        sb.AppendLine("Экзамены:");
        foreach (var exam in exams)
        {
            sb.AppendLine(exam.ToString());
        }
        return sb.ToString();
    }

    public string ToShortString()
    {
        return $"Студент: {person}\nОбразование: {education}\nНомер группы: {groupNumber}\nСредний балл: {AverageGrade}";
    }
}

public static class StudentGenerator
{
    private static Random random = new Random();
    private static string[] names = { "Иван", "Анна", "Петр", "Мария", "Алексей" };
    private static string[] surnames = { "Иванов", "Петрова", "Смит", "Джонсон", "Браун" };
    private static string[] subjects = { "Математика", "Физика", "Химия", "Биология", "История" };

    public static Student GenerateRandomStudent()
    {
        string name = names[random.Next(names.Length)];
        string surname = surnames[random.Next(surnames.Length)];
        Person person = new Person(name, surname);

        Education education = (Education)random.Next(Enum.GetValues(typeof(Education)).Length);
        int groupNumber = random.Next(1, 100);

        Student student = new Student(person, education, groupNumber);

        int numberOfExams = random.Next(1, 5);
        for (int i = 0; i < numberOfExams; i++)
        {
            string subject = subjects[random.Next(subjects.Length)];
            int grade = random.Next(1, 6);
            DateTime examDate = DateTime.Now.AddDays(-random.Next(1, 365));
            Exam exam = new Exam(subject, grade, examDate);
            student.AddExams(exam);
        }

        return student;
    }
}

public static class ArrayPerformance
{
    private static Random random = new Random();

    public static void FillArray<T>(T array, int size) where T : class
    {
        if (array is Exam[] oneDimensionalArray)
        {
            for (int i = 0; i < size; i++)
            {
                oneDimensionalArray[i] = new Exam("Предмет " + i, random.Next(1, 6), DateTime.Now.AddDays(-random.Next(1, 365)));
            }
        }
        else if (array is Exam[,] rectangularArray)
        {
            for (int i = 0; i < rectangularArray.GetLength(0); i++)
            {
                for (int j = 0; j < rectangularArray.GetLength(1); j++)
                {
                    rectangularArray[i, j] = new Exam("Предмет " + (i * rectangularArray.GetLength(1) + j), random.Next(1, 6), DateTime.Now.AddDays(-random.Next(1, 365)));
                }
            }
        }
        else if (array is Exam[][] jaggedArray)
        {
            for (int i = 0; i < jaggedArray.Length; i++)
            {
                for (int j = 0; j < jaggedArray[i].Length; j++)
                {
                    jaggedArray[i][j] = new Exam("Предмет " + (i * jaggedArray[i].Length + j), random.Next(1, 6), DateTime.Now.AddDays(-random.Next(1, 365)));
                }
            }
        }
    }

    public static void MeasurePerformance<T>(T array, string arrayType) where T : class
    {
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();

        int sum = 0;
        if (array is Exam[] oneDimensionalArray)
        {
            foreach (var exam in oneDimensionalArray)
            {
                sum += exam.Grade;
            }
        }
        else if (array is Exam[,] rectangularArray)
        {
            for (int i = 0; i < rectangularArray.GetLength(0); i++)
            {
                for (int j = 0; j < rectangularArray.GetLength(1); j++)
                {
                    sum += rectangularArray[i, j].Grade;
                }
            }
        }
        else if (array is Exam[][] jaggedArray)
        {
            for (int i = 0; i < jaggedArray.Length; i++)
            {
                for (int j = 0; j < jaggedArray[i].Length; j++)
                {
                    sum += jaggedArray[i][j].Grade;
                }
            }
        }

        stopwatch.Stop();
        Console.WriteLine($"{arrayType} - Время выполнения: {stopwatch.ElapsedMilliseconds} мс");
    }
}

public class Program
{
    public static void Main()
    {
        Student student = StudentGenerator.GenerateRandomStudent();
        Console.WriteLine(student.ToShortString());

        Console.WriteLine($"Специалист: {student[Education.Specialist]}");
        Console.WriteLine($"Бакалавр: {student[Education.Bachelor]}");
        Console.WriteLine($"Второе высшее: {student[Education.SecondEducation]}");

        student.Person = new Person("Максим", "Матаков");
        student.Education = Education.SecondEducation;
        student.GroupNumber = 1;
        student.AddExams(new Exam("Математика", 5, new DateTime(2025, 1, 1)), new Exam("Физика", 5, new DateTime(2025, 1, 1)));

        Console.WriteLine(student.ToString());

        int size = 1000;
        Exam[] oneDimensionalArray = new Exam[size];
        ArrayPerformance.FillArray(oneDimensionalArray, size);
        Exam[,] rectangularArray = new Exam[size, size];
        ArrayPerformance.FillArray(rectangularArray, size);
        Exam[][] jaggedArray = new Exam[size][];
        for (int i = 0; i < size; i++)
        {
            jaggedArray[i] = new Exam[size];
        }
        ArrayPerformance.FillArray(jaggedArray, size);

        ArrayPerformance.MeasurePerformance(oneDimensionalArray, "Одномерный массив");
        ArrayPerformance.MeasurePerformance(rectangularArray, "Прямоугольный массив");
        ArrayPerformance.MeasurePerformance(jaggedArray, "Зубчатый массив");
    }
}
