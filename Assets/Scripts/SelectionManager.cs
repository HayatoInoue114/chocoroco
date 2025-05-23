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
	// なぞった部分
	public List<Vector2Int> traced = new List<Vector2Int>();


	// Update is called once per frame
	void Update()
	{
		// クリックした瞬間
		if (Input.GetMouseButtonDown(0))
		{
			// 選択中フラグ
			isSelecting = true;
			selectedSet.Clear();
			selectedList.Clear();
			traced.Clear();
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
			if (traced.Count > 0)
			{
				HandleSelection();
			}
		}
	}

	/// <summary>
	/// 隣接しているか 
	/// </summary>
	bool IsNeighbor(Block a, Block b)
	{
		Vector2Int pa = a.GridPosition; // 例：マス目の位置 (x,y)
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
				traced.Remove(removed.GridPosition); // ← ここ！
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

		// 選択
		target.Select();
		selectedList.Add(target);
		selectedSet.Add(target);
		traced.Add(target.GridPosition);
	}

	/// <summary>
	/// 選択し終わった後の処理
	/// </summary>
	private void HandleSelection()
	{
		// 用意しているパターンと一致しているか
		Pattern match = GameManager.instance.patternManager.Match(traced);

        // 一致していない
        if (match == null)
		{
			// 選択を解除
			foreach (Block block in selectedList)
			{
				block.Unselect();
			}
			return;
		}

        //ブロック削除をタスクに渡す
        GameManager.instance.taskManager.CheckTask(selectedList);

        // 確定したのでループ
        foreach (Block b in selectedSet)
		{
			// リセット（ここは条件によって消したりも可）
			b.Decision();
		}

		// 新しくパターンを選ぶ
		GameManager.instance.patternManager.ChoosePattern();

		// リスト消去
		selectedSet.Clear();
		selectedList.Clear();
		traced.Clear();
	}
}
