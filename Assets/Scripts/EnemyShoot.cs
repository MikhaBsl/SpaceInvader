using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    public float SpeedEnemyShoot;
    private Rigidbody2D Rigidbody;
    public Vector2 Direction;

    private bool IsDead;

    public void Awake()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Rigidbody.velocity = Direction * SpeedEnemyShoot;
    }

    void Update()
    {
        if (transform.position.x < GameManager.Instance.LeftBottomMap.x)
            Destroy(gameObject);
    }

    public void Dead() 
    {
        if (!IsDead)
        {
            IsDead = true;
            Destroy(gameObject);
        }
    }
}
