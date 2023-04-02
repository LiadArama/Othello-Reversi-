using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ex05_OtheloForms
{
    public partial class GameBoard : Form
    {
        private int m_ChosenBoardSize;
        // $G$ CSS-999 (-3) Bad member variable name - should be in the form of m_PascalCase.
        private Button[,] arrayOfButtons;
        private Ex02_Othelo.GameManager m_GameManager;
        // $G$ CSS-999 (-3) Bad member variable name - should be in the form of m_PascalCase.
        // $G$ DSN-999 (-3) This list should have been readonly.
        private List<Tuple<int, int>> possibleMoves = new List<Tuple<int, int>>();
        // $G$ CSS-999 (-3) Bad member variable name - should be in the form of m_PascalCase.
        private int NumberOfWinsForBlack = 0;
        // $G$ CSS-999 (-3) Bad member variable name - should be in the form of m_PascalCase.
        private int NumberOfWinsForWhite = 0;
        private bool m_AgainstComputer = true;

        public GameBoard(int i_BoardSize, bool i_AgainstComputer)
        {
            InitializeComponent();
            this.m_ChosenBoardSize = i_BoardSize;
            this.m_AgainstComputer = i_AgainstComputer;
            MakeBoardBySize();
        }

        private void MakeBoardBySize()
        {
            this.Text = "Othello - Black's Turn";
            this.m_GameManager = new Ex02_Othelo.GameManager
                (
                m_ChosenBoardSize,
                "Black",
                Ex02_Othelo.eNumsOfGame.eTileStates.Black,
                "White",
                Ex02_Othelo.eNumsOfGame.eTileStates.White);
            InitializeButtons();
            InitializeButtonColors();
            SetValidMovesOnBoard();
        }

        // $G$ CSS-999 (-3) Private methods should start with a lower-case letter.
        private void InitializeButtons()
        {
            int buttonSize = (this.ClientSize.Width / 6) - 2;
            this.arrayOfButtons = new Button[m_ChosenBoardSize, m_ChosenBoardSize];
            for (int rowIdx = 0; rowIdx < m_ChosenBoardSize; rowIdx++)
            {
                for (int colIdx = 0; colIdx < m_ChosenBoardSize; colIdx++)
                {
                    this.arrayOfButtons[rowIdx, colIdx] = new Button();
                    this.arrayOfButtons[rowIdx, colIdx].Size = new Size(60, 60);
                    this.arrayOfButtons[rowIdx, colIdx].Location = new Point((buttonSize - 70) * colIdx, (buttonSize - 70) * rowIdx);
                    this.arrayOfButtons[rowIdx, colIdx].Click += new EventHandler(BoardButton_Click);
                    this.Controls.Add(arrayOfButtons[rowIdx, colIdx]);
                }
            }
        }

        private void InitializeButtonColors()
        {
            for (int rowIdx = 0; rowIdx < m_ChosenBoardSize; rowIdx++)
            {
                for (int colIdx = 0; colIdx < m_ChosenBoardSize; colIdx++)
                {
                    if (this.m_GameManager.Board.Tiles[rowIdx, colIdx].TileState == Ex02_Othelo.eNumsOfGame.eTileStates.Black)
                    {
                        this.arrayOfButtons[rowIdx, colIdx].Text = "O";
                        this.arrayOfButtons[rowIdx, colIdx].BackColor = Color.Black;
                        this.arrayOfButtons[rowIdx, colIdx].ForeColor = Color.White;
                        this.arrayOfButtons[rowIdx, colIdx].Font = new Font("Arial", 24, FontStyle.Bold);
                    }
                    else if (this.m_GameManager.Board.Tiles[rowIdx, colIdx].TileState == Ex02_Othelo.eNumsOfGame.eTileStates.White)
                    {
                        this.arrayOfButtons[rowIdx, colIdx].Text = "O";
                        this.arrayOfButtons[rowIdx, colIdx].BackColor = Color.White;
                        this.arrayOfButtons[rowIdx, colIdx].ForeColor = Color.Black;
                        this.arrayOfButtons[rowIdx, colIdx].Font = new Font("Arial", 24, FontStyle.Bold);
                    }
                }
            }
        }

        private void SetValidMovesOnBoard()
        {
            for (int rowIdx = 0; rowIdx < m_ChosenBoardSize; rowIdx++)
            {
                for (int colIdx = 0; colIdx < m_ChosenBoardSize; colIdx++)
                {
                    if (m_GameManager.Board.Tiles[rowIdx, colIdx].TileState == Ex02_Othelo.eNumsOfGame.eTileStates.Empty)
                    {
                        this.arrayOfButtons[rowIdx, colIdx].Enabled = false;
                    }
                    else
                    {
                        this.arrayOfButtons[rowIdx, colIdx].Enabled = true;
                    }
                }
            }

            this.possibleMoves = m_GameManager.GetPossibleMovesForPlayer();
            foreach (Tuple<int, int> indexes in possibleMoves)
            {
                this.arrayOfButtons[indexes.Item1, indexes.Item2].BackColor = Color.Green;
                this.arrayOfButtons[indexes.Item1, indexes.Item2].Enabled = true;
            }
        }

        private void ChangeCurrentButtonColor(Button i_CurrentButton)
        {
            if (i_CurrentButton.BackColor == Color.Green && m_GameManager.CurrentPlayer == m_GameManager.Player1)
            {
                i_CurrentButton.BackColor = Color.Black;
                i_CurrentButton.Text = "O";
                i_CurrentButton.ForeColor = Color.White;
                i_CurrentButton.Font = new Font("Arial", 24, FontStyle.Bold);
            }
            else if (i_CurrentButton.BackColor == Color.Green && m_GameManager.CurrentPlayer == m_GameManager.Player2)
            {
                i_CurrentButton.BackColor = Color.White;
                i_CurrentButton.Text = "O";
                i_CurrentButton.ForeColor = Color.Black;
                i_CurrentButton.Font = new Font("Arial", 24, FontStyle.Bold);
            }
        }

        private void ChangeGameManagerTilesState(Button i_CurrentButton)
        {
            for (int rowIdx = 0; rowIdx < m_ChosenBoardSize; rowIdx++)
            {
                for (int colIdx = 0; colIdx < m_ChosenBoardSize; colIdx++)
                {
                    if (this.arrayOfButtons[rowIdx, colIdx] == i_CurrentButton)
                    {
                        this.m_GameManager.MakeMove(rowIdx, colIdx);
                        break;
                    }
                }
            }
        }

        private void ChangeCapturedTilesOnUI()
        {
            for (int rowIdx = 0; rowIdx < m_ChosenBoardSize; rowIdx++)
            {
                for (int colIdx = 0; colIdx < m_ChosenBoardSize; colIdx++)
                {
                    if (m_GameManager.Board.Tiles[rowIdx, colIdx].TileState == Ex02_Othelo.eNumsOfGame.eTileStates.White)
                    {
                        this.arrayOfButtons[rowIdx, colIdx].BackColor = Color.White;
                        this.arrayOfButtons[rowIdx, colIdx].ForeColor = Color.Black;
                    }
                    else if (m_GameManager.Board.Tiles[rowIdx, colIdx].TileState == Ex02_Othelo.eNumsOfGame.eTileStates.Black)
                    {
                        this.arrayOfButtons[rowIdx, colIdx].BackColor = Color.Black;
                        this.arrayOfButtons[rowIdx, colIdx].ForeColor = Color.White;
                    }
                }
            }
        }

        private void UpdateBoardState(Button i_CurrentButton)
        {
            foreach (Tuple<int, int> indexes in this.possibleMoves)
            {
                if (i_CurrentButton != this.arrayOfButtons[indexes.Item1, indexes.Item2])
                {
                    this.arrayOfButtons[indexes.Item1, indexes.Item2].BackColor = Color.Transparent;
                    this.arrayOfButtons[indexes.Item1, indexes.Item2].Enabled = false;
                }
            }

            SetValidMovesOnBoard();
            this.Text = "Othello - " + m_GameManager.CurrentPlayer.GetPlayerName + "'s Turn";
        }

        private void NotifyForNoMoves(Button i_CurrentButton)
        {
            if (possibleMoves.Count == 0)
            {
                MessageBox.Show(string.Format("No Valid Moves For {0}!{1}Switcing Turns", m_GameManager.CurrentPlayer.GetPlayerName, Environment.NewLine));
                m_GameManager.CurrentPlayer = m_GameManager.CurrentPlayer == m_GameManager.Player1 ? m_GameManager.Player2 : m_GameManager.Player1;
                UpdateBoardState(i_CurrentButton);
            }
        }

        // $G$ NTT-999 (-10) You should have use: Environment.NewLine instead of "\n".
        private void AnotherGame(object sender, EventArgs e, int i_NumberOfWins)
        {
            string winner = m_GameManager.GetWinner();
            string numberOfWinsAsString = string.Format(" ({0}/3)", i_NumberOfWins);
            string message = winner + numberOfWinsAsString + "\nDo you wish to play another game?";
            string title = "Othello";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result = MessageBox.Show(message, title, buttons);
            if (result == DialogResult.Yes)
            {
                this.Controls.Clear();
                this.InitializeComponent();
                this.MakeBoardBySize();
                this.GameBoard_Load(sender, e);
            }
            else
            {
                MessageBox.Show(string.Format("Thanks For Playing!"));
                Application.Exit();
            }
        }

        private void BoardButton_Click(object sender, EventArgs e)
        {
            Button currButton = sender as Button;
            if (!m_AgainstComputer)
            {
                if (currButton.BackColor == Color.Green)
                {
                    ChangeCurrentButtonColor(currButton);
                    ChangeGameManagerTilesState(currButton);
                    ChangeCapturedTilesOnUI();
                    UpdateBoardState(currButton);
                    if (!m_GameManager.IsGameOverCheck())
                    {
                        NotifyForNoMoves(currButton);
                    }
                    else
                    {
                        DeclareWinner(sender, e);
                    }
                }
            }
            else
            {
                if (currButton.BackColor == Color.Green)
                {
                    this.m_GameManager.CurrentPlayer = this.m_GameManager.Player1;
                    ChangeCurrentButtonColor(currButton);
                    ChangeGameManagerTilesState(currButton);
                    ChangeCapturedTilesOnUI();
                    UpdateBoardState(currButton);
                    if(!this.m_GameManager.IsGameOverCheck())
                    {
                        NotifyForNoMoves(currButton);
                    }
                    else
                    {
                        DeclareWinner(sender, e);
                    }

                    m_GameManager.CurrentPlayer = m_GameManager.Player2;
                    if(!this.m_GameManager.IsGameOverCheck())
                    {
                        Tuple<int, int> computerMove = m_GameManager.MakeComputerMove();
                        currButton = this.arrayOfButtons[computerMove.Item1, computerMove.Item2];
                        currButton.BackColor = Color.White;
                        currButton.ForeColor = Color.Black;
                        currButton.Text = "O";
                        currButton.Font = new Font("Arial", 24, FontStyle.Bold);
                        ChangeCapturedTilesOnUI();
                        UpdateBoardState(currButton);
                        if (!this.m_GameManager.IsGameOverCheck())
                        {
                            NotifyForNoMoves(currButton);
                        }
                        else
                        {
                            DeclareWinner(sender, e);
                        }
                    }
                    else
                    {
                        DeclareWinner(sender, e);
                    }
                }
            }
        }

        private void DeclareWinner(object sender, EventArgs e)
        {
            if (this.NumberOfWinsForWhite == 3 || this.NumberOfWinsForBlack == 3)
            {
                this.NumberOfWinsForBlack = 0;
                this.NumberOfWinsForWhite = 0;
            }

            if (m_GameManager.CurrentPlayer == m_GameManager.Player1)
            {
                this.NumberOfWinsForBlack++;
                AnotherGame(sender, e, this.NumberOfWinsForBlack);
                this.m_GameManager.CurrentPlayer = this.m_GameManager.Player1;
            }
            else
            {
                this.NumberOfWinsForWhite++;
                AnotherGame(sender, e, this.NumberOfWinsForWhite);
                this.m_GameManager.CurrentPlayer = this.m_GameManager.Player1;
            }
        }

        // $G$ DSN-999 (-1) Not in use method - should have been removed.
        private void GameBoard_Load(object sender, EventArgs e)
        {
        }
    }
}