namespace board
{
    abstract class Piece
    {
        public Position Position { get; set; }
        public Color Color { get; protected set; }
        public int QntdMoves { get; protected set; }
        public Board Board { get; protected set; }

        public Piece()
        {
        }

        public Piece(Color color, Board board)
        {
            Position = null;
            Color = color;
            QntdMoves = 0;
            Board = board;
        }

        public void QntdMovesIncrement()
        {
            QntdMoves++;
        }

        public void QntdMovesdecrement()
        {
            QntdMoves--;
        }

        public bool ThereRPossibleMoves()
        {
            bool[,] mat = PossibleMovements();
            for (int i=0; i<Board.Lines; i++)
            {
                for(int j=0; j<Board.Columns; j++)
                {
                    if (mat[i,j] == true)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public bool PossibleMoves(Position pos)
        {
            return PossibleMovements()[pos.Line, pos.Column];
        }

        public abstract bool[,] PossibleMovements();
    }
}
