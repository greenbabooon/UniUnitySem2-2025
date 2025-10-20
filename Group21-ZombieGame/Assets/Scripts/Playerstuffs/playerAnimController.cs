using TMPro;
using UnityEngine;

public class playerAnimController : MonoBehaviour
{
    Animator anim;
    float walkSpeed = 0f;
    bool isJumping = false;
    bool anyEquiped = false;
    bool isCharged = false;
    bool isMoving = true;
    AudioClip[] clips;
    AudioSource source;
    void Awake()
    {
        anim = GetComponent<Animator>();
        source = GetComponent<AudioSource>();
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
    }
    public void setAnyEquiped(bool b)
    {
        anyEquiped = b;
        anim.SetBool("anyEquiped", anyEquiped);
    }
    public void setIsCharged(bool b)
    {
        isCharged = b;
        anim.SetBool("IsCharged", isCharged);
    }
    public void Swing()
    {
        anim.SetTrigger("Swing");
    }
    public void setIsMoving(bool b)
    {
        isMoving = b;
        anim.SetBool("isMoving", isMoving);
    }
}
