using UnityEngine;
using UnityEngine.UI;

public class WindowScript : MonoBehaviour
{
	public GameObject panel;
	public Button functionButton;
	public Button closeButton;

	void Start()
	{
		if (panel)
			// 最初は非表示
			panel.SetActive(false);

		// 閉じるボタンがあれば設定
		closeButton.onClick.AddListener(Close);
		// ボタンが押されたら表示
		if (functionButton)
			functionButton.onClick.AddListener(OnButton);
	}

	void OnButton()
	{
		// 自分のパネルは非表示
		gameObject.SetActive(false);

		if (panel)
			// 次のパネルを表示
			panel.SetActive(true);
		else
		{
			// ゲーム再開
			Time.timeScale = 1f;
			GameManager.instance.selectionManager.EnableSelection();
		}

		// ゲームの入力を一時停止（必要に応じて）
		//Time.timeScale = 0f;
		//GameManager.instance.selectionManager.DisableSelection();
	}

	void Close()
	{
		// 自分のパネルは非表示
		gameObject.SetActive(false);
		// ゲーム再開
		Time.timeScale = 1f;
		GameManager.instance.selectionManager.EnableSelection();
	}
}
