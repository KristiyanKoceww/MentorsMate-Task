namespace BrickworkTask
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    public class Layer : ILayer
    {
        private int rows;
        private int columns;

        public Layer(int rows, int columns)
        {
            this.Rows = rows;
            this.Cols = columns;

            this.Wall = new int[Rows, Cols];
            this.Bricks = new Dictionary<Brick, int>();
        }

        public int Rows 
        {
            get => this.rows;
            set
            {
                // Validate every row
                if (value > 1 && value <= 100 && value % 2 == 0)
                {
                    this.rows = value;
                }
                else
                {
                    throw new ArgumentException("Value must be between 1 and 100 and even number!");
                }
            }
        }

        public int Cols 
        {
            get => this.columns;
            set
            {
                // Validate every col
                if (value > 1 && value <= 100 && value % 2 == 0)
                {
                    this.columns = value;
                }
                else
                {
                    throw new ArgumentException("Value must be between 1 and 100 and even number!");
                }
            }
        }

        public Dictionary<Brick, int> Bricks { get; set; }

        public int[,] Wall { get; set; }

        public void Output(ILayer outputLayer)
        {
            Brick brick;
            // Order bricks by key
            var orderedBricks = this.Bricks.OrderBy(x => x.Key.Number).ToList();

            for (int row = 0; row < this.Wall.GetLength(0); row++)
            {
                for (int col = 0; col < this.Wall.GetLength(1); col++)
                {
                    if (CheckIfIsInsideMatrix(this.Wall, row, col + 1) && this.Wall[row, col] != this.Wall[row, col + 1] && (outputLayer.Wall[row, col] == 0 && outputLayer.Wall[row, col + 1] == 0))
                    {
                        // Get first brick
                        brick = orderedBricks.First().Key;

                        // Set the brick horizontally:  1 1
                        outputLayer.Wall[row, col] = brick.Number;
                        outputLayer.Wall[row, col + 1] = brick.Number;

                        // Remove used brick from the list
                        orderedBricks.Remove(orderedBricks[0]);
                    }

                    if (CheckIfIsInsideMatrix(this.Wall, row - 1, col) && this.Wall[row, col] != this.Wall[row - 1, col] && (outputLayer.Wall[row, col] == 0 && outputLayer.Wall[row - 1, col] == 0))
                    {
                        // Get first brick
                        brick = orderedBricks.First().Key;

                        // Set the brick vertically:  1
                        //                            1
                        outputLayer.Wall[row, col] = brick.Number;
                        outputLayer.Wall[row - 1, col] = brick.Number;

                        // Remove used brick from the list
                        orderedBricks.Remove(orderedBricks[0]);
                    }
                }
            }
        }

        public bool CheckIfIsInsideMatrix(int[,] matrix, int row, int col)
        {
            return row >= 0 && row < matrix.GetLength(0) && col >= 0 && col < matrix.GetLength(1);
        }
       
        public bool CheckIfLayerIsValid()
        {
            for (int row = 0; row < this.Wall.GetLength(0); row++)
            {
                for (int col = 0; col < this.Wall.GetLength(1); col++)
                {
                    var currentBrick = new Brick(this.Wall[row, col]);

                    // Check if bricks collection does not contains current brick and add it
                    if (!this.Bricks.Keys.Any(x => x.Number == currentBrick.Number))
                    {
                        this.Bricks.Add(currentBrick, 0);
                    }

                    if (CheckIfIsInsideMatrix(this.Wall, row, col + 1) && currentBrick.Number == this.Wall[row, col + 1])
                    {
                        this.Bricks[currentBrick]++;
                    }
                    if (CheckIfIsInsideMatrix(this.Wall, row + 1, col) && currentBrick.Number == this.Wall[row + 1, col])
                    {
                        this.Bricks[currentBrick]++;
                    }
                }
            }

            // Check every brick in collection of bricks if the value is valid
            foreach (var brick in this.Bricks)
            {
                if (brick.Value != 1)
                {
                    return false;
                }
            }

            return true;
        }
        
        public void PrintLayer()
        {
            for (int row = 0; row < this.Wall.GetLength(0); row++)
            {
                for (int col = 0; col < this.Wall.GetLength(1); col++)
                {
                    // Sets "-" at the begging of the col
                    if (col == 0)
                    {
                        Console.Write($"-{this.Wall[row, col]}");
                    }

                    if (CheckIfIsInsideMatrix(this.Wall, row, col + 1) && this.Wall[row, col] == this.Wall[row, col + 1])
                    {
                        Console.Write($" {this.Wall[row, col + 1]}");

                    }
                    // Sets "-" if they are different bricks
                    else if (CheckIfIsInsideMatrix(this.Wall, row, col + 1) && this.Wall[row, col] != this.Wall[row, col + 1])
                    {
                        Console.Write($"-{this.Wall[row, col + 1]}");
                    }

                    // Sets "-" at the end of the col
                    if (col + 1 == this.Wall.GetLength(1) - 1)
                    {
                        Console.Write($"-");
                    }
                }
                Console.WriteLine();
            }
        }
    }
}
