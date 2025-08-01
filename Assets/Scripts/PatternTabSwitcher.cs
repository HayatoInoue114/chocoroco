using UnityEngine;

public class PatternTabSwitcher : MonoBehaviour
{
	public GameObject[] categoryGroups; // 1~4ブロックのカテゴリ親オブジェクト
	
	public void ShowCategory(int index)
	{
		for (int i = 0; i < categoryGroups.Length; i++)
		{
			categoryGroups[i].SetActive(i == index);
		}
	}
}
