namespace BrickworkTask
{
    using System;
    using System.Linq;

    public class Program
    {
        private static ILayer firstLayer;
        private static ILayer secondLayer;

        public static void Main(string[] args)
        {
            // Read input dimentions
            int[] dimentions = Console.ReadLine()
                .Split(" ", StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();

            // Define rows and cols from the input
            int rows = dimentions[0];
            int columns = dimentions[1];

            // Default layer
            firstLayer = new Layer(rows, columns);

            // Check if default layer is valid
            if (firstLayer.Rows != 0 && firstLayer.Cols != 0)
            {
                secondLayer = new Layer(rows, columns);

                // Fill default layer with data
                for (int row = 0; row < firstLayer.Wall.GetLength(0); row++)
                {
                    int[] input = Console.ReadLine()
                    .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(int.Parse)
                    .ToArray();

                    // Check if input is valid
                    if (input.Count() < columns)
                    {
                        ValidationError("The input is invalid!");
                    }

                    for (int col = 0; col < firstLayer.Wall.GetLength(1); col++)
                    {
                        firstLayer.Wall[row, col] = input[col];
                    }
                }

                Console.WriteLine();
            }
            else
            {
                ValidationError("Valid layer must contain no more than 100 rows or columns!");
            }


            if (firstLayer.CheckIfLayerIsValid())
            {
                firstLayer.Output(secondLayer);
                secondLayer.PrintLayer();
            }
            else
            {
                ValidationError("Layer contains invalid blocks!");
            }
        }

        private static void ValidationError(string message)
        {
            Console.WriteLine("-1");
            Console.WriteLine($"{message}");
            System.Environment.Exit(1);
        }
    }
}
