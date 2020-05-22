namespace CosmosEfCore.Models
{
    public class Student : Person
    {
        public int StartYear { get; private set; }

        public Student(
            string firstName,
            string lastName,
            College college,
            int startYear) : base(firstName, lastName, college)
        {
            StartYear = startYear;
        }
    }
}
