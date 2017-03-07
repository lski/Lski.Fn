using FluentAssertions;
using Xunit;
using System.Threading.Tasks;
using System;

namespace Lski.Fn.Tests
{
    public class EitherTests
    {
        [Fact]
        public void SetLeftValueTest()
        {
            var left = Either.Left<string, int>("a value");

            var result = left.ToValue((v) => v + " plus", (v) => v.ToString());

            result.Should().Be("a value plus");
        }

        [Fact]
        public void SetRightValueTest()
        {
            var right = Either.Right<string, int>(1);

            var result = right.ToValue(v => v + " plus", (v) => v.ToString());

            result.Should().Be("1");
        }

        [Fact]
        public void SetEitherWithSameTypesTest()
        {
            var right = Either.Left<string, string>("a left value");

            var result = right.ToValue(v => v + " plus", v => v.ToString());

            result.Should().Be("a left value plus");
        }

        [Fact]
        public async Task LeftOrRightAsyncTest()
        {
            var either = Either.Left<string, string>("left1");

            var val = await either.ToValue(async (vall) => await Blah("left2"), async (valr) => await Blah("right"));

            val.Should().Be("left2");
        }

        public async Task<string> Blah(string blah) => await Task.FromResult(blah);

        [Fact]
        public void CorrectActionFiredTest()
        {
            string result = null;

            var left = Either.Left<string, string>("left")
                .LeftOrRight((l) => { result = l; }, (r) => { result = r; });

            result.Should().Be("left");

            left.Left(v => { result = v + "2"; }).Right(v => { result = v + "not run"; });

            result.Should().Be("left2");

            var right = Either.Right<string, string>("right")
                .LeftOrRight((l) => { result = l; }, (r) => { result = r; });

            result.Should().Be("right");

            right.Left(l => { result = l + "not run"; }).Right(v => { result = v + "2"; });

            result.Should().Be("right2");
        }

        [Fact]
        public void CorrectFuncFiredTest()
        {
            var left = Either.Left<string, string>("left")
                .LeftOrRight(l => l += 1, r => r += "not run");

            left.ToLeft().Should().Be("left1");

            var left2 = left.Left(v => v += "2").Right(v => v += "not run");

            left2.ToLeft().Should().Be("left12");

            var right = Either.Right<string, string>("right")
                .LeftOrRight(l => l += "not run", r => r += 1);

            right.ToRight().Should().Be("right1");

            var right2 = right.Left(l => l += "not run").Right(v => v + "3");

            right2.ToRight().Should().Be("right13");
        }

        [Fact]
        public void LeftAndRightChainTests()
        {
            var either = Either.Right<string, int>(10)
                .Left(val => "hello world")
                .Right(val => val + 10)
                .Left(val => val.Replace("world", "universe"))
                .Right(val => "wow its " + val);

            var right = either.ToRight();

            right.Should().Be("wow its 20");
        }

        [Fact]
        public void LeftOnlyActionTest()
        {
            var either = Either.Left<string, string>("left");

            var resultOne = either.Left((val) => 10);

            resultOne.Should().Be(10);

            var eitherTwo = Either.Left<string, string>("left");

            var didRun = false;
            var resultTwo = eitherTwo.Right(val =>
            {
                didRun = true;
                return "right";
            });

            resultTwo.ToLeft().Should().Be("left");
            didRun.Should().BeFalse();
        }

        [Fact]
        public void RightOnlyActionTest()
        {
            var either = Either.Right<string, string>("right");

            var resultOne = either.Right((val) => 10);

            resultOne.ToRight().Should().Be(10);

            var eitherTwo = Either.Right<string, string>("right");

            var didRun = false;
            var resultTwo = eitherTwo.Left(val =>
            {
                didRun = true;
                return "left";
            });

            resultTwo.ToRight().Should().Be("right");
            didRun.Should().BeFalse();
        }

        [Fact]
        public void CastTests()
        {
            Either<string, int> either = 1;

            either.ToRight().Should().Be(1);

            Either<string, int> either2 = "either2";

            either2.ToLeft().Should().Be("either2");

            Action a = () => { either.ToLeft(); };

            a.ShouldThrow<InvalidOperationException>();
        }

        public void CompareTests()
        {
            var e1 = Either.Left<string, int>("a");
            var e2 = Either.Left<string, int>("a");
            var e3 = Either.Right<string, int>(1);

            (e1 == e2).Should().BeTrue();
            (e1 == e3).Should().BeFalse();

            var e4 = Either.Right<string, string>("a");
            var e5 = Either.Right<string, string>("a");

            (e4 == e5).Should().BeTrue();
        }
    }
}