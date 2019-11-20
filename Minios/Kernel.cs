using System;
using System.Collections.Generic;
using System.Text;
using Sys = Cosmos.System;

namespace Minios
{
    public enum Player
    {
        None,
        X,
        Y
    }

    public class TicTacToe
    {
        public Player[][] Board { get; set; }
        private Player currentPlayer;
        public TicTacToe()
        {
            Board = new Player[3][];
            currentPlayer = Player.X;
            for (int i = 0; i < 3; i++)
            {
                Board[i] = new Player[3];
            }

        }

        public void ShowBoard()
        {
            Console.Clear();
            for (var i = 0; i < Board.Length; i++)
            {
                for (var i1 = 0; i1 < Board[i].Length; i1++)
                {
                    var item = Board[i][i1];

                    Console.SetCursorPosition((i+1)*2,(i1+1)*2);
                    Console.Write((int)item);
                }
            }

            Console.WriteLine();
        }
        public void Play()
        {
            ShowBoard();
            MakeMove();
        }

        private void MakeMove()
        {
            Console.WriteLine($"Player {(int) currentPlayer}'s turn");
            Console.WriteLine("Make a move (column)");
            var pass = int.TryParse(Console.ReadLine(), out var x);
            if (!pass)
            {
                Console.WriteLine("Invalid Move!");
                MakeMove();
            }
            Console.WriteLine("Make a move (row)");
            var pass2 = int.TryParse(Console.ReadLine(), out var y);
            if (!pass2)
            {
                Console.WriteLine("Invalid Move!");
                MakeMove();
            }

            if (x > 2 || y > 2 || x < 0 || y < 0)
            {
                Console.WriteLine("Invalid Move!");
                MakeMove();
            }

            if ( Board[x][y] != Player.None)
            {
                Console.WriteLine("Already Taken!");
                MakeMove();
            }

            Board[x][y] = currentPlayer;
            if (currentPlayer == Player.X) currentPlayer = Player.Y;
            else currentPlayer = Player.X;
        }
    }

    public class System
    {
        private TicTacToe ticTacToe;
        public System()
        {
            ticTacToe = new TicTacToe();
        }
        public bool PlayingTicTacToe { get; set; }

        public void ShowOptions()
        {
            Console.WriteLine(@"
What do you want to play?
1. TicTacToe
2. TicTacToe
3. TicTacToe
4. TicTacToe");
            Console.ReadLine();
            PlayingTicTacToe = true;
        }
        public void Start()
        {
            if (PlayingTicTacToe)
            {
                ticTacToe.Play();
            }
            else
            {
                ShowOptions();
            }
        }
    }


    public class Kernel : Sys.Kernel
    {
        System system = new System();
        protected override void BeforeRun()
        {
            Console.WriteLine("Cosmos booted successfully. Type a line of text to get it echoed back.");
        }

        protected override void Run()
        {
            system.Start();
        }
    }
}
