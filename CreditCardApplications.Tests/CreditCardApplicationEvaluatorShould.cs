namespace CreditCardApplications.Tests;

using System;
using System.Collections.Generic;
using Moq;
using Moq.Protected;
using Xunit;

public class CreditCardApplicationEvaluatorShould
{
    private readonly Mock<IFrequentFlyerNumberValidator> mockValidator;
    
    private CreditCardApplicationEvaluator sut;
    
    public CreditCardApplicationEvaluatorShould()
    {
        this.mockValidator = new Mock<IFrequentFlyerNumberValidator>();
        this.mockValidator.SetupAllProperties();
        this.mockValidator.Setup(x => x.ServiceInformation.License.LicenseKey).Returns("OK");
        this.mockValidator.Setup(x => x.IsValid(It.IsAny<string>())).Returns(true);
        this.sut = new CreditCardApplicationEvaluator(this.mockValidator.Object);
    }
    
    [Fact]
    public void AcceptHighIncomeApplications()
    {
        // Arrange
        var application = new CreditCardApplication { GrossAnnualIncome = 100_000 };
    
        // Act
        var decision = this.sut.Evaluate(application);
    
        // Assert
        Assert.Equal(CreditCardApplicationDecision.AutoAccepted, decision);
    }
    
    [Fact]
    public void ReferYoungApplications()
    {
        // Arrange
        this.mockValidator.DefaultValue = DefaultValue.Mock;
        var application = new CreditCardApplication { Age = 19 };
    
        // Act
        var decision = this.sut.Evaluate(application);
    
        // Assert
        Assert.Equal(CreditCardApplicationDecision.ReferredToHuman, decision);
    }
    
    [Fact]
    public void DeclineLowIncomeApplications()
    {
        // Arrange
        var application = new CreditCardApplication
        {
            GrossAnnualIncome = 19_999,
            Age = 42,
            FrequentFlyerNumber = "y"
        };
    
        // Act
        var decision = this.sut.Evaluate(application);
    
        // Assert
        Assert.Equal(CreditCardApplicationDecision.AutoDeclined, decision);
    }
    
    [Fact]
    public void ReferInvalidFrequentFlyerApplications()
    {
        // Arrange
        this.mockValidator.Setup(x => x.IsValid(It.IsAny<string>())).Returns(false);
        var application = new CreditCardApplication();
    
        // Act
        var decision = this.sut.Evaluate(application);
    
        // Assert
        Assert.Equal(CreditCardApplicationDecision.ReferredToHuman, decision);
    }
    
    [Fact]
    public void ReferWhenLicenseKeyExpired()
    {
        // Arrange
        this.mockValidator.Setup(x => x.ServiceInformation.License.LicenseKey)
            .Returns(this.GetLicenseKeyExpiryString);
    
        var application = new CreditCardApplication { Age = 42 };
    
        // Act
        var decision = this.sut.Evaluate(application);
    
        // Assert
        Assert.Equal(CreditCardApplicationDecision.ReferredToHuman, decision);
    }
    
    [Fact]
    public void UseDetailedLookupForOlderApplications()
    {
        // Arrange
        var application = new CreditCardApplication { Age = 30 };
    
        // Act
        this.sut.Evaluate(application);
    
        // Assert
        Assert.Equal(ValidationMode.Detailed, this.mockValidator.Object.ValidationMode);
    }
    
    [Fact]
    public void ValidateFrequentFlyerNumberForLowIncomeApplications()
    {
        // Arrange
        var application = new CreditCardApplication();
    
        // Act
        this.sut.Evaluate(application);
    
        // Assert
        this.mockValidator.Verify(x => x.IsValid(It.IsAny<string>()));
    }
    
    [Fact]
    public void NotValidateFrequentFlyerNumberForHighIncomeApplications()
    {
        // Arrange
        var application = new CreditCardApplication { GrossAnnualIncome = 100_000 };
    
        // Act
        this.sut.Evaluate(application);
    
        // Assert
        this.mockValidator.Verify(x => x.IsValid(It.IsAny<string>()), Times.Never);
    }
    
    [Fact]
    public void CheckLicenseKeyForLowIncomeApplications()
    {
        // Arrange
        var application = new CreditCardApplication { GrossAnnualIncome = 99_000 };
    
        // Act
        this.sut.Evaluate(application);
    
        // Assert
        this.mockValidator.VerifyGet(x => x.ServiceInformation.License.LicenseKey, Times.Once);
    }
    
    [Fact]
    public void SetDetailedLookupForOlderApplications()
    {
        // Arrange
        var application = new CreditCardApplication { Age = 30 };
    
        // Act
        this.sut.Evaluate(application);
    
        // Assert
        this.mockValidator.VerifySet(x => x.ValidationMode = It.IsAny<ValidationMode>(), Times.Once);
    }
    
    [Fact]
    public void ReferWhenFrequentFlyerValidationError()
    {
        // Arrange
        this.mockValidator.Setup(x => x.IsValid(It.IsAny<string>())).Throws<Exception>();
        var application = new CreditCardApplication { Age = 42 };
    
        // Act
        var decision = this.sut.Evaluate(application);
    
        // Assert
        Assert.Equal(CreditCardApplicationDecision.ReferredToHuman, decision);
    }
    
    [Fact]
    public void IncrementLookupCount()
    {
        // Arrange
        this.mockValidator.Setup(x => x.IsValid(It.IsAny<string>()))
            .Returns(true)
            .Raises(x => x.ValidatorLookupPerformed += null, EventArgs.Empty);
    
        var application = new CreditCardApplication { FrequentFlyerNumber = "x", Age = 25 };
    
        // Act
        this.sut.Evaluate(application);
    
        // Assert
        Assert.Equal(1, this.sut.ValidatorLookupCount);
    }
    
    [Fact]
    public void ReferInvalidFrequentFlyerApplications_ReturnValuesSequence()
    {
        // Arrange
        this.mockValidator.SetupSequence(x => x.IsValid(It.IsAny<string>()))
            .Returns(false)
            .Returns(true);
    
        var application = new CreditCardApplication { Age = 25 };
    
        // Act
        var firstDecision = this.sut.Evaluate(application);
        var secondDecision = this.sut.Evaluate(application);
    
        // Assert
        Assert.Equal(CreditCardApplicationDecision.ReferredToHuman, firstDecision);
        Assert.Equal(CreditCardApplicationDecision.AutoDeclined, secondDecision);
    }
    
    [Fact]
    public void ReferInvalidFrequentFlyerApplications_MultipleCallsSequence()
    {
        // Arrange
        var frequentFlyerNumbersPassed = new List<string>();
        this.mockValidator.Setup(x => x.IsValid(Capture.In(frequentFlyerNumbersPassed)));
        var application1 = new CreditCardApplication { Age = 25, FrequentFlyerNumber = "aa" };
        var application2 = new CreditCardApplication { Age = 25, FrequentFlyerNumber = "bb" };
        var application3 = new CreditCardApplication { Age = 25, FrequentFlyerNumber = "cc" };
    
        // Act
        this.sut.Evaluate(application1);
        this.sut.Evaluate(application2);
        this.sut.Evaluate(application3);
    
        // Assert that IsValid was called three times with "aa", "bb" and "cc"
        Assert.Equal(new List<string> { "aa", "bb", "cc" }, frequentFlyerNumbersPassed);
    }
    
    [Fact]
    public void ReferFraudRisk()
    {
        // Arrange
        var mockFraudLookup = new Mock<FraudLookup>();
        mockFraudLookup
            .Protected()
            .Setup<bool>("CheckApplication", ItExpr.IsAny<CreditCardApplication>())
            .Returns(true);
    
        this.sut = new CreditCardApplicationEvaluator(this.mockValidator.Object, mockFraudLookup.Object);
        var application = new CreditCardApplication();
    
        // Act
        var decision = this.sut.Evaluate(application);
    
        // Assert
        Assert.Equal(CreditCardApplicationDecision.ReferredToHumanFraudRisk, decision);
    }
    
    [Fact]
    public void LinqToMocks()
    {
        // Arrange
        this.sut = new CreditCardApplicationEvaluator(Mock.Of<IFrequentFlyerNumberValidator>(
            validator => validator.ServiceInformation.License.LicenseKey == "OK" &&
                         validator.IsValid(It.IsAny<string>())));
    
        var application = new CreditCardApplication { Age = 25 };
    
        // Act
        var decision = this.sut.Evaluate(application);
    
        // Assert
        Assert.Equal(CreditCardApplicationDecision.AutoDeclined, decision);
    }
    
    private string GetLicenseKeyExpiryString()
    {
        // E.g. read from vendor-supplied constants file
        return "EXPIRED";
    }
}

// public class CreditCardApplicationEvaluatorShould
// {
//     [Fact]
//     public void AcceptHighIncomeApplications()
//     {
//         // Arrange
//         var mockValidator = new Mock<IFrequentFlyerNumberValidator>();
//         var sut = new CreditCardApplicationEvaluator(mockValidator.Object);
//         var application = new CreditCardApplication { GrossAnnualIncome = 100_000 };
//
//         // Act
//         var decision = sut.Evaluate(application);
//
//         // Assert
//         Assert.Equal(CreditCardApplicationDecision.AutoAccepted, decision);
//     }
//
//     [Fact]
//     public void ReferYoungApplications()
//     {
//         // Arrange
//         var mockValidator = new Mock<IFrequentFlyerNumberValidator> { DefaultValue = DefaultValue.Mock };
//         mockValidator.Setup(x => x.IsValid(It.IsAny<string>())).Returns(true);
//
//         var sut = new CreditCardApplicationEvaluator(mockValidator.Object);
//         var application = new CreditCardApplication { Age = 19 };
//
//         // Act
//         var decision = sut.Evaluate(application);
//
//         // Assert
//         Assert.Equal(CreditCardApplicationDecision.ReferredToHuman, decision);
//     }
//
//     [Fact]
//     public void DeclineLowIncomeApplications()
//     {
//         // Arrange
//         var mockValidator = new Mock<IFrequentFlyerNumberValidator>();
//         //mockValidator.Setup(x => x.IsValid("x")).Returns(true);
//         //mockValidator.Setup(x => x.IsValid(It.IsAny<string>())).Returns(true);
//         //mockValidator.Setup(x => x.IsValid(It.Is<string>(number => number.StartsWith("y")))).Returns(true);
//         //mockValidator.Setup(x => x.IsValid(It.IsInRange("a", "z", Range.Inclusive))).Returns(true);
//         //mockValidator.Setup(x => x.IsValid(It.IsIn("z", "y", "x"))).Returns(true);
//         mockValidator.Setup(x => x.IsValid(It.IsRegex("[a-z]"))).Returns(true);
//         mockValidator.Setup(x => x.ServiceInformation.License.LicenseKey).Returns("OK");
//
//         var sut = new CreditCardApplicationEvaluator(mockValidator.Object);
//         var application = new CreditCardApplication
//         {
//             GrossAnnualIncome = 19_999,
//             Age = 42,
//             FrequentFlyerNumber = "y"
//         };
//
//         // Act
//         var decision = sut.Evaluate(application);
//
//         // Assert
//         Assert.Equal(CreditCardApplicationDecision.AutoDeclined, decision);
//     }
//
//     [Fact]
//     public void ReferInvalidFrequentFlyerApplications()
//     {
//         // Arrange
//         var mockValidator = new Mock<IFrequentFlyerNumberValidator>();
//         mockValidator.Setup(x => x.IsValid(It.IsAny<string>())).Returns(false);
//         mockValidator.Setup(x => x.ServiceInformation.License.LicenseKey).Returns("OK");
//
//         var sut = new CreditCardApplicationEvaluator(mockValidator.Object);
//         var application = new CreditCardApplication();
//
//         // Act
//         var decision = sut.Evaluate(application);
//
//         // Assert
//         Assert.Equal(CreditCardApplicationDecision.ReferredToHuman, decision);
//     }
//
//     //[Fact]
//     //public void DeclineLowIncomeApplicationsOutDemo()
//     //{
//     //    // Arrange
//     //    var mockValidator = new Mock<IFrequentFlyerNumberValidator>();
//     //    bool isValid = true;
//     //    mockValidator.Setup(x => x.IsValid(It.IsAny<string>(), out isValid));
//     //    var sut = new CreditCardApplicationEvaluator(mockValidator.Object);
//     //    var application = new CreditCardApplication
//     //    {
//     //        GrossAnnualIncome = 19_999,
//     //        Age = 42
//     //    };
//
//     //    // Act
//     //    var decision = sut.EvaluateUsingOut(application);
//
//     //    // Assert
//     //    Assert.Equal(CreditCardApplicationDecision.AutoDeclined, decision);
//     //}
//
//     [Fact]
//     public void ReferWhenLicenseKeyExpired()
//     {
//         // Arrange
//         //var mockLicenseData = new Mock<ILicenseData>();
//         //mockLicenseData.Setup(x => x.LicenseKey).Returns("EXPIRED");
//
//         //var mockServiceInfo = new Mock<IServiceInformation>();
//         //mockServiceInfo.Setup(x => x.License).Returns(mockLicenseData.Object);
//
//         var mockValidator = new Mock<IFrequentFlyerNumberValidator>();
//         mockValidator.Setup(x => x.IsValid(It.IsAny<string>())).Returns(true);
//         // Auto-mocking property hierarchy
//         mockValidator.Setup(x => x.ServiceInformation.License.LicenseKey).Returns(this.GetLicenseKeyExpiryString);
//         //mockValidator.Setup(x => x.ServiceInformation).Returns(mockServiceInfo.Object);
//         //mockValidator.Setup(x => x.LicenseKay).Returns("EXPIRED");
//         //mockValidator.Setup(x => x.LicenseKay).Returns(this.GetLicenseKeyExpiryString);
//
//         var sut = new CreditCardApplicationEvaluator(mockValidator.Object);
//         var application = new CreditCardApplication { Age = 42 };
//
//         // Act
//         var decision = sut.Evaluate(application);
//
//         // Assert
//         Assert.Equal(CreditCardApplicationDecision.ReferredToHuman, decision);
//     }
//
//     [Fact]
//     public void UseDetailedLookupForOlderApplications()
//     {
//         // Arrange
//         var mockValidator = new Mock<IFrequentFlyerNumberValidator>();
//         //mockValidator.SetupProperty(x => x.ValidationMode);
//         mockValidator.SetupAllProperties();
//         mockValidator.Setup(x => x.ServiceInformation.License.LicenseKey).Returns("OK");
//         var sut = new CreditCardApplicationEvaluator(mockValidator.Object);
//         var application = new CreditCardApplication { Age = 30 };
//
//         // Act
//         sut.Evaluate(application);
//
//         // Assert
//         Assert.Equal(ValidationMode.Detailed, mockValidator.Object.ValidationMode);
//     }
//
//     [Fact]
//     public void ValidateFrequentFlyerNumberForLowIncomeApplications()
//     {
//         // Arrange
//         var mockValidator = new Mock<IFrequentFlyerNumberValidator>();
//         mockValidator.Setup(x => x.ServiceInformation.License.LicenseKey).Returns("OK");
//         var sut = new CreditCardApplicationEvaluator(mockValidator.Object);
//         var application = new CreditCardApplication();
//
//         // Act
//         sut.Evaluate(application);
//
//         // Assert
//         mockValidator.Verify(x => x.IsValid(It.IsAny<string>()));
//     }
//
//     [Fact]
//     public void NotValidateFrequentFlyerNumberForHighIncomeApplications()
//     {
//         // Arrange
//         var mockValidator = new Mock<IFrequentFlyerNumberValidator>();
//         mockValidator.Setup(x => x.ServiceInformation.License.LicenseKey).Returns("OK");
//         var sut = new CreditCardApplicationEvaluator(mockValidator.Object);
//         var application = new CreditCardApplication { GrossAnnualIncome = 100_000 };
//
//         // Act
//         sut.Evaluate(application);
//
//         // Assert
//         mockValidator.Verify(x => x.IsValid(It.IsAny<string>()), Times.Never);
//     }
//
//     [Fact]
//     public void CheckLicenseKeyForLowIncomeApplications()
//     {
//         // Arrange
//         var mockValidator = new Mock<IFrequentFlyerNumberValidator>();
//         mockValidator.Setup(x => x.ServiceInformation.License.LicenseKey).Returns("OK");
//         var sut = new CreditCardApplicationEvaluator(mockValidator.Object);
//         var application = new CreditCardApplication { GrossAnnualIncome = 99_000 };
//
//         // Act
//         sut.Evaluate(application);
//
//         // Assert
//         mockValidator.VerifyGet(x => x.ServiceInformation.License.LicenseKey, Times.Once);
//     }
//
//     [Fact]
//     public void SetDetailedLookupForOlderApplications()
//     {
//         // Arrange
//         var mockValidator = new Mock<IFrequentFlyerNumberValidator>();
//         mockValidator.Setup(x => x.ServiceInformation.License.LicenseKey).Returns("OK");
//         var sut = new CreditCardApplicationEvaluator(mockValidator.Object);
//         var application = new CreditCardApplication { Age = 30 };
//
//         // Act
//         sut.Evaluate(application);
//
//         // Assert
//         //mockValidator.VerifySet(x => x.ValidationMode = ValidationMode.Detailed);
//         //mockValidator.VerifySet(x => x.ValidationMode = It.IsAny<ValidationMode>());
//         mockValidator.VerifySet(x => x.ValidationMode = It.IsAny<ValidationMode>(), Times.Once);
//         //mockValidator.VerifyNoOtherCalls();
//     }
//
//     [Fact]
//     public void ReferWhenFrequentFlyerValidationError()
//     {
//         // Arrange
//         var mockValidator = new Mock<IFrequentFlyerNumberValidator>();
//         mockValidator.Setup(x => x.ServiceInformation.License.LicenseKey).Returns("OK");
//         mockValidator.Setup(x => x.IsValid(It.IsAny<string>())).Throws<Exception>();
//         var sut = new CreditCardApplicationEvaluator(mockValidator.Object);
//         var application = new CreditCardApplication { Age = 42 };
//
//         // Act
//         var decision = sut.Evaluate(application);
//
//         // Assert
//         Assert.Equal(CreditCardApplicationDecision.ReferredToHuman, decision);
//     }
//
//     [Fact]
//     public void IncrementLookupCount()
//     {
//         // Arrange
//         var mockValidator = new Mock<IFrequentFlyerNumberValidator>();
//         mockValidator.Setup(x => x.ServiceInformation.License.LicenseKey).Returns("OK");
//         mockValidator.Setup(x => x.IsValid(It.IsAny<string>()))
//             .Returns(true)
//             .Raises(x => x.ValidatorLookupPerformed += null, EventArgs.Empty);
//
//         var sut = new CreditCardApplicationEvaluator(mockValidator.Object);
//         var application = new CreditCardApplication { FrequentFlyerNumber = "x", Age = 25 };
//
//         // Act
//         sut.Evaluate(application);
//
//         // Manually raising an event
//         //mockValidator.Raise(x => x.ValidatorLookupPerformed += null, EventArgs.Empty);
//
//         // Assert
//         Assert.Equal(1, sut.ValidatorLookupCount);
//     }
//
//     [Fact]
//     public void ReferInvalidFrequentFlyerApplications_ReturnValuesSequence()
//     {
//         // Arrange
//         var mockValidator = new Mock<IFrequentFlyerNumberValidator>();
//         mockValidator.Setup(x => x.ServiceInformation.License.LicenseKey).Returns("OK");
//         mockValidator.SetupSequence(x => x.IsValid(It.IsAny<string>()))
//             .Returns(false)
//             .Returns(true);
//
//         var sut = new CreditCardApplicationEvaluator(mockValidator.Object);
//         var application = new CreditCardApplication { Age = 25 };
//
//         // Act
//         var firstDecision = sut.Evaluate(application);
//         var secondDecision = sut.Evaluate(application);
//
//         // Assert
//         Assert.Equal(CreditCardApplicationDecision.ReferredToHuman, firstDecision);
//         Assert.Equal(CreditCardApplicationDecision.AutoDeclined, secondDecision);
//     }
//
//     [Fact]
//     public void ReferInvalidFrequentFlyerApplications_MultipleCallsSequence()
//     {
//         // Arrange
//         var mockValidator = new Mock<IFrequentFlyerNumberValidator>();
//         var frequentFlyerNumbersPassed = new List<string>();
//         mockValidator.Setup(x => x.IsValid(Capture.In(frequentFlyerNumbersPassed)));
//         mockValidator.Setup(x => x.ServiceInformation.License.LicenseKey).Returns("OK");
//         var sut = new CreditCardApplicationEvaluator(mockValidator.Object);
//         var application1 = new CreditCardApplication { Age = 25, FrequentFlyerNumber = "aa" };
//         var application2 = new CreditCardApplication { Age = 25, FrequentFlyerNumber = "bb" };
//         var application3 = new CreditCardApplication { Age = 25, FrequentFlyerNumber = "cc" };
//
//         // Act
//         sut.Evaluate(application1);
//         sut.Evaluate(application2);
//         sut.Evaluate(application3);
//
//         // Assert that IsValid was called three times with "aa", "bb" and "cc"
//         Assert.Equal(new List<string> { "aa", "bb", "cc" }, frequentFlyerNumbersPassed);
//     }
//
//     [Fact]
//     public void ReferFraudRisk()
//     {
//         // Arrange
//         var mockValidator = new Mock<IFrequentFlyerNumberValidator>();
//         var mockFraudLookup = new Mock<FraudLookup>();
//         //mockFraudLookup.Setup(x => x.IsFraudRisk(It.IsAny<CreditCardApplication>())).Returns(true);
//         mockFraudLookup.Protected()
//             .Setup<bool>("CheckApplication", ItExpr.IsAny<CreditCardApplication>())
//             .Returns(true);
//
//         var sut = new CreditCardApplicationEvaluator(mockValidator.Object, mockFraudLookup.Object);
//         var application = new CreditCardApplication();
//
//         // Act
//         var decision = sut.Evaluate(application);
//
//         // Assert
//         Assert.Equal(CreditCardApplicationDecision.ReferredToHumanFraudRisk, decision);
//     }
//
//     [Fact]
//     public void LinqToMocks()
//     {
//         // Arrange
//         //var mockValidator = new Mock<IFrequentFlyerNumberValidator>();
//         //mockValidator.Setup(x => x.ServiceInformation.License.LicenseKey).Returns("OK");
//         //mockValidator.Setup(x => x.IsValid(It.IsAny<string>())).Returns(true);
//
//         var mockValidator = Mock.Of<IFrequentFlyerNumberValidator>(validator =>
//             validator.ServiceInformation.License.LicenseKey == "OK" && validator.IsValid(It.IsAny<string>()));
//
//         var sut = new CreditCardApplicationEvaluator(mockValidator);
//         var application = new CreditCardApplication { Age = 25 };
//
//         // Act
//         var decision = sut.Evaluate(application);
//
//         // Assert
//         Assert.Equal(CreditCardApplicationDecision.AutoDeclined, decision);
//     }
//
//     private string GetLicenseKeyExpiryString()
//     {
//         // E.g. read from vendor-supplied constants file
//         return "EXPIRED";
//     }
// }