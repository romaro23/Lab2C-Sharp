using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2
{
    internal class Person : IDateAndCopy
    {
        protected string name;
        protected string surname;
        protected DateTime dateOfBirth;
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }
        public string Surname
        {
            get
            {
                return surname;
            }
            set
            {
                surname = value;
            }
        }
        public DateTime Date
        {
            get
            {
                return dateOfBirth;
            }
            set
            {
                dateOfBirth = value;
            }
        }

        public int Year
        {
            get
            {
                return dateOfBirth.Year;
            }
            set
            {
                dateOfBirth = new DateTime(value, dateOfBirth.Month, dateOfBirth.Day);
            }
        }
        public Person()
        {
            Name = "Roman";
            Surname = "Nagorniy";
            Date = DateTime.Parse("08.07.2005");
        }
        public Person(string name, string surname, DateTime date)
        {
            Name = name;
            Surname = surname;
            Date = date;
        }
        public override string ToString()
        {
            return $"Name: {Name}, Surname: {Surname}, DateOfBirth: {Date}";
        }
        public virtual string ToShortString()
        {
            return $"Name: {Name}, Surname: {Surname}";
        }
        public override bool Equals(object obj)
        {
            if(obj is Person && obj != null)
            {
                Person tmp;
                tmp = (Person)obj;
                if (tmp.Name == this.Name && tmp.Surname == this.Surname && tmp.Date == Date) {
                    return true;
                }
                else
                {
                    return false;
                }
                
            }
            return false;
        }
        public static bool operator == (Person a, Person b)
        { return a.Equals(b); }
        public static bool operator != (Person a, Person b)
        { return !a.Equals(b); }
        public override int GetHashCode()
        {
            return this.ToString().GetHashCode();
        }
        public virtual object DeepCopy()
        {
            return new Person(Name, Surname, Date);
        }
    }
}
