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
    public int scanModeCount = 6;

    private Board board;
    private Cell[,] state;

    public int resource = 0;
    public bool scanModeOn;
    
    private void Awake()
    {
        board = GetComponentInChildren<Board>();
    }

    private void Start()
    {
        print(scanModeOn);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (scancount <= 0) return;
            
            Extract();
        }
    }

    public void NewGame()
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
            int x = Random.Range(0, width - 2); 
            int y = Random.Range(0, height - 2);

            while (state[x,y].type == Cell.Type.MAX)
            {
                x++;

                if (x > width)
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
            //state[x, y].scanned = true;
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
        for (int adjacentX =-2; adjacentX <=2; adjacentX++)
        {
            for (int adjacentY =-2; adjacentY <= 2; adjacentY++)
            {

                if (adjacentX == 0 && adjacentY == 0)
                {
                    continue;
                }
                int x = cellx + adjacentX;
                int y = celly + adjacentY;
                
                if (x<0 || x > width || y < 0 || y>= height)
                {
                    continue;
                }
                
                state[x, y].type = Cell.Type.MIN;
            }
        }
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

                if (x<0 || x > width || y < 0 || y>= height)
                {
                    continue;
                }
                state[x, y].type = Cell.Type.MED;
            }
        }
        state[cellx, celly].type = Cell.Type.MAX;
    }


    private void Extract()
    {
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3Int cellPosition = board.tilemap.WorldToCell(worldPosition);
        Cell cell = GetCell(cellPosition.x, cellPosition.y);

        if (cell.type == Cell.Type.INVALID)
        {
            return;
        }

        cell.scanned = true;
        state[cellPosition.x, cellPosition.y] = cell;
        board.Draw(state);
        scancount--;

        if (cell.type == Cell.Type.MAX)
        {
            resource += 8;
        }
        else if (cell.type == Cell.Type.MED)
        {
            resource += 4;
        }
        else if (cell.type == Cell.Type.MIN)
        {
            resource += 2;
        }
        else if (cell.type == Cell.Type.EMPTY)
        {
            resource += 1;
        }
    }

    private void Scan()
    {

    }

    private Cell GetCell(int x, int y)
    {
        if (IsValid(x, y))
        {
            return state[x, y];
        }
        else
        {
            return new Cell();
        }
    }

    private bool IsValid(int x, int y)
    {
        return x >= 0 && x < width && y >= 0 && y < height;
    }

}
