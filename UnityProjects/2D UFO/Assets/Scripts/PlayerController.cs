using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float speed;

    private Rigidbody2D rb2d;

    private int count;
    private int score;

    private bool isEnd;

    public TMP_Text countText;
    public TMP_Text winText;
    public TMP_Text scoreText;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        
        count = 0;
        score = 0;
        
        countText.SetText($"Count: {count}");
        scoreText.SetText($"Score: {score}");

    }

    private void Update()
    {
        if (count >= 7) 
        { 
            winText.SetText("You Win!");
            speed = 0;
            isEnd = true;

        }
        if(score < 0)
        {
            winText.SetText("GameOver");
            speed = 0;
            isEnd = true;
        }
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector2 movement = new Vector2 (moveHorizontal, moveVertical);

        rb2d.AddForce (movement * speed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Pickups") && !isEnd)
        {
            Destroy(collision.gameObject);

            count++;
            score += 10;

            countText.SetText($"Count: {count}");
            scoreText.SetText($"Score: {score}");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Background") && !isEnd)
        {
            score -= 5;
            scoreText.SetText($"Score: {score}");
        }
    }
}
