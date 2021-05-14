namespace CreditCardApplications
{
    public class FraudLookup
    {
        //public virtual bool IsFraudRisk(CreditCardApplication application) => application.LastName == "Smith";

        public bool IsFraudRisk(CreditCardApplication application) => this.CheckApplication(application);

        protected virtual bool CheckApplication(CreditCardApplication application) => application.LastName == "Smith";
    }
}
