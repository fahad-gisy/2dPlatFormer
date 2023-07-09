using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MainMenuHandler : MonoBehaviour
{
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void QutiGameBtn()
    {
        Application.Quit();
    }

    public void PlayTheGameBtn()
    {
        SceneManager.LoadScene("Game");
    }
}
