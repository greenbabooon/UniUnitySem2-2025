using UnityEngine;
using UnityEngine.UI;

public class SensHandle : MonoBehaviour
{
    [SerializeField] private Slider sensitivitySlider;
    [SerializeField] private PlayerController PlayerController;

    private const string SensitivityPrefKey = "PlayerSensitivity";

    private void Start()
    {
        PlayerController = FindAnyObjectByType<PlayerController>();

        float savedSensitivity = PlayerPrefs.GetFloat(SensitivityPrefKey, PlayerController.lookSensitivity);
        sensitivitySlider.value = savedSensitivity;
        PlayerController.SetLookSensitivity(savedSensitivity);
        sensitivitySlider.onValueChanged.AddListener(PlayerController.SetLookSensitivity);
    }
}

