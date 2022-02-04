using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIBehaviour : MonoBehaviour
{
    public Game game;

    [Header("TMPro")]
    public TextMeshProUGUI resourceGathered;
    public TextMeshProUGUI currentMode;
    public TextMeshProUGUI extractCounter;
    public TextMeshProUGUI scanCounter;

    private string getCurrentMode;

    // Start is called before the first frame update
    void Start()
    {
        getCurrentMode = "EXTRACT";
    }

    // Update is called once per frame
    void Update()
    {
        resourceGathered.text = "Resource collected: " + game.resource;
        currentMode.text = "Current Mode: " + getCurrentMode;
        extractCounter.text = "Extract left: " + game.scancount;
        scanCounter.text = "Scan left: " + game.scanModeCount;
    }

    public void StartNewGame()
    {
        game.NewGame();
    }

    public void SwitchMode()
    {
        game.scanModeOn = !game.scanModeOn;
        if (game.scanModeOn == true)
        {
            getCurrentMode = "SCAN";
        }
        else getCurrentMode = "EXTRACT";
    }


}
