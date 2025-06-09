using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// ゲーム全体を管理
/// 変数の橋渡しも担う
/// </summary>
public class GameManager : MonoBehaviour
{
	public static GameManager instance;

	public GridManager gridManager;
	public SelectionManager selectionManager;
	public PatternManager patternManager;
	public PatternDisplay patternDisplayCurrent;
	public PatternDisplay patternDisplayNext;
	public PatternDisplay patternDisplayHold;
	public ScoreManager scoreManager;
	public ScreenFader screenFader;
	public BlockManager blockManager;

	// 状態管理用
	private enum GameState { Title, Playing, GameOver }
	private GameState currentState;
	public TaskManager taskManager;
	public bool isGameOver = false;

	private void Awake()
	{
		if (instance == null)
		{
			instance = this;
		}
		else if (instance != this)
		{
			Destroy(gameObject);
			return;
		}
	}
	void Start()
	{
		bool initializationError = false;

		// --- 参照を取得し、nullチェックを行う ---

		gridManager = GetComponent<GridManager>();
		if (gridManager == null)
		{
			Debug.LogError("[GameManager] GridManager component not found on this GameObject. Please attach it in the Inspector.", this);
			initializationError = true;
		}

		selectionManager = GetComponent<SelectionManager>();
		if (selectionManager == null)
		{
			Debug.LogError("[GameManager] SelectionManager component not found on this GameObject. Please attach it in the Inspector.", this);
			initializationError = true;
		}

		patternManager = GetComponent<PatternManager>();
		if (patternManager == null)
		{
			Debug.LogError("[GameManager] PatternManager component not found on this GameObject. Please attach it in the Inspector.", this);
			initializationError = true;
		}

		GameObject currentAnchorObj = GameObject.Find("PatternDisplayCurrentAnchor");
		if (currentAnchorObj != null)
		{
			patternDisplayCurrent = currentAnchorObj.GetComponent<PatternDisplay>();
			if (patternDisplayCurrent == null)
			{
				Debug.LogError("[GameManager] PatternDisplay component not found on 'PatternDisplayCurrentAnchor' GameObject.", currentAnchorObj);
			}
		}
		else
		{
			Debug.LogError("[GameManager] 'PatternDisplayCurrentAnchor' GameObject not found in the scene.", this);
		}

		GameObject nextAnchorObj = GameObject.Find("PatternDisplayNextAnchor");
		if (nextAnchorObj != null)
		{
			patternDisplayNext = nextAnchorObj.GetComponent<PatternDisplay>();
			if (patternDisplayNext == null)
			{
				Debug.LogError("[GameManager] PatternDisplay component not found on 'PatternDisplayNextAnchor' GameObject.", nextAnchorObj);
				initializationError = true;
			}
		}
		else
		{
			Debug.LogError("[GameManager] 'PatternDisplayNextAnchor' GameObject not found in the scene.", this);
			initializationError = true;
		}

		GameObject holdAnchorObj = GameObject.Find("PatternDisplayHoldAnchor");
		if (holdAnchorObj != null)
		{
			patternDisplayHold = holdAnchorObj.GetComponent<PatternDisplay>();
			if (patternDisplayHold == null)
			{
				Debug.LogError("[GameManager] PatternDisplay component not found on 'PatternDisplayHoldAnchor' GameObject.", holdAnchorObj);
				initializationError = true;
			}
		}
		else
		{
			Debug.LogError("[GameManager] 'PatternDisplayHoldAnchor' GameObject not found in the scene.", this);
			initializationError = true;
		}

		// スコアマネージャー
		scoreManager = GetComponent<ScoreManager>();
		if (scoreManager == null)
		{
			Debug.LogError("[GameManager] ScoreManager component not found on this GameObject. Please attach it in the Inspector.", this);
			initializationError = true;
		}

		// タスクマネージャー
		taskManager = GetComponent<TaskManager>();
		if (taskManager == null)
		{
			Debug.LogError("[GameManager] taskManager component not found on this GameObject. Please attach it in the Inspector.", this);
			initializationError = true;
		}
		
		// スクリーンをフェード
		screenFader = GetComponent<ScreenFader>();
		if (screenFader == null)
		{
			Debug.LogError("[GameManager] screenFader component not found on this GameObject. Please attach it in the Inspector.", this);
			initializationError = true;
		}

		// ブロックマネージャー
		blockManager = GetComponent<BlockManager>();
		if (blockManager == null)
		{
			Debug.LogError("[GameManager] blcokManager component not found on this GameObject. Please attach it in the Inspector.", this);
			initializationError = true;
		}

		// --- 初期化処理の実行 ---
		if (initializationError)
		{
			Debug.LogError("[GameManager] Initialization failed due to missing components or GameObjects. Further game logic might be affected. Please check the errors above.", this);
			// ゲームの実行をここで停止させるか、エラー状態を示すフラグを立てるなどの対応も検討できます。
			// enabled = false; // GameManagerのUpdateを停止するなど
			return;
		}

		// 必須コンポーネントが取得できた場合のみ実行
		if (gridManager != null)
		{
			gridManager.GenerateAllLines();
		}
		else
		{
			// このログは上のチェックで既に出ているはずだが、念のため
			Debug.LogError("[GameManager] GridManager is null, cannot generate lines.", this);
		}

		if (patternManager != null)
		{
			patternManager.ChoosePattern();
		}
		else
		{
			// このログは上のチェックで既に出ているはずだが、念のため
			Debug.LogError("[GameManager] PatternManager is null, cannot choose pattern.", this);
		}

	}

	// Update is called once per frame
	void Update()
	{
		// ゲームオーバーだったら早期リターン
		// なんかしててもいい
		if (isGameOver)
		{
			return;
		}

		if (Input.GetKeyDown(KeyCode.Escape)) // 例：スペースキーでゲーム開始
		{
			StartCoroutine(TransitionGameOver());
			isGameOver = true;
		}
		if (Input.GetKeyDown(KeyCode.Q))
		{
			patternManager.HoldPattern();
		}


	}
	public bool CanEraseWithPatterns()
	{
		// ホールド含めてまだ消せる状態か
		if (gridManager.HasValidPattern(patternManager.currentPattern) ||
			gridManager.HasValidPattern(patternManager.holdPattern))
		{
			isGameOver = false;
			return true;
		}
		isGameOver = true;
		return false;
	}

	IEnumerator TransitionGameOver()
	{
		// 操作できないようにする
		selectionManager.DisableSelection();
		Debug.Log("[DEBUG] ゲームオーバー！！");
		// スコアを保存
		PlayerPrefs.SetInt("Score", scoreManager.score);
		// 手動保存(一応)
		PlayerPrefs.Save();

		// ここらへんで演出しても、いい
		// 1 秒待つ
		yield return new WaitForSeconds(1f);
		// 画面を黒くフェードさせる
		screenFader.FadeTo(new Color(0f, 0f, 0f, 0.8f));  // 黒, 不透明度0.8

		// 2 秒待つ
		yield return new WaitForSeconds(2f);
		// シーン移動
		SceneManager.LoadScene("GameOverScene");
	}

	/// <summary>
	/// ゲームオーバーが起きる状況か判断
	/// </summary>
	public IEnumerator HandleAfterBlockClear()
	{
		// ゲームオーバー判定をここで行う
		if (!CanEraseWithPatterns())
		{
			// ゲームオーバー演出へ
			StartCoroutine(TransitionGameOver());
			yield break;
		}
	}

}