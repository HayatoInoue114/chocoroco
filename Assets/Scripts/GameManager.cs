using UnityEngine;

/// <summary>
/// ゲーム全体を管理
/// 変数の橋渡しも担う
/// </summary>
public class GameManager : MonoBehaviour
{
	// シングルトンインスタンス
	public static GameManager instance;

	// 参照できるように変数化
	// インスペクターで参照できるようにしてね
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
		// マップを生成する
		gridManager.GenerateAllLines();
	}

	// Update is called once per frame
	void Update()
	{

	}
}
