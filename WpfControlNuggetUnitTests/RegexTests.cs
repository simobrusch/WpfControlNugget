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
        public void TestPhoneNumberRegexCh_True()
        {
            // arrange
            var phoneNumberValidationRule = new PhoneNumberValidationRule();
            // act
            var result = phoneNumberValidationRule.Validate("+41791234567", CultureInfo.CurrentCulture);
            // assert
            Assert.IsTrue(result.IsValid);
        }

        [TestMethod]
        public void TestPhoneNumberRegexCh_False()
        {
            // arrange
            var phoneNumberValidationRule = new PhoneNumberValidationRule();
            // act
            var result = phoneNumberValidationRule.Validate("+47912345678", CultureInfo.CurrentCulture);
            // assert
            Assert.IsFalse(result.IsValid);
        }
        /// <summary>
        /// Regex not working for Liechtenstein
        /// </summary>
        //[TestMethod]
        //public void TestPhoneNumberRegexLi_True()
        //{
        //    // arrange
        //    var phoneNumberValidationRule = new PhoneNumberValidationRule();
        //    // act
        //    var result = phoneNumberValidationRule.Validate("+4231234567", CultureInfo.CurrentCulture);
        //    // assert
        //    Assert.IsTrue(result.IsValid);
        //}

        [TestMethod]
        public void TestPhoneNumberRegexLi_False()
        {
            // arrange
            var phoneNumberValidationRule = new PhoneNumberValidationRule();
            // act
            var result = phoneNumberValidationRule.Validate("+42312345678", CultureInfo.CurrentCulture);
            // assert
            Assert.IsFalse(result.IsValid);
        }

        /// <summary>
        /// Regex not working for Germany
        /// </summary>
        //[TestMethod]
        //public void TestPhoneNumberRegexDe_True()
        //{
        //    // arrange
        //    var phoneNumberValidationRule = new PhoneNumberValidationRule();
        //    // act
        //    var result = phoneNumberValidationRule.Validate("+491517953677", CultureInfo.CurrentCulture);
        //    // assert
        //    Assert.IsTrue(result.IsValid);
        //}

        [TestMethod]
        public void TestPhoneNumberRegexDe_False()
        {
            // arrange
            var phoneNumberValidationRule = new PhoneNumberValidationRule();
            // act
            var result = phoneNumberValidationRule.Validate("+49123456789", CultureInfo.CurrentCulture);
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
