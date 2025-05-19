using Unity.VisualScripting;
using UnityEngine;


/// <summary>
/// マップやブロックを管理
/// </summary>
public class GridManager : MonoBehaviour
{
	// マップ管理
	public int width = 8;
	public int height = 8;
	// ブロックを入れる
	private Transform[,] grid;

	public GameObject blockPrefab;

	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
	{
		grid = new Transform[width, height];
	}

	/// <summary>
	/// セルが占有されているか
	/// </summary>
	/// <returns>true : false</returns>
	public bool IsCellOccupied(int x, int y)
	{
		return grid[x, y] != null;
	}

	/// <summary>
	/// セルにブロックを入れる
	/// </summary>
	/// <param name="block">入れるブロック</param>
	public void AddToGrid(Transform block)
	{
		Vector2 pos = (block.position);
		grid[(int)pos.x, (int)pos.y] = block;
	}

	/// <summary>
	/// 消えているラインを検証、処理
	/// </summary>
	public void CheckForLines()
	{
		// ライン消去処理
		for (int i = 0; i < grid.GetLength(0); i++)
		{
			for (int j = 0; j < grid.GetLength(1); j++)
			{ }
		}
	}

	/// <summary>
	/// 一列生成する
	/// </summary>
	public void GenerateNewLine()
	{
		for (int x = 0; x < width; x++)
		{
			// ブロック生成
			GameObject block = Instantiate(blockPrefab, new Vector3(x, height - 1, 0), Quaternion.identity);
			block.GetComponentInChildren<Collider>().GetComponent<Block>().GridPosition = new Vector2Int(x, height - 1);
			AddToGrid(block.transform);
		}
	}

	/// <summary>
	/// すべてのブロックを生成する
	/// </summary>
	public void GenerateAllLines()
	{
		for (int y = 0; y < height; y++)
		{
			for (int x = 0; x < width; x++)
			{
				// ブロック生成
				GameObject block = Instantiate(blockPrefab, new Vector3(x, y, 0), Quaternion.identity);
				block.GetComponentInChildren<Collider>().GetComponent<Block>().GridPosition = new Vector2Int(x, y);
				AddToGrid(block.transform);
			}
		}
	}
}
