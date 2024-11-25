using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Audio : MonoBehaviour
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

    public void Run(bool isLouder)
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);

        if (isLouder)
            _source.Play();

        float targetVolume = isLouder ? MaxVolume : MinVolume;
        _coroutine = StartCoroutine(ChangingVolume(targetVolume));
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