using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerController : ChecksController
{

    public float _speed = 5f;
    private void FixedUpdate()
    {
        Grounded();
        _rb2D.MovePosition((Vector2)transform.position + _moveInput * Time.fixedDeltaTime * _speed);
    }
    void OnMove(InputValue movementValue)
    {
        _moveInput = movementValue.Get<Vector2>();
    }
    void OnShoot()
    {
        Debug.Log("Kill Cats");
    }
}   
