using UnityEngine;

public class Task : MonoBehaviour
{
    //変数
    public int needBlockNum; //必要ブロック数
    public int taskNo; //タスク番号
    public bool isCleared; //クリア状態
    public Color color; //対応カラー
    public int score; // 達成したときのスコア

    // Start is called once before the first execution of Update after the MonoBehaviour is created
   public void Initialize(int i, int needNum)
    {
        needBlockNum = needNum;
        taskNo = i;
        isCleared = false;
        color = Color.red;
        score = 5;
    }

    // Update is called once per frame
    public void Update()
    {
        
    }

    public bool CheckTask(Block deletedBlock)
    {
        // 一回だけ処理させたいものがあるので早期リターン
        if (isCleared)
        {
            return true;
        }
        //目的の色と同じBlockが削除されたら必要数から減らす
        if (deletedBlock.color == color)
        {
            needBlockNum--;
        }

        //必要数壊したら次のタスクへ移動
        if(needBlockNum <= 0)
        {
            //クリアしていたらTRUE
            isCleared = true;
            // スコア追加
            GameManager.instance.scoreManager.AddScore(score);
            // タスク完了の報酬を与える
            GameManager.instance.patternManager.AddTaskBonus();
            return true;
        }
        return false;
    }
}
