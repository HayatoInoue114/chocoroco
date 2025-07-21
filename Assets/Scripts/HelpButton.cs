using UnityEngine;
using UnityEngine.UI;

public class HelpButton : MonoBehaviour
{
	
	public GameObject panel;
	public Button helpButton;

	void Start()
	{
		// 最初は表示
		panel.SetActive(false);

		// ボタンが押されたら表示
		helpButton.onClick.AddListener(OpenWindow);
	}

	void OpenWindow()
	{
		panel.SetActive(true);

		// ゲームの入力を一時停止（必要に応じて）
		Time.timeScale = 0f;
		GameManager.instance.selectionManager.DisableSelection();
	}
}
