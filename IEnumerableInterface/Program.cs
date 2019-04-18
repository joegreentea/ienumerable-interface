using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IEnumerableInterface
{
    class Program
    {
        static void Main(string[] args)
        {
            Person[] peopleArray = new Person[3]
            {
                new Person("John", "Smith"),
                new Person("Jane", "Doe"),
                new Person("Robert", "Abbott"),
            };

            // Using foreach Array construct
            Console.WriteLine("Using System.Array GetEnumerator() which already implements IEnumerator:");
            People peopleList = new People(peopleArray);
            foreach (Person person in peopleList)
            {
                Console.WriteLine($"- {person.firstName} {person.lastName}");
            }

            Console.WriteLine("\n###########################################\n");

            // Manually work with IEnumerator 
            Console.WriteLine("Manually loop through IEnumerator implementation:");
            IEnumerator iterator = peopleArray.GetEnumerator();
            while (iterator.MoveNext() && iterator.Current != null)
            {
                Person person = (Person)iterator.Current;
                Console.WriteLine($"- {person.firstName} {person.lastName}");
            }
                
        }
    }

    public class Person
    {
        public string firstName { get; set; }
        public string lastName { get; set; }

        public Person(string firstName, string lastName)
        {
            this.firstName = firstName;
            this.lastName = lastName;
        }
    }

    public class People : IEnumerable
    {
        private Person[] persons;

        public People(Person[] pArray)
        {
            persons = new Person[pArray.Length];

            for (int i = 0; i < pArray.Length; i++)
            {
                persons[i] = pArray[i];
            }
        }

        public IEnumerator GetEnumerator()
        {
            // using manual implementation of IEnumerator's methods
            IEnumerator peopleEnumerator = new PeopleEnumerator(persons);
            return peopleEnumerator;

            // using built-in array GetEnumerator() method
            //return persons.GetEnumerator();
        }
    }

    public class PeopleEnumerator : IEnumerator
    {
        // field
        private Person[] persons;
        private int position = -1;

        // property
        public object Current
        {
            get { return persons[position]; }
        }

        // constructor
        public PeopleEnumerator(Person[] pArray)
        {
            persons = pArray;
        }

        public bool MoveNext()
        {
            position++;
            return (position < persons.Length);
        }

        public void Reset()
        {
            position = -1;
        }
    }

}
