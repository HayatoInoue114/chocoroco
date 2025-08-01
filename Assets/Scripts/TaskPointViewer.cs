using TMPro;
using UnityEngine;

public class TaskPointViewer : MonoBehaviour
{
	public TMP_Text taskPointText;
	private void Update()
	{
		// タスクのポイントを表示
		if (taskPointText != null)
		{
			int points = GameManager.instance.patternManager.taskBonusCount;

			taskPointText.text = "ポイント: " + points.ToString();
		}
		else
		{
			Debug.LogWarning("Task Point Text is not assigned.");
		}
	}
}
