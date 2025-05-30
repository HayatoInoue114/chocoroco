using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScene : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) // 例：スペースキーでゲーム開始
        {
            SceneManager.LoadScene("GameScene"); // GameScene に切り替え
        }
    }
}
