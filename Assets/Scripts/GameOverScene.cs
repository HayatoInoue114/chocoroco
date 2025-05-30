using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScene : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) // ��F�X�y�[�X�L�[�ŃQ�[���J�n
        {
            SceneManager.LoadScene("TitleScene"); // GameScene �ɐ؂�ւ�
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene("GameScene"); // GameScene �ɐ؂�ւ�
        }
    }
}
