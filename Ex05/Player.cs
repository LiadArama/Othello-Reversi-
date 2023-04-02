namespace Ex02_Othelo
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class Player
    {
        private readonly string r_PlayerName;
        private eNumsOfGame.eTileStates m_PlayerColor;
        private int m_Score;
        private eNumsOfGame.eTileStates m_TileState;

        public Player(string i_PlayerName, eNumsOfGame.eTileStates i_PlayerColor, eNumsOfGame.eTileStates i_TileState)
        {
            this.m_PlayerColor = i_PlayerColor;
            this.r_PlayerName = i_PlayerName;
            this.m_TileState = i_TileState;
            this.m_Score = 0;
        }

        public eNumsOfGame.eTileStates GetPlayerColor
        {
            get
            {
                return this.m_PlayerColor;
            }

            set
            {
                m_PlayerColor = value;
            }
        }

        public string GetPlayerName
        {
            get
            {
                return this.r_PlayerName;
            }
        }

        public int GetScore
        {
            get
            {
                return this.m_Score;
            }

            set
            {
                m_Score = value;
            }
        }

        public eNumsOfGame.eTileStates TileState
        {
            get
            {
                return this.m_TileState;
            }

            set
            {
                this.m_TileState = value;
            }
        }

        public eNumsOfGame.eTileStates GetOpponentTileState()
        {
            eNumsOfGame.eTileStates playerColor;
            if (this.TileState == eNumsOfGame.eTileStates.Black)
            {
                playerColor = eNumsOfGame.eTileStates.White;
            }
            else
            {
                playerColor = eNumsOfGame.eTileStates.Black;
            }

            return playerColor;
        }

        public eNumsOfGame.eTileStates GetOpponentPlayerColor()
        {
            eNumsOfGame.eTileStates opponentColor;
            if(this.m_PlayerColor == eNumsOfGame.eTileStates.White)
            {
                opponentColor = eNumsOfGame.eTileStates.Black;
            }
            else
            {
                opponentColor = eNumsOfGame.eTileStates.White;
            }

            return opponentColor;
        }

        public void UpdateScore(int i_BoardSize, Tile[,] io_Tiles)
        {
            this.m_Score = 0;
            for (int row = 0; row < i_BoardSize; row++)
            {
                for (int col = 0; col < i_BoardSize; col++)
                {
                    if (io_Tiles[row, col].TileState == this.TileState)
                    {
                        this.m_Score++;
                    }
                }
            }
        }
    }
}
