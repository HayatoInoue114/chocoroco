using UnityEngine;

/// <summary>
/// ブロック自体の管理
/// </summary>
public class Block : MonoBehaviour
{
	// ブロックのマス位置を設定
	public Vector2Int GridPosition;
    // 選択済み
    private bool isSelected = false;
    // 消去されている
    public bool destroyed = false;
	// 色で分ける
	// white    : 普通、一番多い
	// red      : 赤、1/3
	// green    : 緑、1/3
	// blur     : 青、1/3
	// 最初に設定して以降変更しない
	public Color color = Color.white;


    public void Select()
    {
        isSelected = true;
        // 表示する色を変える
        GetComponent<Renderer>().material.color = color + Color.gray;
    }

    public void Unselect()
    {
        isSelected = false;
        // 表示する色を変える
        GetComponent<Renderer>().material.color = color;
	}

    // 選択確定
	public void Decision()
	{
        destroyed = true;
        // 色変えたり演出させる
		GetComponent<Renderer>().material.color = color - Color.gray;
        // 親ごと消す
		Destroy(transform.root.gameObject,0.5f);
	}
}
