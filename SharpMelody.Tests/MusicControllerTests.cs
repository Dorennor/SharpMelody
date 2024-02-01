using SharpMelody.Music.Controllers;
using SharpMelody.Music.Services;

public class MusicControllerTests
{
    [Fact]
    public void GetGreetingsReturnsString()
    {
        MusicController MusicController = new MusicController(new LibraryService());

        var actual = MusicController.GetGreetings();
        var expected = "SharpMelody";

        Assert.Equal(expected, actual);
    }
}