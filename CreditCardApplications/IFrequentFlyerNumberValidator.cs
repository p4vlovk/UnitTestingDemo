namespace CreditCardApplications
{
    using System;

    public interface ILicenseData
    {
        string LicenseKey { get; }
    }

    public interface IServiceInformation
    {
        ILicenseData License { get; }
    }

    public interface IFrequentFlyerNumberValidator
    {
        //string LicenseKay { get; }

        IServiceInformation ServiceInformation { get; }

        ValidationMode ValidationMode { get; set; }

        event EventHandler ValidatorLookupPerformed;

        bool IsValid(string frequentFlyerNumber);

        void IsValid(string frequentFlyerNumber, out bool isValid);
    }
}
