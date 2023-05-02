using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class UIManager : MonoBehaviour
{
    public Animator startButton;

    public void StartGame()
    {
        SceneManager.LoadScene("Main");
    }
    
    public void OpenSettings()
    {
        startButton.SetBool("isHidden", true);
    }
}
