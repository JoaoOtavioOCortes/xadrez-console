using board;

namespace Xadrez
{
    internal class XadrezMatch
    {
        public Board Board { get; private set; }
        public int Turn { get; private set; }
        public Color CurrentPlayer { get; private set; }
        public bool Finished { get; private set; }

        public XadrezMatch()
        {
            Board = new Board(8, 8);
            Turn = 1;
            CurrentPlayer = Color.Branco;
            Finished = false;
            PuttingAllPieces();
            
        }

        private void RunMoviment(Position origin, Position destiny)
        {
            Piece p = Board.RemovePiece(origin);
            p.QntdMovesIncrement();
            Piece CapturedPiece = Board.RemovePiece(destiny);
            Board.PutPiece(p, destiny);
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

        private void PuttingAllPieces()
        {
            Board.PutPiece(new Tower(Color.Branco, Board), new XadrezPosition('c',1).ToPosition());
            Board.PutPiece(new Tower(Color.Branco, Board), new XadrezPosition('c', 2).ToPosition());
            Board.PutPiece(new Tower(Color.Branco, Board), new XadrezPosition('d', 2).ToPosition());
            Board.PutPiece(new Tower(Color.Branco, Board), new XadrezPosition('e', 2).ToPosition());
            Board.PutPiece(new Tower(Color.Branco, Board), new XadrezPosition('e', 1).ToPosition());
            Board.PutPiece(new King(Color.Branco, Board), new XadrezPosition('d', 1).ToPosition());

            Board.PutPiece(new Tower(Color.Preto, Board), new XadrezPosition('c', 7).ToPosition());
            Board.PutPiece(new Tower(Color.Preto, Board), new XadrezPosition('c', 8).ToPosition());
            Board.PutPiece(new Tower(Color.Preto, Board), new XadrezPosition('d', 7).ToPosition());
            Board.PutPiece(new Tower(Color.Preto, Board), new XadrezPosition('e', 7).ToPosition());
            Board.PutPiece(new Tower(Color.Preto, Board), new XadrezPosition('e', 8).ToPosition());
            Board.PutPiece(new King(Color.Preto, Board), new XadrezPosition('d', 8).ToPosition());


        }
    }
}
