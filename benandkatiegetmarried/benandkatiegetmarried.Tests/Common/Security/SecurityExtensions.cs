using System;
using Xunit;
using benandkatiegetmarried.Common.Security;

namespace benandkatiegetmarriedTests.Common.Security
{
    public class SecurityExtensionsTests
    {
		[Fact]
        public void PasswordHashesandVerifysCorrectly()
        {
			var text = "pooface";

            var hash = SecurityExtensions.EncryptPassword(text);

            Assert.True(hash.CheckPassword(text));
        }

    }
}
