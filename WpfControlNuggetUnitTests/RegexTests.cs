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
        public void TestAddressNumberRegex_True()
        {
            // arrange
            var addressNumberValidationRule = new AddressNumberValidationRule();
            // act
            var result = addressNumberValidationRule.Validate("CU12345", CultureInfo.CurrentCulture);
            // assert
            Assert.IsTrue(result.IsValid);
        }
        [TestMethod]
        public void TestAddressNumberRegex_False()
        {
            // arrange
            var addressNumberValidationRule = new AddressNumberValidationRule();
            // act
            var result = addressNumberValidationRule.Validate("CU123456", CultureInfo.CurrentCulture);
            // assert
            Assert.IsFalse(result.IsValid);
        }

        [TestMethod]
        public void TestEmailRegex_True()
        {
            // arrange
            var emailValidationRule = new EmailValidationRule();
            // act
            var result = emailValidationRule.Validate("test.test@test.com", CultureInfo.CurrentCulture);
            // assert
            Assert.IsTrue(result.IsValid);
        }
        [TestMethod]
        public void TestEmailRegex_False()
        {
            // arrange
            var emailValidationRule = new EmailValidationRule();
            // act
            var result = emailValidationRule.Validate("test.test@.com", CultureInfo.CurrentCulture);
            // assert
            Assert.IsFalse(result.IsValid);
        }

        [TestMethod]
        public void TestIntRangeValidation_True()
        {
            // arrange
            var intRangeValidationRule = new IntRangeValidationRule();
            // act
            var result = intRangeValidationRule.Validate("8", CultureInfo.CurrentCulture);
            // assert
            Assert.IsTrue(result.IsValid);
        }

        [TestMethod]
        public void TestIntRangeValidation_False()
        {
            // arrange
            var intRangeValidationRule = new IntRangeValidationRule();
            // act
            var result = intRangeValidationRule.Validate("100", CultureInfo.CurrentCulture);
            // assert
            Assert.IsFalse(result.IsValid);
        }

        [TestMethod]
        public void TestPasswordRegex_True()
        {
            // arrange
            var passwordValidationRule = new PasswordValidationRule();
            // act
            var result = passwordValidationRule.Validate("Test1234?", CultureInfo.CurrentCulture);
            // assert
            Assert.IsTrue(result.IsValid);
        }

        [TestMethod]
        public void TestPasswordRegex_False()
        {
            // arrange
            var passwordValidationRule = new PasswordValidationRule();
            // act
            var result = passwordValidationRule.Validate("Test12", CultureInfo.CurrentCulture);
            // assert
            Assert.IsFalse(result.IsValid);
        }

        [TestMethod]
        public void TestPhoneNumberRegex_True()
        {
            // arrange
            var phoneNumberValidationRule = new PhoneNumberValidationRule();
            // act
            var result = phoneNumberValidationRule.Validate("+41790000000", CultureInfo.CurrentCulture);
            // assert
            Assert.IsTrue(result.IsValid);
        }

        [TestMethod]
        public void TestPhoneNumberRegex_False()
        {
            // arrange
            var phoneNumberValidationRule = new PhoneNumberValidationRule();
            // act
            var result = phoneNumberValidationRule.Validate("+417900000000", CultureInfo.CurrentCulture);
            // assert
            Assert.IsFalse(result.IsValid);
        }

        [TestMethod]
        public void TestStringRangeValidation_True()
        {
            // arrange
            var stringRangeValidationRule = new StringRangeValidationRule();
            // act
            var result = stringRangeValidationRule.Validate("Test", CultureInfo.CurrentCulture);
            // assert
            Assert.IsTrue(result.IsValid);
        }

        [TestMethod]
        public void TestStringRangeValidation_False()
        {
            // arrange
            var stringRangeValidationRule = new StringRangeValidationRule();
            // act
            var result = stringRangeValidationRule.Validate("", CultureInfo.CurrentCulture);
            // assert
            Assert.IsFalse(result.IsValid);
        }

        [TestMethod]
        public void TestUrlRegex_True()
        {
            // arrange
            var urlValidationRule = new UrlValidationRule();
            // act
            var result = urlValidationRule.Validate("www.google.com", CultureInfo.CurrentCulture);
            // assert
            Assert.IsTrue(result.IsValid);
        }

        [TestMethod]
        public void TestUrlRegex_False()
        {
            // arrange
            var urlValidationRule = new UrlValidationRule();
            // act
            var result = urlValidationRule.Validate("google", CultureInfo.CurrentCulture);
            // assert
            Assert.IsFalse(result.IsValid);
        }
    }
}
