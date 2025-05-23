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
	public PatternDisplay patternDisplayCullent;
	public PatternDisplay patternDisplayNext;

	public TaskManager taskManager;

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
		patternDisplayCullent = GameObject.Find("PatternDisplayCullentAnchor").GetComponent<PatternDisplay>();
		patternDisplayNext = GameObject.Find("PatternDisplayNextAnchor").GetComponent<PatternDisplay>();
		// マップを生成する
		gridManager.GenerateAllLines();
		taskManager.Start();

		// 新しくパターンを選ぶ
		patternManager.ChoosePattern();
	}

	// Update is called once per frame
	void Update()
	{
		taskManager.Update();
		// 行が綺麗に消えているか判定
		gridManager.ProcessClearedRows();
	}
}
