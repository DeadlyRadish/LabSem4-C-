using Lab2;
using System;

public enum Education
{
    Specialist,
    Bachelor,
    SecondEducation
}


public class Program
{
    public static void Main()
    {
        // 1. Создать два объекта типа Person с совпадающими данными
        Person person1 = new Person("Максим", "Матаков", new DateTime(2005, 11, 17));
        Person person2 = new Person("Максим", "Матаков", new DateTime(2005, 11, 17));

        Console.WriteLine($"Ссылки равны: {ReferenceEquals(person1, person2)}");
        Console.WriteLine($"Объекты равны: {person1 == person2}");
        Console.WriteLine($"Хэш-код person1: {person1.GetHashCode()}");
        Console.WriteLine($"Хэш-код person2: {person2.GetHashCode()}");

        // 2. Создать объект типа Student
        Student student = new Student(person1, Education.Bachelor, 101);
        student.AddExams(new Exam("Математика", 5, DateTime.Now), new Exam("Физика", 4, DateTime.Now));
        student.AddTests(new Test("История", true), new Test("Философия", false));

        Console.WriteLine(student.ToString());

        // 3. Вывести значение свойства типа Person для объекта типа Student
        Console.WriteLine(student.ToShortString());

        // 4. Создать полную копию объекта Student
        Student copiedStudent = (Student)student.DeepCopy();
        student.Name = "Студент";
        student.Surname = "Студент";
        student.GroupNumber = 102;

        Console.WriteLine("Исходный объект:");
        Console.WriteLine(student.ToString());

        Console.WriteLine("Копия:");
        Console.WriteLine(copiedStudent.ToString());

        // 5. Обработка исключения для номера группы
        try
        {
            student.GroupNumber = 50;
        }
        catch (ArgumentOutOfRangeException e)
        {
            Console.WriteLine(e.Message);
        }

        // 6. Вывод всех зачетов и экзаменов
        Console.WriteLine("Все зачеты и экзамены:");
        foreach (var item in student.GetAllExamsAndTests())
        {
            Console.WriteLine(item);
        }

        // 7. Вывод экзаменов с оценкой выше 3
        Console.WriteLine("Экзамены с оценкой выше 3:");
        foreach (var exam in student.GetExamsWithGradeGreaterThan(3))
        {
            Console.WriteLine(exam);
        }
    }
}