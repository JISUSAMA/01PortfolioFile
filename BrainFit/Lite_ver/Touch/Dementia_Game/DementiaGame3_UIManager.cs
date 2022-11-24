using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DementiaGame3_UIManager : MonoBehaviour
{
    [Header("UI")]
    public Image ObjectImg;
    public Sprite[] ObjectSpriteKinds;
    public Text[] ObjectText;

    public void OnClick_AnswerBtn(int btnNum)
    {
        //���� ����
        if (ObjectText[btnNum].text.Equals(DementiaGame3_DataManager.instance.QusetionStr))
        {
            DementiaGame3_DataManager.instance.TimerManager_sc.Question_Success();
        }
        else
        {

        }
    }
}
