using System;

namespace CosmosEfCore.Models
{
    public abstract class Person
    {
        public Guid Id { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public College College { get; private set; }

        public Person(string firstName, string lastName, College college)
        {
            Id = Guid.NewGuid();

            FirstName = firstName;
            LastName = lastName;
            College = college;
        }
    }
}
