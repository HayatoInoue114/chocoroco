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

	private void Awake()
	{
		LoadPatterns();

		int rand = Random.Range(0, patterns.Count);
		nextPattern = patterns[rand];
	}

	private void LoadPatterns()
	{
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
		// パターン表示リセット
		GameManager.instance.patternDisplayCullent.Clear();
		GameManager.instance.patternDisplayNext.Clear();
		// ランダムで選出
		int rand = Random.Range(0, patterns.Count);
		cullentPattern = nextPattern;
		nextPattern = patterns[rand];
		//Debug.Log("指定形状:" + cullentPattern.name);
		// パターン表示
		GameManager.instance.patternDisplayCullent.ShowPattern(cullentPattern.shape);
		GameManager.instance.patternDisplayNext.ShowPattern(nextPattern.shape);
	}
}
