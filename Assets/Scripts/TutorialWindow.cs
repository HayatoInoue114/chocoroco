using UnityEngine;
using UnityEngine.UI;

public class TutorialWindow : MonoBehaviour
{
	public GameObject panel;
	public Button closeButton;

	void Start()
	{
		// 最初は表示
		panel.SetActive(false);

		// ボタンが押されたら非表示に
		closeButton.onClick.AddListener(CloseTutorial);

		// ゲームの入力を一時停止（必要に応じて）
		//Time.timeScale = 0f;
	}

	void CloseTutorial()
	{
		panel.SetActive(false);

		// ゲーム再開
		Time.timeScale = 1f;
		GameManager.instance.selectionManager.EnableSelection();
		// 初回表示のフラグなどがあればここで記録
		// PlayerPrefs.SetInt("TutorialShown", 1);
	}
}
