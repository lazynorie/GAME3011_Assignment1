using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasScript : MonoBehaviour
{
    [SerializeField] private GameObject minigame;
    private bool active = false;
    
    public void ToggleGame()
    {
        active = !active;
        minigame.SetActive(active);
    }
}
