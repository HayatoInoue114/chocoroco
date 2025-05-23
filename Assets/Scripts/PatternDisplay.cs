using System.Collections.Generic;
using UnityEngine;

public class PatternDisplay : MonoBehaviour
{
	// 表示用の小ブロックプレハブ
	public GameObject blockPrefab;
	// 表示する場所（空の親オブジェクト）
	public Transform anchor;

	// 配置するオブジェクト
	private List<GameObject> blocks = new();

	/// <summary>
	/// ブロックを配置してパターンを表示する
	/// </summary>
	/// <param name="pattern">配置</param>
	public void ShowPattern(List<Vector2Int> pattern)
	{
		Clear();
		foreach (var pos in pattern)
		{
			var go = Instantiate(blockPrefab, anchor);
			go.transform.localPosition = new Vector3(pos.x, pos.y, 0); // 平面表示
			blocks.Add(go);
		}
	}

	/// <summary>
	/// 表示していたブロックを解放
	/// </summary>
	public void Clear()
	{
		foreach (var b in blocks)
			Destroy(b);
		blocks.Clear();
	}
}
