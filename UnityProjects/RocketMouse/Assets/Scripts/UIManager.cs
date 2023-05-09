using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Animator startButton;
    public Animator settingsButton;
    public Animator dialog;
    public Animator contentPnl;
    public Animator gearImg;

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

    public void ToggleMenu()
    {
        bool isHidden = contentPnl.GetBool("isHidden");
        contentPnl.SetBool("isHidden", !isHidden);

        gearImg.SetTrigger("isClicked");
    }
}
