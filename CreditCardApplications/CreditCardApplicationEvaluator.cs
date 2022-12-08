namespace CreditCardApplications;

using System;

public class CreditCardApplicationEvaluator
{
    private readonly IFrequentFlyerNumberValidator validator;
    private readonly FraudLookup fraudLookup;

    private const int AutoReferralMaxAge = 20;
    private const int HighIncomeThreshold = 100_000;
    private const int LowIncomeThreshold = 20_000;

    public CreditCardApplicationEvaluator(IFrequentFlyerNumberValidator validator, FraudLookup fraudLookup = null)
    {
        this.validator = validator ?? throw new ArgumentNullException(nameof(validator));
        this.validator.ValidatorLookupPerformed += this.ValidatorLookupPerformed;
        this.fraudLookup = fraudLookup;
    }

    public int ValidatorLookupCount { get; private set; }

    public CreditCardApplicationDecision Evaluate(CreditCardApplication application)
    {
        if (this.fraudLookup != null && this.fraudLookup.IsFraudRisk(application))
        {
            return CreditCardApplicationDecision.ReferredToHumanFraudRisk;
        }

        if (application.GrossAnnualIncome >= HighIncomeThreshold)
        {
            return CreditCardApplicationDecision.AutoAccepted;
        }

        if (this.validator.ServiceInformation.License.LicenseKey == "EXPIRED")
        {
            return CreditCardApplicationDecision.ReferredToHuman;
        }

        this.validator.ValidationMode = application.Age >= 30 ? ValidationMode.Detailed : ValidationMode.Quick;
        bool isValidFrequentFlyerNumber;
        try
        {
            isValidFrequentFlyerNumber = this.validator.IsValid(application.FrequentFlyerNumber);
        }
        catch (Exception)
        {
            // Log
            return CreditCardApplicationDecision.ReferredToHuman;
        }

        if (!isValidFrequentFlyerNumber)
        {
            return CreditCardApplicationDecision.ReferredToHuman;
        }

        if (application.Age <= AutoReferralMaxAge)
        {
            return CreditCardApplicationDecision.ReferredToHuman;
        }

        return application.GrossAnnualIncome < LowIncomeThreshold
            ? CreditCardApplicationDecision.AutoDeclined
            : CreditCardApplicationDecision.ReferredToHuman;
    }

    private void ValidatorLookupPerformed(object sender, EventArgs e) => this.ValidatorLookupCount++;

    //public CreditCardApplicationDecision EvaluateUsingOut(CreditCardApplication application)
    //{
    //    if (application.GrossAnnualIncome >= HighIncomeThreshold)
    //    {
    //        return CreditCardApplicationDecision.AutoAccepted;
    //    }

    //    this.validator.IsValid(application.FrequentFlyerNumber, out bool isValidFrequentFlyerNumber);
    //    if (!isValidFrequentFlyerNumber)
    //    {
    //        return CreditCardApplicationDecision.ReferredToHuman;
    //    }

    //    if (application.Age <= AutoReferralMaxAge)
    //    {
    //        return CreditCardApplicationDecision.ReferredToHuman;
    //    }

    //    if (application.GrossAnnualIncome < LowIncomeThreshold)
    //    {
    //        return CreditCardApplicationDecision.AutoDeclined;
    //    }

    //    return CreditCardApplicationDecision.ReferredToHuman;
    //}
}