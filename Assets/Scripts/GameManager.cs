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
	// インスペクターに入れなくてもいい
	public GridManager gridManager;
	public SelectionManager selectionManager;
	public PatternManager patternManager;

	private void Awake()
	{
		instance = this;
	}

	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
	{
		// 参照を取得
		gridManager = GetComponent<GridManager>();
		selectionManager = GetComponent<SelectionManager>();
		patternManager = GetComponent<PatternManager>();

		// マップを生成する
		gridManager.GenerateAllLines();

		// 新しくパターンを選ぶ
		GameManager.instance.patternManager.ChoosePattern();
	}

	// Update is called once per frame
	void Update()
	{
		// 行が綺麗に消えているか判定
		gridManager.ProcessClearedRows();
	}
}
