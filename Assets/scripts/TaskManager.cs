using System.Collections.Generic;
using System.Drawing;
using System.Numerics;
using UnityEngine;

public class TaskManager : MonoBehaviour
{
    public List<Task> tasks_ = new List<Task>();

    //クリアしたタスクの数
    public int clearedTaskCount;
    public int nowTaskNum;

    public int MAXTASKNUMBER = 10;
    public int needNum;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Start()
    {
        //初期化
        nowTaskNum = 0;
        clearedTaskCount = 0;
        needNum = 0;

        //タスククリア
        tasks_.Clear();

        //全タスクの生成
        for (int i = 0; i < MAXTASKNUMBER; i++)
        {
            Task task = new Task();
            task.Initialize(i);

            //PushBack
            tasks_.Add(task);
        }
    }

    //現在のタスク進行状況
    public void CheckTask(int deleteNum)
    {
        //現在のタスクのクリア状況
        if (tasks_[nowTaskNum].CheckTask(deleteNum))
        {
            //クリアしてたら次のタスクへ
            nowTaskNum++;
            //クリア済み++
            clearedTaskCount++;
        }
    }

    public void Update()
    {
        needNum = tasks_[nowTaskNum].needBlockNum;
    }
}

