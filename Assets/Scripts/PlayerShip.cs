using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShip : MonoBehaviour
{
    public Vector2 Speed;
    public List<LifeBar> Lifebar = new List<LifeBar>();

    private Rigidbody2D m_Rigidbody2D;
    private Vector2 m_SpriteSize;
    private int CurrentLife = 5;

    public Shoot ShootPrefab;

    void Awake()
    {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
        m_SpriteSize = GetComponent<SpriteRenderer>().bounds.size;
    }

    void Update()
    {
        MovePlayerShip();
        CheckPlayerPosition();

        if (Input.GetKeyDown(KeyCode.Space))
            Fire();
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            Asteroid asteroid;
            if (collision.gameObject.TryGetComponent<Asteroid>(out asteroid))
                asteroid.Dead();

            if (CurrentLife == 0)
            {
                Debug.Log("Tu es mort sale nul");

            }
            else
            {
                Lifebar[--CurrentLife].DestroyItemLife();
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
