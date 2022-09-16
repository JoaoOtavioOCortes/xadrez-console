using board;
using Xadrez;

namespace board
{
    internal class Window
    {
        public static void PrintOutBoard(Board board)
        {
            for(int i=0; i<board.Lines; i++)
            {
                ConsoleColor ax = Console.BackgroundColor;
                Console.BackgroundColor = ConsoleColor.Red;
                Console.Write(8-i + " ");
                Console.BackgroundColor = ax;

                for(int j=0; j<board.Columns; j++)
                {
                    if (board.piece(i, j) == null)
                    {
                        Console.Write("- ");
                    }

                    else
                    {
                        PiecePrint(board.piece(i, j));
                        Console.Write(" ");
                    }
                }
                Console.WriteLine();
            }
            Console.Write("  ");
            ConsoleColor aux = Console.BackgroundColor;
            Console.BackgroundColor = ConsoleColor.Red;
            Console.WriteLine("a b c d e f g h");
            Console.BackgroundColor = aux;
        }


        public static XadrezPosition ReadXadrezPosition()
        {
            string s = Console.ReadLine();
            char column = s[0];
            int line = int.Parse(s[1] + "");
            return new XadrezPosition(column, line);
        } 

        public static void PiecePrint(Piece piece)
        {
            if(piece.Color == Color.White)
            {
                Console.Write(piece);
            }
            else
            {
                ConsoleColor aux = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(piece);
                Console.ForegroundColor = aux;
            }
        }
    }
}
