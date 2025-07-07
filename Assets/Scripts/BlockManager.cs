using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockManager : MonoBehaviour
{

	public AudioClip destroySE;

	// 色は後で微妙に変えるかもしれないのでタイプ判別
	// ブロックの色
	public enum BlockType
	{
		WHITE,
		RED,
		GREEN,
		BLUE,
	}

	public IEnumerator WaitUntilAllDestroyed(List<Block> targets)
	{
		// ブロックの色を取得
		List<BlockType> types = new List<BlockType>();
		foreach (Block target in targets)
		{
			types.Add(target.type);
		}

		// 破壊音を再生
		AudioSource audio = GetComponent<AudioSource>();
		if (audio != null && destroySE != null)
		{
			audio.PlayOneShot(destroySE);
		}

		// null になっていない GameObject がある限り待機
		while (targets.Exists(go => go != null))
		{
			yield return null; // 1フレーム待機してまたチェック
		}

		// 全部 null ＝消滅済み
		Debug.Log("すべてのブロックが削除されました");

		// 次の処理があればここに書く（例：行の削除チェックなど）
		foreach (BlockType type in types)
		{
			int score = type == BlockType.WHITE ? 1 : 2;
			// スコア加算呼び出し
			ScoreManager.Instance.AddScore(score);
			Debug.Log("スコア: " + score + " 加算");
		}
	}
}
