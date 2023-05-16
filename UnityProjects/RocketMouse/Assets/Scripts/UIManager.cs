using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public Animator startButton;
    public Animator settingButton;
    public Animator dialog;
    public Animator contentPanel;
    public Animator gearImage;

    public void ToggleMenu()
    {
        bool isHidden = contentPanel.GetBool("isHidden");
        contentPanel.SetBool("isHidden", !isHidden);
        gearImage.SetBool("isHidden", !isHidden);
    }

    public void OpenCloseSettings(bool isOpen)
    {
        startButton.SetBool("isHidden", isOpen);
        settingButton.SetBool("isHidden", isOpen);
        dialog.SetBool("isHidden", !isOpen);
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Main");
    }
}
