using UnityEngine;

public class GameManager : SingletonMonoBehaviour<GameManager>
{
	[HideInInspector]
	public Vector3 RightTopMap;
	[HideInInspector]
	public Vector3 RightBottomMap;
	[HideInInspector]
	public Vector3 LeftBottomMap;
	[HideInInspector]
	public Vector3 LeftTopMap;

	public void Awake()
	{
		RightTopMap = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, 0));
		RightBottomMap = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, 0));
		LeftBottomMap = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0));
		LeftTopMap = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, 0));
	}
}
