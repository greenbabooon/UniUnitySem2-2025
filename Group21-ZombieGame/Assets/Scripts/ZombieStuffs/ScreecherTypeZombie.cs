using UnityEngine;

public class ScreecherTypeZombie : StandardZombie
{
    public wave w;
    bool isScreech = true;
    bool hasScreeched = false;
    public bool Type;//true TV head false phone head
    void Screech()
    {
        anim.SetTrigger("screech");
        w.fire();
        isScreech = false;
        Invoke("DelayedReset", 20);
    }
    void Update()
    {
        if (isScreech&&distance<8f&&distance>attackRange)
        {
            Invoke("DelayedScreech",5);
        }
    }
    void DelayedScreech()
    {
        if (distance < 8f && distance > attackRange)
        {
            hasScreeched = true;
            Screech();
        }
    }
    void DelayedReset()
    {
        isScreech = true;
        hasScreeched = false;
    }
    public void SetHasScreeched(bool b)
    {
        isStopped = b;
    }

}
