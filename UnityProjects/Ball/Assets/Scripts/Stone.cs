using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stone : MonoBehaviour
{
    Vector3 target;
    AudioSource stoneSource;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("Ball").transform.position;
        stoneSource = GetComponent<AudioSource>();
        gameObject.transform.SetParent(GameObject.Find("Stage").transform);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target, Time.deltaTime);
        transform.Rotate(new Vector3(0, 0, 5));
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.name == "Ball")
        {
            stoneSource.Play();
            GameManager gmComponent = GameObject.Find("GameManager").GetComponent<GameManager>();
            gmComponent.RestartGame("");
        }
    }
}
