using Unity.AI.Navigation;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.AI;

public class Door : MonoBehaviour, IInteractable
{
    public bool isOpen = false;
    bool moving = false;
    public Transform hingeAngle1;
    int OpenAngle1 = -90;
    public Transform hingeAngle2;
    int OpenAngle2 = 90;
    int CloseAngle = 0;
    bool isDoubleDoor = false;
    float currentAngle1;
    float currentAngle2;
    float targetAngle1;
    float targetAngle2;
    float startingAngle;
    public NavMeshLink navMeshLink;
    public Renderer rend1;
    public Renderer rend2;
    Renderer OriginalRend1=new Renderer();
    Renderer OriginalRend2=new Renderer();
    bool isGlowing = false;
    void Awake()
    {
       /* OriginalRend1 = rend1;
        if (rend2 != null)OriginalRend2 = rend2;
                OriginalRend1 = rend1;
        OriginalRend2 = rend2;
        Material[] ogMats = new Material[rend1.materials.Length + 1];
        rend1.materials.CopyTo(ogMats, 0);
        OriginalRend1.materials = gameObject.AddComponent<Renderer>().materials;
        if (rend2 != null)
        {
            Material[] ogMats2 = new Material[rend2.materials.Length + 1];
            rend2.materials.CopyTo(ogMats2, 0);
            OriginalRend2.materials = gameObject.AddComponent<Renderer>().materials;
        }*/

        if (hingeAngle1 != null && hingeAngle2 != null)
        {
            isDoubleDoor = true;
            currentAngle1 = hingeAngle1.localEulerAngles.y;
            currentAngle2 = hingeAngle2.localEulerAngles.y;
        }
        else if (hingeAngle1 != null && hingeAngle2 == null)
        {
            isDoubleDoor = false;
            currentAngle1 = hingeAngle1.localEulerAngles.y;
        }
    }

    public void Interact()
    {
        if (isOpen)
        {
            if (isDoubleDoor)
            {
                ToggleDoor(OpenAngle1, OpenAngle2, CloseAngle);

            }
            else
            {
                ToggleDoor(OpenAngle1, CloseAngle);

            }
        }
        else
        {
            if (isDoubleDoor)
            {
                ToggleDoor(OpenAngle1, OpenAngle2, CloseAngle);
            }
            else
            {
                ToggleDoor(OpenAngle1, CloseAngle);

            }
        }
    }
    public string InteractionPrompt()
    {
        if (!isOpen)
        {
            return "Press E to open the door";
        }
        else
        {
            return "Press E to close the door";
        }
    }
    void FixedUpdate()
    {
        if (moving)
        {
            currentAngle1 = Mathf.MoveTowards(currentAngle1, targetAngle1, Time.fixedDeltaTime * 100);
            hingeAngle1.localEulerAngles = new Vector3(0, currentAngle1, 0);

            if (isDoubleDoor)
            {
                currentAngle2 = Mathf.MoveTowards(currentAngle2, targetAngle2, Time.fixedDeltaTime * 100);
                hingeAngle2.localEulerAngles = new Vector3(0, currentAngle2, 0);
            }
            if (Mathf.Approximately(currentAngle1, targetAngle1) && (!isDoubleDoor || Mathf.Approximately(currentAngle2, targetAngle2)))
            {
                moving = false;
            }
            if (navMeshLink != null)
            {
                navMeshLink.activated = isOpen; // Set the NavMeshLink's activated state based on the door's state
            }
        }
    }
    private void ToggleDoor(float OpenAngle, float CloseAngle)
    {
        if (!isOpen)
        {
            targetAngle1 = OpenAngle;
            startingAngle = CloseAngle;
            isOpen = true;
        }
        else
        {
            targetAngle1 = CloseAngle;
            startingAngle = OpenAngle;
            isOpen = false;
        }
        moving = true;
    }
    private void ToggleDoor(float OpenAngle1, float OpenAngle2, float CloseAngle)
    {
        if (!isOpen)
        {
            targetAngle1 = OpenAngle1;
            targetAngle2 = OpenAngle2;
            startingAngle = CloseAngle;
            isOpen = true;
        }
        else
        {
            targetAngle1 = CloseAngle;
            targetAngle2 = CloseAngle;
            startingAngle = OpenAngle1;
            isOpen = false;
        }
        moving = true;
    }
    public void MakeGlow(Material glowMat)
    {
       /* if (isGlowing) return;
        Material[] newMats = new Material[rend1.materials.Length + 1];
        rend1.materials.CopyTo(newMats, 0);
        newMats[newMats.Length - 1] = glowMat;
        rend1.materials = newMats;
        if (isDoubleDoor)
        {
            Material[] newMats2 = new Material[rend2.materials.Length + 1];
            rend2.materials.CopyTo(newMats2, 0);
            newMats2[newMats2.Length - 1] = glowMat;
            rend2.materials = newMats2;
        }
        isGlowing = true;*/
    }
    public void StopGlow()
    {
       /* if (!isGlowing) return;
        rend1.materials = OriginalRend1.materials;
        if (isDoubleDoor)
        {
            rend2.materials = OriginalRend2.materials;
        }
        isGlowing = false;*/
    }
    
}
