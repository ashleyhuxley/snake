using Moq;
using Snake.Game;

namespace Snake.UnitTests
{
    public class SnakeGameTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Initialize_GameTooSmall_ThrowsArgumentException()
        {
            var random = Mock.Of<IRandomGenerator>();
            var game = new SnakeGame(random);

            Assert.Multiple(() =>
            {
                Assert.Throws<ArgumentException>(() => { game.InitializeGame(SnakeGame.MinimumGameSize - 1, SnakeGame.MinimumGameSize); });
                Assert.Throws<ArgumentException>(() => { game.InitializeGame(SnakeGame.MinimumGameSize, SnakeGame.MinimumGameSize - 1); });
            });
        }

        [Test]
        public void Constructor_ValidParameters_InitializesGame()
        {
            var random = Mock.Of<IRandomGenerator>();
            var game = new SnakeGame(random);

            game.InitializeGame(SnakeGame.MinimumGameSize, SnakeGame.MinimumGameSize);

            var allItems = FlattenBoard(game);

            Assert.Multiple(() =>
            {
                Assert.That(game.Direction, Is.EqualTo(Direction.Up));
                Assert.That(game.Score, Is.EqualTo(0));
                Assert.That(allItems.Count(g => g == GameItem.Food), Is.EqualTo(1));
                Assert.That(allItems.Count(g => g == GameItem.Head), Is.EqualTo(1));
                Assert.That(allItems.Count(g => g == GameItem.Body), Is.EqualTo(1));
            });
        }

        [Test]
        public void Tick_NoDirectionChange_MovesSnakeUp()
        {
            var random = Mock.Of<IRandomGenerator>();
            var game = new SnakeGame(random);

            game.InitializeGame(SnakeGame.MinimumGameSize, SnakeGame.MinimumGameSize);

            game.Tick();

            Assert.Multiple(() =>
            {
                Assert.That(game[SnakeGame.MinimumGameSize / 2, (SnakeGame.MinimumGameSize / 2) - 1], Is.EqualTo(GameItem.Head));
                Assert.That(game[SnakeGame.MinimumGameSize / 2, (SnakeGame.MinimumGameSize / 2)], Is.EqualTo(GameItem.Body));
                Assert.That(game[SnakeGame.MinimumGameSize / 2, (SnakeGame.MinimumGameSize / 2) + 1], Is.EqualTo(GameItem.None));
            });
        }

        private static IEnumerable<GameItem> FlattenBoard(SnakeGame game)
        {
            for (int x = 0; x < game.BoardWidth; x++)
            {
                for (int y = 0; y < game.BoardHeight; y++)
                {
                    yield return game[x, y];
                }
            }
        }
    }
}