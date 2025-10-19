using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class OptionsMenu : MonoBehaviour
{
    public Canvas canvas;
    public Button back;

    public void OpenOptions()
    {
        canvas.enabled = true;
    }

    public void CloseOptions()
    {
        canvas.enabled = false;
    }

}
