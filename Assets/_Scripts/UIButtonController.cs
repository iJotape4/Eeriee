using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIButtonController : MonoBehaviour
{

    #region Scenes Ids
    [SerializeField] private string _newGameLevel = "Jpruebas";
    [SerializeField] private string _mainmenu = "MainMenu";
    #endregion


    public void NewGameButton()
    {
        SceneManager.LoadScene(_newGameLevel);
    }
    
    public void ResumeGame()
    {
       GameObject.FindGameObjectWithTag("Arms").GetComponent<Animator>().SetBool("SmartWatch", false);
       
    }

    public void GOMainMenu()
    {

        SceneManager.LoadScene(_mainmenu);
        
    }

}
