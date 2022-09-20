﻿using board;
using Xadrez;

try
{

    XadrezMatch match = new XadrezMatch();

    while(!match.Finished)
    {
        Console.Clear();
        Window.PrintOutBoard(match.Board);

        Console.WriteLine("");
        Console.Write("Origem: ");
        Position origin = Window.ReadXadrezPosition().ToPosition();

        bool[,] possiblePosition = match.Board.piece(origin).PossibleMoviments(); 

        Console.Clear();
        Window.PrintOutBoard(match.Board, possiblePosition);

        Console.WriteLine("");
        Console.Write("Destino: ");
        Position destiny = Window.ReadXadrezPosition().ToPosition();

        match.RunMoviment(origin, destiny);
    }





    Window.PrintOutBoard(match.Board);
}
catch (ExceptionBoard e)
{
    Console.WriteLine(e.Message);
}