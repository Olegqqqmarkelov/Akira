using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PhysicsMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private SurfaceSlider _sl;

    public void Move(Vector3 direction, float _speed)
    {
        Vector3 directionAlongSurface = _sl.Project(direction.normalized, _speed);
        Vector3 offset = directionAlongSurface * (_speed * Time.deltaTime);

        _rb.MovePosition(_rb.position + offset);
    }
}
