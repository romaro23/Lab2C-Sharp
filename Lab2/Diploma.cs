using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2
{
    internal class Diploma
    {

        public string Name { get; set; }
        public string Qualification { get; set; }
        public DateTime Date { get; set; }

        public Diploma(string name, string qualification, DateTime date)
        {
            Name = name;
            Qualification = qualification;
            Date = date;
        }
        public Diploma()
        {
            Name = "DNU";
            Qualification = "Bachelor";
            Date = DateTime.Today;
        }
        public override string ToString()
        {
            return $"Name: {this.Name}, Qualification: {this.Qualification}, Date: {this.Date}";
        }

    }
}

