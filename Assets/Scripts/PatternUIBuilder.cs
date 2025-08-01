using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PatternUIBuilder : MonoBehaviour
{
	public GameObject patternButtonPrefab; // 作成したプレハブ
	public Transform[] categoryGroups;     // 1〜4ブロックの親Transform

	public void Initializ()
	{
		CreatePatternButtons();
	}

	private void CreatePatternButtons()
	{
		var allPatterns = GameManager.instance.patternManager.patterns;
		int index = 0;
		foreach (var pattern in allPatterns)
		{
			int blockCount = pattern.shape.Count;

			// 対応するカテゴリグループを決定（1ブロックは category 0）
			int category = Mathf.Clamp(blockCount - 1, 0, categoryGroups.Length - 1);
			Transform parent = categoryGroups[category];

			// ボタンを生成して親にセット
			GameObject buttonObj = Instantiate(patternButtonPrefab, parent);
			buttonObj.transform.localScale = Vector3.one; // スケールをリセット

			// ボタンのテキストを更新
			TextMeshProUGUI text = buttonObj.GetComponentInChildren<TextMeshProUGUI>();
			if (text != null)
				text.text = "コスト:" + pattern.pointCost;

			// ボタンのPatternDisplayコンポーネントを取得してパターンを設定
			PatternDisplay patternDisplay = buttonObj.GetComponent<PatternButton>().PatternDisplay;
			patternDisplay.SetPattern(pattern.shape);


			// 押されたときの挙動を追加
			Button button = buttonObj.GetComponent<Button>();
			if (button != null)
			{
				var selectedPattern = pattern; // ラムダキャプチャ回避
				int selectedIndex = index; // ラムダキャプチャ回避
				button.onClick.AddListener(() =>
				{
					Debug.Log($"選択されたパターン: {selectedPattern.name}");
					Debug.Log($"index: {selectedIndex}");
					if (GameManager.instance.patternManager.taskBonusCount >= pattern.pointCost)
					{
						GameManager.instance.patternManager.taskBonusCount -= pattern.pointCost;
						// Patternを適用する処理を書く
						GameManager.instance.patternManager.ChangeCurrentPattern(selectedIndex);
						// パターン選択パネルを閉じる
						GameManager.instance.patternSelectPanel.SetActive(false);
						// ゲームを再開
						Time.timeScale = 1f;
						// 操作を可能に
						GameManager.instance.selectionManager.EnableSelection();
					}

				});
			}
			index++;
		}
	}
	void TrySelectPattern(Pattern pattern)
	{
	}
}
