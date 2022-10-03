using board;

namespace Xadrez
{
    internal class XadrezMatch
    {
        public Board Board { get; private set; }
        public int Turn { get; private set; }
        public Color CurrentPlayer { get; private set; }
        public bool Finished { get; private set; }
        private HashSet<Piece> Pieces;
        private HashSet<Piece> Captured;
        public bool Check { get; private set; }
        
        public XadrezMatch()
        {
            Board = new Board(8, 8);
            Turn = 1;
            CurrentPlayer = Color.Branco;
            Finished = false;
            Pieces = new HashSet<Piece>();
            Captured = new HashSet<Piece>();
            PuttingAllPieces();
            
        }

        private Piece RunMoviment(Position origin, Position destiny)
        {
            Piece p = Board.RemovePiece(origin);
            p.QntdMovesIncrement();
            Piece capturedPiece = Board.RemovePiece(destiny);
            Board.PutPiece(p, destiny);
            if(capturedPiece != null)
            {
                Captured.Add(capturedPiece);
            }

            //#SpecialPlay roque pequeno
            if(p is King && destiny.Column == origin.Column + 2)
            {
                Position originT = new Position(origin.Line, origin.Column + 3);
                Position destinyT = new Position(origin.Line, origin.Column + 1);
                Piece T = Board.RemovePiece(originT);
                T.QntdMovesIncrement();
                Board.PutPiece(T, destinyT);
            }
            //#SpecialPlay roque grande
            if (p is King && destiny.Column == origin.Column - 2)
            {
                Position originT = new Position(origin.Line, origin.Column - 4);
                Position destinyT = new Position(origin.Line, origin.Column - 1);
                Piece T = Board.RemovePiece(originT);
                T.QntdMovesIncrement();
                Board.PutPiece(T, destinyT);
            }

            return capturedPiece;
        }

        public void UndoMove(Position origin, Position destiny, Piece capturedPiece)
        {
            Piece p = Board.RemovePiece(destiny);
            p.QntdMovesdecrement();
            if (capturedPiece != null)
            {
                Board.PutPiece(capturedPiece, destiny);
                Captured.Remove(capturedPiece);
            }
            Board.PutPiece(p, origin);

            //#SpecialPlay roque pequeno
            if (p is King && destiny.Column == origin.Column + 2)
            {
                Position originT = new Position(origin.Line, origin.Column + 3);
                Position destinyT = new Position(origin.Line, origin.Column + 1);
                Piece T = Board.RemovePiece(destinyT);
                T.QntdMovesdecrement();
                Board.PutPiece(T, originT);
            }
            //#SpecialPlay roque grande
            if (p is King && destiny.Column == origin.Column - 2)
            {
                Position originT = new Position(origin.Line, origin.Column -4);
                Position destinyT = new Position(origin.Line, origin.Column - 1);
                Piece T = Board.RemovePiece(destinyT);
                T.QntdMovesdecrement();
                Board.PutPiece(T, originT);
            }
        }

        public void MakesMove(Position origin, Position destiny) 
        {
            Piece capturedPiece = RunMoviment(origin, destiny);

            if (InCheck(CurrentPlayer))
            {
                UndoMove(origin, destiny, capturedPiece);
                throw new ExceptionBoard("Você não pode se colocar em xeque!");
            }

            if (InCheck(Adversary(CurrentPlayer)))
            {
                Check = true;
            }
            else 
            {
                Check = false;
            }

            if (CheckMateTest(Adversary(CurrentPlayer)))
            {
                Finished = true;
            }

            else
            { 
                Turn++;
                ChangePlayer();
            }
        }

        public void ValidateOriginPosition(Position pos)
        {
            if (Board.Piece(pos) == null)
            {
                throw new ExceptionBoard("Não existe peça na posição de origem escolhida");
            }
            if (CurrentPlayer != Board.Piece(pos).Color)
            {
                throw new ExceptionBoard("A peça de origem escolhida não é sua!");
            }
            if (!Board.Piece(pos).ThereRPossibleMoves())
            {
                throw new ExceptionBoard("Não há movimentos possíveis para  a peça de origem escolhida!");
            }
        }

        public void ValidateDestinyPosition(Position origin, Position destiny)
        {
            if (!Board.Piece(origin).PossibleMoves(destiny))
            {
                throw new ExceptionBoard("Posição de destino inválida!");
            }
        }

        private void ChangePlayer()
        {
            if(CurrentPlayer == Color.Branco)
            {
                CurrentPlayer = Color.Preto;
            }
            else
            {
                CurrentPlayer = Color.Branco;
            }
        }

        public HashSet<Piece> CapturedPieces(Color color)
        {
            HashSet<Piece> aux = new HashSet<Piece>();
            foreach(Piece x in Captured)
            {
                if(x.Color == color)
                {
                    aux.Add(x);
                }
            }
            return aux; 
        }

        public HashSet<Piece> PiecesInGame(Color color)
        {
            HashSet<Piece> aux = new HashSet<Piece>();
            foreach(Piece x in Pieces)
            {
                if (x.Color == color)
                {
                    aux.Add(x);
                }
            }
            aux.ExceptWith(CapturedPieces(color));
            return aux;
        }

        private Color Adversary(Color color)
        {
            if (color == Color.Branco)
            {
                return Color.Preto;
            }
            else
            {
                return Color.Branco;
            } 
        }

        private Piece King(Color color)
        {
            foreach(Piece x in PiecesInGame(color))
            {
                if (x is King)
                {
                    return x;
                }
            }
            return null;
        }

        public bool InCheck(Color color)
        {
            Piece K = King(color);
            if(K == null)
            {
                throw new ExceptionBoard("Não tem rei da cor " + color + " no tabuleiro!");
            }

            foreach(Piece x in PiecesInGame(Adversary(color)))
            {
                bool[,] mat = x.PossibleMovements();
                if (mat[K.Position.Line, K.Position.Column])
                {
                    return true;
                }
            }
            return false;
        }

        public bool CheckMateTest(Color color)
        {
            if (!InCheck(color))
            {
                return false;
            }

            foreach(Piece x in PiecesInGame(color))
            {
                bool[,] mat = x.PossibleMovements();
                for (int i=0; i<Board.Lines; i++)
                {
                    for(int j=0; j<Board.Columns; j++)
                    {
                        if (mat[i, j])
                        {
                            Position origin = x.Position;
                            Position destiny = new Position(i, j);
                            Piece capturedPiece = RunMoviment(origin, destiny);
                            bool checkTest = InCheck(color);
                            UndoMove(origin, destiny, capturedPiece);
                            if (!checkTest)
                            {
                                return false;
                            }
                        }
                    }
                }
            }
            return true;
        }

        public void PuttingNewPiece(char column, int line, Piece piece) 
        {
            Board.PutPiece(piece, new XadrezPosition(column, line).ToPosition());
            Pieces.Add(piece);
        }

        private void PuttingAllPieces()
        {
            PuttingNewPiece('a', 1, new Tower(Color.Branco, Board));
            PuttingNewPiece('b', 1, new Horse(Color.Branco, Board));
            PuttingNewPiece('c', 1, new Bishop(Color.Branco, Board));
            PuttingNewPiece('d', 1, new Queen(Color.Branco, Board));
            PuttingNewPiece('e', 1, new King(Color.Branco, Board, this));
            PuttingNewPiece('f', 1, new Bishop(Color.Branco, Board));
            PuttingNewPiece('g', 1, new Horse(Color.Branco, Board));
            PuttingNewPiece('h', 1, new Tower(Color.Branco, Board));
            PuttingNewPiece('a', 2, new Pawn(Color.Branco, Board));
            PuttingNewPiece('b', 2, new Pawn(Color.Branco, Board));
            PuttingNewPiece('c', 2, new Pawn(Color.Branco, Board));
            PuttingNewPiece('d', 2, new Pawn(Color.Branco, Board));
            PuttingNewPiece('e', 2, new Pawn(Color.Branco, Board));
            PuttingNewPiece('f', 2, new Pawn(Color.Branco, Board));
            PuttingNewPiece('g', 2, new Pawn(Color.Branco, Board));
            PuttingNewPiece('h', 2, new Pawn(Color.Branco, Board));

            PuttingNewPiece('a', 8, new Tower(Color.Preto, Board));
            PuttingNewPiece('b', 8, new Horse(Color.Preto, Board));
            PuttingNewPiece('c', 8, new Bishop(Color.Preto, Board));
            PuttingNewPiece('d', 8, new Queen(Color.Preto, Board));
            PuttingNewPiece('e', 8, new King(Color.Preto, Board, this));
            PuttingNewPiece('f', 8, new Bishop(Color.Preto, Board));
            PuttingNewPiece('g', 8, new Horse(Color.Preto, Board));
            PuttingNewPiece('h', 8, new Tower(Color.Preto, Board));
            PuttingNewPiece('a', 7, new Pawn(Color.Preto, Board));
            PuttingNewPiece('b', 7, new Pawn(Color.Preto, Board));
            PuttingNewPiece('c', 7, new Pawn(Color.Preto, Board));
            PuttingNewPiece('d', 7, new Pawn(Color.Preto, Board));
            PuttingNewPiece('e', 7, new Pawn(Color.Preto, Board));
            PuttingNewPiece('f', 7, new Pawn(Color.Preto, Board));
            PuttingNewPiece('g', 7, new Pawn(Color.Preto, Board));
            PuttingNewPiece('h', 7, new Pawn(Color.Preto, Board));

        }
    }
}
