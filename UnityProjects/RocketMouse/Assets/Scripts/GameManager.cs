using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    static GameManager instance;
    public GameObject mouse;
    public Text levelTxt;
    public int level { get; private set; }
    public float speedPerLv;

    private void Start()
    {
        instance = this;
        level = 1;
        StartCoroutine("LevelUp");
    }
    private void Update()
    {
        levelTxt.text = "Lv." + level;        
    }

    IEnumerator LevelUp()
    {
        yield return new WaitForSecondsRealtime(20f);
        level++;
        mouse.GetComponent<MouseController>().forwardMovementSpeed += speedPerLv;
    }
}
