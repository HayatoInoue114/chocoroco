using System.Collections.Generic;
using UnityEngine;

public class PatternDisplay : MonoBehaviour
{
	public GameObject blockPrefab; // 表示用の小ブロックプレハブ
	public Transform anchor;       // 表示する場所（空の親オブジェクト）

	private List<GameObject> blocks = new();

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

	public void Clear()
	{
		foreach (var b in blocks)
			Destroy(b);
		blocks.Clear();
	}
}
