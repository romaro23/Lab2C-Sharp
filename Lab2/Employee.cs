using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2
{
    
    internal class Employee : Person, IDateAndCopy, IEnumerable, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private string position;
        private TimeWork work_time;
        private int salary;
        private List<Diploma> diplomas = new List<Diploma>();
        private List<Experience> experiences = new List<Experience>();

        public string Position
        {
            get { return position; }
            set
            {
                position = value;
                OnPropertyChanged(nameof(Position));
            }
        }
        public TimeWork WorkTime
        {
            get { return work_time; }
            set { work_time = value; }
        }
        public int Salary
        {
            get { return salary; }
            set
            {
                if (value <= 0 || value > 2000)
                {
                    throw new ArgumentOutOfRangeException("The value must be from 1 to 2000");
                }
                else
                {
                    salary = value;
                    OnPropertyChanged(nameof(Salary));
                }
            }
        }
        public Person PersonData
        {
            get { return new Person(this.Name, this.Surname, this.Date); }
            set
            {
                this.Name = value.Name;
                this.Surname = value.Surname;
                this.Date = value.Date;
            }
        }
        public List<Diploma> Diplomas
        {
            get { return diplomas; }
            set { diplomas = value; }
        }
        public List<Experience> Experiences
        {
            get { return experiences; }
            set { experiences = value; }
        }
        public Diploma lastDiploma
        {
            get
            {
                if (Diplomas.Count == 0)
                {
                    return null;
                }
                else
                {
                    Diploma latestDiploma = Diplomas[0];
                    for (int i = 1; i < Diplomas.Count; i++)
                    {
                        if (Diplomas[i].Date > latestDiploma.Date)
                        {
                            latestDiploma = Diplomas[i];
                        }
                    }

                    return latestDiploma;
                }
            }
        }
        public void AddDiplomas(params Diploma[] new_diplomas)
        {
            for (int i = 0; i < new_diplomas.Length; i++)
            {
                Diplomas.Add(new_diplomas[i]);
            }

        }
        public Employee(Person person, string position, TimeWork work_time, int salary)
            : base(person.Name, person.Surname, person.Date)
        {
            Position = position;
            WorkTime = work_time;
            Salary = salary;
        }
        public Employee()
        {
            Position = "Beginner";
            WorkTime = new TimeWork();
            Salary = 1;
        }
        public override string ToString()
        {
            return $"Name: {Name}, Surname: {Surname}, DateOfBirth: {Date}, Position: {Position}, WorkTime: {WorkTime}, Salary: {Salary}, Diplomas: {string.Join("; ", Diplomas)}, Experience: {string.Join("; ", Experiences)}";
        }
        public override string ToShortString()
        {
            return $"Name: {Name}, Surname: {Surname}, DateOfBirth: {Date}, Position: {Position}, WorkTime: {WorkTime}, Salary: {Salary}, NumberOfDiplomas {Diplomas.Count}";
        }
        public override object DeepCopy()
        {
            Employee newEmployee = new Employee();
            newEmployee.PersonData = PersonData;
            newEmployee.Position = Position;
            newEmployee.WorkTime = WorkTime;
            newEmployee.Salary = Salary;
            foreach (Diploma diploma in Diplomas)
            {
                Diploma newDiploma = new Diploma(diploma.Name, diploma.Qualification, diploma.Date);
                newEmployee.AddDiplomas(newDiploma);
            }
            foreach (Experience experience in Experiences)
            {
                Experience newExperience = new Experience(experience.NameOfWork, experience.LastPosition, experience.DateOfStart, experience.DateOfEnd);
                newEmployee.Experiences.Add(newExperience);
            }
            return newEmployee;
        }


        public IEnumerable<object> GetDiplomasAndExperiences()
        {
            foreach (Object diploma in Diplomas)
            {
                yield return diploma;
            }
            foreach (Object experience in Experiences)
            {
                yield return experience;
            }
        }
        public IEnumerable<Diploma> GetDiplomas(int n)
        {
            DateTime now = DateTime.Now;
            int year = now.Year;
            foreach (Diploma diploma in Diplomas)
            {
                if (diploma.Date.Year >= year - n)
                {
                    yield return diploma;
                }
            }
        }
        public IEnumerable<string> Accountant()
        {
            foreach (Experience name in experiences)
            {
                if (name.LastPosition == "Accountant")
                {
                    yield return name.NameOfWork;
                }
            }
        }
        public IEnumerator GetEnumerator()
        {
            return new EmployeeEnumerator(experiences);
        }
        private class EmployeeEnumerator: IEnumerator<object>
        {
            private List<Experience> experiences;
            private int position;
            public EmployeeEnumerator(List<Experience> experiences)
            {
                this.experiences = experiences;
                position = -1;
            }
            public bool MoveNext() 
            {
                position++;
                while (position < experiences.Count) 
                {

                    if ((experiences[position].DateOfEnd - experiences[position].DateOfStart).TotalDays < 365) 
                    { return true; }
                    
                    position++;
                }
                return false;
            }
            public object Current
            {
                get { return experiences[position].NameOfWork; }
            }
            public void Reset() {
                position = -1;
            }
            public void Dispose() { }
        }
        
}
}
