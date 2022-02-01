using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameScript : MonoBehaviour
{
    private System.Random rand = new System.Random();
    [SerializeField]
    public GameObject[] tiles;
    public GameObject[] shuffled = new GameObject[9];
    public GameObject[,] grid = new GameObject[3,3];
    public bool gameOn = false;

    void Start()
    {
        gameObject.SetActive(false);
    }

    void ResetGame()
    {
        for (int r = 0; r < 3; r++)
        {
            for (int c = 0; c < 3; c++)
            {
                grid[r, c] = tiles[(r * 3) + c];
                grid[r, c].GetComponent<TileScript>().SetGridIndices(r, c);
                grid[r, c].GetComponent<RectTransform>().localPosition = new Vector3((c - 1) * 182, (r - 1) * -182, 0);
            }
        }
        tiles[8].SetActive(false);
        gameOn = false;
    }

    void OnEnable()
    {
        ResetGame();
    }

    public void Shuffle()
    {
        for (int i = 0; i < tiles.Length; i++)
            shuffled[i] = tiles[i];
        shuffled[8].SetActive(false);
        int p = shuffled.Length;
        for (int n = p - 1; n > 0; n--)
        {
            int r = rand.Next(0, n);
            GameObject t = shuffled[r];
            shuffled[r] = shuffled[n];
            shuffled[n] = t;
        }
        for (int r = 0; r < 3; r++)
        {
            for (int c = 0; c < 3; c++)
            {
                grid[r, c] = shuffled[(r * 3) + c];
                grid[r, c].GetComponent<TileScript>().SetGridIndices(r, c);
                grid[r, c].GetComponent<RectTransform>().localPosition = new Vector3((c - 1) * 182, (r - 1) * -182, 0);
            }
        }
        gameOn = true;
    }

    public void Solve()
    {
        ResetGame();
        tiles[8].SetActive(true);
    }

    public void SwapTiles(int r1, int c1, int r2, int c2)
    {
        GameObject tempTile = grid[r1, c1];
        grid[r1, c1] = grid[r2, c2];
        grid[r2, c2] = tempTile;
        for (int r = 0; r < 3; r++)
        {
            for (int c = 0; c < 3; c++)
            {
                grid[r, c].GetComponent<TileScript>().SetGridIndices(r, c);
                grid[r, c].GetComponent<RectTransform>().localPosition = new Vector3((c - 1) * 182, (r - 1) * -182, 0);
            }
        }
    }
}
