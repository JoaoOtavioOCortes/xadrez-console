using board;

namespace Xadrez
{
    internal class XadrezMatch
    {
        public Board Board { get; private set; }
        private int Turn;
        private Color CurrentPlayer;
        public bool Finished { get; private set; }

        public XadrezMatch()
        {
            Board = new Board(8, 8);
            Turn = 1;
            CurrentPlayer = Color.White;
            PuttingAllPieces();
            Finished = false;
        }

        public void RunMoviment(Position origin, Position destiny)
        {
            Piece p = Board.RemovePiece(origin);
            p.QntdMovesIncrement();
            Piece CapturedPiece = Board.RemovePiece(destiny);
            Board.PutPiece(p, destiny);
        }

        private void PuttingAllPieces()
        {
            Board.PutPiece(new Tower(Color.White, Board), new XadrezPosition('c',1).ToPosition());
            Board.PutPiece(new Tower(Color.White, Board), new XadrezPosition('c', 2).ToPosition());
            Board.PutPiece(new Tower(Color.White, Board), new XadrezPosition('d', 2).ToPosition());
            Board.PutPiece(new Tower(Color.White, Board), new XadrezPosition('e', 2).ToPosition());
            Board.PutPiece(new Tower(Color.White, Board), new XadrezPosition('e', 1).ToPosition());
            Board.PutPiece(new King(Color.White, Board), new XadrezPosition('d', 1).ToPosition());

            Board.PutPiece(new Tower(Color.Black, Board), new XadrezPosition('c', 7).ToPosition());
            Board.PutPiece(new Tower(Color.Black, Board), new XadrezPosition('c', 8).ToPosition());
            Board.PutPiece(new Tower(Color.Black, Board), new XadrezPosition('d', 7).ToPosition());
            Board.PutPiece(new Tower(Color.Black, Board), new XadrezPosition('e', 7).ToPosition());
            Board.PutPiece(new Tower(Color.Black, Board), new XadrezPosition('e', 8).ToPosition());
            Board.PutPiece(new King(Color.Black, Board), new XadrezPosition('d', 8).ToPosition());


        }
    }
}
