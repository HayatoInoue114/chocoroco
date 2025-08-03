using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PatternManager : MonoBehaviour
{
	// パターンを管理
	public List<Pattern> patterns = new List<Pattern>();

	// 今のパターンを持っている
	// ネクストとかホールドとかのために配列化するかも
	public Pattern currentPattern = null;
	// ネクスト
	public Pattern nextPattern = null;
	// ホールド
	public Pattern holdPattern = null;
	// タスク達成の報酬
	public int taskBonusCount = 0;
	// ポイント表示
	[SerializeField]
	private TMP_Text pointText;
	// 操作表示
	[SerializeField]
	private TMP_Text operateText;

	// パターン変更音
	[SerializeField]
	private AudioClip changePatternSound;

	private AudioSource audioSource;

	public class PatternAndIndex
	{
		public Pattern pattern;
		public int index;
	}

	private void Awake()
	{
		LoadPatterns();

		// ネクストを先に設定
		int rand = Random.Range(0, patterns.Count);
		nextPattern = patterns[rand];
		// ホールドを設定
		holdPattern = patterns[0];
		// 操作を灰色に
		operateText.color = Color.gray;
		
		audioSource = GameManager.instance.GetComponent<AudioSource>();
	}

	private void LoadPatterns()
	{
		// コスト
		int cost = 5;
		// 一文字
		patterns.Add(new Pattern("1字", new List<Vector2Int> {
			new Vector2Int(0,0) }, cost));

		// I字パターン
		#region I
		// 2 ブロック
		cost = 4;
		patterns.Add(new Pattern("I字2横", new List<Vector2Int> {
			new Vector2Int(0,0), new Vector2Int(1,0),
		}, cost));
		patterns.Add(new Pattern("I字2縦", new List<Vector2Int> {
			new Vector2Int(0,0), new Vector2Int(0,1),
		}, cost));
		// 3 ブロック
		cost = 3;
		patterns.Add(new Pattern("I字3横", new List<Vector2Int> {
			new Vector2Int(0,0), new Vector2Int(1,0),
			new Vector2Int(2,0)
		}, cost));
		patterns.Add(new Pattern("I字3縦", new List<Vector2Int> {
			new Vector2Int(0,0), new Vector2Int(0,1),
			new Vector2Int(0,2)
		}, cost));
		// 4 ブロック
		cost = 2;
		patterns.Add(new Pattern("I字4横", new List<Vector2Int> {
			new Vector2Int(0,0), new Vector2Int(1,0),
			new Vector2Int(2,0), new Vector2Int(3,0)
		}, cost));
		patterns.Add(new Pattern("I字4縦", new List<Vector2Int> {
			new Vector2Int(0,0), new Vector2Int(0,1),
			new Vector2Int(0,2), new Vector2Int(0,3)
		}, cost));
		#endregion
		// L字パターン
		#region L
		// 3 ブロック
		cost = 3;
		patterns.Add(new Pattern("L字3右上", new List<Vector2Int> {
			new Vector2Int(0,0), new Vector2Int(0,1),
			new Vector2Int(1,1)
		}, cost));
		patterns.Add(new Pattern("L字3右下", new List<Vector2Int> {
			new Vector2Int(0,1), new Vector2Int(1,1),
			new Vector2Int(1,0)
		}, cost));
		patterns.Add(new Pattern("L字3左下", new List<Vector2Int> {
			new Vector2Int(0,0),new Vector2Int(0,1),
			new Vector2Int(1,1),
		}, cost));
		patterns.Add(new Pattern("L字3左上", new List<Vector2Int> {
			new Vector2Int(0,0), new Vector2Int(1,0),
			new Vector2Int(0,1)
		}, cost));
		// 4 ブロック
		cost = 2;
		patterns.Add(new Pattern("L字縦右上", new List<Vector2Int> {
			new Vector2Int(0,0), new Vector2Int(0,1),
			new Vector2Int(0,2), new Vector2Int(1,2)
		}, cost));
		patterns.Add(new Pattern("L字横右上", new List<Vector2Int> {
			new Vector2Int(0,0), new Vector2Int(1,0),
			new Vector2Int(2,0), new Vector2Int(2,1)
		}, cost));
		patterns.Add(new Pattern("L字横右下", new List<Vector2Int> {
			new Vector2Int(0,1), new Vector2Int(1,1),
			new Vector2Int(2,1), new Vector2Int(2,0)
		}, cost));
		patterns.Add(new Pattern("L字横左下", new List<Vector2Int> {
			new Vector2Int(0,1), new Vector2Int(1,1),
			new Vector2Int(2,1), new Vector2Int(0,0)
		}, cost));
		patterns.Add(new Pattern("L字横左上", new List<Vector2Int> {
			new Vector2Int(0,0), new Vector2Int(1,0),
			new Vector2Int(2,0), new Vector2Int(0,1)
		}, cost));
		patterns.Add(new Pattern("L字縦右上", new List<Vector2Int> {
			new Vector2Int(0,0), new Vector2Int(0,1),
			new Vector2Int(0,2), new Vector2Int(1,2)
		}, cost));
		patterns.Add(new Pattern("L字縦右下", new List<Vector2Int> {
			new Vector2Int(0,0), new Vector2Int(0,1),
			new Vector2Int(0,2), new Vector2Int(1,0)
		}, cost));
		patterns.Add(new Pattern("L字縦左下", new List<Vector2Int> {
			new Vector2Int(0,0), new Vector2Int(1,0),
			new Vector2Int(1,1), new Vector2Int(1,2)
		}, cost));
		patterns.Add(new Pattern("L字縦左上", new List<Vector2Int> {
			new Vector2Int(0,2), new Vector2Int(1,0),
			new Vector2Int(1,1), new Vector2Int(1,2)
		}, cost));
		#endregion
		// Z 字パターン
		#region Z
		// 4 ブロック
		cost = 2;
		patterns.Add(new Pattern("Z字横", new List<Vector2Int> {
			new Vector2Int(0,1), new Vector2Int(1,1),
			new Vector2Int(1,0), new Vector2Int(2,0)
		}, cost));
		patterns.Add(new Pattern("Z字縦", new List<Vector2Int> {
			new Vector2Int(0,0), new Vector2Int(0,1),
			new Vector2Int(1,1), new Vector2Int(1,2)
		}, cost));
		#endregion
		// S 字パターン
		#region S
		// 4 ブロック
		cost = 2;
		patterns.Add(new Pattern("S字横", new List<Vector2Int> {
			new Vector2Int(0,0), new Vector2Int(1,0),
			new Vector2Int(1,1), new Vector2Int(2,1)
		}, cost));
		patterns.Add(new Pattern("S字縦", new List<Vector2Int> {
			new Vector2Int(0,2), new Vector2Int(0,1),
			new Vector2Int(1,1), new Vector2Int(1,0)
		}, cost));
		#endregion
		// O 字パターン
		// 4 ブロック
		cost = 1;
		patterns.Add(new Pattern("O字", new List<Vector2Int> {
			new Vector2Int(0,0),new Vector2Int(0,1),
			new Vector2Int(1,1),new Vector2Int(1,0)
		}, cost));
	}

	/// <summary>
	/// 形状とパターンが一致しているか判定
	/// </summary>
	/// <param name="traced">マウスでなぞった形状</param>
	/// <returns>一致したパターン</returns>
	public Pattern Match(List<Vector2Int> traced)
	{
		// すべてのパターンを検索
		// 実際はこれで判定しない
		//foreach (var pattern in patterns)
		//{
		//	if (pattern.MatchesAllRotate(traced))
		//		return pattern;
		//}
		// 現在のパターンと検証
		if (currentPattern.Matches(traced))
		{
			return currentPattern;
		}
		return null;
	}

	/// <summary>
	/// 新しくパターンを選択
	/// </summary>
	public void ChoosePattern()
	{
		// ランダムで選出
		int rand = Random.Range(0, patterns.Count);
		currentPattern = nextPattern;
		nextPattern = patterns[rand];
		UpdatePatternDisplay();
	}

	/// <summary>
	/// パターンを保持
	/// </summary>
	public void HoldPattern()
	{
		audioSource.PlayOneShot(changePatternSound);
		Pattern ptn = holdPattern;
		holdPattern = currentPattern;
		currentPattern = ptn;
		UpdatePatternDisplay();
	}

	/// <summary>
	/// タスク報酬を使用
	/// </summary>
	public void UseTaskBonus()
	{
		// ボーナスが 1 以上
		//if (taskBonusCount > 0)
		{
			// 現在のパターンを変更する
			//ChangeCurrentPattern(0); // 0 はデフォルトのパターン
			GameManager.instance.patternSelectPanel.SetActive(true);
			// ゲームを一時停止
			Time.timeScale = 0f;
			// 操作を不可能に
			GameManager.instance.selectionManager.DisableSelection();
		}
	}

	/// <summary>
	/// 現在のパターンを変更する
	/// 後々好きなものに変えれる可能性がでてくる
	/// </summary>
	public void ChangeCurrentPattern(int index)
	{
		audioSource.PlayOneShot(changePatternSound);
		// パターンの変更
		currentPattern = patterns[index];
		UpdatePatternDisplay();
		UpdateTaskBonusPoint();
	}

	/// <summary>
	/// タスク報酬を追加
	/// </summary>
	public void AddTaskBonus()
	{
		taskBonusCount++;
		UpdateTaskBonusPoint();
	}

	// 分類されたパターンリストを返す
	public Dictionary<int, List<PatternAndIndex>> GetPatternsGroupedByBlockCount()
	{
		Dictionary<int, List<PatternAndIndex>> grouped = new Dictionary<int, List<PatternAndIndex>>();
		int index = 0;
		// パターンをブロック数ごとに分類
		foreach (Pattern p in patterns)
		{
			// ブロック数をキーにして分類
			int count = p.shape.Count;
			if (!grouped.ContainsKey(count))
				grouped[count] = new List<PatternAndIndex>();

			grouped[count].Add(new PatternAndIndex { pattern = p, index = index });
			index++;
		}
		return grouped;
	}

	// 今のマップの中で消せるパターンを検索し、
	// そのパターンを交換できるか検証する
	public bool CanEraseChangePattern()
	{
		// タスクボーナスがあるか
		if (taskBonusCount < 0)
		{
			return false;
		}
		// すべてのパターンを検証
		foreach (Pattern p in patterns)
		{
			if (p.pointCost > taskBonusCount)
			{
				continue; // コストが足りないのでスキップ
			}
			// 現在のパターンと検証
			if (p.Matches(currentPattern.shape))
			{
				continue; // 現在のパターンと同じなのでスキップ
			}
			// パターンが消せるか検証
			if (GameManager.instance.gridManager.HasValidPattern(p))
			{
				// パターンが消せるので交換可能
				return true;
			}
		}
		return false;
	}

	/// <summary>
	/// UI の描画更新
	/// </summary>
	public void UpdatePatternDisplay()
	{
		// 更新
		GameManager.instance.patternDisplayCurrent.SetPattern(currentPattern.shape);
		GameManager.instance.patternDisplayNext.SetPattern(nextPattern.shape);
		GameManager.instance.patternDisplayHold.SetPattern(holdPattern.shape);
		Debug.Log(currentPattern.name);
	}

	public void UpdateTaskBonusPoint()
	{
		string text = "";
		if (taskBonusCount >= 1000)
		{
			text = "999+pt";
		}
		else
		{
			text = taskBonusCount.ToString() + "pt";
		}
		pointText.text = text;
		if (taskBonusCount > 0)
		{
			// 操作を可能に
			operateText.color = Color.black;
		}
		else
		{
			operateText.color = Color.gray;
		}
	}
}
