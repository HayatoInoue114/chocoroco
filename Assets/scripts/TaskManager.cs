using System.Collections.Generic;
using System.Drawing;
using System.Numerics;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class TaskManager : MonoBehaviour
{
    public List<Task> tasks_ = new List<Task>();

    //クリアしたタスクの数
    public int clearedTaskCount;
    public int nowTaskNum;

    //タスク数
    public int StartTaskNum = 10;
    public int howManyTaksk = 0;
    //必要数
    public int needNum;
    [SerializeField] private TMP_Text needText;
    // 色
    public UnityEngine.Color whatColor;

    //必要数
    public int nextNeedNum;
    [SerializeField] private TMP_Text nextNeedText;
    // 色
    public UnityEngine.Color nextWhatColor;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Start()
    {
        //初期化
        nowTaskNum = 0;
        clearedTaskCount = 0;
        needNum = 0;
        nextNeedNum = 0;

        //タスククリア
        tasks_.Clear();

        //全タスクの生成
        for (int i = 0; i < StartTaskNum; i++)
        {
            Task task = new Task();
            int needDeleteNum = 5;

            task.Initialize(i, needDeleteNum);
            if (i % 2 == 0)
            {
                task.color = UnityEngine.Color.blue;
            }

            //PushBack
            tasks_.Add(task);
        }
        //現在のタスク数
        howManyTaksk = StartTaskNum;
    }

    //現在のタスク進行状況
    public void CheckTask(List<Block> blocks)
    {
        //現在のタスクのクリア状況
        foreach (Block block in blocks)
        {
            if (tasks_[nowTaskNum].CheckTask(block))
            {
                //クリアしてたら次のタスクへ
                nowTaskNum++;
                //クリア済み++
                clearedTaskCount++;
            }
        }
    }

    public void Update()
    {
        //必要ブロック数と今の色の計算
        needNum = tasks_[nowTaskNum].needBlockNum;
        whatColor = tasks_[nowTaskNum].color;

        if(howManyTaksk - 4 == clearedTaskCount)
        {
            CreateTask();
        }

        UpdateUI();
    }

    private void UpdateUI()
    {
        if (needText != null)
        {
            string color = "";
            if (whatColor == UnityEngine.Color.red)
            {
                color = "赤";
            }
            if (whatColor == UnityEngine.Color.blue)
            {
                color = "青";
            }
            if (whatColor == UnityEngine.Color.green)
            {
                color = "緑";
            }
            needText.text = "現在のタスク: " + color + "色のブロックを" + tasks_[nowTaskNum].needBlockNum.ToString() + "個消そう！";
        }

        //
        nextWhatColor = tasks_[nowTaskNum+1].color;

        if (nextNeedText != null)
        {
            string color = "";
            if (nextWhatColor == UnityEngine.Color.red)
            {
                color = "赤";
            }
            if (nextWhatColor == UnityEngine.Color.blue)
            {
                color = "青";
            }
            if (nextWhatColor == UnityEngine.Color.green)
            {
                color = "緑";
            }
            nextNeedText.text = "現在のタスク: " + color + "色のブロックを" + tasks_[nowTaskNum+1].needBlockNum.ToString() + "個消そう！";
        }
    }

    //タスクの生成
    private void CreateTask()
    {
        Task task = new Task();
        int needDeleteNum = 5;

        task.Initialize(howManyTaksk, needDeleteNum);

        //色をランダムで設定
        int randColor = Random.Range(0, 2);
        switch (randColor)
        {
            case 0:
                task.color = UnityEngine.Color.red;
                break;
            case 1:
                task.color = UnityEngine.Color.green;
                break;
            case 2:
                task.color = UnityEngine.Color.blue;
                break;
        }

        //PushBack
        tasks_.Add(task);

        howManyTaksk++;
    }
}