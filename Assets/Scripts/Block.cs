using UnityEngine;

public class Block : MonoBehaviour
{
	public Vector2Int GridPosition;
	private bool isSelected = false;
	public bool destroyed = false;
	public Color color = Color.white;
	public BlockManager.BlockType type;

	public void Select()
	{
		isSelected = true;
		GetComponent<Renderer>().material.color = color - Color.gray;
	}
	
	public void Unselect()
	{
		isSelected = false;
		GetComponent<Renderer>().material.color = color;
	}

	public void Decision()
	{
		destroyed = true;

		// 色変更＆エフェクト
		GetComponent<Renderer>().material.color = color - new Color(0.9f, 0.9f, 0.9f, 1.0f);
		Destroy(transform.root.gameObject, 0.5f);
	}
}
