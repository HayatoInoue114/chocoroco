using System.Collections.Generic;
using UnityEngine;

public class PatternManager : MonoBehaviour
{
	// パターンを管理
	public List<Pattern> patterns = new List<Pattern>();
	// パターンを表示するためのブロック群

	// 今のパターンを持っている
	// ネクストとかホールドとかのために配列化するかも
	public Pattern cullentPattern = null;
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
		patterns.Add(new Pattern("1", new List<Vector2Int> {
			new Vector2Int(0,0)
		}));
		// L字パターン
		patterns.Add(new Pattern("L字", new List<Vector2Int> {
			new Vector2Int(0,0), new Vector2Int(0,1),
			new Vector2Int(0,2), new Vector2Int(1,2)
		}));
		// 他にも T字、I字、Z字 など
		// I字パターン
		patterns.Add(new Pattern("I字2横", new List<Vector2Int> {
			new Vector2Int(0,0), new Vector2Int(1,0),
		}));
		// I字パターン
		patterns.Add(new Pattern("I字2縦", new List<Vector2Int> {
			new Vector2Int(0,0), new Vector2Int(0,1),
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
		if (cullentPattern.Matches(traced))
		{
			return cullentPattern;
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
		cullentPattern = nextPattern;
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
			holdPattern = cullentPattern;
			cullentPattern = ptn;
			IsHeld = true;
			UpdatePatternDisplay();
		}
	}

	public void UpdatePatternDisplay()
	{
		// 消去
		GameManager.instance.patternDisplayCullent.Clear();
		GameManager.instance.patternDisplayNext.Clear();
		GameManager.instance.patternDisplayHold.Clear();

		// 更新
		GameManager.instance.patternDisplayCullent.ShowPattern(cullentPattern.shape);
		GameManager.instance.patternDisplayNext.ShowPattern(nextPattern.shape);
		GameManager.instance.patternDisplayHold.ShowPattern(holdPattern.shape);
	}
}
