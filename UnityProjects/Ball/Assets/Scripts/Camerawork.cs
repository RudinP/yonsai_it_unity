using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camerawork : MonoBehaviour
{
    public GameObject ball;
    // Start is called before the first frame update
    void Start()
    {
        //ball = GameObject.Find("Ball");
        GameObject[] coins = GameObject.FindGameObjectsWithTag("Coin");
        foreach (GameObject coin in coins) {
            Debug.Log(coin.name);
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(0, ball.transform.position.y + 6, ball.transform.position.z - 14);

    }
}
