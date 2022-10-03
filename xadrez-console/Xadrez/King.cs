using board;

namespace Xadrez
{
     class King : Piece 
    {

        private XadrezMatch Match;


        public King(Color color, Board board, XadrezMatch match) : base(color, board)
        {
            Match = match;
        }


        public override string ToString()
        {
            return "R";
        }

        private bool CanMove(Position pos)
        {
            Piece p = Board.Piece(pos);
            return p == null || p.Color != Color;
        }

        private bool TowerTestToRoque(Position pos)
        {
            Piece p = Board.Piece(pos);
            return p != null && p is Tower && p.Color == Color && p.QntdMoves == 0;
        }
        
        public override bool[,] PossibleMovements()
        {
            bool[,] mat = new bool[Board.Lines, Board.Columns];

            Position pos = new Position(0, 0);

            //above
            pos.ValuesDefinition(Position.Line - 1, Position.Column);
            if(Board.ValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Line, pos.Column] = true;
            }
            //north east
            pos.ValuesDefinition(Position.Line - 1, Position.Column +1);
            if (Board.ValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Line, pos.Column] = true;
            }
            //right
            pos.ValuesDefinition(Position.Line, Position.Column + 1);
            if (Board.ValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Line, pos.Column] = true;
            }
            //south east
            pos.ValuesDefinition(Position.Line + 1, Position.Column + 1);
            if (Board.ValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Line, pos.Column] = true;
            }
            //below
            pos.ValuesDefinition(Position.Line + 1, Position.Column);
            if (Board.ValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Line, pos.Column] = true;
            }
            //south west
            pos.ValuesDefinition(Position.Line + 1, Position.Column - 1);
            if (Board.ValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Line, pos.Column] = true;
            }
            //left
            pos.ValuesDefinition(Position.Line, Position.Column -1);
            if (Board.ValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Line, pos.Column] = true;
            }
            //north west
            pos.ValuesDefinition(Position.Line - 1, Position.Column - 1);
            if (Board.ValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Line, pos.Column] = true;
            }

            // #SpecialPlay roque
            if (QntdMoves == 0 && !Match.Check)
            {
                // #SpecialPlay roque pequeno
                Position posT1 = new Position(Position.Line, Position.Column + 3);
                if (TowerTestToRoque(posT1))
                {
                    Position p1 = new Position(Position.Line, Position.Column + 1);
                    Position p2 = new Position(Position.Line, Position.Column + 2);
                    if(Board.Piece(p1) == null && Board.Piece(p2) == null)
                    {
                        mat[Position.Line, Position.Column + 2] = true;
                    }
                }
                // #SpecialPlay roque grande
                Position posT2 = new Position(Position.Line, Position.Column - 4);
                if (TowerTestToRoque(posT2))
                {
                    Position p1 = new Position(Position.Line, Position.Column - 1);
                    Position p2 = new Position(Position.Line, Position.Column - 2);
                    Position p3 = new Position(Position.Line, Position.Column - 3);
                    if (Board.Piece(p1) == null && Board.Piece(p2) == null  && Board.Piece(p3) == null)
                    {
                        mat[Position.Line, Position.Column - 2] = true;                
                    }
                }
            }
            return mat;
        }


    }
}
