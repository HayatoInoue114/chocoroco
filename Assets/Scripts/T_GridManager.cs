using Unity.VisualScripting;
using UnityEngine;

public class T_GridManager : MonoBehaviour
{
	// マップ管理
	public int width = 8;
	public int height = 8;
	// ブロックを入れる
	private Transform[,] grid;

	public GameObject[] blockPrefabs;
	public int numOfBlockType = 2;
	public int normalBlockCount;
	public int colorBlockCount;

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
		for (int i = 0; i < width; i++)
		{
			// ブロックのタイプを決める
			int type = Random.Range(0, numOfBlockType);
			int index = 0;
			switch (type)
			{
				case 0: // normal
					index = Random.Range(0, normalBlockCount);
					break;
				case 1: // color
					index = Random.Range(0, colorBlockCount) + normalBlockCount;
					break;
			}
			// オブジェクト生成
			Vector3 pos = new Vector3((int)i, (int)height - 1, (int)0.0f);
			AddToGrid(Instantiate(blockPrefabs[index], pos, blockPrefabs[index].transform.rotation).transform);
		}
	}

	/// <summary>
	/// すべてのブロックを生成する
	/// </summary>
	public void GenerateAllLines()
	{

		for (int i = 0; i < height; i++)
		{
			for (int j = 0; j < width; j++)
			{
				// ブロックのタイプを決める
				int persent = Random.Range(0, 100) % 20 < 5 ? 2 : 1;
				Debug.Log(persent);
				int type = Random.Range(0, persent);
				int index = 0;
				switch (type)
				{
					case 0: // normal
						index = Random.Range(0, normalBlockCount);
						break;
					case 1: // color
						index = Random.Range(0, colorBlockCount) + normalBlockCount;
						break;
				}
				// オブジェクト生成
				Vector3 pos = new Vector3((int)j, (int)i, (int)0.0f);
				AddToGrid(Instantiate(blockPrefabs[index], pos, blockPrefabs[index].transform.rotation).transform);
			}
		}
	}
}
