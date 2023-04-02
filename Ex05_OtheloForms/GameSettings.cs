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
    public partial class GameSettings : Form
    {
        private int m_BoardSize = 6;

        public GameSettings()
        {
            InitializeComponent();
        }

        private void ButtonChangeBoardSize_Click()
        {
            this.m_BoardSize += 2;
            if (this.m_BoardSize > 12)
            {
                this.m_BoardSize = 6;
            }
        }

        // $G$ CSS-999 (-3) Private methods should start with a lower-case letter.
        private void ButtonBoardSize_Click(object sender, EventArgs e)
        {
            ButtonChangeBoardSize_Click();
            Button btnBoardSize = sender as Button;
            string buttonText = string.Format("Board Size:{0}x{1} (Click To Increase Board Size)", m_BoardSize, m_BoardSize);
            btnBoardSize.Text = buttonText;
        }

        // $G$ CSS-999 (-3) Private methods should start with a lower-case letter.
        private void ButtonSingleplayer_Click(object sender, EventArgs e)
        {
            GameBoard newGame = new GameBoard(this.m_BoardSize, true);
            this.Hide();
            newGame.Show();
            newGame.Activate();
        }

        // $G$ CSS-999 (-3) Private methods should start with a lower-case letter.
        private void ButtonMultiplayer_Click(object sender, EventArgs e)
        {
            GameBoard newGame = new GameBoard(this.m_BoardSize, false);
            this.Hide();
            newGame.Show();
            newGame.Activate();
        }

        // $G$ DSN-999 (-1) Not in use method - should have been removed.
        private void GameSettings_Load(object sender, EventArgs e)
        {
        }
    }
}
