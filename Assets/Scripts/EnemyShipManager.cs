using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShipManager : SingletonMonoBehaviour<EnemyShipManager>
{
    public EnemyShip EnemyShipPrefab;

    public int StartEnemyCount = 2;
    public int EnemyShipCurrentCount;
    public int EnemyShipMaxCount;

    public AudioSource AudioEnemyShipDestroy;

    void Update()
    {
        EnemyShipMaxCount = (int)(GameManager.Instance.Score * 0.075f) + StartEnemyCount;
        if (EnemyShipCurrentCount < EnemyShipMaxCount)
        {
            ++EnemyShipCurrentCount;
            CreateNewEnemyShip();
        }
    }

    public void CreateNewEnemyShip()
    {
        Instantiate(EnemyShipPrefab, GetPosition(), Quaternion.identity, transform);
    }

    public Vector3 GetPosition()
    {
        return new Vector3(GameManager.Instance.RightTopMap.x - (EnemyShipPrefab.SpriteSize.x * 0.5f),
        Random.Range(GameManager.Instance.RightTopMap.y, GameManager.Instance.LeftBottomMap.y), 0f);
    }
}
