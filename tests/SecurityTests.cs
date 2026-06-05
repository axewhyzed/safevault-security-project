using Microsoft.VisualStudio.TestTools.UnitTesting;
using SafeVault;

namespace SafeVault.Tests
{
    [TestClass]
    public class SecurityTests
    {
        private readonly SecureInputValidation _validation =
            new SecureInputValidation();

        private readonly AuthenticationService _authentication =
            new AuthenticationService();

        private readonly AuthorizationService _authorization =
            new AuthorizationService();

        // INPUT VALIDATION TESTS

        [TestMethod]
        public void ValidUsername_ShouldPassValidation()
        {
            bool result = _validation.IsValidUsername("john_doe");

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void InvalidUsername_ShouldFailValidation()
        {
            bool result = _validation.IsValidUsername("john@doe!");

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void ValidEmail_ShouldPassValidation()
        {
            bool result = _validation.IsValidEmail("john@example.com");

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void InvalidEmail_ShouldFailValidation()
        {
            bool result = _validation.IsValidEmail("invalid-email");

            Assert.IsFalse(result);
        }

        // SQL INJECTION PREVENTION TEST

        [TestMethod]
        public void SqlInjectionInput_ShouldNotPassUsernameValidation()
        {
            string maliciousInput =
                "'; DROP TABLE Users; --";

            bool result =
                _validation.IsValidUsername(maliciousInput);

            Assert.IsFalse(result);
        }

        // AUTHENTICATION TESTS

        [TestMethod]
        public void CorrectCredentials_ShouldLoginSuccessfully()
        {
            bool result =
                _authentication.Login(
                    "admin",
                    "SecurePassword123");

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IncorrectCredentials_ShouldFailLogin()
        {
            bool result =
                _authentication.Login(
                    "admin",
                    "WrongPassword");

            Assert.IsFalse(result);
        }

        // AUTHORIZATION TESTS

        [TestMethod]
        public void Admin_ShouldBeAbleToEditVault()
        {
            bool result =
                _authorization.CanEditVault(
                    UserRole.Admin);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void User_ShouldNotBeAbleToEditVault()
        {
            bool result =
                _authorization.CanEditVault(
                    UserRole.User);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Admin_ShouldBeAbleToDeleteVault()
        {
            bool result =
                _authorization.CanDeleteVault(
                    UserRole.Admin);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void User_ShouldNotBeAbleToDeleteVault()
        {
            bool result =
                _authorization.CanDeleteVault(
                    UserRole.User);

            Assert.IsFalse(result);
        }
    }
}