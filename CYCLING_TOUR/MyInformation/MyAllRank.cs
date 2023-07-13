using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MyAllRank : MonoBehaviour
{
    public Text RankText;
    public Text MapText;
    public Image Mode;
    [SerializeField] Sprite[] modeImge;
    public Text CorseText;
    public Text BikeText;
    public Text TimeText;

    public void SetMyAllRankingData(string rank, string map, string mode, string corse, string bike, string time)
    {
        RankText.text = rank.ToString();
        MapText.text = map.ToString();
        CorseText.text = corse;
        BikeText.text = bike.ToString();
        TimeText.text = time.ToString();
        if (mode.Equals("1")) Mode.sprite = modeImge[0]; //노말
        else if (mode.Equals("2")) Mode.sprite = modeImge[1]; //하드
        else Mode.gameObject.SetActive(false);
    }
}
