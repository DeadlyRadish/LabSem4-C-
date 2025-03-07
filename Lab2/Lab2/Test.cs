using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2
{
    public class Test
    {
        public string Subject { get; set; }
        public bool IsPassed { get; set; }

        public Test(string subject, bool isPassed)
        {
            Subject = subject;
            IsPassed = isPassed;
        }

        public Test()
        {
            Subject = "Unknown";
            IsPassed = false;
        }

        public override string ToString()
        {
            return $"Предмет: {Subject}, Сдан: {IsPassed}";
        }
    }
}
