using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class PatternSelectUI : MonoBehaviour
{
	public GameObject patternItemPrefab;
	public Transform[] categoryParents; // 1～4ブロックのContent（カテゴリごとのGrid）

	public List<Pattern> allPatterns;

	void Start()
	{
		foreach (var pattern in allPatterns)
		{
			GameObject item = Instantiate(patternItemPrefab, categoryParents[pattern.shape.Count - 1]);
			item.GetComponentInChildren<PatternDisplay>().SetPattern(pattern.shape);

			// ポイント制限チェック
			Button btn = item.GetComponent<Button>();
			if (pattern.pointCost > GameManager.instance.patternManager.taskBonusCount)
			{
				btn.interactable = false;
			}

			btn.onClick.AddListener(() => {
				TrySelectPattern(pattern);
			});
		}
	}

	void TrySelectPattern(Pattern pattern)
	{
		if (GameManager.instance.patternManager.taskBonusCount >= pattern.pointCost)
		{
			GameManager.instance.patternManager.taskBonusCount -= pattern.pointCost;
			// 選択処理ここに
			Debug.Log($"Selected {pattern.name}");
		}
	}
}
