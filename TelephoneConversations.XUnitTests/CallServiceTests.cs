using System.Reflection;
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
        [InlineData(134, 2, 4.47)]
        public void CalculateBaseCost_ShouldReturnExpectedResult(int duration, decimal tariffRate, decimal expected)
        {
            var call = new Call { Duration = duration };
            var method = GetCalculateBaseCostMethod();

            var result = method.Invoke(null, new object[] { call, tariffRate });

            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(100, 10, 90)]    // 10% discount on 100 = 90
        [InlineData(200, 25, 150)]   // 25% discount on 200 = 150
        [InlineData(50, 5, 47.5)]    // 5% discount on 50 = 47.5
        [InlineData(80, 0, 80)]      // 0% discount should return the same value
        [InlineData(99.99, 15, 84.99)] // Rounded result
        public void CalculateCostWithDiscount_ShouldReturnExpectedResult(decimal baseCost, decimal discountRate, decimal expected)
        {
            var method = GetCalculateCostWithDiscountMethod();

            var result = method.Invoke(null, new object[] { baseCost, discountRate });

            Assert.Equal(expected, result);
        }

        private static MethodInfo GetCalculateBaseCostMethod()
        {
            var method = typeof(CallService)
                .GetMethod("CalculateBaseCost", BindingFlags.Static | BindingFlags.NonPublic);
            Assert.NotNull(method);
            return method;
        }

        private static MethodInfo GetCalculateCostWithDiscountMethod()
        {
            var method = typeof(CallService)
                .GetMethod("CalculateCostWithDiscount", BindingFlags.Static | BindingFlags.NonPublic);
            Assert.NotNull(method);
            return method;
        }
    }
}
