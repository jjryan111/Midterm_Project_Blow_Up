using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Midterm_Blow_Me_up
{
    class Program
    {//GIT YOU'RE A GIT
        static void Main(string[] args)
        {
            Board board = new Board();

            // board.GameBoard()
            PlayGame newGame = new PlayGame();
            int size = newGame.GetSize();
            int bomb = newGame.GetBomb();
            int[,] arr1 = new int[size + 2, size + 2];
            arr1 = board.AddMines(arr1, bomb);
            int[,] backBoard = board.GameBoard(arr1);
            int[,] originalboard = arr1;
            bool cont = true;

            string[,] playerBoard = newGame.MakeBoard();
            while (cont)
            {
                int[] choices = newGame.GetChoice();
                playerBoard = newGame.UpdateBoard(backBoard, playerBoard, choices, bomb);
                newGame.PrintBoard(playerBoard);
                Console.WriteLine();
                if (playerBoard[0, 0] == "1")
                {
                    cont = false;

                }
                Console.WriteLine();
            }
            Console.WriteLine("\n");
            newGame.PrintFinalBoard(backBoard);
        }
    }
}
