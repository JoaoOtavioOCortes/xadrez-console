using board;
using Xadrez;

try
{

    Board board = new Board(8, 8);

    board.PutPiece(new Tower(Color.Black, board), new Position(0, 0));
    board.PutPiece(new Tower(Color.Black, board), new Position(1, 3));
    board.PutPiece(new King(Color.Black, board), new Position(0, 2));

    Window.PrintOutBoard(board);
}
catch (ExceptionBoard e)
{
    Console.WriteLine(e.Message);
}