namespace OpenGLGame
{
    internal class Starter
    {
        static void Main()
        {
            WindowedGame game = new(560, 500);
            game.Run();
        }
    }
}
