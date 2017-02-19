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

            var left = Either.Left<string, int>("a value");

            var result = left.Do((val) => val + " plus", (v) => v.ToString());

            result.Should().Be("a value plus");
        }

        public async Task AsyncTest() {

            var either = Either.Left<string, string>("left");

            var val = await either.Do(async (vall) => await Blah("left"), async (valr) => await Blah("right"));

            val.Should().Be("left");
        }

        public async Task<string> Blah(string blah) {
            return await Task.FromResult("");
        }

        [Fact]
        public void SetRightValueTest()
        {

            var right = Either.Right<string, int>(1);

            var result = right.Do((val) => val + " plus", (v) => v.ToString());

            result.Should().Be("1");
        }

        [Fact]
        public void SetSameTypesTest()
        {
            var right = Either.Left<string, string>("a left value");

            var result = right.Do((val) => val + " plus", (v) => v.ToString());

            result.Should().Be("a left value plus");
        }

        [Fact]
        public async Task DoAsyncTest()
        {
            var right = Either.Left<string, string>("a left value");

            var result = await right.Do(async (val) => await Task.FromResult(val + " plus"), async (v) => await Task.FromResult(v.ToString()));

            result.Should().Be("a left value plus");
        }

        [Fact]
        public void ActionFiredTest()
        {

            var left = Either.Left<string, string>("left");

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

        [Fact]
        public void LeftOnlyActionTest()
        {
            var either = Either.Left<string, string>("left");

            var resultOne = either.Left((val) => 10);

            resultOne.Should().Be(10);

            var eitherTwo = Either.Left<string, string>("left");

            var resultTwo = eitherTwo.Right(val => 100);

            resultTwo.Should().Be(0);
        }

        [Fact]
        public void RightOnlyActionTest()
        {
            var either = Either.Right<string, string>("right");

            var resultOne = either.Right((val) => 10);

            resultOne.Should().Be(10);

            var eitherTwo = Either.Right<string, string>("right");

            var resultTwo = eitherTwo.Left(val => 100);

            resultTwo.Should().Be(0);
        }
    }
}