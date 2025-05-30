using UnityEngine;

public class Block : MonoBehaviour
{
    public Vector2Int GridPosition;
    private bool isSelected = false;
    public bool destroyed = false;
    public Color color = Color.white;

    public void Select()
    {
        isSelected = true;
        GetComponent<Renderer>().material.color = color + Color.gray;
    }

    public void Unselect()
    {
        isSelected = false;
        GetComponent<Renderer>().material.color = color;
    }

    public void Decision()
    {
        destroyed = true;

        // スコア加算処理
        int points = (color == Color.white) ? 1 : 2;
        ScoreManager.Instance.AddScore(points); // スコア加算呼び出し

        // 色変更＆エフェクト
        GetComponent<Renderer>().material.color = color - new Color(0.7f, 0.7f, 0.7f, 1.0f);
        Destroy(transform.root.gameObject, 0.5f);
    }
}
