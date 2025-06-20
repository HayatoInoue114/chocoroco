using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// パターンを画面上に配置するスクリプト
/// </summary>
public class PatternDisplay : MonoBehaviour
{
	[SerializeField] private int gridSize = 4; // 4x4を想定
	[SerializeField] private Image[] blockImages; // 16個分

	public void SetPattern(List<Vector2Int> pattern)
	{
		int gridSize = 4;

		// すべて透明にリセット
		for (int i = 0; i < blockImages.Length; i++)
			blockImages[i].color = new Color(1, 1, 1, 0);

		foreach (var pos in pattern)
		{
			int flippedY = (gridSize - 1) - pos.y; // 上下反転
			int index = flippedY * gridSize + pos.x;

			if (index >= 0 && index < blockImages.Length)
				blockImages[index].color = Color.white;
		}
	}

}
