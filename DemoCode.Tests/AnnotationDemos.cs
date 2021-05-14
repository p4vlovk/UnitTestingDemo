namespace DemoCode.Tests
{
    using AutoFixture;
    using Xunit;

    public class AnnotationDemos
    {
        [Fact]
        public void BasicStrings()
        {
            // Arrange
            var fixture = new Fixture();
            var player = fixture.Create<PlayerCharacter>();

            // Act
            // Assert
        }
    }
}
