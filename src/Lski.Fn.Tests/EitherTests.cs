using System;
using FluentAssertions;
using Xunit;
using System.Threading.Tasks;

namespace Lski.Fn.Tests
{
    public class EitherTests
    {
        [Fact]
        public void SetLeftValueTest()
        {

            var left = Either.First<string, int>("a value");

            var result = left.Do((val) => val + " plus", (v) => v.ToString());

            result.Should().Be("a value plus");
        }

        [Fact]
        public void SetRightValueTest()
        {

            var right = Either.Second<string, int>(1);

            var result = right.Do((val) => val + " plus", (v) => v.ToString());

            result.Should().Be("1");
        }

        [Fact]
        public void SetSameTypesTest()
        {
            var right = Either.First<string, string>("a left value");

            var result = right.Do((val) => val + " plus", (v) => v.ToString());

            result.Should().Be("a left value plus");
        }

        [Fact]
        public async Task DoAsyncTest()
        {
            var right = Either.First<string, string>("a left value");

            var result = await right.Do(async (val) => await Task.FromResult(val + " plus"), async (v) => await Task.FromResult(v.ToString()));

            result.Should().Be("a left value plus");
        }

        [Fact]
        public void ActionFiredTest()
        {

            var left = Either.First<string, string>("left");

            string result = null;

            left.Do((l) =>
            {
                result = l;
            }, (r) =>
            {
                result = r;
            });

            result.Should().Be("left");
        }
    }
}