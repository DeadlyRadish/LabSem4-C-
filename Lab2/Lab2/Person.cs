using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2
{
    public class Person : IDateAndCopy
    {
        protected string name;
        protected string surname;
        protected DateTime birthDate;

        public Person(string name, string surname, DateTime birthDate)
        {
            this.name = name;
            this.surname = surname;
            this.birthDate = birthDate;
        }

        public Person()
        {
            this.name = "Unknown";
            this.surname = "Unknown";
            this.birthDate = DateTime.MinValue;
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public string Surname
        {
            get { return surname; }
            set { surname = value; }
        }

        public DateTime BirthDate
        {
            get { return birthDate; }
            set { birthDate = value; }
        }

        public int BirthYear
        {
            get { return birthDate.Year; }
            set { birthDate = new DateTime(value, birthDate.Month, birthDate.Day); }
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            Person p = (Person)obj;
            return name == p.name && surname == p.surname && birthDate == p.birthDate;
        }

        public static bool operator ==(Person p1, Person p2)
        {
            if (ReferenceEquals(p1, p2))
                return true;
            if (p1 is null || p2 is null)
                return false;
            return p1.Equals(p2);
        }

        public static bool operator !=(Person p1, Person p2)
        {
            return !(p1 == p2);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(name, surname, birthDate);
        }

        public virtual object DeepCopy()
        {
            return new Person(name, surname, birthDate);
        }

        public DateTime Date
        {
            get { return birthDate; }
            set { birthDate = value; }
        }

        public override string ToString()
        {
            return $"{Name} {Surname}, Дата рождения: {BirthDate.ToShortDateString()}";
        }

        public virtual string ToShortString()
        {
            return $"{Name} {Surname}";
        }
    }
}
