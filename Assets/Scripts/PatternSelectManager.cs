using System.Collections;
using UnityEngine;

public class PatternSelectManager : MonoBehaviour
{
	public GameObject buttonPrefab;
	public Transform contentParent;

	public void initialize()
	{
		int availablePatternIds = GetAvailablePatterns();

		for (int id = 0; id < availablePatternIds; id++)
		{
			GameObject obj = Instantiate(buttonPrefab, contentParent);
			PatternButton pb = obj.GetComponent<PatternButton>();
			pb.Initialize(id, OnPatternSelected);
		}
	}

	void OnPatternSelected(int id)
	{
		GameManager.instance.patternManager.ChangeCurrentPattern(id); // 実際のゲーム側に反映
	}

	int GetAvailablePatterns()
	{
		// 例：T字除くブロック数1〜4まで
		return GameManager.instance.patternManager.patterns.Count; // 実際のID群に差し替えてください
	}
}
