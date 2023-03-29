using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    AudioSource coinSource;
    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.name == "Ball")
        {
            coinSource.Play();
            GameObject.Find("GameManager").GetComponent<GameManager>().GetCoin();
            Invoke("DestroySelf", 0.3f);
        }
    }

     void DestroySelf()
    {
        Destroy(gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        coinSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.localEulerAngles = new Vector3(this.transform.localEulerAngles.x ,this.transform.localEulerAngles.y + 0.5f * Time.deltaTime *100 ,0); 
        //transform.Rotate(new Vector3(0f, 100 * Time.deltaTime, 0f));
    }
}
