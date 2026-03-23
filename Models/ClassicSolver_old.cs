//using System;

//namespace SudokuSolverRazor.Models
//{
//    public class ClassicSolver : ISudokuSolver
//    {
//        private string[,] resultBoard = new string[9, 9];
//        private SudokuSolverEngine engine = new SudokuSolverEngine();

//        public bool Solve(string[,] input)
//        {
//            int[,] board = new int[9, 9];

//            // Convert string[,] input to int[,] board
//            for (int i = 0; i < 9; i++)
//            {
//                for (int j = 0; j < 9; j++)
//                {
//                    board[i, j] = int.TryParse(input[i, j], out int val) ? val : 0;
//                }
//            }

//            // Use your existing solver engine
//            if (!engine.Solve(board))
//                return false;

//            // Convert int[,] board back to string[,] resultBoard
//            for (int i = 0; i < 9; i++)
//            {
//                for (int j = 0; j < 9; j++)
//                {
//                    resultBoard[i, j] = board[i, j] == 0 ? "" : board[i, j].ToString();
//                }
//            }

//            return true;
//        }

//        public string[,] GetBoard() => resultBoard;
//    }
//}
