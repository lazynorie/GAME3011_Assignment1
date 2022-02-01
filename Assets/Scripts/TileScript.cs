using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TileScript : MonoBehaviour, IPointerClickHandler
{
    public int row, col;
    private MinigameScript mgs;
    
    void Start()
    {
        mgs = transform.parent.GetComponent<MinigameScript>();
    }

    public void SetGridIndices(int r, int c)
    {
        row = r;
        col = c;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (mgs.gameOn == false)
            return;
        if (row != 0 && mgs.grid[row - 1, col].name == "Image_BR") // Blank tile is to the north
            mgs.SwapTiles(row, col, row - 1, col);
        else if (row != 2 && mgs.grid[row + 1, col].name == "Image_BR") // Blank tile is to the south
            mgs.SwapTiles(row, col, row + 1, col);
        else if (col != 0 && mgs.grid[row, col - 1].name == "Image_BR") // Blank tile is to the west
            mgs.SwapTiles(row, col, row, col - 1);
        else if (col != 2 && mgs.grid[row, col + 1].name == "Image_BR") // Blank tile is to the east
            mgs.SwapTiles(row, col, row, col + 1);
    }
}
