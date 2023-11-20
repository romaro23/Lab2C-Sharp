using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2
{
    internal class Experience
    {
        public string NameOfWork { get; set; }
        public string LastPosition { get; set; }
        public DateTime DateOfStart { get; set; }
        public DateTime DateOfEnd { get; set; }
        public Experience() {
            NameOfWork = "SoftServe";
            LastPosition = "Junior";
            DateOfStart = DateTime.Parse("15.02.2020");
            DateOfEnd = DateTime.Parse("01.10.2020");
        }
        public Experience(string name, string position, DateTime start, DateTime end) {
            NameOfWork = name;
            LastPosition = position;
            DateOfStart = start;
            DateOfEnd = end;
        }
        public override string ToString()
        {
            return $"NameOfWork: {NameOfWork}, LastPosition: {LastPosition}, DateOfStart: {DateOfStart}, DateOfEnd: {DateOfEnd}";
        }
    }
}

