using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidManager : SingletonMonoBehaviour<AsteroidManager>
{
    public Asteroid AsteroidPrefab;

    public int AsteroidsCurrentCount;
    public int AsteroidsMaxCount;

    void Update()
    {
        if (AsteroidsCurrentCount < AsteroidsMaxCount)
        {
            ++AsteroidsCurrentCount;
            CreateNewAsteroid();
        }
    }

    public void CreateNewAsteroid()
    {
        Instantiate(AsteroidPrefab, GetPosition(), Quaternion.identity, transform);
    }

    public Vector3 GetPosition()
    {
        return new Vector3(GameManager.Instance.RightTopMap.x - (AsteroidPrefab.SpriteSize.x * 0.5f),
        Random.Range(GameManager.Instance.RightTopMap.y, GameManager.Instance.LeftBottomMap.y), 0f);
    }
}
