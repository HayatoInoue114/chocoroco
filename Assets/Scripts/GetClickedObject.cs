using UnityEngine;

public class GetClickedObject : MonoBehaviour
{
	// 画面上のクリックしたオブジェクト
	public GameObject clickedStartObject;
	public GameObject clickingObject;

	// Update is called once per frame
	void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			// オブジェクトを空にする
			clickedStartObject = null;

			// カメラからレイを出す
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit = new RaycastHit();

			// 判定
			if (Physics.Raycast(ray, out hit))
			{
				clickedStartObject = hit.collider.gameObject;
				Debug.Log(clickedStartObject.name);
			}

			//Debug.Log(clickedStartObject);
		}
		else if (Input.GetMouseButton(0))
		{
			// オブジェクトを空にする
			clickingObject = clickedStartObject;

			// カメラからレイを出す
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit = new RaycastHit();

			// 判定
			if (Physics.Raycast(ray, out hit))
			{
				clickingObject = hit.collider.gameObject;
				Debug.Log(clickingObject.name);
			}
		}
		else
		{
			clickedStartObject = null;
			clickingObject = null;
		}
	}
}
