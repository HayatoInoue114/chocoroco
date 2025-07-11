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
    }

    public void Select()
    {
        isSelected = true;
        GetComponent<Renderer>().material.color = color * 0.8f;

        if (audioSource != null && selectSE != null)
        {
            audioSource.PlayOneShot(selectSE);
        }
    }

    public void Unselect()
    {
        isSelected = false;
        GetComponent<Renderer>().material.color = color;
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
            Vector3 effectPosition = transform.position;
            effectPosition.z -= 1f; // ブロックより手前に出す
            Instantiate(destroyEffectPrefab, transform.position, Quaternion.identity);
        }
    }
}
