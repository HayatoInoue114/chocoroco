using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScreenFader : MonoBehaviour
{
	public Image overlayImage; // Inspectorでアサイン
	public float fadeDuration = 1f;

	public void FadeTo(Color targetColor)
	{
		StartCoroutine(FadeRoutine(targetColor));
	}

	IEnumerator FadeRoutine(Color targetColor)
	{
		Color startColor = overlayImage.color;
		float time = 0f;
		while (time < fadeDuration)
		{
			time += Time.deltaTime;
			overlayImage.color = Color.Lerp(startColor, targetColor, time / fadeDuration);
			yield return null;
		}
		overlayImage.color = targetColor;
	}
}
