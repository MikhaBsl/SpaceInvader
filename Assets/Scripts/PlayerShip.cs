using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerShip : MonoBehaviour
{
    public Vector2 Speed;
    public List<LifeBar> Lifebar = new List<LifeBar>();

    private Rigidbody2D m_Rigidbody2D;
    private Vector2 m_SpriteSize;
    private int CurrentLife = 5;

    public Shoot ShootPrefab;

    public AudioSource AudioShipDestroy;

    private SpriteRenderer m_SpriteRenderer;

    void Awake()
    {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
        m_SpriteSize = GetComponent<SpriteRenderer>().bounds.size;
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (CurrentLife > 0)
        {
            MovePlayerShip();
            CheckPlayerPosition();

            if (Input.GetKeyDown(KeyCode.Space))
                Fire();
        }
        else 
        {
            m_Rigidbody2D.velocity = Vector2.zero;

            if (m_SpriteRenderer.color.a > 0.01f)
            {
                var color = m_SpriteRenderer.color;
                color.a = Mathf.Lerp(m_SpriteRenderer.color.a, 0, Time.deltaTime * 60f * 0.1f);
                m_SpriteRenderer.color = color; 
            }
            else
            {
                Destroy(gameObject);
            }
        }

    }

    void MovePlayerShip()
    {
        float inputY = Input.GetAxis("Vertical");
        float inputX = Input.GetAxis("Horizontal");

        m_Rigidbody2D.velocity = new Vector2(Speed.x * inputX, Speed.y * inputY);
    }

    void CheckPlayerPosition()
    {
        var leftBottomY = GameManager.Instance.LeftBottomMap.y + (m_SpriteSize.y * 0.5f);
        if (transform.position.y < leftBottomY)
            transform.position = new Vector3(transform.position.x, leftBottomY, 0);

        var rightTopY = GameManager.Instance.RightTopMap.y - (m_SpriteSize.y * 0.5f);
        if (transform.position.y > rightTopY)
            transform.position = new Vector3(transform.position.x, rightTopY, 0);

        var rightBotX = GameManager.Instance.RightBottomMap.x - (m_SpriteSize.x * 0.5f);
        if (transform.position.x > rightBotX)
            transform.position = new Vector3(rightBotX, transform.position.y, 0);

        var leftTopX = GameManager.Instance.LeftTopMap.x + (m_SpriteSize.x * 0.5f);
        if (transform.position.x < leftTopX)
            transform.position = new Vector3(leftTopX, transform.position.y, 0);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            Asteroid asteroid;
            if (collider.gameObject.TryGetComponent<Asteroid>(out asteroid))
                asteroid.Dead();

            EnemyShip enemyShip;
            if (collider.gameObject.TryGetComponent<EnemyShip>(out enemyShip))
            {
                enemyShip.Dead();
            }

            EnemyShoot enemyShoot;
            if (collider.gameObject.TryGetComponent<EnemyShoot>(out enemyShoot))
            {
                enemyShoot.Dead();
            }

            if (CurrentLife == 1)
            {
                AudioShipDestroy.Play();
            }
            if (CurrentLife > 0 )
                Lifebar[--CurrentLife].DestroyItemLife();
        }

        if (collider.gameObject.layer == LayerMask.NameToLayer("Collectable"))
        {
            LifeCollectable lifeCollectable;
            if (collider.gameObject.TryGetComponent<LifeCollectable>(out lifeCollectable))
            {
                RestoreLife();
                Destroy(lifeCollectable.gameObject);
            }
        }
    }

    [ContextMenu("RestoreLife")]
    private void RestoreLife()
    {
        if (CurrentLife < 5)
        {
            Lifebar[CurrentLife].RestoreItemLife();
            ++CurrentLife;
        }
    }
    private void Fire()
    {
        var shoot = Instantiate(ShootPrefab, transform.position, Quaternion.identity);
        shoot.Direction = transform.right;
    }
}
