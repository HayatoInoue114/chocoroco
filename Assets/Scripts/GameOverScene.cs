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
		highscoreText.text = "High\n" + highScore;
	}

	// Update is called once per frame
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space)) // 例：スペースキーでゲーム開始
		{
			SceneManager.LoadScene("TitleScene"); // GameScene に切り替え
		}

		if (Input.GetKeyDown(KeyCode.R))
		{
			SceneManager.LoadScene("GameScene"); // GameScene に切り替え
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

}
