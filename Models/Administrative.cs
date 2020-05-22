namespace CosmosEfCore.Models
{
    public class Administrative : Staff
    {
        public string Department { get; private set; }

        public Administrative(
            string firstName,
            string lastName,
            ContractType contractType,
            College college,
            string department)
            : base(firstName, lastName, contractType, college)
        {
            Department = department;
        }
    }
}
