using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Training_DataManager : MonoBehaviour
{
    public bool DescriptionOver; //����� �ð�
    public bool TutorialPlayOver;//Ʃ�丮�� �ð�
    public bool TrainingPlayOver;   //Ʈ���̴� �ð�
    public bool BreakTimeOver; //���� �ð�
    public bool VideoPlayOver = false;

   // public int Training_Number;
    public int Total_Training_count; //��� ������ ������ ���� ���ߴ°� Ȯ��
    public int TrainingRound_count; //�� ������ ��� �ߴ��� Ȯ��, 1 : �ʱ�, 2: �߱� 3:���
    public string Training_step_str;

    public string[] training_Name = {};  //� �̸�
    public float trainingTime;
    // public string[] training_descript; //�����
   

    //ó�� �÷��� �������� �� �� �ʱ�ȭ
    public void Initialization()
    {
     //   Training_Number = 0;
        Total_Training_count = 0;
        Training_step_str = "";
        DescriptionOver = false;
        TutorialPlayOver = false;
        TrainingPlayOver = false;
        VideoPlayOver = false;
        BreakTimeOver = false;
    }
    //���� �÷��� ���� ������ �ʱ�ȭ
    public void Check_PlayVideo_init()
    {
        DescriptionOver = false;
        TutorialPlayOver = false;
        TrainingPlayOver = false;
        VideoPlayOver = false;
        BreakTimeOver = false;
        TrainingRound_count = 0;
    }
  

}
