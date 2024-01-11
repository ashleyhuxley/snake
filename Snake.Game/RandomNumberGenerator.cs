namespace Snake.Game
{
    public class RandomNumberGenerator : IRandomGenerator
    {
        private readonly Random random = new();

        public int GetRandomNumber(int max)
        {
            return random.Next(max);
        }
    }
}
