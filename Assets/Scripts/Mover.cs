using System;
using System.Linq;
using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField] private Rogue _rogue;
    [SerializeField] private Transform[] _targetPoints;
    [SerializeField, Min(0.01f)] private float _speed;

    private int _idTarget = 0;

#if UNITY_EDITOR
    [ContextMenu("Refresh Child Array")]
    private void RefreshChaildArray()
    {
        int pointCount = transform.childCount;
        _targetPoints = new Transform[pointCount];

        for (int i = 0; i < pointCount; i++)
        {
            _targetPoints[i] = transform.GetChild(i);
        }
    }
#endif

    private void Start()
    {
        if (_targetPoints == null || _targetPoints.Length == 0)
            throw new ArgumentNullException(nameof(_targetPoints), "Косяк косячный! Нет массива целей для перемещения");
    }

    private void Update()
    {
        if (_idTarget >= _targetPoints.Length)
            return;

        Vector3 currentPosition = _rogue.transform.position;
        Vector3 targetPosition = _targetPoints[_idTarget].position;
        targetPosition.y = currentPosition.y;

        currentPosition = Vector3.MoveTowards(currentPosition, targetPosition, _speed * Time.deltaTime);
        _rogue.transform.position = currentPosition;
        _rogue.transform.LookAt(targetPosition);

        float distanceSqr = (targetPosition - currentPosition).sqrMagnitude;

        if (distanceSqr < 0.1f)
            ++_idTarget;
    }
}