using UnityEngine;

public class MovingSaw : MonoBehaviour
{
    [SerializeField] Transform _pointA;
    [SerializeField] Transform _pointB;
    [SerializeField] float _moveSpeed = 3f;

    private float _time;

    private void Update()
    {
        _time += Time.deltaTime * _moveSpeed;

        // Oscillation between 0 and 1
        float t = (Mathf.Sin(_time) + 1f) / 2f;

        // Interpolation between A and B
        transform.position = Vector3.Lerp(_pointA.position, _pointB.position, t);
    }
}
