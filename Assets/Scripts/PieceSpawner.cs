using UnityEngine;

/// <summary>
/// 指定ブロックのなんやかんやを管理
/// </summary>
public class PieceSpawner : MonoBehaviour
{

	public GameObject[] piecePrefabs;

	public void SpawnLine()
	{
		int i = Random.Range(0, piecePrefabs.Length);
		Instantiate(piecePrefabs[i], transform.position, piecePrefabs[i].transform.rotation);
	}
}
