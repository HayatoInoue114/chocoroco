using UnityEngine;

public class T_GameManager : MonoBehaviour
{
	public static T_GameManager instance;

	public T_GridManager gridManager;
	public T_PieceSpawner spawner;
	public GetClickedObject getClickedObject;

	private void Awake()
	{
		instance = this;
	}

	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
	{
		gridManager.GenerateAllLines();
	}

	// Update is called once per frame
	void Update()
	{

	}
}
