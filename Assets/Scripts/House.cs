using UnityEngine;

public class House : MonoBehaviour
{
    [SerializeField] private Detector _detector;
    [SerializeField] private AlarmSystem[] _responders;

    private Coroutine _coroutine;

    private void OnEnable()
    {
        _detector.Detected += OnAlarm;
        _detector.Exited += OffAlarm;
    }

    private void OnDisable()
    {
        _detector.Detected -= OnAlarm;
        _detector.Exited -= OffAlarm;
    }

    private void OnAlarm()
    {
        foreach (var responder in _responders)
            responder.RunGain();
    }

    private void OffAlarm()
    {
        foreach (var responder in _responders)
            responder.RunDecline();
    }
}