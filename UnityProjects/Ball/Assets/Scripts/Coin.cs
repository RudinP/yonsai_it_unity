using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.name == "Ball")
        {
            GameObject.Find("GameManager").SendMessage("GetCoin");

            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        transform.localEulerAngles = new Vector3(90 ,this.transform.localEulerAngles.y + 0.5f * Time.deltaTime *100 ,0); 
        //transform.Rotate(new Vector3(0f, 100 * Time.deltaTime, 0f));
    }
}
