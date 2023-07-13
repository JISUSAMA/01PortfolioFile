using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using HtmlAgilityPack;
using System.Text.RegularExpressions;
using UnityEngine.UI;



public class VersionUpdate : MonoBehaviour
{
    public GameObject versioncheck_pannel;  // 게임 업데이트 알림창 오브젝트




    private void Start()
    {
        UnsafeSecurityPolicy.Instate();
        string marketVersion = "";

        string url = "https://play.google.com/store/apps/details?id=com.gateways.bojamaja2";
        HtmlWeb web = new HtmlWeb();
        HtmlDocument doc = web.Load(url);

        foreach(HtmlNode node in doc.DocumentNode.SelectNodes("//span[@class='htlgb']"))
        {
            marketVersion = node.InnerText.Trim();
            if(marketVersion != null)
            {
                if(System.Text.RegularExpressions.Regex.IsMatch(marketVersion, @"^\d{1}\.\d{1}\.\d{1}$"))
                {
                    Debug.Log("내 앱 버전 : " + Application.version);
                    Debug.Log("마켓 버전 : " + marketVersion);

                    string a = marketVersion.ToString();
                    string b = Application.version.ToString();

                    if(a == b)
                    {
                        //같은 버전이라 알람 놉
                    }
                    else
                    {
                        //버전이 달라서 알림이 뜸
                        versioncheck_pannel.SetActive(true);
                    }
                }
            }
        }
    }

    //입력된 링크에 따라 스토어 이동
    public void OpenURL(string url)
    {
        Application.OpenURL(url);
    }
}
