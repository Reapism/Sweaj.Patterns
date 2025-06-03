using Stats = Sweaj.Patterns.Math.MathExt.Statistics;

namespace Sweaj.Patterns.Tests.Math.Statistics;

public class FrequentCharacterTests
{
    [Theory]
    [InlineData("a", 'a')]
    [InlineData("banana", 'a')]
    [InlineData("abcabcabc", 'a')]
    [InlineData("mississippi", 'i')]
    [InlineData("aabbccddeeff", 'a')]
    [InlineData("zxyzzx", 'z')]
    [InlineData("1233211", '1')]
    public void MostFrequentCharacter_ValidInputs_ReturnsExpected(string input, char expected)
    {
        // Act
        char result = Stats.MostFrequentCharacter(input);

        // Assert
        Assert.Equal(expected, result);
    }

    [Fact]
    public void MostFrequentCharacter_NullInput_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() => Stats.MostFrequentCharacter(null));
    }

    [Fact]
    public void MostFrequentCharacter_EmptyInput_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(() => Stats.MostFrequentCharacter(""));
    }


    [Theory]
    [InlineData("a", 'a')]
    [InlineData("banana", 'b')]
    [InlineData("abcabcabc", 'a')]
    [InlineData("mississippi", 'm')]
    [InlineData("aabbccddeeffg", 'g')]
    [InlineData("zxyzzx", 'y')]
    [InlineData("1233211", '2')]
    public void LeastFrequentCharacter_ValidInputs_ReturnsExpected(string input, char expected)
    {
        // Act
        char result = Stats.LeastFrequentCharacter(input);

        // Assert
        Assert.Equal(expected, result);
    }

    [Fact]
    public void LeastFrequentCharacter_NullInput_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() => Stats.LeastFrequentCharacter(null));
    }

    [Fact]
    public void LeastFrequentCharacter_EmptyInput_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(() => Stats.LeastFrequentCharacter(""));
    }
}
