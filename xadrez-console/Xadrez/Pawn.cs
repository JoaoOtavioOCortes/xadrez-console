using board;

namespace Xadrez
{
    internal class Pawn : Piece
    {
        public Pawn(Color color, Board board) : base(color, board)
        {
        }

        public override string ToString()
        {
            return "P";
        }

        private bool EnemyExist(Position pos)
        {
            Piece p = Board.Piece(pos);
            return p != null && p.Color != Color;
        }

        private bool Free(Position pos)
        {
            return Board.Piece(pos) == null;
        }

        public override bool[,] PossibleMovements()
        {
            bool[,] mat = new bool[Board.Lines, Board.Columns];

            Position pos = new Position(0, 0);

            if(Color == Color.Branco)
            {
                pos.ValuesDefinition(Position.Line - 1, Position.Column);
                if(Board.ValidPosition(pos) && Free(pos))
                {
                    mat[pos.Line, pos.Column] = true;
                }
                pos.ValuesDefinition(Position.Line - 2, Position.Column);
                Position p2 = new Position(Position.Line - 1, Position.Column);
                if(Board.ValidPosition(p2) && Free(p2) && Board.ValidPosition(pos) && Free(pos) && QntdMoves == 0)
                {
                    mat[pos.Line, pos.Column] = true;
                }
                pos.ValuesDefinition(Position.Line - 1, Position.Column - 1);
                if(Board.ValidPosition(pos) && EnemyExist(pos))
                {
                    mat[pos.Line, pos.Column] = true;
                }
                pos.ValuesDefinition(Position.Line - 1, Position.Column + 1);
                if (Board.ValidPosition(pos) && EnemyExist(pos))
                {
                    mat[pos.Line, pos.Column] = true;
                }
            }
            else
            {
                pos.ValuesDefinition(Position.Line + 1, Position.Column);
                if (Board.ValidPosition(pos) && Free(pos))
                {
                    mat[pos.Line, pos.Column] = true;
                }
                pos.ValuesDefinition(Position.Line + 2, Position.Column);
                Position p2 = new Position(Position.Line + 1, Position.Column);
                if (Board.ValidPosition(p2) && Free(p2) && Board.ValidPosition(pos) && Free(pos) && QntdMoves == 0)
                {
                    mat[pos.Line, pos.Column] = true;
                }
                pos.ValuesDefinition(Position.Line + 1, Position.Column - 1);
                if (Board.ValidPosition(pos) && EnemyExist(pos))
                {
                    mat[pos.Line, pos.Column] = true;
                }
                pos.ValuesDefinition(Position.Line + 1, Position.Column + 1);
                if (Board.ValidPosition(pos) && EnemyExist(pos))
                {
                    mat[pos.Line, pos.Column] = true;
                }
            }

            return mat;
        }


    }
}
