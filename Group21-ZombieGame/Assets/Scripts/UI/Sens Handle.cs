using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

public class SensHandle : MonoBehaviour
{
    [SerializeField] private Slider sensitivitySlider;
    [SerializeField] private PlayerController PlayerController;

    private const string SensitivityPrefKey = "PlayerSensitivity";
    public Slider SensitivitySlider;

    public void Initialise(PlayerController temp)
    {
        PlayerController = temp;

        sensitivitySlider.onValueChanged.AddListener(PlayerController.SetLookSensitivity);
        float savedSensitivity = PlayerPrefs.GetFloat(SensitivityPrefKey, PlayerController.lookSensitivity);
        sensitivitySlider.value = savedSensitivity;
        PlayerController.SetLookSensitivity(savedSensitivity);

    }

    public void SetSens()
    {
        PlayerPrefs.SetFloat(SensitivityPrefKey, sensitivitySlider.value);
        PlayerPrefs.Save();
        PlayerController.SetLookSensitivity(sensitivitySlider.value);
    }
}

