﻿using board;
using Xadrez;

try
{

    XadrezMatch match = new XadrezMatch();

    while (!match.Finished)
    {

        try
        {
            Console.Clear();
            Window.PrintOutBoard(match.Board);
            Console.WriteLine();
            Console.WriteLine("Turno: " + match.Turn);
            Console.WriteLine("Aguardando jogada: " + match.CurrentPlayer);

            Console.WriteLine("");
            Console.Write("Origem: ");
            Position origin = Window.ReadXadrezPosition().ToPosition();
            match.ValidateOriginPosition(origin);

            bool[,] possiblePosition = match.Board.piece(origin).PossibleMoviments();

            Console.Clear();
            Window.PrintOutBoard(match.Board, possiblePosition);

            Console.WriteLine("");
            Console.Write("Destino: ");
            Position destiny = Window.ReadXadrezPosition().ToPosition();
            match.ValidateDestinyPosition(origin, destiny);

            match.MakesMove(origin, destiny);
        }
        catch(ExceptionBoard e)
        {
            Console.WriteLine(e.Message);
            Console.ReadLine();
        }
    }


    Window.PrintOutBoard(match.Board);
}
catch (ExceptionBoard e)
{
    Console.WriteLine(e.Message);
}