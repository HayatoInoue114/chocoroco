using System.Collections.Generic;
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
	private Block[,] grid;

	public GameObject blockPrefab;

	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
	{
		grid = new Block[width, height];
	}

	/// <summary>
	/// 消えているラインを検証、処理
	/// </summary>
	public void ProcessClearedRows()
	{
		var clearedRows = GetClearedRows();
		// 消えている行があるか
		if (clearedRows.Count == 0)
			return;

		Debug.Log("完全に消えた行:" + clearedRows.Count);

		DropBlocks(clearedRows);
		AddNewTopRow(clearedRows.Count);
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
				GameObject newBlock = Instantiate(blockPrefab, new Vector3(x, y, 0), Quaternion.identity);
				Block block = newBlock.GetComponentInChildren<Collider>().GetComponent<Block>();
				block.GridPosition = new Vector2Int(x, y);
				grid[x, y] = block;
			}
		}
	}

	/// <summary>
	/// すべてのブロックが消えている行を取得
	/// </summary>
	List<int> GetClearedRows()
	{
		List<int> clearedRows = new List<int>();
		for (int y = 0; y < height; y++)
		{
			bool isCleared = true;
			for (int x = 0; x < width; x++)
			{
				if (grid[x, y] != null)
				{
					isCleared = false;
					break;
				}
			}
			if (isCleared)
				clearedRows.Add(y);
		}
		return clearedRows;
	}
	/// <summary>
	/// 完全に消えている行を無くすように上から下へブロックを移動させる
	/// </summary>
	private void DropBlocks(List<int> clearedRows)
	{
		foreach (int row in clearedRows)
		{
			for (int y = row + 1; y < height; y++)
			{
				for (int x = 0; x < width; x++)
				{
					grid[x, y - 1] = grid[x, y];
					if (grid[x, y - 1] != null)
					{
						grid[x, y - 1].transform.position += Vector3.down;
						grid[x, y - 1].GridPosition += Vector2Int.down;
					}
					grid[x, y] = null;
				}
			}
		}
	}

	/// <summary>
	/// 一番上に足りてない行分のブロックを生成する
	/// </summary>
	/// <param name="numberOfRows">何行追加するか</param>
	private void AddNewTopRow(int numberOfRows)
	{
		for (int i = 0; i < numberOfRows; i++)
		{
			int y = height - (1 + i);
			for (int x = 0; x < width; x++)
			{
				GameObject newBlock = Instantiate(blockPrefab, new Vector3(x, y, 0), Quaternion.identity);
				Block block = newBlock.GetComponentInChildren<Collider>().GetComponent<Block>();
				block.GridPosition = new Vector2Int(x, y);
				grid[x, y] = block;
				block.Select();
			}
		}
	}
}
