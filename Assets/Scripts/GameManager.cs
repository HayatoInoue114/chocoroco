using UnityEngine;

public class GameManager : MonoBehaviour
{
	public static GameManager instance;

	public GridManager gridManager;
	public SelectionManager selectionManager;
	public PieceSpawner spawner;

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
