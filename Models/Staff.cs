namespace CosmosEfCore.Models
{
    public abstract class Staff : Person
    {
        public ContractType ContractType { get; private set; }

        public Staff(
            string firstName,
            string lastName,
            ContractType contractType,
            College college)
            : base(firstName, lastName, college)
        {
            ContractType = contractType;
        }
    }
}
