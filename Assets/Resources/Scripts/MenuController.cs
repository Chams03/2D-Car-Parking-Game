using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    private PlayerScript pScript;

    public string _newGameModeOne;
    public string _newGameModeTwo;

    public void NewGameOneDialog_Yes() {
        SceneManager.LoadScene(_newGameModeOne);
    }
    
    public void NewGameTwoDialog_Yes() {
        SceneManager.LoadScene(_newGameModeTwo);
    }

    public void ExitDialog_Yes(){
        Application.Quit();
        UnityEditor.EditorApplication.isPlaying = false;
    }
}
