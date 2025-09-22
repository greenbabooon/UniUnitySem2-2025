using Unity.Mathematics;
using UnityEngine;

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
    public Material highlightMat;
    Material defaultMat;
    void Awake()
    {
        defaultMat = GetComponent<Renderer>().material;
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
    
    
}
