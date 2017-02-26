using FluentAssertions;
using System;
using Xunit;
using System.Threading.Tasks;

namespace Lski.Fn.Tests
{
    public class MaybeTests
    {
        [Fact]
        public void MaybeSetValueTests()
        {
            var stringValue = Maybe.Create("aString");
            var intValue = Maybe.Create(1);
            var stringValue2 = "aString".ToMaybe();
            var intValue2 = 1.ToMaybe();

            string aNull = null;
            var nullValue = aNull.ToMaybe();
            var nullValue2 = Maybe.Create<string>(null);

            // Following doesnt compile
            // var intValue3 = Maybe.Create<int>(null);

            stringValue.HasValue.Should().BeTrue();
            stringValue.Value.Should().Be("aString");

            intValue.HasValue.Should().BeTrue();
            intValue.Value.Should().Be(1);

            stringValue2.HasValue.Should().BeTrue();
            stringValue2.Value.Should().Be("aString");

            intValue2.HasValue.Should().BeTrue();
            intValue2.Value.Should().Be(1);

            nullValue.HasNoValue.Should().BeTrue();
            nullValue2.HasNoValue.Should().BeTrue();

            Action action = () =>
            {
                nullValue.Value.Should().NotBeNull();
            };

            action.ShouldThrow<InvalidOperationException>();
        }

        [Fact]
        public async Task AsyncTests()
        {
            var maybe1 = await AsyncTestsHelper("a value").Bind(val => "changed".ToMaybe());

            maybe1.Value.Should().Be("changed");

            var maybe2 = await "start".ToMaybe().Bind(AsyncTestsHelper);

            maybe2.Value.Should().Be("start");
        }

        public async Task<Maybe<string>> AsyncTestsHelper(string val)
        {
            return await Task.FromResult(val.ToMaybe());
        }
    }
}