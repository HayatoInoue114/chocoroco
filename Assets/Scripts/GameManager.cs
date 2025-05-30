using UnityEngine;

/// <summary>
/// ゲーム全体を管理
/// 変数の橋渡しも担う
/// </summary>
public class GameManager : MonoBehaviour
{
    // シングルトンインスタンス
    public static GameManager instance;

    // 参照できるように変数化
    public GridManager gridManager;
    public SelectionManager selectionManager;
    public PatternManager patternManager;
    public PatternDisplay patternDisplayCullent;
    public PatternDisplay patternDisplayNext;
    public PatternDisplay patternDisplayHold;
    public ScoreManager scoreManager;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            // DontDestroyOnLoad(gameObject); // シーンをまたいで GameManager を維持する場合
        }
        else if (instance != this) // 既に他のインスタンスが存在する場合
        {
            Destroy(gameObject); // このインスタンスを破棄
            return;
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
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

        scoreManager = GetComponent<ScoreManager>();
        if (scoreManager == null)
        {
            Debug.LogError("[GameManager] ScoreManager component not found on this GameObject. Please attach it in the Inspector.", this);
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

        GameObject currentAnchorObj = GameObject.Find("PatternDisplayCurrentAnchor");
        if (currentAnchorObj != null)
        {
            patternDisplayCullent = currentAnchorObj.GetComponent<PatternDisplay>();
            if (patternDisplayCullent == null)
            {
                Debug.LogError("[GameManager] PatternDisplay component not found on 'PatternDisplayCurrentAnchor' GameObject.", currentAnchorObj);
            }
        }
        else
        {
            Debug.LogError("[GameManager] 'PatternDisplayCurrentAnchor' GameObject not found in the scene.", this);
        }

    }

    // Update is called once per frame
    void Update()
    {
        // gridManager や patternManager が null でないことを確認してからメソッドを呼び出すのがより安全です。
        // Start()でエラーがあれば、これらの参照がnullのままUpdate()が呼ばれる可能性があるため。
        if (gridManager != null)
        {
            gridManager.ProcessClearedRows();
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (patternManager != null)
            {
                patternManager.HoldPattern();
            }
            else
            {
                Debug.LogWarning("[GameManager] PatternManager is not available to hold pattern.", this);
            }
        }
    }
}