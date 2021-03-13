using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShip : MonoBehaviour
{
    public Vector2 Speed;

    [HideInInspector]
    public Vector2 SpriteSize;
    [HideInInspector]
    public Rigidbody2D Rigidbody;

    private bool IsDead;

    public EnemyShoot EnemyShootPrefab;
    public AudioSource AudioEnemyShoot;

    //private bool coroutine;
    //private bool test;

    public void Awake()
    {
        SpriteSize = GetComponent<SpriteRenderer>().bounds.size;
        Rigidbody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Rigidbody.velocity = Speed;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < GameManager.Instance.LeftBottomMap.x + (SpriteSize.x * 0.5f))
        {
            --EnemyShipManager.Instance.EnemyShipCurrentCount;
            Destroy(gameObject);
        }

        /*if (!coroutine)
        {
            coroutine = true;
            StartCoroutine("EnemyShootDelay");
        }

        if (!test)
            FireEnemy();*/

    }

    public void Dead()
    {
        if (!IsDead)
        {
            EnemyShipManager.Instance.AudioEnemyShipDestroy.Play();
            IsDead = true;
            --EnemyShipManager.Instance.EnemyShipCurrentCount;
            Destroy(gameObject);
        }
    }

    private void FireEnemy()
    {
        AudioEnemyShoot.Play();
        var enemyShoot = Instantiate(EnemyShootPrefab, transform.position, Quaternion.identity);
        enemyShoot.Direction = transform.right * -1f;  
    }

    IEnumerable EnemyShootDelay() 
    {
        
        yield return new WaitForSeconds(3);
        //test = true;
    }
}
