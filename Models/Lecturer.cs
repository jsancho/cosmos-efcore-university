namespace CosmosEfCore.Models
{
    public class Lecturer : Staff
    {
        public bool HeadOfDepartment { get; private set; }

        public Lecturer(
            string firstName,
            string lastName,
            ContractType contractType,
            College college,
            bool headOfDepartment = false)
            : base(firstName, lastName, contractType, college)
        {
            HeadOfDepartment = headOfDepartment;
        }
    }
}
