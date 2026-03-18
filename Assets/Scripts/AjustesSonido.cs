using UnityEngine;
using UnityEngine.UI;

public class SoundConfig : MonoBehaviour
{
    public GameObject configPanel;
    private SoundManager soundManager;

    public Slider motorSlider;
    public Slider ropeSlider;
    public Slider windSlider;
    public Slider seaSlider;
    public Slider seagullSlider;
    public Slider deliverySlider;

    void Start()
    {
        soundManager = FindObjectOfType<SoundManager>();
        configPanel.SetActive(false);

        // Set sliders to current volume
        motorSlider.value = soundManager.motorSource.volume;
        ropeSlider.value = soundManager.ropeSource.volume;
        windSlider.value = soundManager.windSource.volume;
        seaSlider.value = soundManager.seaSource.volume;
        seagullSlider.value = soundManager.seagullSource.volume;
        deliverySlider.value = soundManager.deliverySource.volume;

        // Add listeners
        motorSlider.onValueChanged.AddListener(val => soundManager.motorSource.volume = val);
        ropeSlider.onValueChanged.AddListener(val => soundManager.ropeSource.volume = val);
        windSlider.onValueChanged.AddListener(val => soundManager.windSource.volume = val);
        seaSlider.onValueChanged.AddListener(val => soundManager.seaSource.volume = val);
        seagullSlider.onValueChanged.AddListener(val => soundManager.seagullSource.volume = val);
        deliverySlider.onValueChanged.AddListener(val => soundManager.deliverySource.volume = val);
    }

    public void OpenConfig()
    {
        configPanel.SetActive(true);
    }

    public void CloseConfig()
    {
        configPanel.SetActive(false);
    }
}
