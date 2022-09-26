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

        private void RunMoviment(Position origin, Position destiny)
        {
            Piece p = Board.RemovePiece(origin);
            p.QntdMovesIncrement();
            Piece CapturedPiece = Board.RemovePiece(destiny);
            Board.PutPiece(p, destiny);
            if(CapturedPiece != null)
            {
                Captured.Add(CapturedPiece);
            }
        }

        public void MakesMove(Position origin, Position destiny) 
        {
            RunMoviment(origin, destiny);
            Turn++;
            ChangePlayer();
        }

        public void ValidateOriginPosition(Position pos)
        {
            if (Board.piece(pos) == null)
            {
                throw new ExceptionBoard("Não existe peça na posição de origem escolhida");
            }
            if (CurrentPlayer != Board.piece(pos).Color)
            {
                throw new ExceptionBoard("A peça de origem escolhida não é sua!");
            }
            if (!Board.piece(pos).ThereRPossibleMoves())
            {
                throw new ExceptionBoard("Não há movimentos possíveis para  a peça de origem escolhida!");
            }
        }

        public void ValidateDestinyPosition(Position origin, Position destiny)
        {
            if (!Board.piece(origin).CanMoveTo(destiny))
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

        public void PuttingNewPiece(char column, int line, Piece piece) 
        {
            Board.PutPiece(piece, new XadrezPosition(column, line).ToPosition());
            Pieces.Add(piece);
        }

        private void PuttingAllPieces()
        {
            PuttingNewPiece('c', 1, new Tower(Color.Branco, Board));
            PuttingNewPiece('c', 2, new Tower(Color.Branco, Board));
            PuttingNewPiece('d', 2, new Tower(Color.Branco, Board));
            PuttingNewPiece('e', 2, new Tower(Color.Branco, Board));
            PuttingNewPiece('e', 1, new Tower(Color.Branco, Board));
            PuttingNewPiece('d', 1, new King(Color.Branco, Board));

            PuttingNewPiece('c', 7, new Tower(Color.Preto, Board));
            PuttingNewPiece('c', 8, new Tower(Color.Preto, Board));
            PuttingNewPiece('d', 7, new Tower(Color.Preto, Board));
            PuttingNewPiece('e', 7, new Tower(Color.Preto, Board));
            PuttingNewPiece('e', 8, new Tower(Color.Preto, Board));
            PuttingNewPiece('d', 8, new King(Color.Preto, Board));

        }
    }
}
