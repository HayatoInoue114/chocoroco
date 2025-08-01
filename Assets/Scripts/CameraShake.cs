using UnityEngine;

public class CameraShake : MonoBehaviour
{
	public static CameraShake Instance;

	private Vector3 originalPosition;
	private float shakeDuration = 0f;
	private float shakeMagnitude = 0.1f;
	private float dampingSpeed = 1.0f;

	void Awake()
	{
		Instance = this;
		originalPosition = transform.localPosition;
	}

	void Update()
	{
		if (shakeDuration > 0)
		{
			transform.localPosition = originalPosition + Random.insideUnitSphere * shakeMagnitude;

			shakeDuration -= Time.deltaTime * dampingSpeed;
		}
		else
		{
			shakeDuration = 0f;
			transform.localPosition = originalPosition;
		}
	}


	/// <summary>
	/// カメラシェイク
	/// </summary>
	/// <param name="duration">シェイク時間</param>
	/// <param name="magnitude">揺れの強さ</param>
	/// <param name="damiping">揺れ収束の速さ/param>
	public void Shake(float duration = 0.2f, float magnitude = 0.1f,float damiping = 1.0f)
	{
		shakeDuration = duration;
		shakeMagnitude = magnitude;
		dampingSpeed = damiping;
	}
}
