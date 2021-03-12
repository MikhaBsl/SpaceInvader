using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public float SpeedShoot;
    private Rigidbody2D Rigidbody;
    public Vector2 Direction;

    public void Awake()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Rigidbody.velocity = Direction * SpeedShoot;
    }

    private void Update()
    {
        if (transform.position.x > GameManager.Instance.RightTopMap.x)
            Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
       if (collider.gameObject.layer == LayerMask.NameToLayer("Enemy"))
       {
            Asteroid asteroid;
            if (collider.gameObject.TryGetComponent<Asteroid>(out asteroid))
                asteroid.Dead();
            ++GameManager.Instance.Score;
            Destroy(gameObject);
       }
    }
}
