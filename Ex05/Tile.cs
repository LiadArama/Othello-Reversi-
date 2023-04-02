namespace Ex02_Othelo
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class Tile
    {
        private (int, int) m_Position;
        private eNumsOfGame.eTileStates m_TileState;
        private bool m_IsValidMove;

        public (int RowValue, int ColumnValue) Position
        {
            get
            {
                return m_Position;
            }

            set
            {
                m_Position = value;
            }
        }

        public eNumsOfGame.eTileStates TileState
        {
            get
            {
                return m_TileState;
            }

            set
            {
                m_TileState = value;
            }
        }

        public bool IsValidMove
        {
            get
            {
                return this.m_IsValidMove;
            }

            set
            {
                this.m_IsValidMove = value;
            }
        }

        public Tile((int RowValues, int ColumnValues) i_Position, eNumsOfGame.eTileStates i_TileState, bool i_IsValidMove)
        {
            this.m_Position = i_Position;
            this.m_TileState = i_TileState;
            this.m_IsValidMove = i_IsValidMove;
        }
    }
}
