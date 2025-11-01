using NUnit.Framework;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class playerAnimController : MonoBehaviour
{
    Animator anim;
    float walkSpeed = 0f;
    bool isJumping = false;
    bool anyEquiped = false;
    bool isCharged = false;
    bool isMoving = true;
    bool isGrounded = true;

    [Header("Audio")]
    [SerializeField] AudioSource SFXsource;
    public AudioClip jump;
    public AudioClip move;
    public AudioClip meleeAttack;
    public AudioClip shootAttack;
    public AudioClip pickUp;
    public AudioClip chargeUp;
    public AudioClip reload;
    public AudioClip death;
    public AudioClip takeDamage;
    public AudioSource source;
    void Awake()
    {
        anim = GetComponent<Animator>();
        //source = GetComponent<AudioSource>();
    }
    public void setWalkSpeed(float speed)
    {
        walkSpeed = speed;
        anim.SetFloat("speed",walkSpeed);

    }
    public void SetIsJumping(bool b)
    {
        isJumping = b;
        anim.SetBool("IsJumping", isJumping);
        if (isJumping)
        {
            PlaySFX(jump);
        }
    }
    public void setAnyEquiped(bool b)
    {
        anyEquiped = b;
        anim.SetBool("anyEquiped", anyEquiped);
        PlaySFX(pickUp);
    }
    public void setIsCharged(bool b)
    {
        isCharged = b;
        anim.SetBool("IsCharged", isCharged);
        if (isCharged)
        {
            PlaySFX(chargeUp);
        }
    }
    public void Swing()
    {
        anim.SetTrigger("Swing");
        PlaySFX(meleeAttack);
    }
    public void setIsMoving(bool b)
    {
        isMoving = b;
        anim.SetBool("isMoving", isMoving);
        if (isMoving && !SFXsource.isPlaying && isGrounded)
        {
            PlaySFX(move);
        }
    }

    public void SetIsGrounded(bool b)
    {
        isGrounded = b;
    }

    public void isShooting()
    {
        PlaySFX(shootAttack);
    }

    public void reloadSFX()
    {
        PlaySFX(reload);
    }

    public void takeDamageSFX()
    {
        PlaySFX(takeDamage);
    }

    public void deathSFX()
    {
        PlaySFX(death);
    }

    public void PlaySFX(AudioClip clip)
    {
        SFXsource.PlayOneShot(clip);
    }
}
