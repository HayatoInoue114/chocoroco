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

		//Debug.Log("完全に消えた行:" + clearedRows.Count);

		DropBlocks(clearedRows);
		AddNewTopRow(clearedRows.Count);
	}

	/// <summary>
	/// すべてのブロックを生成する
	/// </summary>
	public void GenerateAllLines()
	{
		grid = new Block[width, height];
		for (int y = 0; y < height; y++)
		{
			for (int x = 0; x < width; x++)
			{
				// ブロック生成
				CreateAndRegistBlock(x, y);
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
		for (int i = 0; i < clearedRows.Count; i++)
		{
			// 下に詰めた分消えている行もずらす
			for (int y = clearedRows[i] + 1 - i; y < height; y++)
			{
				for (int x = 0; x < width; x++)
				{
					// 下にずらす
					grid[x, y - 1] = grid[x, y];
					// 中身があるなら
					if (grid[x, y - 1] != null)
					{
						// 下に移動
						grid[x, y - 1].transform.position += Vector3.down;
						grid[x, y - 1].GridPosition += Vector2Int.down;
					}
					// 元の位置のものは消す
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
				CreateAndRegistBlock(x, y);
			}
		}
	}

	/// <summary>
	/// ブロックを作成して登録
	/// </summary>
	private void CreateAndRegistBlock(int x, int y)
	{
		// インスタンス生成
		GameObject newBlock = Instantiate(blockPrefab, new Vector3(x, y, 0), Quaternion.identity);
		// 機能を参照
		Block block = newBlock.GetComponentInChildren<Collider>().GetComponent<Block>();
		// 設定
		block.GridPosition = new Vector2Int(x, y);
		block.color = Color.white;
		// 色をランダムで設定(設定は要調整)
		// 1/10 で色付き
		int rand = Random.Range(0, 10);
		if (rand == 0)
		{
			rand = Random.Range(0, 3);
			switch (rand)
			{
				case 0:
					block.color = Color.red;
					break;
				case 1:
					block.color = Color.green;
					break;
				case 2:
					block.color = Color.blue;
					break;
			}
		}
		block.GetComponent<Renderer>().material.color = block.color;
		// 登録
		grid[x, y] = block;
	}

}
