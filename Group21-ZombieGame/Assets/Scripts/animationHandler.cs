using UnityEngine;

public class animationHandler : MonoBehaviour
{
    bool isAnimated = false;
    float animRuntime = 0;

    void OnAnimationEnded()
    {
        isAnimated = false;
    }
    public void OnAnimationStarted()
    {
        isAnimated = true;
    }
    public bool GetAnimationState()
    {
        return isAnimated;
    }
    public float GetAnimationRunTime()
    {
        return animRuntime;
    }
    void FixedUpdate()
    {
        if (isAnimated)
        {
            animRuntime++;   
        }
    }

}
