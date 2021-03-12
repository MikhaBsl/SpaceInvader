using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public Vector2 Speed;

    [HideInInspector]
    public Vector2 SpriteSize;
    [HideInInspector]
    public Rigidbody2D Rigidbody;

    private bool IsDead;

    public void Awake()
    {
        SpriteSize = GetComponent<SpriteRenderer>().bounds.size;
        Rigidbody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Rigidbody.velocity = Speed;
    }

    void Update()
    {
        if (transform.position.x < GameManager.Instance.LeftBottomMap.x + (SpriteSize.x * 0.5f))
        {
            --AsteroidManager.Instance.AsteroidsCurrentCount;
            Destroy(gameObject);
        }
    }

    public void Dead()
    {
        if (!IsDead)
        {
            AsteroidManager.Instance.AudioAsteroidDestroy.Play();
            IsDead = true;
            --AsteroidManager.Instance.AsteroidsCurrentCount;
            Destroy(gameObject);
        }
    }
}
