using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class GameOverScene : MonoBehaviour
{

	[SerializeField]
	private TMP_Text scoreText;
	[SerializeField]
	private TMP_Text highscoreText;
	[SerializeField]
	private AudioSource audioSource; // AudioResource を参照するための変数

	private int score;
	private int highScore;

	private void Start()
	{
		// スコア反映
		score = PlayerPrefs.GetInt("Score");
		scoreText.text = "Score\n" + score;
		// 演出として更新されるものがあってもいいかも
		// ハイスコア判定
		SaveHighScore();
		// ハイスコア表示
		highscoreText.text = "Top\n" + highScore;
	}

	// Update is called once per frame
	void Update()
	{
		// タイトルへ
		if (Input.GetKeyDown(KeyCode.Space)) // 例：スペースキーでゲーム開始
		{
			StartCoroutine(SceneTransition(false, 1.0f)); // 1秒後にシーンを切り替え
		}
		// リスタート
		if (Input.GetKeyDown(KeyCode.R))
		{
			StartCoroutine(SceneTransition(true, 1.0f)); // 1秒後にシーンを切り替え
		}
	}

	private void SaveHighScore()
	{
		int best = PlayerPrefs.GetInt("HighScore", 0);
		if (score > best)
		{
			highScore = score;
			PlayerPrefs.SetInt("HighScore", highScore);
			PlayerPrefs.Save();
		}
		else
		{
			highScore = best;
		}
	}

	private IEnumerator SceneTransition(bool isRestart, float delay)
	{
		audioSource.Play(); // 音声を再生
		yield return new WaitForSeconds(delay);
		if (isRestart)
		{
			SceneManager.LoadScene("GameScene"); // GameScene に切り替え
		}
		else
		{
			SceneManager.LoadScene("TitleScene");
		}
	}

}
