using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScene : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) // ��F�X�y�[�X�L�[�ŃQ�[���J�n
        {
            SceneManager.LoadScene("GameScene"); // GameScene �ɐ؂�ւ�
        }
    }
}
