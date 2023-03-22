using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FailZone : MonoBehaviour
{

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.name == "Ball")
        {
            GameManager gmComponent = GameObject.Find("GameManager").GetComponent<GameManager>();
            gmComponent.RestartGame();
        }
    }

    private void Update()
    {
        GameManager gmComponent = GameObject.Find("GameManager").GetComponent<GameManager>();
        //Debug.Log(gmComponent.coinCount);
    }
}
