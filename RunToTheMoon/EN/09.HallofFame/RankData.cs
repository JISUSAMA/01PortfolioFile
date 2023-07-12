using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Rank_
{
    public int my_rank;
    public string my_name;
    public string my_date;

    //public List<Rank> rankingList = new List<Rank>();
    public Rank_() { }

    public Rank_(int rank, string name, string date)
    {
        my_rank = rank;
        my_name = name;
        my_date = date;
    }
}

public class RankData : MonoBehaviour
{
    public GameObject Line; 
    public Text RankNum;
    public Text RankName; 
    public Text RankDate;

    public int PanelNum;
    public int PanelPosNum;
}
