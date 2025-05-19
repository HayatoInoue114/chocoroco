using UnityEngine;

public class Task : MonoBehaviour
{
    //�ϐ�
    public int needBlockNum; //�K�v�u���b�N��
    public int taskNo; //�^�X�N�ԍ�
    public bool isCleared; //�N���A���

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
        //Block�폜���ꂽ��K�v�����猸�炷
        needBlockNum -= deletedBlockNum;

        //�K�v���󂵂��玟�̃^�X�N�ֈړ�
        if(needBlockNum <= 0)
        {
            isCleared = true;
            return true;
        }
        return false;
    }
}
