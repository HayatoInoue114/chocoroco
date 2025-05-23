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

    public void Select()
    {
        isSelected = true;
        GetComponent<Renderer>().material.color = Color.yellow;
        //Debug.Log("選択中：" + GridPosition + "を選択");
    }

    public void Unselect()
    {
        isSelected = false;
        GetComponent<Renderer>().material.color = Color.white;
        //Debug.Log("選択中：" + GridPosition + "を選択解除");
	}

    // 選択確定
	public void Decision()
	{
        destroyed = true;
        // 色変えたり演出させる
		GetComponent<Renderer>().material.color = Color.red;
        // 親ごと消す
		Destroy(transform.root.gameObject,0.5f);
	}
}
