using System;

namespace CSharp_Udemy_Course.Start
{
    class Program1 : Program3
    {
        string firstName;
        string lastName;

        public Program1(string firstName, string lastName)
        {
            this.firstName = firstName;
            this.lastName = lastName;
        }

        public void greetUser()
        {
            Console.Write($"Hello {firstName} {lastName}");
            addSmiley();

        }
        static void Main(string[] args)
        {
            Program1 program1 = new Program1("Kieran", "Tierney");

            program1.greetUser();
        }
    }
}

