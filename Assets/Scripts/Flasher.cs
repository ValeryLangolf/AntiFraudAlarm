using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Flasher : MonoBehaviour
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

    public void Run(bool isFaster)
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);

        if (isFaster)
            gameObject.SetActive(true);

        float targetAnimationSpeed = isFaster ? MaxSpeedAnimation : MinSpeedAnimation;
        _coroutine = StartCoroutine(ChangingSpeed(targetAnimationSpeed));
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