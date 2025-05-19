using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SelectionManager : MonoBehaviour
{
	private List<Block> selectedList = new List<Block>();
	public HashSet<Block> selectedSet = new HashSet<Block>();
	public bool isSelecting = false;

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
			HandleSelection();
		}
	}

	// 隣接しているか
	bool IsNeighbor(Block a, Block b)
	{
		Vector2Int pa = a.GridPosition; // 例：マス目の位置 (x,y)
		Vector2Int pb = b.GridPosition;

		Vector2Int delta = pb - pa;
		return (Mathf.Abs(delta.x) + Mathf.Abs(delta.y)) == 1;
	}

	// ひとつ前のブロックに戻ったか
	bool IsBacktrack(Block block)
	{
		if (selectedList.Count < 2)
			return false;
		return selectedList[selectedList.Count - 2] == block;
	}

	// ブロックを選択できるか
	void TrySelect(Block target)
	{
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
	}

	// 選択し終わった後の処理
	private void HandleSelection()
	{
		Debug.Log("選択終了：" + selectedSet.Count + "個のブロックを選択しました");

		// ここで形と一致するかチェックして消すなどの処理
		// 例: MatchesPattern(selectedBlocks)

		foreach (Block b in selectedSet)
		{
			// リセット（ここは条件によって消したりも可）
			b.GetComponent<Renderer>().material.color = Color.white;
		}

		selectedSet.Clear();
		selectedList.Clear();
	}
}
