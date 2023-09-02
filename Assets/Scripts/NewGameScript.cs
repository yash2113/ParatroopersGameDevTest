using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewGameScript : MonoBehaviour
{
    public GameObject instructionMenu;
    public GameObject mainPlayCanvas;

    private void Start()
    {
        mainPlayCanvas.SetActive(true);
        instructionMenu.SetActive(false);
        Time.timeScale = 1.0f;
    }

    public void NewGame()
    {

        SceneManager.LoadScene(1);

    }

    public void LoadInstructions()
    {
        instructionMenu.SetActive(true);
        mainPlayCanvas.SetActive(false);
    }

    public void CloseInstruction()
    {
        mainPlayCanvas.SetActive(true);
        instructionMenu.SetActive(false);
    }

}
