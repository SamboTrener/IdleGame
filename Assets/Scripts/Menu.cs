using UnityEngine;
using UnityEngine.UI;
using System.IO; //for File.Exists
using UnityEngine.SceneManagement;

//script used in "Menu" Sence.
public class Menu : MonoBehaviour
{

    public Button newGameBtn;
    public Button continueBtn;

    public static bool newGame = true; //if to start a new game

    void Start()
    {
        if (File.Exists(Application.persistentDataPath + "/PlayerData.xml")) //if there is a saved game, we enable the continue button
            continueBtn.interactable = true;
        else
            continueBtn.interactable = false;

        //Debug.Log (Application.persistentDataPath); //if you don't know where is the presistent data directory, uncomment this line
    }

    public void NewGame() //function called when click new game button
    {
        newGame = true; //set newGame to true

        SceneManager.LoadScene("Game");//load "Game" scene
    }

    public void LoadGame() //function called when click continue button
    {
        newGame = false;

        SceneManager.LoadScene("Game");//load "Game" scene
    }
}
