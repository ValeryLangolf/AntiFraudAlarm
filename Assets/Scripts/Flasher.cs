using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Flasher : AlarmSystem
{
    [SerializeField] private float _currentAnimationSpeed;
    [SerializeField] private float _speedChangeRate;

    private const float MinSpeedAnimation = 0;
    private const float MaxSpeedAnimation = 1f;

    private Animator _animator;
    private Coroutine _coroutine;

    private void Awake()
    {
        _animator = GetComponent<Animator>();

        _currentAnimationSpeed = MinSpeedAnimation;
        _animator.speed = _currentAnimationSpeed;
    }

    public override void RunGain()
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);

        gameObject.SetActive(true);
        _coroutine = StartCoroutine(ChangingSpeed(MaxSpeedAnimation));
    }

    public override void RunDecline()
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _coroutine = StartCoroutine(ChangingSpeed(MinSpeedAnimation));
    }

    private IEnumerator ChangingSpeed(float targetAnimationSpeed)
    {
        _currentAnimationSpeed = _animator.speed;

        while (_currentAnimationSpeed != targetAnimationSpeed)
        {
            yield return null;

            _currentAnimationSpeed = Mathf.MoveTowards(_currentAnimationSpeed, targetAnimationSpeed, _speedChangeRate * Time.deltaTime);
            _animator.speed = _currentAnimationSpeed;
        }

        if (targetAnimationSpeed == MinSpeedAnimation)
            gameObject.SetActive(false);
    }
}