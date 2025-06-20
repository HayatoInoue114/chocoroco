using UnityEngine;
using TMPro;

public class ScoreMultiplierDisplay : MonoBehaviour
{
    public TextMeshProUGUI multiplierText;
    public float duration = 1.5f;
    public Vector3 popScale = new Vector3(1.5f, 1.5f, 1.5f);

    private float timer = 0f;
    private Color originalColor;
    private Vector3 originalScale;
    private bool isShowing = false;

    private void Start()
    {
        if (multiplierText == null)
        {
            Debug.LogError("TextMeshProUGUI is not assigned.");
            return;
        }

        originalColor = multiplierText.color;
        originalScale = multiplierText.rectTransform.localScale;
        multiplierText.alpha = 0f;
    }

    public void ShowMultiplier(float multiplier)
    {
        multiplierText.text = $"Score ×{multiplier:F1}";
        multiplierText.alpha = 1f;
        multiplierText.rectTransform.localScale = popScale;

        timer = 0f;
        isShowing = true;
    }

    private void Update()
    {
        if (!isShowing) return;

        timer += Time.deltaTime;
        float t = timer / duration;

        // Scaleアニメーション
        multiplierText.rectTransform.localScale = Vector3.Lerp(popScale, originalScale, t);

        // Alpha（透明度）を徐々に0に
        multiplierText.alpha = Mathf.Lerp(1f, 0f, t);

        if (t >= 1f)
        {
            isShowing = false;
            multiplierText.alpha = 0f;
        }
    }
}
