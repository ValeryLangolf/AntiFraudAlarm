using System;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Detector : MonoBehaviour
{
    public event Action Detected;
    public event Action Exited;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<Rogue>(out _))
            Detected?.Invoke();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.TryGetComponent<Rogue>(out _))
            Exited?.Invoke();
    }
}