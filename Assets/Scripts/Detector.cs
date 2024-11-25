using System;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Detector : MonoBehaviour
{
    public event Action<bool> Detected;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<Mover>(out _))
            Detected?.Invoke(true);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.TryGetComponent<Mover>(out _))
            Detected?.Invoke(false);
    }
}