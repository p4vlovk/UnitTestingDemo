namespace GameEngine.Tests
{
    using System;
    using Xunit;
    using Xunit.Abstractions;

    /// <summary>
    /// Asserts evaluate and verify the outcome of a test, based on a returned result, final object state,
    /// or the occurrence of events observed during execution. Asserts can either pass of fail. If all asserts pass,
    /// the test passes; if any assert fails the test fails.
    /// </summary>
    public class PlayerCharacterShould : IDisposable
    {
        private readonly PlayerCharacter sut;
        private readonly ITestOutputHelper output;

        public PlayerCharacterShould(ITestOutputHelper output)
        {
            this.output = output;
            this.output.WriteLine("Creating new PlayerCharacter");
            this.sut = new PlayerCharacter("Sarah", "Smith");
        }

        #region Assert against boolean values
        [Fact]
        public void BeInexperiencedWhenNew() => Assert.True(this.sut.IsNoob);
        #endregion

        #region Assert against string values
        [Fact]
        public void CalculateFullName() => Assert.Equal("Sarah Smith", this.sut.FullName);

        [Fact]
        public void HaveFullNameStartingWithFirstName() => Assert.StartsWith("Sarah", this.sut.FullName);

        [Fact]
        public void HaveFullNameEndingWithLastName() => Assert.EndsWith("Smith", this.sut.FullName);

        [Fact]
        public void CalculateFullName_IgnoreCaseAssertExample() => Assert.Equal("Sarah Smith", new PlayerCharacter("SARAH", "SMITH").FullName, ignoreCase: true);

        // [Fact(Skip = "Don't need to run this")]
        [Fact]
        public void CalculateFullName_SubstringAssertExample() => Assert.Contains("ah Sm", this.sut.FullName);

        [Fact]
        public void CalculateFullNameWithTitleCase() => Assert.Matches("[A-Z]{1}[a-z]+ [A-Z]{1}[a-z]+", this.sut.FullName);
        #endregion

        #region Assert against numeric values
        [Fact]
        public void StartWithDefaultHealth() => Assert.Equal(100, this.sut.Health);

        [Fact]
        public void StartWithDefaultHealth_NotEqualExample() => Assert.NotEqual(0, this.sut.Health);

        [Fact]
        public void IncreaseHealthAfterSleeping()
        {
            this.sut.Sleep(); // Expect increase between 1 to 100 inclusive

            // Assert.True(this.sut.Health is >= 101 and <= 200); // This assert also works but if it fails, the error message doesn't give the specific reason.
            Assert.InRange(this.sut.Health, 101, 200);
        }
        #endregion

        #region Assert against null
        [Fact]
        public void NotHaveNicknameByDefault() => Assert.Null(this.sut.Nickname);
        #endregion

        #region Assert against collections
        [Fact]
        public void HaveALongBow() => Assert.Contains("Long Bow", this.sut.Weapons);

        [Fact]
        public void NotHaveAStaffOfWonder() => Assert.DoesNotContain("Staff Of Wonder", this.sut.Weapons);

        [Fact]
        public void HaveAtLeastOneKindOfSword() => Assert.Contains(this.sut.Weapons, weapon => weapon.Contains("Sword"));

        [Fact]
        public void HaveAllExpectedWeapons()
        {
            var expectedWeapons = new[]
            {
                "Long Bow",
                "Short Bow",
                "Short Sword"
            };

            Assert.Equal(expectedWeapons, this.sut.Weapons);
        }

        [Fact]
        public void HaveNoEmptyDefaultWeapons() => Assert.All(this.sut.Weapons, weapon => Assert.False(string.IsNullOrWhiteSpace(weapon)));
        #endregion

        [Fact]
        public void RaiseSleptEvent() => Assert.Raises<EventArgs>(
            handler => this.sut.PlayerSlept += handler,
            handler => this.sut.PlayerSlept -= handler,
            () => this.sut.Sleep());

        [Fact]
        public void RaisePropertyChangedEvent() => Assert.PropertyChanged(this.sut, nameof(PlayerCharacter.Health), () => this.sut.TakeDamage(10));

        [Theory]
        [HealthDamageData]
        // [MemberData(nameof(ExternalHealthDamageTestData.TestData), MemberType = typeof(ExternalHealthDamageTestData))]
        // [MemberData(nameof(InternalHealthDamageTestData.TestData), MemberType = typeof(InternalHealthDamageTestData))]
        // [InlineData(0, 100)]
        // [InlineData(1, 99)]
        // [InlineData(50, 50)]
        // [InlineData(101, 1)]
        public void TakeDamage(int damage, int expectedHealth)
        {
            this.sut.TakeDamage(damage);

            Assert.Equal(expectedHealth, this.sut.Health);
        }

        public void Dispose()
        {
            this.output.WriteLine($"Disposing PlayerCharacter {this.sut.FullName}");
            // this.sut.Dispose();
        }
    }
}
