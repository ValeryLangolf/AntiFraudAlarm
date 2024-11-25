using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField] private Transform _targeHouse;
    [SerializeField] private float _speed;
    [SerializeField] private bool _isWalkingIn = true;

    private Vector3 _targetStreet;
    private Vector3 _targetHouse;

    private void Awake()
    {
        _targetStreet = transform.position;
        _targetHouse = _targeHouse.position;
    }

    private void Update()
    {
        if (transform.position.x == _targetHouse.x)
        {
            transform.LookAt(_targetStreet);
            _isWalkingIn = false;
        }
        else if (transform.position.x == _targetStreet.x)
        {
            transform.LookAt(_targetHouse);
            _isWalkingIn = true;
        }

        float target = _isWalkingIn ? _targetHouse.x : _targetStreet.x;
        Move(target);
    }

    private void Move(float target)
    {
        float positionX = Mathf.MoveTowards(transform.position.x, target, _speed * Time.deltaTime);
        Vector3 position = new(positionX, transform.position.y, transform.position.z);
        transform.position = position;
    }
}
