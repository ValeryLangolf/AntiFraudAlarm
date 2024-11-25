using UnityEngine;

public class House : MonoBehaviour
{
    [SerializeField] private Detector _detector;
    [SerializeField] private Flasher _flasher;
    [SerializeField] private Audio _audio;

    private Coroutine _coroutine;

    private void OnEnable()
    {
        _detector.Detected += SwitchAlarm;
    }

    private void OnDisable()
    {
        _detector.Detected -= SwitchAlarm;
    }

    private void SwitchAlarm(bool isOn)
    {
        _audio.Run(isOn);
        _flasher.Run(isOn);
    }
}