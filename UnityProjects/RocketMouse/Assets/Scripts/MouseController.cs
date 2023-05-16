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

    public AudioClip coinCollectSound;
    public AudioSource jetpackAudio;
    public AudioSource footstepsAudio;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        textCoins.text = coins.ToString();
    }

    private void FixedUpdate()
    {
        bool jetpackActive = Input.GetButton("Fire1");

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

    // ���ΰ��� 1 ����, ���ο�����Ʈ ����
    private void CollectCoin(Collider2D coinCollider)
    {
        coins++;
        Destroy(coinCollider.gameObject);

        textCoins.text = coins.ToString();

        AudioSource.PlayClipAtPoint(coinCollectSound, transform.position);
    }

    private void HitByLaser(Collider2D laserCollider)
    {
        if (!dead)
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
        bool active = buttonRestart.gameObject.activeSelf;
        if (grounded && dead && !active)
            buttonRestart.gameObject.SetActive(true);
    }

    public void OnClickedRestartButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void AdjustFootstepsAndJetpackSound(bool jetpackActive)
    {
        footstepsAudio.enabled = !dead && grounded;
        jetpackAudio.enabled = !dead && !grounded;
        jetpackAudio.volume = jetpackActive ? 1.0f : 0.5f;
    }
}