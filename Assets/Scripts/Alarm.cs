using System.Collections;
using UnityEngine;

public class Alarm : MonoBehaviour
{
    [SerializeField] private Animator _flasher;
    [SerializeField] private AudioSource _audio;
    [SerializeField] private float _speed;

    private const float MinVolume = 0;
    private const float MaxVolume = 1f;
    private const string TagName = "Rogue";

    private Coroutine _coroutine;

    private void Awake()
    {
        _audio.volume = MinVolume;
        _flasher.speed = MinVolume;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(TagName))
        {
            _flasher.gameObject.SetActive(true);
            _audio.Play();

            StartSafeCoroutine(MaxVolume);            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(TagName))
            StartSafeCoroutine(MinVolume);
    }

    private void StartSafeCoroutine(float target)
    {
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
            _coroutine = null;
        }

        _coroutine = StartCoroutine(ChangeVolume(target));
    }

    private IEnumerator ChangeVolume(float target)
    {
        float currentVolume = _audio.volume;

        while (currentVolume != target)
        {
            yield return null;

            currentVolume = Mathf.MoveTowards(currentVolume, target, _speed * Time.deltaTime);

            _audio.volume = currentVolume;
            _flasher.speed = currentVolume;
        }

        if (target == MinVolume)
        {
            _flasher.gameObject.SetActive(false);
            _audio.Stop();
        }
    }
}