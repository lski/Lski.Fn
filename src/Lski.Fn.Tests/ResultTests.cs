using FluentAssertions;
using System;
using Xunit;

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

            result.OnFailure((err) => {

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
    }
}