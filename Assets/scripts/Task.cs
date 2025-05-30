using UnityEngine;

public class Task : MonoBehaviour
{
    //�ϐ�
    public int needBlockNum; //�K�v�u���b�N��
    public int taskNo; //�^�X�N�ԍ�
    public bool isCleared; //�N���A���
    Color color; //�Ή��J���[

    // Start is called once before the first execution of Update after the MonoBehaviour is created
   public void Initialize(int i, int needNum)
    {
        needBlockNum = needNum;
        taskNo = i;
        isCleared = false;
        color = Color.red;
    }

    // Update is called once per frame
    public void Update()
    {
        
    }

    public bool CheckTask(Block deletedBlock)
    {
        //�ړI�̐F�Ɠ���Block���폜���ꂽ��K�v�����猸�炷
        if (deletedBlock.color == color)
        {
            needBlockNum--;
        }

        //�K�v���󂵂��玟�̃^�X�N�ֈړ�
        if(needBlockNum <= 0)
        {
            //�N���A���Ă�����TRUE
            isCleared = true;
            return true;
        }
        return false;
    }
}
