using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float speed;

    public Text countText;

    public Text winText;

    public Text scoreText;
 
    private Rigidbody rb;

    private int count;

    private int score;
    
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        score = 0;
        SetCountText();
        winText.text = "";
    }
    
    private void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        rb.AddForce(movement * speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            count += 1;
            SetCountText();

            switch(other.gameObject.name)
            {
                case "Pick Up": score += 10; break;
                case "Pick Up 2": score += 20; break;
                case "Pick Up 3": score += 30; break;
                default: break;
            }

            SetScoreText();
        }
    }

    private void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
        if(count >= 12)
        {
            winText.text = "You Win!";
        }
    }

    private void SetScoreText()
    {
        scoreText.text = "Score: " + score.ToString();
    }

}
