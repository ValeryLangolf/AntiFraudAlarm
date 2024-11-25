using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Mover : MonoBehaviour
{
    [SerializeField] private Targets _parent;
    [SerializeField, Min(0.01f)] private float _speed;

    int _idTarget = 0;
    List<Transform> _targets = new List<Transform>();

    private void Start()
    {
        _targets.AddRange(_parent.transform.GetComponentsInChildren<Transform>());
        _targets = _targets.OrderBy(target => target.gameObject.name).ToList();
    }

    private void Update()
    {
        if (_idTarget == _targets.Count - 1)
            return;

        if (_targets == null || _targets.Count == 0)
            throw new ArgumentNullException(nameof(_targets), "Косяк косячный! Нет массива целей для перемещения");

        Vector3 currentPosition = transform.position;
        Vector3 targetPosition = _targets[_idTarget].position;
        targetPosition.y = currentPosition.y;

        currentPosition = Vector3.MoveTowards(currentPosition, targetPosition, _speed * Time.deltaTime);

        transform.position = currentPosition;
        transform.LookAt(targetPosition);

        if (Vector3.Distance(currentPosition, targetPosition) < 0.1f)
            ++_idTarget;
    }
}