using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerOption
{
    NONE, //0
    X, // 1
    O // 2
}

public class TTT : MonoBehaviour
{
    public int Rows;
    public int Columns;
    [SerializeField] BoardView board;

    PlayerOption currentPlayer = PlayerOption.X;
    Cell[,] cells;

    public bool cornerTaken = false;
    public int takenCornerX;
    public int takenCornerY;

    // Start is called before the first frame update
    void Start()
    {
        cells = new Cell[Columns, Rows];

        board.InitializeBoard(Columns, Rows);

        for(int i = 0; i < Rows; i++)
        {
            for(int j = 0; j < Columns; j++)
            {
                cells[j, i] = new Cell();
                cells[j, i].current = PlayerOption.NONE;
            }
        }
    }

    public void MakeOptimalMove()
    {
        //Winning
        //rows
        int playerScore = 0;
        for(int i = 0;i < Rows; i++)
        {
            for (int j = 0;j < Columns; j++)
            {
                if (cells[j, i].current == currentPlayer)
                {
                    playerScore++;
                    if (playerScore >= 2)
                    {
                        if (j == Columns-1)
                        {
                            if (cells[0, i].current == PlayerOption.NONE)
                            {
                                ChooseSpace(0, i);
                                return;
                            }
                            else if (cells[1,i].current == PlayerOption.NONE)
                            {
                                ChooseSpace(1, i);
                                return;
                            }
                        }
                        else if(cells[j+1,i].current == PlayerOption.NONE)
                        {
                            ChooseSpace(j + 1, i);
                            return;
                        }
                    }
                }
            }
            playerScore = 0;
        }
        //columbs
        playerScore = 0;
        for (int j = 0; j < Columns; j++)
        {
            for (int i = 0; i < Rows; i++)
            {
                if (cells[j, i].current == currentPlayer)
                {
                    playerScore++;
                    if (playerScore >= 2)
                    {
                        if (i == Rows - 1)
                        {
                            if (cells[j,0].current == PlayerOption.NONE)
                            {
                                ChooseSpace(j,0);
                                return;
                            }
                            else if (cells[j,1].current == PlayerOption.NONE)
                            {
                                ChooseSpace(j,1);
                                return;
                            }
                        }
                        else if (cells[j, i + 1].current == PlayerOption.NONE)
                        {
                            ChooseSpace(j, i + 1);
                            return;
                        }
                    }
                }
            }
            playerScore = 0;
        }
        //Diagonal
        //left->right
        playerScore = 0;
        for(int i = 0; i < Rows; i++)
        {
            if (cells[i,i].current == currentPlayer)
            {
                playerScore++;
                if(playerScore >= 2)
                {
                    if(i == Rows - 1)
                    {
                        if (cells[0,0].current == PlayerOption.NONE)
                        {
                            ChooseSpace(0,0);
                            return;
                        }
                        else if (cells[1,1].current == PlayerOption.NONE)
                        {
                            ChooseSpace(1, 1);
                            return;
                        }
                    }
                    else if (cells[i + 1, i + 1].current == PlayerOption.NONE)
                    {
                        ChooseSpace(i+1, i + 1);
                        return;
                    }
                }
            }
        }
        //right->left
        playerScore = 0;
        for (int i = 0; i < Rows; i++)
        {
            if (cells[Columns - 1 - i, i].current == currentPlayer)
            {
                playerScore++;
                if (playerScore >= 2)
                {
                    if (i == Rows - 1)
                    {
                        if (cells[2, 0].current == PlayerOption.NONE)
                        {
                            ChooseSpace(2, 0);
                            return;
                        }
                        if (cells[1, 1].current == PlayerOption.NONE)
                        {
                            ChooseSpace(1, 1);
                            return;
                        }
                    }
                    else if (cells[Columns - 1 - (i+1),i+1].current == PlayerOption.NONE)
                    {
                        ChooseSpace(Columns - 1 - (i+1), i+1);
                        return ;
                    }
                }
            }
        }

        //Blocking
        //rows
        playerScore = 0;
        for (int i = 0; i < Rows; i++)
        {
            for (int j = 0; j < Columns; j++)
            {
                if (cells[j, i].current != currentPlayer && cells[j,i].current != PlayerOption.NONE)
                {
                    playerScore++;
                    if (playerScore >= 2)
                    {
                        if (j == Columns - 1)
                        {
                            if (cells[0, i].current == PlayerOption.NONE)
                            {
                                ChooseSpace(0, i);
                                return;
                            }
                            else if (cells[1, i].current == PlayerOption.NONE)
                            {
                                ChooseSpace(1, i);
                                return;
                            }
                        }
                        else if (cells[j + 1, i].current == PlayerOption.NONE)
                        {
                            ChooseSpace(j + 1, i);
                            return ;
                        }
                    }
                }
            }
            playerScore = 0;
        }
        //columbs
        playerScore = 0;
        for (int j = 0; j < Columns; j++)
        {
            for (int i = 0; i < Rows; i++)
            {
                if (cells[j, i].current != currentPlayer && cells[j,i].current != PlayerOption.NONE)
                {
                    playerScore++;
                    if (playerScore >= 2)
                    {
                        if (i == Rows - 1)
                        {
                            if (cells[j, 0].current == PlayerOption.NONE)
                            {
                                ChooseSpace(j, 0);
                                return;
                            }
                            else if (cells[j, 1].current == PlayerOption.NONE)
                            {
                                ChooseSpace(j, 1);
                                return;
                            }
                        }
                        else if (cells[j, i + 1].current == PlayerOption.NONE)
                        {
                            ChooseSpace(j, i + 1);
                            return;
                        }
                    }
                }
            }
            playerScore = 0;
        }
        //diagonal
        //left->Right
        playerScore = 0;
        for (int i = 0; i < Rows; i++)
        {
            if (cells[i, i].current != currentPlayer && cells[i,i].current != PlayerOption.NONE)
            {
                playerScore++;
                if (playerScore >= 2)
                {
                    if (i == Rows - 1)
                    {
                        if (cells[0, 0].current == PlayerOption.NONE)
                        {
                            ChooseSpace(0, 0);
                            return ;
                        }
                        else if (cells[1, 1].current == PlayerOption.NONE)
                        {
                            ChooseSpace(1, 1);
                            return ;
                        }
                    }
                    else if (cells[i + 1, i + 1].current == PlayerOption.NONE)
                    {
                        ChooseSpace(i + 1, i + 1);
                        return ;
                    }
                }
            }
        }
        //right->left
        playerScore = 0;
        for (int i = 0; i < Rows; i++)
        {
            if (cells[Columns - 1 - i, i].current != currentPlayer && cells[Columns -1-i,i].current != PlayerOption.NONE)
            {
                playerScore++;
                if (playerScore >= 2)
                {
                    if (i == Rows - 1)
                    {
                        if (cells[2, 0].current == PlayerOption.NONE)
                        {
                            ChooseSpace(2, 0);
                            return ;
                        }
                        if (cells[1, 1].current == PlayerOption.NONE)
                        {
                            ChooseSpace(1, 1);
                            return ;
                        }
                    }
                    else if (cells[Columns - 1 - (i + 1), i + 1].current == PlayerOption.NONE)
                    {
                        ChooseSpace(Columns - 1 - (i + 1), i + 1);
                        return ;
                    }
                }
            }
        }

        //best move
        if (cornerTaken == false)
        {
            if (cells[0, 0].current != PlayerOption.NONE)//leftTop
            {
                takenCornerX = 0;
                takenCornerY = 0;
                cornerTaken = true;

            }
            else if (cells[2, 0].current != PlayerOption.NONE)//rightTop
            {
                takenCornerX = 2;
                takenCornerY = 0;
                cornerTaken = true;
            }
            else if (cells[0, 2].current != PlayerOption.NONE)//botLeft
            {
                takenCornerX = 0;
                takenCornerY = 2;
                cornerTaken = true;
            }
            else if (cells[2, 2].current != PlayerOption.NONE)//botRight
            {
                takenCornerX = 2;
                takenCornerY = 2;
                cornerTaken = true;
            }
            else
            {
                ChooseSpace(0, 0);
                takenCornerX = 0;
                takenCornerY = 0;
                cornerTaken = true;
                return;
            }
        }
        //pick center
        if (cells[1, 1].current == PlayerOption.NONE)
        {
            ChooseSpace(1, 1);
            return;
        }
        //left corner
        if (takenCornerX == 0)
        {
            if (takenCornerY == 0)
            {
                if (cells[takenCornerX + 1, takenCornerY].current == PlayerOption.NONE)
                {
                    ChooseSpace(takenCornerX + 1, takenCornerY);
                    return;
                }
                else if (cells[takenCornerX, takenCornerY + 1].current == PlayerOption.NONE)
                {
                    ChooseSpace(takenCornerX, takenCornerY + 1);
                    return;
                }
            }
            else if (takenCornerY == 2)
            {
                if (cells[takenCornerX + 1, takenCornerY].current == PlayerOption.NONE)
                {
                    ChooseSpace(takenCornerX + 1, takenCornerY);
                    return;
                }
                else if (cells[takenCornerX, takenCornerY - 1].current == PlayerOption.NONE)
                {
                    ChooseSpace(takenCornerX, takenCornerY - 1);
                }
            }
        }
        //right corner
        else if (takenCornerX == 2)
        {
            if (takenCornerY == 0)
            {
                if (cells[takenCornerX - 1, takenCornerY].current == PlayerOption.NONE)
                {
                    ChooseSpace(takenCornerX - 1, takenCornerY);
                    return;
                }
                else if (cells[takenCornerX, takenCornerY + 1].current == PlayerOption.NONE)
                {
                    ChooseSpace(takenCornerX, takenCornerY + 1);
                    return;
                }
            }
            else if (takenCornerY == 2)
            {
                if (cells[takenCornerX - 1, takenCornerY].current == PlayerOption.NONE)
                {
                    ChooseSpace(takenCornerX - 1, takenCornerY);
                    return;
                }
                else if (cells[takenCornerX, takenCornerY - 1].current == PlayerOption.NONE)
                {
                    ChooseSpace(takenCornerX, takenCornerY - 1);
                    return;
                }
            }
        }

        //last move
        for (int i = 0; i < Rows; i++)
        {
            for (int j = 0; j < Columns; j++)
            {
                if (cells[j, i].current == PlayerOption.NONE)
                {
                    ChooseSpace(j, i);
                    return;
                }
            }
        }
    }

    public void ChooseSpace(int column, int row)
    {
        // can't choose space if game is over
        if (GetWinner() != PlayerOption.NONE)
            return;

        // can't choose a space that's already taken
        if (cells[column, row].current != PlayerOption.NONE)
            return;

        // set the cell to the player's mark
        cells[column, row].current = currentPlayer;

        // update the visual to display X or O
        board.UpdateCellVisual(column, row, currentPlayer);

        // if there's no winner, keep playing, otherwise end the game
        if(GetWinner() == PlayerOption.NONE)
            EndTurn();
        else
        {
            Debug.Log("GAME OVER!");
        }
    }

    public void EndTurn()
    {
        // increment player, if it goes over player 2, loop back to player 1
        currentPlayer += 1;
        if ((int)currentPlayer > 2)
            currentPlayer = PlayerOption.X;
    }

    public PlayerOption GetWinner()
    {
        // sum each row/column based on what's in each cell X = 1, O = -1, blank = 0
        // we have a winner if the sum = 3 (X) or -3 (O)
        int sum = 0;

        // check rows
        for (int i = 0; i < Rows; i++)
        {
            sum = 0;
            for (int j = 0; j < Columns; j++)
            {
                var value = 0;
                if (cells[j, i].current == PlayerOption.X)
                    value = 1;
                else if (cells[j, i].current == PlayerOption.O)
                    value = -1;

                sum += value;
            }

            if (sum == 3)
                return PlayerOption.X;
            else if (sum == -3)
                return PlayerOption.O;

        }

        // check columns
        for (int j = 0; j < Columns; j++)
        {
            sum = 0;
            for (int i = 0; i < Rows; i++)
            {
                var value = 0;
                if (cells[j, i].current == PlayerOption.X)
                    value = 1;
                else if (cells[j, i].current == PlayerOption.O)
                    value = -1;

                sum += value;
            }

            if (sum == 3)
                return PlayerOption.X;
            else if (sum == -3)
                return PlayerOption.O;

        }

        // check diagonals
        // top left to bottom right
        sum = 0;
        for(int i = 0; i < Rows; i++)
        {
            int value = 0;
            if (cells[i, i].current == PlayerOption.X)
                value = 1;
            else if (cells[i, i].current == PlayerOption.O)
                value = -1;

            sum += value;
        }

        if (sum == 3)
            return PlayerOption.X;
        else if (sum == -3)
            return PlayerOption.O;

        // top right to bottom left
        sum = 0;
        for (int i = 0; i < Rows; i++)
        {
            int value = 0;

            if (cells[Columns - 1 - i, i].current == PlayerOption.X)
                value = 1;
            else if (cells[Columns - 1 - i, i].current == PlayerOption.O)
                value = -1;

            sum += value;
        }

        if (sum == 3)
            return PlayerOption.X;
        else if (sum == -3)
            return PlayerOption.O;

        return PlayerOption.NONE;
    }
}
