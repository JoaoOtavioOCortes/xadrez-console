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
                 PiecePrint(board.piece(i, j));       
                }
                Console.WriteLine();
            }
            Console.Write("  ");
            ConsoleColor aux = Console.BackgroundColor;
            Console.BackgroundColor = ConsoleColor.Red;
            Console.WriteLine("a b c d e f g h");
            Console.BackgroundColor = aux;
        }

        public static void PrintOutBoard(Board board, bool[,] posiblePosition)
        {
            ConsoleColor originalBackGround = Console.BackgroundColor;
            ConsoleColor diferentBackGround = ConsoleColor.DarkGray;
            
            for (int i = 0; i < board.Lines; i++)
            {
                ConsoleColor ax = Console.BackgroundColor;
                Console.BackgroundColor = ConsoleColor.Red;
                Console.Write(8 - i + " ");
                Console.BackgroundColor = ax;

                for (int j = 0; j < board.Columns; j++)
                {
                    if (posiblePosition[i,j] == true)
                    {
                        Console.BackgroundColor = diferentBackGround;
                    }
                    else
                    {
                        Console.BackgroundColor = originalBackGround;
                    }
                    PiecePrint(board.piece(i, j));
                }
                Console.WriteLine();
            }
            Console.Write("  ");
            ConsoleColor aux = Console.BackgroundColor;
            Console.BackgroundColor = ConsoleColor.Red;
            Console.WriteLine("a b c d e f g h");
            Console.BackgroundColor = aux;
            Console.BackgroundColor = originalBackGround;
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
            if(piece == null)
            {
                Console.Write("- ");
            }
            else
            {
                if (piece.Color == Color.Branco)
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
                Console.Write(" ");
            }


        }
    }
}
