using FluentAssertions;
using System;
using Xunit;
using System.Threading.Tasks;

namespace Lski.Fn.Tests
{
    public class ResultTests
    {
        [Fact]
        public void SetValueIsCorrect()
        {

            var result = Result.Ok("success");

            result.Value.Should().Be("success");

            result = "success2".ToResult();

            result.Value.Should().Be("success2");

            Action action = () => { result.Error.ToString(); };

            action.ShouldThrow<InvalidOperationException>();
        }

        [Fact]
        public void ErrorIsCorrect()
        {
            var result = Result.Fail("error");

            result.Error.ToString().Should().Be("error");

            var result2 = Result.Fail<int>("error");

            Action action = () => { result2.Value.ToString(); };

            action.ShouldThrow<InvalidOperationException>();
        }

        [Fact]
        public void OnSuccessIsValid()
        {
            var result = Result.Ok("success");
            var run = false;

            result.OnSuccess((val) => { run = true; });

            run.Should().BeTrue();

            var result2 = result.OnSuccess((val) => true.ToResult());

            result2.IsSuccess.Should().BeTrue();
            result2.Value.Should().BeTrue();

            result = result.OnSuccess((val) => "second success".ToResult());

            result.IsSuccess.Should().BeTrue();
            result.Value.Should().Be("second success");

            result = result.OnSuccess((val) => "third success");

            result.IsSuccess.Should().BeTrue();
            result.Value.Should().Be("third success");

            run = false;

            result.OnFailure(() => { run = true; });

            run.Should().BeFalse();
        }

        [Fact]
        public void OnFailureIsValid()
        {
            var result = Result.Fail<string>("error");
            var run = false;

            result.OnFailure((err) =>
            {

                err.Should().Be("error");
                run = true;
            });

            run.Should().BeTrue();

            run = false;

            var result2 = result.OnFailure((err) => Result.Ok<string>("success"));

            result2.Should().BeAssignableTo(typeof(Result<string>));

            run = false;

            result.OnSuccess(() => { run = true; });

            run.Should().BeFalse();
        }

        [Fact]
        public void OnSuccessChain()
        {
            var result = "success".ToResult()
                .OnSuccess((val) => val + 1)
                .OnSuccess(val => val + 2)
                .OnSuccess(val => val + 3);

            result.IsSuccess.Should().BeTrue();
            result.Value.Should().Be("success123");

            result = result.OnSuccess(val => 1)
                .OnSuccess(val => val + 1)
                .OnSuccess(val => val + "");

            result.IsSuccess.Should().BeTrue();
            result.Value.Should().Be("2");

            Action a = async () =>
            {
                var taskResult = await Task.FromResult("task".ToResult())
                .OnSuccess(val => val + " ")
                .OnSuccess(val => val + "is great");

                taskResult.IsSuccess.Should().BeTrue();
                taskResult.Value.Should().Be("task is great");
            };
        }

        public void OnFailureAsyncTests()
        {
            var result = Result.Fail<string>("error")
                .OnFailure(val => Result.Ok("we are ok"))
                .OnFailure(val => val.ToString())
                .OnSuccess(val => "we should not hit here at all")
                .OnFailure(val => Result.Fail(val + " no we arent"));

            result.IsFailure.Should().BeTrue();
            result.Value.Should().Be("we are ok no we arent");

            Action a = () =>
            {
                var taskResult = Task.FromResult(Result.Fail<string>("error"))
                    .OnFailure(val => Result.Fail(val + " boo"));

                result.IsFailure.Should().BeTrue();
                result.Value.Should().Be("error boo");
            };
        }

        [Fact]
        public void CompareTest()
        {
            var result = Result.Ok("a");
            var result2 = Result.Ok("a");
            var result3 = Result.Ok("b");

            (result == result2).Should().BeTrue();
            (result == result3).Should().BeFalse();

            var result4 = Result.Fail<string>("a");

            (result == result4).Should().BeFalse();
        }

        [Fact]
        public void CastTest()
        {
            var result = CastTestHelper();

            result.IsSuccess.Should().BeTrue();
        }

        public Result<string> CastTestHelper()
        {
            return "";
        }

        public Result<Either<string, int>> CastTestHelper2()
        {
            return (Either<string, int>)"";
        }
    }
}