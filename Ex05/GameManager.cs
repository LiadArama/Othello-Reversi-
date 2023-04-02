namespace Ex02_Othelo
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class GameManager
    {
        private Board m_Board;
        private Player m_Player1;
        private Player m_Player2;
        private Player m_CurrentPlayer;
        private bool m_IsGameOver;

        public GameManager(int i_SizeOfBoard, string i_Player1Name, eNumsOfGame.eTileStates i_Player1Color, string i_Player2Name, eNumsOfGame.eTileStates i_Player2Color)
        {
            this.m_Board = new Board(i_SizeOfBoard);
            this.m_Player1 = new Player(i_Player1Name, i_Player1Color, i_Player1Color);
            this.m_Player2 = new Player(i_Player2Name, i_Player2Color, i_Player2Color);
            this.m_CurrentPlayer = this.m_Player1;
            this.m_IsGameOver = false;
        }

        public Board Board
        {
            get
            {
                return this.m_Board;
            }
        }

        public Player Player1
        {
            get
            {
                return this.m_Player1;
            }
        }

        public Player Player2
        {
            get
            {
                return this.m_Player2;
            }
        }

        public Player CurrentPlayer
        {
            get
            {
                return this.m_CurrentPlayer;
            }

            set
            {
                this.m_CurrentPlayer = value;
            }
        }

        public bool IsGameOver
        {
            get
            {
                return this.m_IsGameOver;
            }
        }

        public void MakeMove(int i_Row, int i_Col)
        {
            if (this.m_Board.IsValidMove(i_Row, i_Col, this.m_CurrentPlayer))
            {
                this.m_Board.PlaceTile(i_Row, i_Col, this.m_CurrentPlayer.TileState);
                for (int rowToCheck = -1; rowToCheck <= 1; rowToCheck++)
                {
                    for (int colToCheck = -1; colToCheck <= 1; colToCheck++)
                    {
                        if (this.m_Board.WouldCaptureTilesInDirection(i_Row, i_Col, rowToCheck, colToCheck, this.m_CurrentPlayer))
                        {
                            this.m_Board.CaptureTilesInDirection(i_Row, i_Col, rowToCheck, colToCheck, this.m_CurrentPlayer);
                        }
                    }
                }
            }

            this.m_Player1.UpdateScore(this.m_Board.SizeOfBoard, this.m_Board.Tiles);
            this.m_Player2.UpdateScore(this.m_Board.SizeOfBoard, this.m_Board.Tiles);
            this.m_CurrentPlayer = this.m_CurrentPlayer == this.m_Player1 ? this.m_Player2 : this.m_Player1;
            this.m_IsGameOver = this.IsGameOverCheck();
        }

        public bool IsGameOverCheck()
        {
            bool gameOver = true;
            for (int row = 0; row < this.m_Board.SizeOfBoard; row++)
            {
                for (int col = 0; col < this.m_Board.SizeOfBoard; col++)
                {
                    if (this.m_Board.IsValidMove(row, col, this.m_CurrentPlayer))
                    {
                        gameOver = false;
                        break;
                    }
                }
            }

            if (gameOver)
            {
                Player otherPlayer = this.m_CurrentPlayer == this.m_Player1 ? this.m_Player2 : this.m_Player1;
                for (int row = 0; row < this.m_Board.SizeOfBoard; row++)
                {
                    for (int col = 0; col < this.m_Board.SizeOfBoard; col++)
                    {
                        if (this.m_Board.IsValidMove(row, col, otherPlayer))
                        {
                            gameOver = false;
                            break;
                        }
                    }
                }
            }

            return gameOver;
        }

        public string GetWinner()
        {
            string winner = string.Empty;
            if (this.m_Player1.GetScore > this.m_Player2.GetScore)
            {
                winner = string.Format("The winner is: {0}! ({1}/{2})", this.m_Player1.GetPlayerName, this.m_Player1.GetScore, this.m_Player2.GetScore);
            }
            else if (this.m_Player2.GetScore > this.m_Player1.GetScore)
            {
                winner = string.Format("The winner is: {0}! ({1}/{2})", this.m_Player2.GetPlayerName, this.m_Player2.GetScore, this.m_Player1.GetScore);
            }
            else
            {
                 winner = "It's a tie!";
            }

            return winner;
        }

        public List<Tuple<int, int>> GetPossibleMovesForPlayer()
        {
            List<Tuple<int, int>> possibleMoves = new List<Tuple<int, int>>();

            for (int row = 0; row < this.m_Board.SizeOfBoard; row++)
            {
                for (int col = 0; col < this.m_Board.SizeOfBoard; col++)
                {
                    if (this.m_Board.IsValidMove(row, col, m_CurrentPlayer))
                    {
                        possibleMoves.Add(new Tuple<int, int>(row, col));
                    }
                }
            }

            return possibleMoves;
        }

        public Tuple<int, int> MakeComputerMove()
        {
            List<Tuple<int, int>> possibleMoves = GetPossibleMovesForPlayer();
            Tuple<int, int> randomMoveTuple = new Tuple<int, int>(0, 0);
            if(possibleMoves != null && possibleMoves.Any())
            {
                Random makeComputerMove = new Random();
                int randomIndex = makeComputerMove.Next(0, possibleMoves.Count);
                randomMoveTuple = possibleMoves[randomIndex];
                int row = randomMoveTuple.Item1;
                int col = randomMoveTuple.Item2;
                MakeMove(row, col);
            }
            else
            {
                for(int row = 0; row < this.Board.SizeOfBoard; row++)
                {
                    for(int col = 0; col < this.Board.SizeOfBoard; col++)
                    {
                        if (this.m_Board.Tiles[row, col].TileState == eNumsOfGame.eTileStates.Empty)
                        {
                            this.m_Board.Tiles[row, col].IsValidMove = true;
                            randomMoveTuple = new Tuple<int, int>(row, col);
                        }
                    }
                }
            }

            return randomMoveTuple;
        }
    }
}
