using Snake.Game;

namespace Snake.Ui
{
    public partial class MainForm : Form
    {
        private const int GameSize = 20;

        private SnakeGame game = new(new RandomNumberGenerator());

        public MainForm()
        {
            InitializeComponent();
            game.InitializeGame(GameSize, GameSize);
        }

        private void mainImage_Paint(object sender, PaintEventArgs e)
        {
            int pointSizeX = this.mainImage.Width / this.game.BoardWidth;
            int pointSizeY = this.mainImage.Height / this.game.BoardHeight;

            e.Graphics.Clear(Color.White);

            for (int x = 0; x < game.BoardWidth; x++)
            {
                for (int y = 0; y < game.BoardHeight; y++)
                {
                    var from = new Point(x * pointSizeX, y * pointSizeY);

                    var brush = ToBrush(game[x, y]);
                    e.Graphics.FillRectangle(brush, from.X, from.Y, pointSizeX - 1, pointSizeY - 1);
                }
            }
        }

        public static Brush ToBrush(GameItem item) => item switch
        {
            GameItem.Food => new SolidBrush(Color.Red),
            GameItem.Head => new SolidBrush(Color.Black),
            GameItem.Body => new SolidBrush(Color.DarkGray),
            GameItem.None => new SolidBrush(Color.White),
            _ => throw new ArgumentOutOfRangeException(nameof(item), $"Not expected game item: {item}"),
        };

        private void MainForm_Resize(object sender, EventArgs e)
        {
            mainImage.Width = mainImage.Height;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            game.OnUpdateRequired += GameUpdateRequired;
            game.OnEatFood += GameOnEatFood;
            game.OnCollision += GameOnCollision;
        }

        private void GameOnCollision()
        {
            tickTimer.Stop();
            MessageBox.Show("RIP");

            game.InitializeGame(GameSize, GameSize);
            tickTimer.Interval = 300;
            tickTimer.Start();
        }

        private void GameOnEatFood()
        {
            if (tickTimer.Interval > 150)
            {
                tickTimer.Interval -= 10;
            }

            this.scoreLabel.Text = game.Score.ToString();
        }

        private void GameUpdateRequired()
        {
            mainImage.Refresh();
        }

        private void tickTimer_Tick(object sender, EventArgs e)
        {
            game.Tick();
        }

        private void MainForm_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            var keys = new[] { Keys.Up, Keys.Down, Keys.Left, Keys.Right };

            e.IsInputKey = keys.Contains(e.KeyCode);
        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Left: game.Direction = Direction.Left; break;
                case Keys.Right: game.Direction = Direction.Right; break;
                case Keys.Up: game.Direction = Direction.Up; break;
                case Keys.Down: game.Direction = Direction.Down; break;
            }
        }
    }
}