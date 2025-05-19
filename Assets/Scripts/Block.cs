using UnityEngine;

public class Block : MonoBehaviour
{
	// ブロックのマス位置を設定
	public Vector2Int GridPosition;
    // 選択済み
    private bool isSelected = false;

    public void Select()
    {
        isSelected = true;
        GetComponent<Renderer>().material.color = Color.yellow;
        Debug.Log("選択中：" + GridPosition + "を選択");
    }

    public void Unselect()
    {
        isSelected = false;
        GetComponent<Renderer>().material.color = Color.white;
        Debug.Log("選択中：" + GridPosition + "を選択解除");
	}
}
