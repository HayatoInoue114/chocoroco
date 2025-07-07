using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


/// <summary>
/// マップやブロックを管理
/// </summary>
public class GridManager : MonoBehaviour
{
	public GameObject blockPrefab;

	//色付きブロックの確立
	public int randomPercent = 2;

    // マップ管理
    public int width = 8;
	public int height = 8;
	// ブロックを入れる
	private Block[,] grid;

	// ブロックが落ちる処理中
	public bool isDropping = false;


	public AudioClip rowClearSE;
	public AudioClip rowDropSE;
	public AudioClip rowCreateSE;
	private AudioSource audioSource;


	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
	{
		grid = new Block[width, height];
		audioSource = GetComponent<AudioSource>();
	}

	/// <summary>
	/// 消えているラインを検証、処理
	/// </summary>
	public IEnumerator ProcessClearedRows()
	{
		if (isDropping)
		{
			yield break;
		}
		var clearedRows = GetClearedRows();
		// 消えている行があるか
		if (clearedRows.Count == 0)
		{
			yield break;
		}

		// 下に落とす処理を実行
		yield return StartCoroutine(DropBlockRoutine(clearedRows));
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
		// blockPrefabが割り当てられているか確認
		if (blockPrefab == null)
		{
			Debug.LogError("GridManagerのblockPrefabがインスペクターで設定されていません！処理を中断します。");
			return; // blockPrefabがなければ何もできないので処理を中断
		}

		// インスタンス生成
		GameObject newBlock = Instantiate(blockPrefab, new Vector3(x, y, 0), Quaternion.identity);

		// 機能を参照
		// GetComponentInChildren<Collider>() を使っているため、プレハブのColliderが子オブジェクトにあることを想定
		Collider blockCollider = newBlock.GetComponentInChildren<Collider>();
		if (blockCollider == null)
		{
			Debug.LogError($"生成されたブロック ({newBlock.name}) またはその子オブジェクトに Collider が見つかりません。プレハブの構成を確認してください。");
			Destroy(newBlock); // 不完全なオブジェクトは破棄
			return;
		}

		Block block = blockCollider.GetComponent<Block>();
		if (block == null)
		{
			Debug.LogError($"Colliderを持つオブジェクト ({blockCollider.gameObject.name}) に Block スクリプトが見つかりません。プレハブの構成を確認してください。");
			Destroy(newBlock); // 不完全なオブジェクトは破棄
			return;
		}

		// 設定
		block.GridPosition = new Vector2Int(x, y);
		block.color = Color.white; // デフォルトは白

		// Rendererの取得と色の設定
		Renderer blockRenderer = block.GetComponent<Renderer>(); // Blockスクリプトと同じオブジェクトにRendererがあると想定
		if (blockRenderer == null)
		{
			// 子オブジェクトにもRendererがあるか探す場合
			// blockRenderer = newBlock.GetComponentInChildren<Renderer>(); 
			// if (blockRenderer == null)
			// {
			Debug.LogWarning($"ブロック ({block.name}) に Renderer コンポーネントが見つかりません。色は適用されません。");
			// }
		}

		// 色をランダムで設定(設定は要調整)
		// 1/3 で色付き

		int randColorChance = Random.Range(0, randomPercent);
		if (randColorChance == 0)
		{
			// 色は三つなので確率三等分
			int randColor = Random.Range(0, 3);
			switch (randColor)
			{
				case 0:
					block.color = Color.red;
					block.type = BlockManager.BlockType.RED;
					break;
				case 1:
					block.color = Color.green;
					block.type = BlockManager.BlockType.GREEN;
					break;
				case 2:
					block.color = Color.blue;
					block.type = BlockManager.BlockType.BLUE;
					break;
			}
		}
		else
		{
			block.type = BlockManager.BlockType.WHITE;
		}

		if (blockRenderer != null)
		{
			blockRenderer.material.color = block.color;
		}
		else
		{
			// Rendererがない場合でも、block.colorには色がセットされているので、
			// 他のスクリプトから色情報を参照することは可能です。
		}

		// 登録
		grid[x, y] = block;
	}

	/// <summary>
	/// 現在のパターンで消せるか
	/// </summary>
	/// <returns>消せる : true , 消せない : false</returns>
	public bool HasValidPattern(Pattern pattern)
	{
		// 演出中なので挽回の余地あり
		if (isDropping)
		{
			return true;
		}

		for (int x = 0; x < width; x++)
		{
			for (int y = 0; y < height; y++)
			{
				if (CanMatchPatternAt(x, y, pattern))
				{
					Debug.Log(pattern.name + "true");
					return true;
				}
			}
		}
		Debug.Log(pattern.name + "false");
		return false;
	}


	/// <summary>
	/// どこか一つでもパターンが一致するか
	/// </summary>
	private bool CanMatchPatternAt(int startX, int startY, Pattern pattern)
	{
		foreach (Vector2Int offset in pattern.shape)
		{
			int checkX = startX + offset.x;
			int checkY = startY + offset.y;

			// 範囲外チェック
			if (checkX < 0 || checkX >= width || checkY < 0 || checkY >= height)
				return false;

			// ブロックが存在しない or 消されてる
			if (grid[checkX, checkY].destroyed)
				return false;
		}
		return true;
	}

	IEnumerator DropBlockRoutine(List<int> rows)
	{
		// 演出中
		isDropping = true;

		// 音を鳴らす
		if (rowClearSE != null && audioSource != null)
		{
			audioSource.PlayOneShot(rowClearSE);
		}
		// 一行ずつずらす
		for (int i = 0; i < rows.Count; i++)
		{
			yield return new WaitForSeconds(0.3f);
			// 音を鳴らす
			if (rowDropSE != null && audioSource != null)
			{
				audioSource.PlayOneShot(rowDropSE);
			}
			yield return new WaitForSeconds(0.2f);
			// 下に詰めた分消えている行もずらす
			for (int y = rows[i] + 1 - i; y < height; y++)
			{
				for (int x = 0; x < width; x++)
				{
					// 下にずらす
					grid[x, y - 1] = grid[x, y];
					// 中身があるなら
					if (grid[x, y - 1] != null)
					{
						// 下に移動
						grid[x, y - 1].transform.root.position += Vector3.down;
						grid[x, y - 1].GridPosition += Vector2Int.down;
					}
					// 元の位置のものは消す
					grid[x, y] = null;
				}
			}

		}
		// 余韻
		yield return new WaitForSeconds(0.3f);

		// 音を鳴らす
		if (rowCreateSE != null && audioSource != null)
		{
			audioSource.PlayOneShot(rowCreateSE);
		}
		yield return new WaitForSeconds(0.2f);

		// 新しい行を生成
		AddNewTopRow(rows.Count);

		// 演出終了
		isDropping = false;
	}

}
