using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementComponent : MonoBehaviour
{

    private Rigidbody2D _rigidbody2D;

    private float _verticalValue;
    private float _horizontalValue;

    private Vector2 movementVector;
    
    [SerializeField] private float _speed = 1.0f;

    private InputManager _inputManager;

    private void Start()
    {
        _inputManager = World.Instance.GetInputManager();
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        movementVector = _inputManager.movementVector;
    }

    private void FixedUpdate()
    {
        _rigidbody2D.MovePosition( (Vector2)transform.position + _speed * Time.fixedDeltaTime * movementVector );

    }

    public void AddKnockBackToMovement(Vector2 knockBackDirection)
    {
        
    }
}
