using System;
using System.Globalization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WpfControlNugget.Validators;

namespace WpfControlNuggetUnitTests
{
    [TestClass]
    public class RegexTests
    {
        [TestMethod]
        public void TestAddressNumberRegex()
        {
            // arrange
            var addressNumberValidationRule = new AddressNumberValidationRule();
            // act
            var result = addressNumberValidationRule.Validate("CU12345", CultureInfo.CurrentCulture);
            // assert
            Assert.IsTrue(result.IsValid);
        }

        [TestMethod]
        public void TestEmailRegex()
        {
            // arrange
            var emailValidationRule = new EmailValidationRule();
            // act
            var result = emailValidationRule.Validate("test.test@test.com", CultureInfo.CurrentCulture);
            // assert
            Assert.IsTrue(result.IsValid);
        }

        [TestMethod]
        public void TestPasswordRegex()
        {
            // arrange
            var passwordValidationRule = new PasswordValidationRule();
            // act
            var result = passwordValidationRule.Validate("Test1234?", CultureInfo.CurrentCulture);
            // assert
            Assert.IsTrue(result.IsValid);
        }

        [TestMethod]
        public void TestPhoneNumberRegex()
        {
            // arrange
            var phoneNumberValidationRule= new PhoneNumberValidationRule();
            // act
            var result = phoneNumberValidationRule.Validate("+41790000000", CultureInfo.CurrentCulture);
            // assert
            Assert.IsTrue(result.IsValid);
        }

        [TestMethod]
        public void TestUrlRegex()
        {
            // arrange
            var urlValidationRule = new UrlValidationRule();
            // act
            var result = urlValidationRule.Validate("www.google.com", CultureInfo.CurrentCulture);
            // assert
            Assert.IsTrue(result.IsValid);
        }
    }
}
