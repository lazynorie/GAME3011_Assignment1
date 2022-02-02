using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public int width = 32;
    public int height = 32;
    public int goldcount = 3;
    public int scancount = 3;

    private Board board;
    private Cell[,] state;
    private void Awake()
    {
        board = GetComponentInChildren<Board>();
    }

    private void Start()
    {
        NewGame();
    }

    private void NewGame()
    {
        state = new Cell[width, height];

        GenerateCells();
        GenerateGold();

        Camera.main.transform.position = new Vector3(width / 2f, height / 2f, -10f);
        board.Draw(state);
    }

    private void GenerateCells()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Cell cell = new Cell();
                cell.position = new Vector3Int(x, y, 0);
                cell.type = Cell.Type.EMPTY;
                state[x, y] = cell;
            }
        }
    }
    
    private void GenerateGold()
    {
        for (int i = 0; i < goldcount; i++)
        {
            int x = Random.Range(0, width-2); 
            int y = Random.Range(0, height-2);

            while (state[x,y].type == Cell.Type.MAX)
            {
                x++;

                if (x>width)
                {
                    x = 0;
                    y++;

                    if (y > height)
                    {
                        y = 0;
                    }
                }
            }
            
            state[x, y].type = Cell.Type.MAX;
            
            //visializing for testing purpose
            state[x, y].scanned = true;
        }
    }
}
