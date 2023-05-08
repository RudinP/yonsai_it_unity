using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class UIManager : MonoBehaviour
{
    public Animator startButton;
    public Animator settingsButton;
    public Animator dialog;

    public void StartGame()
    {
        SceneManager.LoadScene("Main");
    }
    
    public void OpenCloseSettings(bool isOpen)
    {
        startButton.SetBool("isHidden", isOpen);
        settingsButton.SetBool("isHidden", isOpen);
        dialog.SetBool("isHidden", !isOpen);
    }
}
