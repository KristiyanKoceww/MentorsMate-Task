namespace BrickworkTask
{
    public interface ILayer
    {
        public int Rows { get; set; }

        public int Cols { get; set; }

        public int[,] Wall { get; set; }

        bool CheckIfLayerIsValid();

        bool CheckIfIsInsideMatrix(int[,] matrix, int row, int col);

        void Output(ILayer outputLayer);

        void PrintLayer();
    }
}
