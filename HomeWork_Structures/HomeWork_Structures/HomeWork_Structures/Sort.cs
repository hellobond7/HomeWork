using System.Collections.Generic;

namespace HomeWork_Structures
{
    class Sort : IComparer<Person>
    {
        byte choise;
        public Sort(byte choise)
        {
            this.choise = choise;
        }

        public int Compare(Person p1, Person p2)
        {
            if (choise == 1)
            {
                if (p1.ID < p2.ID) return -1;
                if (p1.ID == p2.ID) return 0;
                return 1;
            }
            else if (choise == 2)
            {
                if (p1.Date < p2.Date) return -1;
                if (p1.Date == p2.Date) return 0;
                return 1;
            }
            else if (choise == 3)
            {
                int i = p1.Name.CompareTo(p2.Name);
                return i;
            }
            else if (choise == 4)
            {
                if (p1.Age < p2.Age) return -1;
                if (p1.Age == p2.Age) return 0;
                return 1;
            }
            else if (choise == 5)
            {
                if (p1.Height < p2.Height) return -1;
                if (p1.Height == p2.Height) return 0;
                return 1;
            }
            else if (choise == 6)
            {
                if (p1.BirthDay < p2.BirthDay) return -1;
                if (p1.BirthDay == p2.BirthDay) return 0;
                return 1;
            }
            else
            {
                int i = p1.PlaceOfBirth.CompareTo(p2.PlaceOfBirth);
                return i;
            }
        }
    }

}
