using board;
using Xadrez;

Board board = new Board(8, 8);

board.PutPiece(new Tower(Color.Black, board), new Position(0, 0));
board.PutPiece(new Tower(Color.Black, board), new Position(1, 3));
board.PutPiece(new King(Color.Black, board), new Position(2, 4));

Window.PrintOutBoard(board);