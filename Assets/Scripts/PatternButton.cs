using UnityEngine;
using UnityEngine.UI;

public class PatternButton : MonoBehaviour
{
	public Button button; // Unity上でボタンを入れる
	private int patternId; // このボタンが示すパターン番号
	public PatternDisplay PatternDisplay; // パターンを表示するためのコンポーネント

	public void Initialize(int id, System.Action<int> onClick)
	{
		patternId = id;
		button.onClick.AddListener(() => onClick(patternId));
	}

	public void SetUp(Pattern pattern)
	{
		PatternDisplay.SetPattern(pattern.shape);
	}
}
