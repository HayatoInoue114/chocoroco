using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class TitleScene : MonoBehaviour
{
	[SerializeField]
	private AudioSource audioSource; // AudioResource を参照するための変数

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space)) // 例：スペースキーでゲーム開始
		{
			audioSource.Play(); // 音声を再生

			// 数秒停止

			// シーンを切り替える
			StartCoroutine(SceneTransition(1.0f)); // 1秒後にシーンを切り替え
		}
	}

	private IEnumerator SceneTransition(float delay)
	{
		yield return new WaitForSeconds(delay);
		SceneManager.LoadScene("GameScene");
	}
}
