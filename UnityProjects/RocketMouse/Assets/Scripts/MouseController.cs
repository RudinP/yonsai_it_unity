using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MouseController : MonoBehaviour
{
    public float jetpackForce;
    public float forwardMovementSpeed;

    private Rigidbody2D rb;

    public Transform groundCheckTransform;
    public LayerMask groundCheckLayerMask;
    private bool grounded;
    private Animator animator;

    public ParticleSystem jetpack;

    private bool dead = false;

    public Text textCoins;
    private uint coins = 0;

    public Button buttonRestart;
    public Button buttonMenu;

    public AudioClip coinCollectSound;
    public AudioSource jetpackAudio;
    public AudioSource footstepsAudio;

    public ParallaxScroll parallaxScroll;

    private void Start()
    {
        Application.targetFrameRate = 60;

        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        textCoins.text = coins.ToString();
    }

    private void FixedUpdate()
    {
        bool jetpackActive = Input.GetButton("Fire1") || Input.GetMouseButton(0);

        jetpackActive = jetpackActive && !dead;
        if (jetpackActive)
        {
            rb.AddForce(jetpackForce * Vector2.up);
        }

        if (!dead)
        {
            Vector2 newVelocity = rb.velocity;
            newVelocity.x = forwardMovementSpeed;
            rb.velocity = newVelocity;
        }

        UpdateGroundedStatus();
        AdjustJetpack(jetpackActive);
        DisplayRestartButton();
        AdjustFootstepsAndJetpackSound(jetpackActive);

        parallaxScroll.offset = transform.position.x;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Coins")
        {
            CollectCoin(collision);
        }
        else
        {
            HitByLaser(collision);
        }
    }

    // 코인갯수 1 증가, 코인오브젝트 제거
    private void CollectCoin(Collider2D coinCollider)
    {
        coins++;
        Destroy(coinCollider.gameObject);

        textCoins.text = coins.ToString();

        if(PlayerPrefs.GetFloat("Volume") != 0)
            AudioSource.PlayClipAtPoint(coinCollectSound, transform.position);
    }

    private void HitByLaser(Collider2D laserCollider)
    {
        if (!dead && PlayerPrefs.GetFloat("Volume") != 0)
        {
            AudioSource laser = laserCollider.GetComponent<AudioSource>();
            laser.Play();
        }
        dead = true;
        animator.SetBool("dead", true);
    }

    private void AdjustJetpack(bool jetpackActive)
    {
        var emission = jetpack.emission;
        emission.enabled = !grounded;
        emission.rateOverTime = jetpackActive ? 300.0f : 75.0f;
        /*
        if (grounded)
        {
            emission.enabled = false;
        }
        else
        {
            emission.enabled = true;
            emission.rateOverTime = jetpackActive ? 300.0f : 75.0f;
        }
        */
    }

    private void UpdateGroundedStatus()
    {
        grounded = Physics2D.OverlapCircle(
            groundCheckTransform.position,
            0.1f,
            groundCheckLayerMask);

        animator.SetBool("grounded", grounded);
    }

    private void DisplayRestartButton()
    {
        bool active = buttonRestart.gameObject.activeSelf && buttonMenu.gameObject.activeSelf;
        if (grounded && dead && !active)
        {
            buttonRestart.gameObject.SetActive(true);
            buttonMenu.gameObject.SetActive(true);
        }
    }

    public void OnClickedRestartButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OnClickedToMenuButton()
    {
        SceneManager.LoadScene("Menu");
    }

    private void AdjustFootstepsAndJetpackSound(bool jetpackActive)
    {
        footstepsAudio.enabled = !dead && grounded;
        jetpackAudio.enabled = !dead && !grounded;
        jetpackAudio.volume = jetpackActive ? 1.0f : 0.5f;
    }
}
