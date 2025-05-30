using System.Collections.Generic;
using UnityEngine;

public class PatternManager : MonoBehaviour
{
	// パターンを管理
	public List<Pattern> patterns = new List<Pattern>();
	// パターンを表示するためのブロック群

	// 今のパターンを持っている
	// ネクストとかホールドとかのために配列化するかも
	public Pattern currentPattern = null;
	// ネクスト
	public Pattern nextPattern = null;
	// ホールド
	public Pattern holdPattern = null;
	// ホールドしたかのフラグ
	public bool IsHeld = false;

	private void Awake()
	{
		LoadPatterns();

		// ネクストを先に設定
		int rand = Random.Range(0, patterns.Count);
		nextPattern = patterns[rand];
		// ホールドを設定
		holdPattern = patterns[0];
	}

	private void LoadPatterns()
	{
		// 一文字
		patterns.Add(new Pattern("天上天下唯我独尊", new List<Vector2Int> {
			new Vector2Int(0,0)
		}));

		// I字パターン
		#region I
		patterns.Add(new Pattern("I字2横", new List<Vector2Int> {
			new Vector2Int(0,0), new Vector2Int(1,0),
		}));
		patterns.Add(new Pattern("I字2縦", new List<Vector2Int> {
			new Vector2Int(0,0), new Vector2Int(0,1),
		}));
		patterns.Add(new Pattern("I字3横", new List<Vector2Int> {
			new Vector2Int(0,0), new Vector2Int(1,0),
			new Vector2Int(2,0)
		}));
		patterns.Add(new Pattern("I字3縦", new List<Vector2Int> {
			new Vector2Int(0,0), new Vector2Int(0,1),
			new Vector2Int(0,2)
		}));
		patterns.Add(new Pattern("I字4横", new List<Vector2Int> {
			new Vector2Int(0,0), new Vector2Int(1,0),
			new Vector2Int(2,0), new Vector2Int(3,0)
		}));
		patterns.Add(new Pattern("I字4縦", new List<Vector2Int> {
			new Vector2Int(0,0), new Vector2Int(0,1),
			new Vector2Int(0,2), new Vector2Int(0,3)
		}));
		#endregion
		// L字パターン
		#region L
		patterns.Add(new Pattern("L字3右上", new List<Vector2Int> {
			new Vector2Int(0,0), new Vector2Int(0,1),
			new Vector2Int(1,1)
		}));
		patterns.Add(new Pattern("L字3右下", new List<Vector2Int> {
			new Vector2Int(0,1), new Vector2Int(1,1),
			new Vector2Int(1,0)
		}));
		patterns.Add(new Pattern("L字3左下", new List<Vector2Int> {
			new Vector2Int(0,0),new Vector2Int(0,1),
			new Vector2Int(1,1),
		}));
		patterns.Add(new Pattern("L字3左上", new List<Vector2Int> {
			new Vector2Int(0,0), new Vector2Int(1,0),
			new Vector2Int(0,1)
		}));
		patterns.Add(new Pattern("L字縦右上", new List<Vector2Int> {
			new Vector2Int(0,0), new Vector2Int(0,1),
			new Vector2Int(0,2), new Vector2Int(1,2)
		}));
		patterns.Add(new Pattern("L字横右上", new List<Vector2Int> {
			new Vector2Int(0,0), new Vector2Int(1,0),
			new Vector2Int(2,0), new Vector2Int(2,1)
		}));
		patterns.Add(new Pattern("L字横右下", new List<Vector2Int> {
			new Vector2Int(0,1), new Vector2Int(1,1),
			new Vector2Int(2,1), new Vector2Int(2,0)
		}));
		patterns.Add(new Pattern("L字横左下", new List<Vector2Int> {
			new Vector2Int(0,1), new Vector2Int(1,1),
			new Vector2Int(2,1), new Vector2Int(0,0)
		}));
		patterns.Add(new Pattern("L字横左上", new List<Vector2Int> {
			new Vector2Int(0,0), new Vector2Int(1,0),
			new Vector2Int(2,0), new Vector2Int(0,1)
		}));
		patterns.Add(new Pattern("L字縦右上", new List<Vector2Int> {
			new Vector2Int(0,0), new Vector2Int(0,1),
			new Vector2Int(0,2), new Vector2Int(1,2)
		}));
		patterns.Add(new Pattern("L字縦右下", new List<Vector2Int> {
			new Vector2Int(0,0), new Vector2Int(0,1),
			new Vector2Int(0,2), new Vector2Int(1,0)
		}));
		patterns.Add(new Pattern("L字縦左下", new List<Vector2Int> {
			new Vector2Int(0,0), new Vector2Int(1,0),
			new Vector2Int(1,1), new Vector2Int(1,2)
		}));
		patterns.Add(new Pattern("L字縦左上", new List<Vector2Int> {
			new Vector2Int(0,2), new Vector2Int(1,0),
			new Vector2Int(1,1), new Vector2Int(1,2)
		}));
		#endregion
		// Z 字パターン
		#region Z
		patterns.Add(new Pattern("Z字横", new List<Vector2Int> {
			new Vector2Int(0,1), new Vector2Int(1,1),
			new Vector2Int(1,0), new Vector2Int(0,2)
		}));
		patterns.Add(new Pattern("Z字縦", new List<Vector2Int> {
			new Vector2Int(0,0), new Vector2Int(0,1),
			new Vector2Int(1,1), new Vector2Int(1,2)
		}));
		#endregion
		// S 字パターン
		#region S
		patterns.Add(new Pattern("S字横", new List<Vector2Int> {
			new Vector2Int(0,0), new Vector2Int(1,0),
			new Vector2Int(1,1), new Vector2Int(2,1)
		}));
		patterns.Add(new Pattern("S字縦", new List<Vector2Int> {
			new Vector2Int(0,2), new Vector2Int(0,1),
			new Vector2Int(1,1), new Vector2Int(1,0)
		}));
		#endregion
		// O 字パターン
		patterns.Add(new Pattern("O字", new List<Vector2Int> {
			new Vector2Int(0,0),new Vector2Int(0,1),
			new Vector2Int(1,1),new Vector2Int(1,0)
		}));
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
		// ホールドフラグを戻す
		IsHeld = false;
		UpdatePatternDisplay();
	}

	public void HoldPattern()
	{
		if (!IsHeld)
		{
			Pattern ptn = holdPattern;
			holdPattern = currentPattern;
			currentPattern = ptn;
			IsHeld = true;
			UpdatePatternDisplay();
		}
	}

	public void UpdatePatternDisplay()
	{
		// 更新
		GameManager.instance.patternDisplayCurrent.ShowPattern(currentPattern.shape);
		GameManager.instance.patternDisplayNext.ShowPattern(nextPattern.shape);
		GameManager.instance.patternDisplayHold.ShowPattern(holdPattern.shape);
		Debug.Log(currentPattern.name);
	}
}
