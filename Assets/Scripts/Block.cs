using NUnit.Framework.Constraints;
using UnityEngine;

public class Block : MonoBehaviour
{
	public Vector2Int GridPosition;
	private bool isSelected = false;
	public bool destroyed = false;
	public Color color = Color.white;
	public BlockManager.BlockType type;

	public AudioClip selectSE;

	public GameObject destroyEffectPrefab; // ← 追加：インスペクターでエフェクトプレハブを割り当て

	private AudioSource audioSource;

	void Awake()
	{
		audioSource = GetComponent<AudioSource>();
		// パーティクルの色をブロックの色にする
		if (destroyEffectPrefab.GetComponent<ParticleSystem>() != null)
		{
			var main = destroyEffectPrefab.GetComponent<ParticleSystem>().main;
			main.startColor = color;
		}
	}

	Vector3 growVec = new Vector3(0.08f, 0.08f, 0.0f);

	public void Select()
	{
		isSelected = true;
		GetComponent<Renderer>().material.color = color * 0.8f;

		if (audioSource != null && selectSE != null)
		{
			audioSource.PlayOneShot(selectSE);
		}
		transform.localScale = transform.localScale + growVec;
	}

	public void Unselect()
	{
		isSelected = false;
		GetComponent<Renderer>().material.color = color;
		transform.localScale = transform.localScale - growVec;
	}

	public void Decision()
	{
		destroyed = true;

		// 色を明るく変化
		GetComponent<Renderer>().material.color = color * 0.1f;



		// 自身を0.5秒後に破壊
		Destroy(gameObject, 0.5f);
	}

	private void OnDestroy()
	{
		// ★ここで破壊エフェクトを生成
		if (destroyEffectPrefab != null)
		{
			// 180度回転させる場合はQuaternion.Euler(0, 180, 0)を使用
			GameObject go = Instantiate(destroyEffectPrefab, transform.position, Quaternion.Euler(0, 180, 0));
		}
		// カメラシェイク
		CameraShake.Instance.Shake(0.2f, 0.05f, 1.0f);
	}
}
