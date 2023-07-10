using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MainMenuHandler : MonoBehaviour
{
   

    public void QutiGameBtn()
    {
        Application.Quit();//quit the game button
    }

    public void PlayTheGameBtn()
    {
        SceneManager.LoadScene("Game");// start the game button
    }
}
