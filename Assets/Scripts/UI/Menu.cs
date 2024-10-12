using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//script used in "Menu" Sence.
public class Menu : MonoBehaviour
{

    [SerializeField] Button newGameBtn;
    [SerializeField] Button continueBtn;

    public static bool newGame = true; //if to start a new game

    private void Awake()
    {
        newGameBtn.onClick.AddListener(NewGame);
        continueBtn.onClick.AddListener(LoadGame);
        if (!SaveLoadManager.CheckIfHasSaves())
        {
            continueBtn.interactable = false;
        }
        else
        {
            continueBtn.interactable = true;
        }
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
