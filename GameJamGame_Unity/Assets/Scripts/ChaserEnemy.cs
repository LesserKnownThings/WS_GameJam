using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaserEnemy : MonoBehaviour
{
    [SerializeField] private GameObject playerObject;

    private Rigidbody2D _rigidbody2D;

    [SerializeField]
    private float speed = 1.0f;
    
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        ChaseEnemy();
    }

    void ChaseEnemy()
    {
        Vector2 movementVector = (playerObject.transform.position - transform.position).normalized;
        _rigidbody2D.MovePosition( (Vector2)transform.position + speed * Time.fixedDeltaTime * movementVector );
    }

}
