using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Cell
{
    public enum Type
    {
        INVALID,
        MAX,
        MED,
        MIN,
        EMPTY
    }

    public Vector3Int position;
    public Type type;

    public bool scanned;
}
