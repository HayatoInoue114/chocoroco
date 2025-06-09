using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockManager : MonoBehaviour
{
	public IEnumerator WaitUntilAllDestroyed(List<Block> targets)
	{
		// null になっていない GameObject がある限り待機
		while (targets.Exists(go => go != null))
		{
			yield return null; // 1フレーム待機してまたチェック
		}

		// 全部 null ＝消滅済み
		Debug.Log("すべてのブロックが削除されました");

		// 次の処理があればここに書く（例：行の削除チェックなど）
	}
}
