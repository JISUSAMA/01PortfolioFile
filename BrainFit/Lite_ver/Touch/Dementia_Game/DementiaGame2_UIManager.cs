using System.Collections;
using System.Collections.Generic;
using UnityEngine; 
using UnityEngine.UI; 

public class DementiaGame2_UIManager : MonoBehaviour
{

    [Header("UI")]
    public Image ProverbImage; //���� �Ӵ� ī�� 
    public Sprite[] ProverbSpriteKinds; //�Ӵ� ī�� �̹��� ���� 
    public Text[] ProverbText; //���� �Ӵ� �ؽ�Ʈ

    public void OnClick_Proverb_Btn(int num)
    {
        if (ProverbText[num].text.Equals(DementiaGame2_DataManager.instance.Proverb_List))
        {
            DementiaGame4_DataManager.instance.TimerManager_sc.Question_Success();
        }
        else
        {

        }

    }
}
