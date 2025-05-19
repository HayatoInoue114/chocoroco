using System.Collections.Generic;
using System.Drawing;
using System.Numerics;
using UnityEngine;

public class TaskManager : MonoBehaviour
{
    public List<Task> tasks_ = new List<Task>();

    //�N���A�����^�X�N�̐�
    public int clearedTaskCount;
    public int nowTaskNum;

    public int MAXTASKNUMBER = 10;
    public int needNum;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Start()
    {
        //������
        nowTaskNum = 0;
        clearedTaskCount = 0;
        needNum = 0;

        //�^�X�N�N���A
        tasks_.Clear();

        //�S�^�X�N�̐���
        for (int i = 0; i < MAXTASKNUMBER; i++)
        {
            Task task = new Task();
            task.Initialize(i);

            //PushBack
            tasks_.Add(task);
        }
    }

    //���݂̃^�X�N�i�s��
    public void CheckTask(int deleteNum)
    {
        //���݂̃^�X�N�̃N���A��
        if (tasks_[nowTaskNum].CheckTask(deleteNum))
        {
            //�N���A���Ă��玟�̃^�X�N��
            nowTaskNum++;
            //�N���A�ς�++
            clearedTaskCount++;
        }
    }

    public void Update()
    {
        needNum = tasks_[nowTaskNum].needBlockNum;
    }
}

