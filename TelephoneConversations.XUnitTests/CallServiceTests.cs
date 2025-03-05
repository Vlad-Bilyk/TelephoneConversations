using Moq;
using System.Reflection;
using TelephoneConversations.Core.Interfaces.IRepository;
using TelephoneConversations.Core.Models;
using TelephoneConversations.Core.Services;

namespace TelephoneConversations.Tests
{
    public class CallServiceTests
    {
        [Theory]
        [InlineData(120, 1, 2)]
        [InlineData(59, 1, 0.98)]
        [InlineData(180, 0.5, 1.5)]
        [InlineData(134, 2, 4.46)]
        public void CalculateBaseCost_ShouldReturnExpectedResult(int duration, decimal tariffRate, decimal expected)
        {
            var call = new Call { Duration = duration };
            var method = GetCalculateBaseCostMethod();

            var result = (decimal)method.Invoke(null, new object[] { call, tariffRate });

            Assert.Equal(expected, result);
        }

        private static MethodInfo GetCalculateBaseCostMethod()
        {
            var method = typeof(CallService)
                .GetMethod("CalculateBaseCost", BindingFlags.Static | BindingFlags.NonPublic);
            Assert.NotNull(method);
            return method;
        }
    }
}
