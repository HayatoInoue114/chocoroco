using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScene : MonoBehaviour
{
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
}
