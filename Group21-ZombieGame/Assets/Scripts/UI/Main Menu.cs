using System.Collections;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public Camera mainCamera;
    public float rotationSpeed = 1f;
    private bool isRotating = false;
    public GameObject[] UIElements;
    public GameObject initialButton;

    public void RotateCameraLeftBy90Degrees()
    {
        if (!isRotating)
        {
            StartCoroutine(RotateCameraCoroutine(90f));
        }
    }

    private IEnumerator RotateCameraCoroutine(float angle)
    {
        isRotating = true;

        Quaternion startRotation = mainCamera.transform.rotation;
        Quaternion endRotation = startRotation * Quaternion.Euler(0, -angle, 0);

        float rotationProgress = 0f;
        while (rotationProgress < 1f)
        {
            rotationProgress += Time.deltaTime * (rotationSpeed / angle);
            mainCamera.transform.rotation = Quaternion.Lerp(startRotation, endRotation, rotationProgress);
            yield return null;
        }
        mainCamera.transform.rotation = endRotation;
        isRotating = false;

        initialButton.SetActive(false);
        foreach (GameObject uiElement in UIElements)
        {
            uiElement.SetActive(true);
        }
    }
}
