using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exercise_DataManager : MonoBehaviour
{
    public static Exercise_DataManager instance { get; private set; }




    public int totalSoccess;    //��������

    //������
    float rightExercisePassCheckTime = 0;    //����Ÿ�̸�(���߾��)
    float rightExerciseNoPassCheckTime = 0;  //����Ÿ�̸�(����ϼ���)
    bool rightExercisePassCheck; //������Ʈ �ѹ��� ������ �ϴ� ���Ǻ���
    bool rightExerciseNoPassCheck;  //���и�Ʈ �ѹ��� �������ϴ� ���Ǻ��� 
    bool r_ExerciseNoPassDoubleCheck;   //���� ���� üũ ���� ����
    //����
    float leftExercisePassCheckTime = 0;
    float leftExerciseNoPassCheckTime = 0;
    bool leftExercisePassCheck;
    bool leftExerciseNoPassCheck;
    bool l_ExerciseNoPassDoubleCheck;

    int r_Shoulder_Curr, r_Shoulder_Before, r_Shoulder_Gap; //������ ���� ��, ������, ��(������ - ���簪)
    int l_Shoulder_Curr, l_Shoulder_Before, l_Shoulder_Gap; //���� ���簪, ������, ��(������ - ���簪)

    private void Awake()
    {
        if (instance != null)
            Destroy(this);
        else instance = this;
    }


    void Start()
    {
        //KinectExerciseUserData.instance.UserKinectExerciseDataInit();   //������� �ʱ�ȭ
    }


    void Update()
    {

    }

    //� ����� ����
    public void ResultSave(string _result)
    {
        int order = Exercise_UIManager.instance.execiseOrder;

        if (order.Equals(1))
            KinectExerciseUserData.instance.SetExercise1_PassSave(_result);
        else if (order.Equals(2))
            KinectExerciseUserData.instance.SetExercise2_PassSave(_result);
        else if (order.Equals(3))
            KinectExerciseUserData.instance.SetExercise3_PassSave(_result);
        else if (order.Equals(4))
            KinectExerciseUserData.instance.SetExercise4_PassSave(_result);
        else if (order.Equals(5))
            KinectExerciseUserData.instance.SetExercise5_PassSave(_result);
        else if (order.Equals(6))
            KinectExerciseUserData.instance.SetExercise6_PassSave(_result);
    }

    //������Ʈ �������� ������
    int VoiceRandom()
    {
        int index = Random.Range(0, 5);

        return index;
    }


    public void ExerciseCheck(string _check1, float _angle)
    {
        int order = Exercise_UIManager.instance.execiseOrder;
        if (PlayerPrefs.GetString("CARE_KinectMode").Equals("Sit"))
        {
            //�� ������ ����
            if (order.Equals(1))
            {
                ArmFrontStretchExercise(_check1, _angle);
                //ShoulderTurn(_check1, _angle);
                //RightArmTurn(_check1, _angle);
                //LeftArmTurn(_check1, _angle);
                //BothArmsPutUp(_check1, _angle);
                //BothArmsSideStretch(_check1, _angle); 
            }
            //��� ������
            else if (order.Equals(2))
            {
                ShoulderTurn(_check1, _angle);
            }
            //������ �� ������
            else if(order.Equals(3))
            {
                RightArmTurn(_check1, _angle);
            }
            //���� �� ������
            else if(order.Equals(4))
            {
                LeftArmTurn(_check1, _angle);
            }
            //���� ���� �ø���
            else if(order.Equals(5))
            {
                BothArmsPutUp(_check1, _angle);
            }
            //���� ������ ����
            else if(order.Equals(6))
            {
                BothArmsSideStretch(_check1, _angle);
            }
        }
    }

    //�� ������ ����
    public void ArmFrontStretchExercise(string _check1, float _angle)
    {
        if (_check1.Equals("RightShoulder"))
        {
            //Debug.Log("������ ��� : " + _angle.ToString("N1"));
            if (int.Parse(_angle.ToString("N0")) >= 60)
            {
                rightExerciseNoPassCheckTime = 0;    //���� Ÿ�̸� �ʱ�ȭ
                rightExercisePassCheckTime += Time.deltaTime;
                //���޴�
                if (rightExercisePassCheckTime < 5.1f)
                {
                    rightExerciseNoPassCheck = false; //���п��� false
                    r_ExerciseNoPassDoubleCheck = false;    //���п��� false

                    if (rightExercisePassCheckTime >= 5 && rightExercisePassCheck.Equals(false))
                    {
                        rightExercisePassCheck = true;  //�������� true
                        rightExercisePassCheckTime = 0;
                        //Debug.LogError("������ ���ߴ�!!!!!!!");
                    }
                }
            }
            else
            {
                rightExercisePassCheckTime = 0;  //���� Ÿ�̸� �ʱ�ȭ
                //���ߴ�
                rightExerciseNoPassCheckTime += Time.deltaTime;

                if (rightExerciseNoPassCheckTime < 3.1f)
                {
                    rightExercisePassCheck = false; //�������� false

                    if (rightExerciseNoPassCheckTime >= 3 && rightExerciseNoPassCheck.Equals(false))
                    {
                        r_ExerciseNoPassDoubleCheck = true;
                        rightExerciseNoPassCheck = true;    //���п��� true
                        rightExerciseNoPassCheckTime = 0;
                        //.LogError("������ ���ߴ�------");
                    }
                }
            }
        }

        if (_check1.Equals("LeftShoulder"))
        {
            if (int.Parse(_angle.ToString("N0")) >= 60)
            {
                //���޴�
                leftExerciseNoPassCheckTime = 0;    //���� Ÿ�̸� �ʱ�ȭ
                leftExercisePassCheckTime += Time.deltaTime;

                if (leftExercisePassCheckTime < 5.1f)
                {
                    leftExerciseNoPassCheck = false;    //���п���false
                    l_ExerciseNoPassDoubleCheck = false;    //���п���false

                    if (leftExercisePassCheckTime >= 5 && leftExercisePassCheck.Equals(false))
                    {
                        leftExercisePassCheck = true;
                        leftExercisePassCheckTime = 0;
                        //Debug.LogError("���� ���ߴ�!!!!!!!");
                    }
                }
            }
            else
            {
                //���ߴ�
                leftExercisePassCheckTime = 0;    //���� Ÿ�̸� �ʱ�ȭ
                leftExerciseNoPassCheckTime += Time.deltaTime;

                if (leftExerciseNoPassCheckTime < 3.1f)
                {
                    leftExercisePassCheck = false;  //��������false

                    if (leftExerciseNoPassCheckTime >= 3 && leftExerciseNoPassCheck.Equals(false))
                    {
                        l_ExerciseNoPassDoubleCheck = true;
                        leftExerciseNoPassCheck = true;
                        leftExerciseNoPassCheckTime = 0;
                        //Debug.LogError("���� ���ߴ�---------");
                    }
                }
            }
        }

        if (rightExercisePassCheck.Equals(true) && leftExercisePassCheck.Equals(true))
        {
            Debug.LogError("���ʸ�� ����!!!!");
            totalSoccess++;
            VoiceSound.instance.VoiceAnnouncement(VoiceRandom());   
            rightExercisePassCheck = false;
            leftExercisePassCheck = false;
        }

        if (r_ExerciseNoPassDoubleCheck.Equals(true) && l_ExerciseNoPassDoubleCheck.Equals(true))
        {
            Debug.LogError("���ʸ�� ���� --------");
            VoiceSound.instance.VoiceAnnouncement(7); //�Ⱦ�����
            r_ExerciseNoPassDoubleCheck = false;
            l_ExerciseNoPassDoubleCheck = false;
            rightExerciseNoPassCheck = false;
            leftExerciseNoPassCheck = false;
        }
        else if (rightExerciseNoPassCheck.Equals(true) && leftExerciseNoPassCheck.Equals(false) &&
            rightExerciseNoPassCheckTime > 1f)
        {
            Debug.LogError(r_ExerciseNoPassDoubleCheck + " +������ ���� --------" + l_ExerciseNoPassDoubleCheck);
            VoiceSound.instance.VoiceAnnouncement(5); //�����Ⱦ����ο÷�
            rightExerciseNoPassCheck = false;
        }
        else if (leftExerciseNoPassCheck.Equals(true) && rightExerciseNoPassCheck.Equals(false) &&
            leftExerciseNoPassCheckTime > 1f)
        {
            Debug.LogError(r_ExerciseNoPassDoubleCheck + "~���� ���� --------" + l_ExerciseNoPassDoubleCheck);
            VoiceSound.instance.VoiceAnnouncement(6); //���Ⱦ����ο÷�
            leftExerciseNoPassCheck = false;
        }
    }


    //��� ������
    public void ShoulderTurn(string _check, float _angle)
    {
        if (_check.Equals("RightShoulder"))
        {
            r_Shoulder_Curr = (int)_angle;
            r_Shoulder_Gap = Mathf.Abs(r_Shoulder_Before - r_Shoulder_Curr);
            //Debug.Log("������ - " + r_Shoulder_Gap);

            rightExercisePassCheckTime += Time.deltaTime;
            rightExerciseNoPassCheckTime += Time.deltaTime;

            //Debug.Log(rightExercisePassCheck + " ������  " + rightExercisePassCheckTime);
            if (r_Shoulder_Gap > 5f)
            {
                rightExerciseNoPassCheck = false;
                rightExerciseNoPassCheckTime = 0;
                //����
                if (rightExercisePassCheckTime >= 0f)
                {
                    if (rightExercisePassCheckTime > 5 && rightExercisePassCheck.Equals(false))
                    {
                        rightExercisePassCheckTime = 0;
                        rightExerciseNoPassCheckTime = 0;
                        rightExercisePassCheck = true;
                    }
                }
                r_Shoulder_Before = r_Shoulder_Curr;
            }
            else
            {
                //����
                if (rightExerciseNoPassCheckTime <= 3.1f)
                {
                    
                    if (rightExerciseNoPassCheckTime >= 1.5f)
                    {
                        rightExercisePassCheck = false; //�������� false
                        rightExercisePassCheckTime = 0; //�������νð� �ʱ�ȭ

                        if (rightExerciseNoPassCheckTime > 3f && rightExerciseNoPassCheck.Equals(false))
                        {
                            rightExerciseNoPassCheckTime = 0;   //�ٽ� �˻��ϱ� ���� ���� Ÿ�̸� �ʱ�ȭ
                            rightExercisePassCheckTime = 0; //���� Ȯ���̶� ���� Ÿ�̸� �ʱ�ȭ
                            rightExerciseNoPassCheck = true;
                            r_ExerciseNoPassDoubleCheck = true;
                        }
                    }
                }
                r_Shoulder_Before = r_Shoulder_Curr;
            }
        }

        if (_check.Equals("LeftShoulder"))
        {
            l_Shoulder_Curr = (int)_angle;
            l_Shoulder_Gap = Mathf.Abs(l_Shoulder_Before - l_Shoulder_Curr);
            //Debug.Log("���� - " + l_Shoulder_Gap);
            leftExercisePassCheckTime += Time.deltaTime;
            leftExerciseNoPassCheckTime += Time.deltaTime;

            if (l_Shoulder_Gap > 5f)
            {
                leftExerciseNoPassCheck = false;
                leftExerciseNoPassCheckTime = 0;
                //����
                if (leftExercisePassCheckTime >= 0)
                {
                    if (leftExercisePassCheckTime > 5 && leftExercisePassCheck.Equals(false))
                    {
                        leftExercisePassCheckTime = 0;
                        leftExerciseNoPassCheckTime = 0;
                        leftExercisePassCheck = true;
                    }
                }
                l_Shoulder_Before = l_Shoulder_Curr;
            }
            else
            {
                //����
                if (leftExerciseNoPassCheckTime < 3.1f)
                {
                    if (leftExerciseNoPassCheckTime >= 1.5f)
                    {
                        leftExercisePassCheck = false;
                        leftExercisePassCheckTime = 0;  //�����ʿ� ���ͼ� �������νð� �ʱ�ȭ
                        if (leftExerciseNoPassCheckTime > 3 && leftExerciseNoPassCheck.Equals(false))
                        {
                            leftExerciseNoPassCheckTime = 0;
                            leftExercisePassCheckTime = 0;
                            leftExerciseNoPassCheck = true;
                            l_ExerciseNoPassDoubleCheck = true;
                        }
                    }
                }
                l_Shoulder_Before = l_Shoulder_Curr;
            }
        }

        if (rightExercisePassCheck.Equals(true))
        {
            //rightExercisePassCheck = false;
            //Debug.Log("������ ����");
        }

        if (leftExercisePassCheck.Equals(true))
        {
            //leftExercisePassCheck = false;
            //Debug.Log("���� ����");
        }

        if(rightExercisePassCheck.Equals(true) && leftExercisePassCheck.Equals(true))
        {
            Debug.Log("���� ����!!!");
            totalSoccess++;
            VoiceSound.instance.VoiceAnnouncement(VoiceRandom());
            rightExercisePassCheck = false;
            leftExercisePassCheck = false;
        }
        
        if (r_ExerciseNoPassDoubleCheck.Equals(true) && l_ExerciseNoPassDoubleCheck.Equals(true))
        {
            Debug.LogError("���ʸ�� ���� ---");
            VoiceSound.instance.VoiceAnnouncement(10);   //�ȵ���
            r_ExerciseNoPassDoubleCheck = false;
            l_ExerciseNoPassDoubleCheck = false;
            rightExerciseNoPassCheck = false;
            leftExerciseNoPassCheck = false;
        }
        else if (rightExerciseNoPassCheck.Equals(true) && leftExerciseNoPassCheck.Equals(false) &&
            rightExerciseNoPassCheckTime > 1f)
        {
            Debug.LogError("������ ���� ---!!!");
            VoiceSound.instance.VoiceAnnouncement(8);   //�����U�������
            rightExerciseNoPassCheck = false;
        }
        else if (leftExerciseNoPassCheck.Equals(true) && rightExerciseNoPassCheck.Equals(false) &&
            leftExerciseNoPassCheckTime > 1f)
        {
            Debug.LogError("���� ����~~~!");
            VoiceSound.instance.VoiceAnnouncement(9);  //���ʾ������
            leftExerciseNoPassCheck = false;
        }
    }


    //������ �� ������
    public void RightArmTurn(string _check, float _angle)
    {
        if (_check.Equals("RightShoulder"))
        {
            r_Shoulder_Curr = (int)_angle;
            r_Shoulder_Gap = Mathf.Abs(r_Shoulder_Before - r_Shoulder_Curr);
            //Debug.Log("������ - " + r_Shoulder_Gap);

            rightExercisePassCheckTime += Time.deltaTime;
            rightExerciseNoPassCheckTime += Time.deltaTime;

            //Debug.Log(rightExercisePassCheck + " ������  " + rightExercisePassCheckTime);
            if (r_Shoulder_Gap > 6f)
            {
                rightExerciseNoPassCheck = false;
                rightExerciseNoPassCheckTime = 0;
                //����
                if (rightExercisePassCheckTime >= 0f)
                {
                    if (rightExercisePassCheckTime > 5 && rightExercisePassCheck.Equals(false))
                    {
                        rightExercisePassCheckTime = 0;
                        rightExerciseNoPassCheckTime = 0;
                        rightExercisePassCheck = true;
                    }
                }
                r_Shoulder_Before = r_Shoulder_Curr;
            }
            else
            {
                //����
                if (rightExerciseNoPassCheckTime <= 3.1f)
                {

                    if (rightExerciseNoPassCheckTime >= 2f)
                    {
                        rightExercisePassCheck = false; //�������� false
                        rightExercisePassCheckTime = 0; //�������νð� �ʱ�ȭ

                        if (rightExerciseNoPassCheckTime > 3f && rightExerciseNoPassCheck.Equals(false))
                        {
                            rightExerciseNoPassCheckTime = 0;   //�ٽ� �˻��ϱ� ���� ���� Ÿ�̸� �ʱ�ȭ
                            rightExercisePassCheckTime = 0; //���� Ȯ���̶� ���� Ÿ�̸� �ʱ�ȭ
                            rightExerciseNoPassCheck = true;
                        }
                    }
                }
                r_Shoulder_Before = r_Shoulder_Curr;
            }
        }

        if(rightExercisePassCheck.Equals(true))
        {
            totalSoccess++;
            rightExercisePassCheck = false;
            VoiceSound.instance.VoiceAnnouncement(VoiceRandom());
            Debug.Log("������ ����!");
        }

        if(rightExerciseNoPassCheck.Equals(true))
        {
            rightExerciseNoPassCheck = false;
            VoiceSound.instance.VoiceAnnouncement(11);  //�����ȵ���
            Debug.Log("������ ���� ~~");
        }
    }


    //���� �� ������
    public void LeftArmTurn(string _check, float _angle)
    {
        if (_check.Equals("LeftShoulder"))
        {
            l_Shoulder_Curr = (int)_angle;
            l_Shoulder_Gap = Mathf.Abs(l_Shoulder_Before - l_Shoulder_Curr);
            //Debug.Log("���� - " + l_Shoulder_Gap);
            leftExercisePassCheckTime += Time.deltaTime;
            leftExerciseNoPassCheckTime += Time.deltaTime;

            if (l_Shoulder_Gap > 6f)
            {
                leftExerciseNoPassCheck = false;
                leftExerciseNoPassCheckTime = 0;
                //����
                if (leftExercisePassCheckTime >= 0)
                {
                    if (leftExercisePassCheckTime > 5 && leftExercisePassCheck.Equals(false))
                    {
                        leftExercisePassCheckTime = 0;
                        leftExerciseNoPassCheckTime = 0;
                        leftExercisePassCheck = true;
                    }
                }
                l_Shoulder_Before = l_Shoulder_Curr;
            }
            else
            {
                //����
                if (leftExerciseNoPassCheckTime < 3.1f)
                {
                    if (leftExerciseNoPassCheckTime >= 2f)
                    {
                        leftExercisePassCheck = false;
                        leftExercisePassCheckTime = 0;  //�����ʿ� ���ͼ� �������νð� �ʱ�ȭ
                        if (leftExerciseNoPassCheckTime > 3 && leftExerciseNoPassCheck.Equals(false))
                        {
                            leftExerciseNoPassCheckTime = 0;
                            leftExercisePassCheckTime = 0;
                            leftExerciseNoPassCheck = true;
                        }
                    }
                }
                l_Shoulder_Before = l_Shoulder_Curr;
            }
        }

        if(leftExercisePassCheck.Equals(true))
        {
            totalSoccess++;
            leftExercisePassCheck = false;
            VoiceSound.instance.VoiceAnnouncement(VoiceRandom());
            Debug.Log("���� ���� ~!");
        }

        if(leftExerciseNoPassCheck.Equals(true))
        {
            leftExerciseNoPassCheck = false;
            VoiceSound.instance.VoiceAnnouncement(12);  //�����ȵ���
            Debug.Log("���� ����~!!!!");
        }
    }


    //���� ���� �ø���
    public void BothArmsPutUp(string _check, float _angle)
    {
        if (_check.Equals("RightShoulder"))
        {
            //Debug.Log("������ ��� : " + _angle.ToString("N1"));
            if (int.Parse(_angle.ToString("N0")) >= 100)
            {
                rightExerciseNoPassCheckTime = 0;    //���� Ÿ�̸� �ʱ�ȭ
                rightExercisePassCheckTime += Time.deltaTime;
                //���޴�
                if (rightExercisePassCheckTime < 5.1f)
                {
                    rightExerciseNoPassCheck = false; //���п��� false
                    r_ExerciseNoPassDoubleCheck = false;    //���п��� false

                    if (rightExercisePassCheckTime >= 5 && rightExercisePassCheck.Equals(false))
                    {
                        rightExercisePassCheck = true;  //�������� true
                        rightExercisePassCheckTime = 0;
                        //Debug.LogError("������ ���ߴ�!!!!!!!");
                    }
                }
            }
            else
            {
                rightExercisePassCheckTime = 0;  //���� Ÿ�̸� �ʱ�ȭ
                //���ߴ�
                rightExerciseNoPassCheckTime += Time.deltaTime;

                if (rightExerciseNoPassCheckTime < 3.1f)
                {
                    rightExercisePassCheck = false; //�������� false

                    if (rightExerciseNoPassCheckTime >= 3 && rightExerciseNoPassCheck.Equals(false))
                    {
                        r_ExerciseNoPassDoubleCheck = true;
                        rightExerciseNoPassCheck = true;    //���п��� true
                        rightExerciseNoPassCheckTime = 0;
                        //.LogError("������ ���ߴ�------");
                    }
                }
            }
        }

        if (_check.Equals("LeftShoulder"))
        {
            if (int.Parse(_angle.ToString("N0")) >= 100)
            {
                //���޴�
                leftExerciseNoPassCheckTime = 0;    //���� Ÿ�̸� �ʱ�ȭ
                leftExercisePassCheckTime += Time.deltaTime;

                if (leftExercisePassCheckTime < 5.1f)
                {
                    leftExerciseNoPassCheck = false;    //���п���false
                    l_ExerciseNoPassDoubleCheck = false;    //���п���false

                    if (leftExercisePassCheckTime >= 5 && leftExercisePassCheck.Equals(false))
                    {
                        leftExercisePassCheck = true;
                        leftExercisePassCheckTime = 0;
                        //Debug.LogError("���� ���ߴ�!!!!!!!");
                    }
                }
            }
            else
            {
                //���ߴ�
                leftExercisePassCheckTime = 0;    //���� Ÿ�̸� �ʱ�ȭ
                leftExerciseNoPassCheckTime += Time.deltaTime;

                if (leftExerciseNoPassCheckTime < 3.1f)
                {
                    leftExercisePassCheck = false;  //��������false

                    if (leftExerciseNoPassCheckTime >= 3 && leftExerciseNoPassCheck.Equals(false))
                    {
                        l_ExerciseNoPassDoubleCheck = true;
                        leftExerciseNoPassCheck = true;
                        leftExerciseNoPassCheckTime = 0;
                        //Debug.LogError("���� ���ߴ�---------");
                    }
                }
            }
        }

        if (rightExercisePassCheck.Equals(true) && leftExercisePassCheck.Equals(true))
        {
            Debug.LogError("���ʸ�� ����!!!!");
            totalSoccess++;
            VoiceSound.instance.VoiceAnnouncement(VoiceRandom());
            rightExercisePassCheck = false;
            leftExercisePassCheck = false;
        }

        if (r_ExerciseNoPassDoubleCheck.Equals(true) && l_ExerciseNoPassDoubleCheck.Equals(true))
        {
            Debug.LogError("���ʸ�� ���� --------");
            VoiceSound.instance.VoiceAnnouncement(18);  //���� ���� �÷�
            r_ExerciseNoPassDoubleCheck = false;
            l_ExerciseNoPassDoubleCheck = false;
            rightExerciseNoPassCheck = false;
            leftExerciseNoPassCheck = false;
        }
        else if (rightExerciseNoPassCheck.Equals(true) && leftExerciseNoPassCheck.Equals(false) &&
            rightExerciseNoPassCheckTime > 1f)
        {
            Debug.LogError(r_ExerciseNoPassDoubleCheck + " +������ ���� --------" + l_ExerciseNoPassDoubleCheck);
            VoiceSound.instance.VoiceAnnouncement(16);  //���������ο÷�
            rightExerciseNoPassCheck = false;
        }
        else if (leftExerciseNoPassCheck.Equals(true) && rightExerciseNoPassCheck.Equals(false) &&
            leftExerciseNoPassCheckTime > 1f)
        {
            Debug.LogError(r_ExerciseNoPassDoubleCheck + "~���� ���� --------" + l_ExerciseNoPassDoubleCheck);
            VoiceSound.instance.VoiceAnnouncement(17);  //�������ο÷�
            leftExerciseNoPassCheck = false;
        }
    }


    //���� ������ ����
    public void BothArmsSideStretch(string _check, float _angle)
    {
        if (_check.Equals("RightShoulder"))
        {
            //Debug.Log("������ ��� : " + _angle.ToString("N1"));
            if (int.Parse(_angle.ToString("N0")) >= 90)
            {
                rightExerciseNoPassCheckTime = 0;    //���� Ÿ�̸� �ʱ�ȭ
                rightExercisePassCheckTime += Time.deltaTime;
                //���޴�
                if (rightExercisePassCheckTime < 5.1f)
                {
                    rightExerciseNoPassCheck = false; //���п��� false
                    r_ExerciseNoPassDoubleCheck = false;    //���п��� false

                    if (rightExercisePassCheckTime >= 5 && rightExercisePassCheck.Equals(false))
                    {
                        rightExercisePassCheck = true;  //�������� true
                        rightExercisePassCheckTime = 0;
                        //Debug.LogError("������ ���ߴ�!!!!!!!");
                    }
                }
            }
            else
            {
                rightExercisePassCheckTime = 0;  //���� Ÿ�̸� �ʱ�ȭ
                //���ߴ�
                rightExerciseNoPassCheckTime += Time.deltaTime;

                if (rightExerciseNoPassCheckTime < 3.1f)
                {
                    rightExercisePassCheck = false; //�������� false

                    if (rightExerciseNoPassCheckTime >= 3 && rightExerciseNoPassCheck.Equals(false))
                    {
                        r_ExerciseNoPassDoubleCheck = true;
                        rightExerciseNoPassCheck = true;    //���п��� true
                        rightExerciseNoPassCheckTime = 0;
                        //.LogError("������ ���ߴ�------");
                    }
                }
            }
        }

        if (_check.Equals("LeftShoulder"))
        {
            if (int.Parse(_angle.ToString("N0")) >= 90)
            {
                //���޴�
                leftExerciseNoPassCheckTime = 0;    //���� Ÿ�̸� �ʱ�ȭ
                leftExercisePassCheckTime += Time.deltaTime;

                if (leftExercisePassCheckTime < 5.1f)
                {
                    leftExerciseNoPassCheck = false;    //���п���false
                    l_ExerciseNoPassDoubleCheck = false;    //���п���false

                    if (leftExercisePassCheckTime >= 5 && leftExercisePassCheck.Equals(false))
                    {
                        leftExercisePassCheck = true;
                        leftExercisePassCheckTime = 0;
                        //Debug.LogError("���� ���ߴ�!!!!!!!");
                    }
                }
            }
            else
            {
                //���ߴ�
                leftExercisePassCheckTime = 0;    //���� Ÿ�̸� �ʱ�ȭ
                leftExerciseNoPassCheckTime += Time.deltaTime;

                if (leftExerciseNoPassCheckTime < 3.1f)
                {
                    leftExercisePassCheck = false;  //��������false

                    if (leftExerciseNoPassCheckTime >= 3 && leftExerciseNoPassCheck.Equals(false))
                    {
                        l_ExerciseNoPassDoubleCheck = true;
                        leftExerciseNoPassCheck = true;
                        leftExerciseNoPassCheckTime = 0;
                        //Debug.LogError("���� ���ߴ�---------");
                    }
                }
            }
        }

        if (rightExercisePassCheck.Equals(true) && leftExercisePassCheck.Equals(true))
        {
            Debug.LogError("���ʸ�� ����!!!!");
            totalSoccess++;
            VoiceSound.instance.VoiceAnnouncement(VoiceRandom());
            rightExercisePassCheck = false;
            leftExercisePassCheck = false;
        }

        if (r_ExerciseNoPassDoubleCheck.Equals(true) && l_ExerciseNoPassDoubleCheck.Equals(true))
        {
            Debug.LogError("���ʸ�� ���� --------");
            VoiceSound.instance.VoiceAnnouncement(15);  //���ȿ�����
            r_ExerciseNoPassDoubleCheck = false;
            l_ExerciseNoPassDoubleCheck = false;
            rightExerciseNoPassCheck = false;
            leftExerciseNoPassCheck = false;
        }
        else if (rightExerciseNoPassCheck.Equals(true) && leftExerciseNoPassCheck.Equals(false) &&
            rightExerciseNoPassCheckTime > 1f)
        {
            Debug.LogError(r_ExerciseNoPassDoubleCheck + " +������ ���� --------" + l_ExerciseNoPassDoubleCheck);
            VoiceSound.instance.VoiceAnnouncement(13);  //�����ȿ�����
            rightExerciseNoPassCheck = false;
        }
        else if (leftExerciseNoPassCheck.Equals(true) && rightExerciseNoPassCheck.Equals(false) &&
            leftExerciseNoPassCheckTime > 1f)
        {
            Debug.LogError(r_ExerciseNoPassDoubleCheck + "~���� ���� --------" + l_ExerciseNoPassDoubleCheck);
            VoiceSound.instance.VoiceAnnouncement(14);  //���ȿ�����
            leftExerciseNoPassCheck = false;
        }
    }

}
