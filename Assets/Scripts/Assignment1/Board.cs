using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Board : MonoBehaviour
{
    public Tilemap tilemap { get; private set; }

    public Tile tileUnknown;
    public Tile Gold;
    public Tile Silver;
    public Tile Bronze;
    public Tile Iron;
    
    private void Awake()
    {
        tilemap = GetComponent<Tilemap>();
    }

    public void Draw(Cell[,] state)
    {
        int width = state.GetLength(0);
        int height = state.GetLength(1);

        for (int x = 0; x < width; x++)
        {
            for (int y= 0; y < height; y++)
            {
                Cell cell = state[x, y];
                //tilemap.SetTile(cell.position, GetTile(cell));
            }
        }
    }

    private Tile GetTile(Cell cell)
    {
        if (cell.scanned)
        {
            return GetRevealTile(cell);
        }
        else
        {
            return tileUnknown;
        }
    }
    
    private Tile GetRevealTile(Cell cell)
    {
        switch (cell.type)
        {
            case Cell.Type.MMin: return Iron;
            case Cell.Type.MIN: return Bronze;
            case Cell.Type.MED: return Silver;
            case Cell.Type.MAX: return Gold;
            default: return null;
        }
    }
}
