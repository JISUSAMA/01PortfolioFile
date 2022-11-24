using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DropdownManager : MonoBehaviour
{
    public static DropdownManager instance { get; private set; }

    [SerializeField] private TMP_Dropdown map_dropDown;
    [SerializeField] private TMP_Dropdown dist_dropDown;

    // ����ٿ� �޴��� �� �ɼ� : 12�� ���õ��� ���� ĸ�Ǳ���
    private string captionName = "None";
    private int[] distArray = new int[11] { 0, 5, 10, 15, 20, 25, 30, 35, 40, 45, 50};
    private string[] mapArray = new string[20] {"1 Map", "2 Map", "3 Map", "4 Map", "5 Map", "6 Map", "7 Map", "8 Map", "9 Map", "10 Map",
                                       "11 Map", "12 Map", "13 Map", "14 Map", "15 Map", "16 Map", "17 Map", "18 Map", "19 Map", "20 Map"};

    private List<TMP_Dropdown.OptionData> map_optionList;
    private List<TMP_Dropdown.OptionData> dist_optionList;

    public bool isReady;

    private void Awake()
    {
        if (instance != null)
            Destroy(this);
        else instance = this;

        isReady = false;
        // ������ ���
        map_dropDown.onValueChanged.AddListener(delegate
        {
            MapDropDownEvent(map_dropDown);
        });

        dist_dropDown.onValueChanged.AddListener(delegate
        {
            DistDropDownEvent(dist_dropDown);
        });
        
        // ����ٿ� �ʱ�ȭ
        map_dropDown.ClearOptions();
        dist_dropDown.ClearOptions();

        map_optionList = new List<TMP_Dropdown.OptionData>();
        dist_optionList = new List<TMP_Dropdown.OptionData>();

        map_optionList.Add(new TMP_Dropdown.OptionData(captionName));
        dist_optionList.Add(new TMP_Dropdown.OptionData(captionName));

        // ���� �ϴ� �ʱ�ȭ
        // �� 21��
        foreach (string str in mapArray)
        {
            map_optionList.Add(new TMP_Dropdown.OptionData(str));
        }

        // dropdown �� �ɼǰ� �߰�
        map_dropDown.AddOptions(map_optionList);
        dist_dropDown.AddOptions(dist_optionList);

        map_dropDown.value = 0; // None ����
        dist_dropDown.value = 0; // None ����
    }

    public void MapDropDownEvent(TMP_Dropdown map_dropdown)
    {
        Debug.Log("map_dropdown.value : " + map_dropdown.value);

        // 0 �� �ƴϸ� map_dropdown.value �� ������ �Ŵ����� ����
        if (map_dropdown.value != 0)
        {
            // �ʿ� �´� �Ÿ� Dist OptionData �߰�
            DistAddOptionData(map_dropdown.value);
        }
        else
        {
            // None ���� �ٲ�
            dist_dropDown.ClearOptions();
            dist_optionList.Clear();

            dist_optionList.Add(new TMP_Dropdown.OptionData(captionName));
            dist_dropDown.AddOptions(dist_optionList);
            dist_dropDown.value = 0; // None ����

            isReady = false;
        }
    }

    public void DistDropDownEvent(TMP_Dropdown dist_dropdown)
    {
        string distValue;

        if (dist_dropdown.value != 0)
        {
            // None �� �ƴ� > ������
            // ������ �Ŵ����� �ֱ�
            Debug.Log("dist_dropdown.captionText : " + dist_dropdown.options[dist_dropdown.value].text);

            distValue = dist_dropdown.options[dist_dropdown.value].text;

            PlayerPrefs.SetFloat("Moon_Distance", 1000f - float.Parse(distValue));

            // �غ��
            isReady = true;
        }
        else
        {
            isReady = false;
        }
    }

    // 1 ~ 20 > 1 Map ~ 20 Map
    private void DistAddOptionData(int index)
    {
        switch (index)
        {
            case 1:
                // None  + 0 ~ 50 : 12��
                CustomizedDistData(0);
                break;
            case 2:
                // None  + 50 ~ 100 : 12��
                CustomizedDistData(50);
                break;
            case 3:
                CustomizedDistData(100);
                break;
            case 4:
                CustomizedDistData(150);
                break;
            case 5:
                CustomizedDistData(200);
                break;
            case 6:
                CustomizedDistData(250);
                break;
            case 7:
                CustomizedDistData(300);
                break;
            case 8:
                CustomizedDistData(350);
                break;
            case 9:
                CustomizedDistData(400);
                break;
            case 10:
                CustomizedDistData(450);
                break;
            case 11:
                CustomizedDistData(500);
                break;
            case 12:
                CustomizedDistData(550);
                break;
            case 13:
                CustomizedDistData(600);
                break;
            case 14:
                CustomizedDistData(650);
                break;
            case 15:
                CustomizedDistData(700);
                break;
            case 16:
                CustomizedDistData(750);
                break;
            case 17:
                CustomizedDistData(800);
                break;
            case 18:
                CustomizedDistData(850);
                break;
            case 19:
                CustomizedDistData(900);
                break;
            case 20:
                CustomizedDistData(950);
                break;
            default:
                break;
        }
    }

    private void CustomizedDistData(int _addValue)
    {
        // Clear
        dist_dropDown.ClearOptions();
        dist_optionList.Clear();
        // None
        dist_optionList.Add(new TMP_Dropdown.OptionData(captionName));
        
        // �ʿ� ���� �Ÿ� �� �����ֱ� : 11��
        foreach (int i in distArray)
        {
            dist_optionList.Add(new TMP_Dropdown.OptionData((i + _addValue).ToString()));
        }

        dist_dropDown.AddOptions(dist_optionList);
        dist_dropDown.value = 0; // None ����
    }
}
