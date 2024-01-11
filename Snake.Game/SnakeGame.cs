using System.Drawing;

namespace Snake.Game
{
    public class SnakeGame
    {
        private const int FoodValue = 10;

        public const int MinimumGameSize = 5;

        private readonly List<Point> snake = new();

        private readonly IRandomGenerator random;

        public delegate void UpdateRequired();

        public delegate void EatFood();

        public delegate void Collision();

        public event UpdateRequired? OnUpdateRequired;

        public event EatFood? OnEatFood;

        public event Collision? OnCollision;

        private Point food;

        public int Score { get; private set; } = 0;

        public SnakeGame(IRandomGenerator randomGenerator)
        {
            this.random = randomGenerator;
        }

        public GameItem this[int x, int y]
        {
            get
            {
                if ((food.X == x) && (food.Y == y))
                {
                    return GameItem.Food;
                } else if (snake.Contains(new Point(x, y)))
                {
                    return GameItem.Body;
                }

                return GameItem.None;
            }
        }

        public Direction Direction { get; set; } = Direction.Up;

        public int BoardWidth { get; private set; }
        public int BoardHeight { get; private set; }

        public void Tick()
        {
            if (snake.Count == 0)
            {
                return;
            }

            var head = this.snake.First();

            Point nextPoint;

            switch (Direction)
            {
                case Direction.Up:
                    nextPoint = new Point(head.X, head.Y - 1);
                    break;
                case Direction.Down:
                    nextPoint = new Point(head.X, head.Y + 1);
                    break;
                case Direction.Left:
                    nextPoint = new Point(head.X - 1, head.Y);
                    break;
                case Direction.Right:
                    nextPoint = new Point(head.X + 1, head.Y);
                    break;
                default: throw new InvalidOperationException("Invalid direction");
            }

            if (nextPoint.X < 0 ||  
                nextPoint.Y < 0 || 
                nextPoint.X >= this.BoardWidth ||
                nextPoint.Y >= this.BoardHeight ||
                this[nextPoint.X, nextPoint.Y] == GameItem.Body)
            {
                this.OnCollision?.Invoke();
                return;
            }

            snake.Insert(0, nextPoint);

            if (this[nextPoint.X, nextPoint.Y] == GameItem.Food)
            {
                food = RandomPoint();
                this.OnEatFood?.Invoke();
                Score += FoodValue;
            }
            else
            {
                snake.RemoveAt(snake.Count - 1);
            }

            this.OnUpdateRequired?.Invoke();
        }

        public void InitializeGame(int width, int height)
        {
            if (width < MinimumGameSize || height < MinimumGameSize)
            {
                throw new ArgumentException("The board size is too small. (5x5 minimum)");
            }

            this.BoardWidth = width;
            this.BoardHeight = height;

            Direction = Direction.Up;
            Score = 0;

            // Position the head in the middle
            var snakeHead = new Point(this.BoardWidth / 2, this.BoardHeight / 2);

            // Make the snake (Starts off as length 2)
            this.snake.Clear();
            this.snake.Add(snakeHead);
            this.snake.Add(new Point(snakeHead.X, snakeHead.Y + 1));

            food = RandomPoint();

            this.OnUpdateRequired?.Invoke();
        }

        private Point RandomPoint()
        {
            List<Point> emptyPoints = new();
            for (int x = 0; x < this.BoardWidth; x++)
            {
                for (int y = 0; y < this.BoardHeight; y++)
                {
                    if (IsEmptySpace(x, y))
                    {
                        emptyPoints.Add(new Point(x, y));
                    }
                }
            }

            int index = this.random.GetRandomNumber(emptyPoints.Count - 1);
            return emptyPoints[index];
        }

        private bool IsEmptySpace(int x, int y)
        {
            return this[x, y] == GameItem.None;
        }
    }
}