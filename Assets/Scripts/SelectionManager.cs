using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// マウスでブロックをなぞった時の処理を担当
/// </summary>

public class SelectionManager : MonoBehaviour
{
	private List<Block> selectedList = new List<Block>();
	public HashSet<Block> selectedSet = new HashSet<Block>();
	// クリック中か
	public bool isSelecting = false;
	// そもそもクリックできるか
	private bool isSelectActive = true;

	// Update is called once per frame
	void Update()
	{
		if (!isSelectActive)
		{
			return;
		}
		// クリックした瞬間
		if (Input.GetMouseButtonDown(0))
		{
			// 選択中フラグ
			isSelecting = true;
			selectedSet.Clear();
			selectedList.Clear();
		}
		// クリック中
		if (isSelecting && Input.GetMouseButton(0))
		{
			// カメラからレイを引く
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			if (Physics.Raycast(ray, out RaycastHit hit))
			{
				// ブロックだった時に実行
				Block block = hit.collider.GetComponent<Block>();
				if (block != null)
				{
					TrySelect(block);
				}
				else
					Debug.Log(hit);
			}
		}
		// 離した瞬間
		if (Input.GetMouseButtonUp(0) && isSelecting)
		{
			isSelecting = false;
			if (selectedList.Count > 0)
			{
				StartCoroutine(HandleSelection());
			}
		}

		// 右クリック
		if (Input.GetMouseButton(1))
		{
			CancelSelection();
		}
	}

	/// <summary>
	/// 隣接しているか 
	/// </summary>
	bool IsNeighbor(Block a, Block b)
	{
		// 同じブロックだった場合選択しない
		if (a == b)
			return false;
		// マス目の位置 (x,y)
		Vector2Int pa = a.GridPosition;
		Vector2Int pb = b.GridPosition;

		Vector2Int delta = pb - pa;
		return (Mathf.Abs(delta.x) + Mathf.Abs(delta.y)) == 1;
	}

	/// <summary>
	/// ひとつ前のブロックに戻ったか
	/// </summary>
	bool IsBacktrack(Block block)
	{
		if (selectedList.Count < 2)
			return false;
		return selectedList[selectedList.Count - 2] == block;
	}

	/// <summary>
	/// ブロックを選択できるか
	/// </summary>
	void TrySelect(Block target)
	{
		// 削除されているか
		if (target.destroyed)
		{
			return;
		}

		// 手戻りが発生するか調べる
		if (selectedSet.Contains(target))
		{
			if (IsBacktrack(target))
			{
				// 戻った → 最後のブロックを解除
				Block removed = selectedList[selectedList.Count - 1];
				removed.Unselect();
				selectedList.RemoveAt(selectedList.Count - 1);
				selectedSet.Remove(removed);
			}

			// それ以外のすでに選ばれたブロックは無視
			return;
		}
		// 隣接してるか調べる
		if (selectedList.Count > 0)
		{
			Block last = selectedList[selectedList.Count - 1];
			if (!IsNeighbor(last, target))
				return; // 隣接してない → 選ばせない
		}

		// 選択数を超過させない
		if (selectedSet.Count >= GameManager.instance.patternManager.currentPattern.shape.Count())
		{
			return;
		}

		// 選択
		target.Select();
		selectedList.Add(target);
		selectedSet.Add(target);
	}

	/// <summary>
	/// 選択を解除
	/// </summary>
	private void CancelSelection()
	{
		foreach (Block block in selectedList)
		{
			block.Unselect();
		}
		// 初期化
		selectedSet.Clear();
		// リスト消去
		selectedList.Clear();
		// フラグ初期化
		isSelecting = false;
		// 操作は可能にする
		EnableSelection();
	}

	/// <summary>
	/// 選択し終わった後の処理
	/// </summary>
	IEnumerator HandleSelection()
	{
		// 操作を不可能に
		DisableSelection();

		// 用意しているパターンと一致しているか
		Pattern match = GameManager.instance.patternManager.Match(GetRelativePattern());

		// 一致していない
		if (match == null)
		{
			// 選択解除
			CancelSelection();
			yield break;
		}
		/// ここから下はブロックが消されることが確定した処理 ///
		// 新しくパターンを選ぶ
		GameManager.instance.patternManager.ChoosePattern();

		Debug.Log("[DEBUG] パターン一致");

		//ブロック削除をタスクに渡す
		GameManager.instance.taskManager.CheckTask(selectedList);

		// 確定したのでループ
		foreach (Block b in selectedSet)
		{
			// リセット（ここは条件によって消したりも可）
			b.Decision();
		}
		Debug.Log("[DEBUG] デストロイ指示");

		// すべてのブロックが消えるまで待機
		yield return StartCoroutine(GameManager.instance.blockManager.WaitUntilAllDestroyed(selectedList));
		Debug.Log("[DEBUG] ブロックが消えた");

		// ブロックを下におろす処理
		yield return StartCoroutine(GameManager.instance.gridManager.ProcessClearedRows());
		Debug.Log("[DEBUG] 行の処理");

		// 初期化
		selectedSet.Clear();
		// リスト消去
		selectedList.Clear();

		// 操作可能
		EnableSelection();

		// ゲームオーバーの判定を取る
		StartCoroutine(GameManager.instance.HandleAfterBlockClear());

	}

	private List<Vector2Int> GetRelativePattern()
	{
		List<Vector2Int> traced = new List<Vector2Int>();
		foreach (Block block in selectedList)
		{
			traced.Add(block.GridPosition);
		}
		return traced;
	}

	/// <summary>
	/// 選択が可能になる
	/// </summary>
	public void EnableSelection()
	{
		// 選択可能
		isSelectActive = true;
	}

	/// <summary>
	/// 選択が不可能になる
	/// </summary>
	public void DisableSelection()
	{
		// 選択不可能
		isSelectActive = false;
	}

}
