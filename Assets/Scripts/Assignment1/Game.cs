using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Game : MonoBehaviour
{
    public int width = 32;
    public int height = 32;
    public int goldcount = 3;
    public int scancount = 3;

    private Board board;
    private Cell[,] state;

    public int resource = 0;
    
    
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
        GenerateSurroundingResource();

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
                
                //reveal all maps for now
                state[x, y].scanned = true;
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

    public void GenerateSurroundingResource()
    {
       
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Cell cell = state[x, y];

                if (cell.type == Cell.Type.MAX)
                {
                    fillSurroundings(x, y);
                }
            }
        }
    }

    public void fillSurroundings(int cellx, int celly)
    {
        for (int adjacentX =-1; adjacentX <=1; adjacentX++)
        {
            for (int adjacentY =-1; adjacentY <= 1; adjacentY++)
            {

                if (adjacentX == 0 && adjacentY == 0)
                {
                    continue;
                }
                int x = cellx + adjacentX;
                int y = celly + adjacentY;
                
                state[x, y].type = Cell.Type.MED;
            }
        }
        state[cellx, celly].type = Cell.Type.MAX;
    }

    public void ClickToMine()
    {
        
    }

    private void OnMouseDown()
    {
        
    }
}
