using Fusion;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMove : NetworkBehaviour
{
    public float moveSpeed = 7f;

    CharacterController cc;

    float gravity = -20;
    public float yVelocity = 0;

    public float jumpPower = 10f;
    public bool isJumping = false;

    public int hp = 20;
    public int maxHp = 20;
    public Slider hpSlider;

    public GameObject hitEffect;

    Animator anim;

    public Transform camPosition;

    NetworkCharacterControllerPrototype netCC;
    [Networked] private NetworkButtons _buttonsPrevious { get; set; }

    public override void Spawned()
    {
        netCC = GetComponent<NetworkCharacterControllerPrototype>();
        cc = GetComponent<CharacterController>();
        anim = GetComponentInChildren<Animator>();

        if (Object.HasInputAuthority)
        {
            GameManager.gm.player = this;

            CamFollow cf = Camera.main.GetComponent<CamFollow>();
            cf.target = camPosition;

            hpSlider = GameManager.gm.hpSlider;
        }
        hitEffect = GameManager.gm.hitEffect;
    }

    public override void FixedUpdateNetwork()
    {
        if (GameManager.gm.gState != GameManager.GameState.Run)
            return;

        if(GetInput(out NetworkInputData data))
        {
            netCC.Move(data.dir * moveSpeed * Runner.DeltaTime);
            if (data.Buttons.WasPressed(_buttonsPrevious, PlayerButtons.Jump))
                netCC.Jump();

            _buttonsPrevious = data.Buttons;
        }


        anim.SetFloat("MoveMotion", netCC.Velocity.magnitude);

        if(hpSlider != null)
            hpSlider.value = (float)hp / maxHp;
    }

    public void DamageAction(int damage)
    {
        hp -= damage;

        if (Object.HasInputAuthority && hp > 0)
        {
            StartCoroutine(PlayHitEffect());
        }
    }

    IEnumerator PlayHitEffect()
    {
        hitEffect.SetActive(true);

        yield return new WaitForSeconds(0.3f);

        hitEffect.SetActive(false);
    }
}

