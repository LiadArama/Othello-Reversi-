namespace Ex02_Othelo
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class Board
    {
        private int m_SizeOfBoard;
        private Tile[,] m_Tiles;

        public Board(int i_SizeOfBoard)
        {
            this.m_SizeOfBoard = i_SizeOfBoard;
            m_Tiles = new Tile[m_SizeOfBoard, m_SizeOfBoard];
            for (int row = 0; row < m_SizeOfBoard; row++)
            {
                for(int col = 0; col < m_SizeOfBoard; col++)
                {
                    m_Tiles[row, col] = new Tile((row, col), eNumsOfGame.eTileStates.Empty, false);
                }
            }

            m_Tiles[(i_SizeOfBoard / 2) - 1, (i_SizeOfBoard / 2) - 1].TileState = eNumsOfGame.eTileStates.White;
            m_Tiles[(i_SizeOfBoard / 2) - 1, i_SizeOfBoard / 2].TileState = eNumsOfGame.eTileStates.Black;
            m_Tiles[i_SizeOfBoard / 2, i_SizeOfBoard / 2].TileState = eNumsOfGame.eTileStates.White;
            m_Tiles[i_SizeOfBoard / 2, (i_SizeOfBoard / 2) - 1].TileState = eNumsOfGame.eTileStates.Black;
        }

        public void PlaceTile(int i_Row, int i_Col, eNumsOfGame.eTileStates i_tileState)
        {
            m_Tiles[i_Row, i_Col].TileState = i_tileState;
        }

        public Tile[,] Tiles
        {
            get
            {
                return this.m_Tiles;
            }
        }

        public int SizeOfBoard
        {
            get
            {
                return this.m_SizeOfBoard;
            }

            set
            {
                m_SizeOfBoard = value;
            }
        }

        public bool IsValidMove(int i_Row, int i_Col, Player io_Player)
        {
            bool isValid = false;
            if (this.Tiles[i_Row, i_Col].TileState == eNumsOfGame.eTileStates.Empty)
            {
                for (int rowToCheck = -1; rowToCheck <= 1; rowToCheck++)
                {
                    for (int colToCheck = -1; colToCheck <= 1; colToCheck++)
                    {
                        if (rowToCheck == 0 && colToCheck == 0)
                        {
                            continue;
                        }

                        int rowToMove = i_Row + rowToCheck;
                        int colToMove = i_Col + colToCheck;
                        if (rowToMove >= 0 && colToMove >= 0 && rowToMove < this.SizeOfBoard && colToMove < this.SizeOfBoard)
                        {
                            if (this.Tiles[rowToMove, colToMove].TileState == io_Player.GetOpponentTileState())
                            {
                                while (rowToMove >= 0 && rowToMove < this.SizeOfBoard && colToMove >= 0 && colToMove < this.SizeOfBoard)
                                {
                                    rowToMove += rowToCheck;
                                    colToMove += colToCheck;
                                    if (rowToMove >= 0 && rowToMove < this.SizeOfBoard && colToMove >= 0 && colToMove < this.SizeOfBoard)
                                    {
                                        if (this.Tiles[rowToMove, colToMove].TileState == io_Player.TileState)
                                        {
                                            isValid = true;
                                            break;
                                        }

                                        if (this.Tiles[rowToMove, colToMove].TileState == eNumsOfGame.eTileStates.Empty)
                                        {
                                            break;
                                        }
                                    }
                                    else
                                    {
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return isValid;
        }

        public bool WouldCaptureTilesInDirection(int i_Row, int i_Col, int i_RowToCheck, int i_ColToCheck, Player io_Player)
        {
            bool wouldCapture = false;
            bool oponnentDiskNotFound = true;
            int currentRow = i_Row + i_RowToCheck;
            int currentColumn = i_Col + i_ColToCheck;
            eNumsOfGame.eTileStates i_OpponentColor = io_Player.GetPlayerColor == eNumsOfGame.eTileStates.Black ? eNumsOfGame.eTileStates.White : eNumsOfGame.eTileStates.Black;
            if (currentRow < 0 || currentRow >= m_SizeOfBoard || currentColumn < 0 || currentColumn >= m_SizeOfBoard)
            {
                wouldCapture = false;
            }

            if (currentRow < 0 || currentRow >= m_SizeOfBoard || currentColumn < 0 || currentColumn >= m_SizeOfBoard)
            {
                wouldCapture = false;
            }
            else if (m_Tiles[currentRow, currentColumn].TileState != i_OpponentColor)
            {
                wouldCapture = false;
            }

            while (oponnentDiskNotFound)
            {
                currentRow += i_RowToCheck;
                currentColumn += i_ColToCheck;
                if (currentRow < 0 || currentRow >= m_SizeOfBoard || currentColumn < 0 || currentColumn >= m_SizeOfBoard)
                {
                    oponnentDiskNotFound = false;
                    wouldCapture = false;
                }

                if (currentRow < 0 || currentRow >= m_SizeOfBoard || currentColumn < 0 || currentColumn >= m_SizeOfBoard)
                {
                    wouldCapture = false;
                }
                else if (m_Tiles[currentRow, currentColumn].TileState == io_Player.GetPlayerColor)
                {
                    oponnentDiskNotFound = false;
                    wouldCapture = true;
                }

                if (currentRow < 0 || currentRow >= m_SizeOfBoard || currentColumn < 0 || currentColumn >= m_SizeOfBoard)
                {
                    wouldCapture = false;
                }
                else if (m_Tiles[currentRow, currentColumn].TileState == eNumsOfGame.eTileStates.Empty)
                {
                    oponnentDiskNotFound = false;
                    wouldCapture = false;
                }
            }

            return wouldCapture;
        }

        public void CaptureTilesInDirection(int i_Row, int i_Col, int i_RowToCheck, int i_ColToCheck, Player io_Player)
        {
            if (!((i_RowToCheck == 0) && (i_ColToCheck == 0)) && WouldCaptureTilesInDirection(i_Row, i_Col, i_RowToCheck, i_ColToCheck, io_Player))
            {
                int currentRow = i_Row + i_RowToCheck;
                int currentCol = i_Col + i_ColToCheck;
                eNumsOfGame.eTileStates opponentTileState = io_Player.GetOpponentTileState();
                while (currentRow >= 0 && currentRow < this.m_Tiles.GetLength(0) && currentCol >= 0 &&
                    currentCol < this.m_Tiles.GetLength(0) && this.m_Tiles[currentRow, currentCol].TileState == opponentTileState)
                {
                    this.m_Tiles[currentRow, currentCol].TileState = io_Player.TileState;
                    currentRow += i_RowToCheck;
                    currentCol += i_ColToCheck;
                }
            }
        }

        public bool IsValidMoveExists(Player io_Player)
        {
            bool isValidMoveExists = false;
            for (int row = 0; row < m_SizeOfBoard; row++)
            {
                for (int col = 0; col < m_SizeOfBoard; col++)
                {
                    if (IsValidMove(row, col, io_Player))
                    {
                        isValidMoveExists = true;
                        break;
                    }
                }
            }

            return isValidMoveExists;
        }
    }
}