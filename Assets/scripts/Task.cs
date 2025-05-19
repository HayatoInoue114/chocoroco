using UnityEngine;

public class Task : MonoBehaviour
{
    //変数
    public int needBlockNum; //必要ブロック数
    public int taskNo; //タスク番号
    public bool isCleared; //クリア状態

    // Start is called once before the first execution of Update after the MonoBehaviour is created
   public void Initialize(int i)
    {
        needBlockNum = 10;
        taskNo = i;
        isCleared = false;
    }

    // Update is called once per frame
    public void Update()
    {
        
    }

    public bool CheckTask(int deletedBlockNum)
    {
        //Block削除されたら必要数から減らす
        needBlockNum -= deletedBlockNum;

        //必要数壊したら次のタスクへ移動
        if(needBlockNum <= 0)
        {
            isCleared = true;
            return true;
        }
        return false;
    }
}
