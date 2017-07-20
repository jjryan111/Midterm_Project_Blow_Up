using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Midterm_Blow_Me_up
{
    class PlayGame
    {
        //GIT YOU'RE A GIT
        // ints declared for size and bomb inputs
        public Board b = new Board();
        private int size;
        private int bomb;
        private Random ran = new Random();
        public int flagsOnBombs = 0;
        // constructor uses SetSize and SetBomb to set the values of each int to user input
        public PlayGame()
        {
            size = SetSize();
            bomb = SetBomb();
        }

        // SetBomb gets user input, validates it, and returns the input
        public int SetBomb()
        {
            // rawChoice is the raw string from Console.ReadLine(). 
            string rawChoice;
            // int choice is the int that is returned. 
            int choice;
            Console.WriteLine("How Many Bombs:\n");
            rawChoice = Console.ReadLine();
            // choice is set to the int that is returned from Validator.CheckInts
            choice = Validator.CheckInts($"Please enter a number of bombs less than {size * size}: \n", rawChoice, 1, (size * size));

            // choice is returned
            return choice;
        }

        // GetBomb returns the bomb field from the object
        public int GetBomb()
        {
            return bomb;
        }

        // SetSize gets the size input from the user, validates it, and returns the size of the grid to be used
        public int SetSize()
        {
            // rawChoice is the raw string from Console.ReadLine().
            string rawChoice;
            // int choice is the int that is returned.
            int choice;
            // int size is the width and height of the board
            int size;

            Console.WriteLine("Please select a board size:\n1) 25\n2) 49\n3)100");
            rawChoice = Console.ReadLine();
            // choice is set to the return value from Validator.CheckInts
            choice = Validator.CheckInts("Please select a board size:\n1) 25\n2) 49\n3)100", rawChoice, 1, 3);

            // size(width and height) is set based on what the user entered for choice
            if (choice == 1)
            {
                size = 5;
            }
            else if (choice == 2)
            {
                size = 7;
            }
            else
            {
                size = 10;
            }

            return size;
        }

        // GetSize returns the size variable from the object
        public int GetSize()
        {
            return size;
        }

        // drawboard draws up the player board.
        public string[,] MakeBoard()
        {
            // a new 2d array is made, with the size set to the size variable + 1. The +1 is added to account for the reference 
            // row and column
            string[,] playBoard = new string[size + 1, size + 1];

            // nested for loop iterates through the playBoard and sets values for each index accordingly.
            for (int i = 0; i < playBoard.GetLength(0); i++)
            {
                // at the end of each row, a line return is made
                Console.WriteLine();

                for (int j = 0; j < playBoard.GetLength(1); j++)
                {
                    if (i == 0)
                    {
                        // when i is 0(so when we're on the first row), each index is set to j. so, 0 - whatever the size of the array
                        // was
                        playBoard[i, j] = j.ToString();
                    }
                    else if (j == 0)
                    {
                        // if j == 0, the letters are printed down the side of the array. letters is set to 65 + i-1. 65 is the ascii 
                        // code for A, each time i goes up, it will be added to 65, making 66 for B, 67 for C and so on and so forth
                        int letters = 65 + (i - 1);
                        playBoard[i, j] = ((char)letters).ToString();
                    }
                    else
                    {
                        // if neither conditions are met, the index is set to #
                        playBoard[i, j] = "#";
                    }
                    Console.Write($" {playBoard[i, j]} ");
                }
            }
            // writeline is added to add a line return
            Console.WriteLine();
            return playBoard;
        }

        // GetChoice gets the users coordinates and action
        public int[] GetChoice()
        {
            // variables are declared to be used
            int h = size;
            string yString;
            string x;
            int y;
            string flagCheck;


            // endLetter is set to 65 + size - 1, and that is cast to a char
            char endLetter = (char)(65 + size - 1);


            // user is asked to enter a letter between A and endLetter, that input is validated and stored in x
            Console.WriteLine($"Please enter A - {endLetter} to select a row: \n");
            x = Console.ReadLine();
            x = Validator.CheckInput(x, endLetter);
            x = x.ToUpper();

            // user is asked for a nuber from 1-size. input is gathered, validated, and stored in y
            Console.WriteLine($"Please enter a number from 1-{size} to select a column: \n");
            yString = Console.ReadLine();
            y = Validator.CheckInts($"Please enter a number from 1-{size} to select a column: \n", yString, 1, size);

            // user is asked for F to flag and C to check, input is gathered, validated, and storred in flagCheck
            Console.WriteLine("Please enter F to Flag the cell you chose or C to check it: \n");
            flagCheck = Console.ReadLine();
            flagCheck = Validator.CheckFlag("Please enter F to Flag the cell you chose or C to check it: \n", flagCheck);
            flagCheck = flagCheck.ToUpper();


            // both x and flagCheck are converted to chars, and then all three inputs are stored in the int array formattedChoices
            char[] chars = x.ToCharArray();
            char charX = chars[0];
            char[] flagChars = flagCheck.ToCharArray();
            char flagChar = flagChars[0];
            int[] formattedChoices = { (int)charX, y, (int)flagChar };

            return formattedChoices;
        }

        // PrintBoard prints the board using a nested for loop
        public void PrintBoard(string[,] playBoard)
        {
            for (int i = 0; i < playBoard.GetLength(0); i++)
            {
                Console.WriteLine();

                for (int j = 0; j < playBoard.GetLength(1); j++)
                {
                    Console.Write($"   {playBoard[i, j]}   ");
                }
            }

        }

        // UpdateBoard takes in the back end board, the front end board, the choices array, and number of bombs
        public string[,] UpdateBoard(int[,] backBoard, string[,] playBoard, int[] choices, int bombs)
        {

            Board b = new Board();
            // y is set to choices[0] - 65. doing so ensures that A(65 in ascii) becomes 1, B becomes 2 and so forth
            int y = choices[0] - 64;
            // x is set to choices[1], the users input for x from earlier
            int x = choices[1];
            // flagCheck is set to choice[2] after it has been cast to a char
            char flagCheck = (char)choices[2];
            //flagsOnBombs counter is set to 0
//            int flagsOnBombs = 0;
            bool end = false;
            List<int> uChoices = new List<int>();
            //Console.WriteLine(bomb);
            uChoices.Add(x);
            uChoices.Add(y);
//            Console.WriteLine(flagsOnBombs);


            // if flagCheck is #, the value at index x,y(the point the user chose) is set to #
            if (flagCheck == '#')
            {
                playBoard[y,x] = "#";
            }
            // if the flagCheck is F and the flagCount is less than the total number of bombs, the users coords are set to ?
            else if (flagCheck == 'F')
            {
               // Console.WriteLine(flagCheck);
                playBoard[y,x] = "?";
                //if the value stored at the backBoard index is greater than or equal to 100, flagsOnBombs is incremented
                if (backBoard[y,x] >= 100)
                {
//                    Console.WriteLine("got here");
                    flagsOnBombs++;
                }
                // if the user matches flags to all the bombs, the user wins!
                if (flagsOnBombs == bombs)
                {
                    playBoard[y,x] = "!";
                    playBoard[0, 0] = "1";
                    Console.WriteLine("YOU WIN");
                    end = true;
                }
            }
            // if flagCheck == C, the cell is revealed
            else if (flagCheck == 'C')
            {
                // if the value stored in F[x,y] is greater than or equal to 100, the user is blown up
                if (backBoard[y, x] >= 100)
                {
                    string dead = SetDead(ran);
                    playBoard[y, x] = "BOOM";
                    playBoard[0, 0] = "1";
                    Console.WriteLine(dead);
                    end = true;
                }
                // if there is a hint number in the backBoard coordinates, the hint number is revealed
                else if (backBoard[y, x] >= 1 && backBoard[y, x] < 9)
                {
                    playBoard[y, x] = backBoard[y, x].ToString();
                }
                // if the space in backBoard is empty, the cell is set to a +
                else if (backBoard[y, x] == 0)
                {
                    int[,] revealed = b.RevealZero(backBoard, uChoices);

                    for (int i = 0; i < revealed.GetLength(0); i++)
                    {
                        for (int j = 0; j < revealed.GetLength(1); j++)
                        {
                            if (i + 1 < revealed.GetLength(0) && j + 1 < revealed.GetLength(1))
                            {
                                if (revealed[j, i] == 10)
                                {
                                    playBoard[j, i] = " ";
                                }
                                else if (revealed[j, i] > 10 && revealed[j, i] < 20)
                                {
                                    int tempNum = revealed[j, i];
                                    playBoard[j, i] = $"{tempNum - 10}";
                                }
                            }
                        }
                    }
                }
            }
            playBoard = WriteHeaders(playBoard, end);
            return playBoard;
        }

        public string[,] WriteHeaders(string[,] playBoard, bool end)
        {
            
            // nested for loop iterates through the playBoard and sets values for each index accordingly.
            for (int i = 0; i < playBoard.GetLength(0); i++)
            {
                // at the end of each row, a line return is made
                Console.WriteLine();

                for (int j = 0; j < playBoard.GetLength(1); j++)
                {
                    if (i == 0)
                    {
                        // when i is 0(so when we're on the first row), each index is set to j. so, 0 - whatever the size of the array
                        // was
                        playBoard[i, j] = j.ToString();
                    }
                    else if (j == 0)
                    {
                        // if j == 0, the letters are printed down the side of the array. letters is set to 65 + i-1. 65 is the ascii 
                        // code for A, each time i goes up, it will be added to 65, making 66 for B, 67 for C and so on and so forth
                        int letters = 65 + (i - 1);
                        playBoard[i, j] = ((char)letters).ToString();
                    }
                }
            }
            if (end == true)
            {
                playBoard[0, 0] = "1";
            }

            return playBoard;
        }

        public string SetDead(Random ran)
        {
            int random = ran.Next(1, 3);
            string dead = "";
            if (random == 1)
            {
                Console.WriteLine("BOMB!!!! I got good news and bad news, you lost an arm, but hey, you got another one right? You better quit while you still have a head!");
                dead = Console.ReadLine();
            }
            else if (random == 2)
            {
                Console.WriteLine("Well, your whole body blew up, so unfortunately you are dead, but hey, at least you tried! Thank you for playing, now RIP");
                dead = Console.ReadLine();
            }
            else
            {
                Console.WriteLine("I got good news and bad news, you lost a leg, but hey, you weren't much of a runner anyway! You better quit while you're ahead.");
                dead = Console.ReadLine();
            }

            return dead;
        }
        public void PrintFinalBoard(int[,] backboard)
        {

            for (int k= 0; k < (backboard.GetLength(1)-1); k++)
            {
                
                Console.Write(k+"\t");

            }
            Console.WriteLine();
            for (int m = 0; m < backboard.GetLength(1); m++)
            {

                Console.Write("______");
            }

            Console.WriteLine();
            for (int j = 1; j < (backboard.GetLength(0) - 1); j++)
            {
                int letters = 65 + (j - 1);
                Console.Write(((char)letters).ToString() + "  |\t");
                for (int i = 1; i < (backboard.GetLength(1) - 1); i++)
                {
                    
                    if (backboard[j, i] > 10 && backboard[j, i] < 100)
                    {
                        Console.Write((backboard[j, i] - 10) + "\t");
                    }
                    else if (backboard[j, i] == 0 || backboard[j, i] == 10)
                    {
                        Console.Write(" \t");
                    }
                    else if (backboard[j, i] < 10)
                    {
                        Console.Write((backboard[j, i]) + "\t");
                    }
                    else
                    {
                        Console.Write("*\t");
                    }
                }
                Console.WriteLine();
            }
            for (int n = 0; n < backboard.GetLength(1); n++)
            {

                Console.Write("______");
            }
            Console.WriteLine();
        }
    }
}
