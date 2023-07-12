using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HallofFame_RankDataManager : MonoBehaviour
{
    public RankData[] RankBarOb; //한 판넬 안에있는 7개 데이터 오브젝트

    public int ranking;

    public void GrupData(int parent, int child)
    {
        int startNum = (parent + 1) * 7 - 6; //한 판넬의 시작하는 숫자
        int EndNum = (parent + 1) * 7;    //힌 판넬의 끝나는 숫자

        for (int i = 0; i < RankBarOb.Length; i++)
        {
            RankBarOb[i].PanelNum = parent + 1;
            RankBarOb[i].PanelPosNum = child + 1;
        }


        int num = (startNum + child);
        if (parent >= (HallofFame_UIManager.instance.hof_quotient))
        {
            Debug.Log("hof_quotient : " + HallofFame_UIManager.instance.hof_quotient + "parent: " + parent);
            if (child <= HallofFame_UIManager.instance.hof_rest)
            {
                Debug.Log("hof_rest 1: " + HallofFame_UIManager.instance.hof_rest + "child: " + child);
                RankBarOb[child].RankNum.text = HallofFame_UIManager.instance.rankingList[num - 1].ranking.ToString();
                RankBarOb[child].RankName.text = HallofFame_UIManager.instance.rankingList[num - 1].nickname.ToString();
                RankBarOb[child].RankDate.text = HallofFame_UIManager.instance.rankingList[num - 1].arrival_time.ToString();
            }
            else
            {
                Debug.Log("hof_rest 2: " + HallofFame_UIManager.instance.hof_rest + "child: " + child);
                RankBarOb[child].RankNum.text = "";
                RankBarOb[child].RankName.text = "";
                RankBarOb[child].RankDate.text = "";
            }
        }
        else
        {
            Debug.Log("리스트 목록 채운느중");
            RankBarOb[child].RankNum.text = HallofFame_UIManager.instance.rankingList[num - 1].ranking.ToString();
            RankBarOb[child].RankName.text = HallofFame_UIManager.instance.rankingList[num - 1].nickname.ToString();
            RankBarOb[child].RankDate.text = HallofFame_UIManager.instance.rankingList[num - 1].arrival_time.ToString();

        }

        //if (parent.Equals(HallofFame_UIManager.instance.RankLists.Count))
        //{
        //    for (int i = 0; i < 7; i++)
        //    {
        //        int num = (startNum + i);
        //        if (i < HallofFame_UIManager.instance.hof_quotient)
        //        {
        //            Debug.LogWarning("num 2 : " + num);
        //            RankBarOb[i].RankNum.text = HallofFame_UIManager.instance.rankingList[num - 1].ranking.ToString();
        //            RankBarOb[i].RankName.text = HallofFame_UIManager.instance.rankingList[num - 1].nickname.ToString();
        //            RankBarOb[i].RankDate.text = HallofFame_UIManager.instance.rankingList[num - 1].arrival_time.ToString();
        //        }

        //    }
        //}
        //else
        //{
        //    for (int i = 0; i < 7; i++)
        //    {
        //        int num = (startNum + i);
        //        if (i < HallofFame_UIManager.instance.hof_quotient)
        //        {
        //            Debug.LogWarning("num 2 : " + num);
        //            RankBarOb[i].RankNum.text = HallofFame_UIManager.instance.rankingList[num - 1].ranking.ToString();
        //            RankBarOb[i].RankName.text = HallofFame_UIManager.instance.rankingList[num - 1].nickname.ToString();
        //            RankBarOb[i].RankDate.text = HallofFame_UIManager.instance.rankingList[num - 1].arrival_time.ToString();
        //        }
        //        else
        //        {
        //            RankBarOb[i].RankNum.text = "";
        //            RankBarOb[i].RankName.text = "";
        //            RankBarOb[i].RankDate.text = "";
        //        }
        //    }
        //}
    }
}

