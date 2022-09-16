namespace board
{
    internal class Board
    {
        public int Lines { get; set; }
        public int Columns { get; set; }
        private Piece[,] Pieces;

        public Board()
        {
        }

        public Board(int lines, int columns)
        {
            Lines = lines;
            Columns = columns;  
            Pieces = new Piece[Lines, Columns];
        }

       public Piece piece(int line, int column)
        {
            return Pieces[line, column];
        }


        public Piece piece(Position pos)
        {
            return Pieces[pos.Line, pos.Column];
        }

        public bool PieceExistence(Position pos)
        {
            ValidatePosition(pos);
            return piece(pos) != null;
        }



        public void PutPiece(Piece p, Position pos)
        {
            if (PieceExistence(pos)) 
            {
                throw new ExceptionBoard("Já existe uma peça nessa posição!");
            }
            
            Pieces[pos.Line, pos.Column] = p;
            p.Position = pos;
        }

        public Piece RemovePiece(Position pos)
        {
            if (piece(pos) == null)
            {
                return null;
            }
            Piece aux = piece(pos);
            aux.Position = null;
            Pieces[pos.Line, pos.Column] = null;
            return aux;
        }


        public bool ValidPosition(Position pos)
        {
           if(pos.Line<0 || pos.Line>=Lines || pos.Column<0 || pos.Column >= Columns)
            {
                return false;
            }
            return true;
        }

        public void ValidatePosition(Position pos)
        {
            if (!ValidPosition(pos))
            {
                throw new ExceptionBoard("Posição inválida!");
            }
        }
    }
}
