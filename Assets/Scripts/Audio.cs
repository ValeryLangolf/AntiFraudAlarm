using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Audio : AlarmSystem
{
    [SerializeField] private float _volume;
    [SerializeField] private float _speed;

    private const float MinVolume = 0;
    private const float MaxVolume = 1f;

    private AudioSource _source;
    private Coroutine _coroutine;

    private void Awake()
    {
        _source = GetComponent<AudioSource>();

        _volume = MinVolume;
        _source.volume = _volume;
    }

    public override void RunGain()
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _source.Play();
        _coroutine = StartCoroutine(ChangingVolume(MaxVolume));
    }

    public override void RunDecline()
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _coroutine = StartCoroutine(ChangingVolume(MinVolume));
    }

    private IEnumerator ChangingVolume(float targetVolume)
    {
        _volume = _source.volume;

        while (_volume != targetVolume)
        {
            yield return null;

            _volume = Mathf.MoveTowards(_volume, targetVolume, _speed * Time.deltaTime);
            _source.volume = _volume;
        }

        if (targetVolume == MinVolume)
            _source.Stop();
    }
}