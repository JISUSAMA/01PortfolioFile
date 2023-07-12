using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
public class MarketingViewManager : MonoBehaviour
{
    public Button CloseBtn; // 공지 창 닫기

    // [Header("WebView Setting")]
    // private float LeftMargin = 50;
    // private float TopMargin = 50;
    // private float RightMargin = 50;
    // private float BottomMargin = 50;
    // private bool IsRelativeMargin = true;

    public bool MarketingTabActiveBool = false;
    [SerializeField] private WebViewObject marketingViewM;
    public const string MARKETING_VIEW_OBJECT_NAME = "WebViewObject";
    [SerializeField] private GameObject WebView_PanelOb;
    [SerializeField] private GameObject WebView_BaseTargetPanel;

    public UnityAction ActionEvent_Market_Show;
    public UnityAction ActionEvent_Market_Hide;
    public const string MARKETING_VEIW_URL 
        = "https://bojamajafitness.com/article/%ED%88%AC%EC%96%B4-%EB%A7%88%EC%BC%93/2/47/";
    private Text debugText;

    public void Start()
    {
        ActionEvent_Market_Show += MarketingView_SHOW;
        ActionEvent_Market_Hide += MarketingView_HIDE;
       //MarketingView_SHOW();
    }
    public void MarketingView_SHOW()
    {
        Debug.Log("SHOW_MARKET");
        WebView_PanelOb.SetActive(true);
        MarketingView_Init();
    }
    public void MarketingView_HIDE()
    {
        Debug.Log("HIDE_MARKET");
       WebView_PanelOb.SetActive(false);
        if (marketingViewM != null) { Destroy(marketingViewM); }
    }
    private void MarketingView_Init()
    {
        int[] margins = new int[4];
        Vector3[] corners = new Vector3[4];

        WebView_BaseTargetPanel.GetComponent<RectTransform>().GetWorldCorners(corners);

        Vector2[] spos = new Vector2[4];
        spos[0] = Camera.main.WorldToScreenPoint(corners[0]); /*하단*/
        spos[1] = Camera.main.WorldToScreenPoint(corners[1]); /*왼쪽 위*/
        spos[2] = Camera.main.WorldToScreenPoint(corners[2]); /*오른쪽 위*/
        spos[3] = Camera.main.WorldToScreenPoint(corners[3]); /*우하*/

        Debug.Log("Screen.width : " + Screen.width + "Screen.with : " + Screen.height);
        Debug.Log("spos 0 : " + spos[0] + " spos 1 : " + spos[1] + " spos 2 : " + spos[2] + " spos 3 : " + spos[3]);

        margins[0] = (int)spos[0].x;
        margins[1] = (int)(Screen.height - spos[2].y);
        margins[2] = (int)(Screen.width - spos[2].x);
        margins[3] = (int)spos[0].y;

        Debug.Log("margins 0 : " + margins[0] + " margins 1 : " + margins[1] + " margins 2 : " + margins[2] + " margins 3 : " + margins[3]);

        marketingViewM = (new GameObject(MARKETING_VIEW_OBJECT_NAME)).AddComponent<WebViewObject>();
        marketingViewM.Init(
            ld: (msg) => Debug.Log(string.Format("CallOnLoaded[{0}]", msg)),
            enableWKWebView: true);

        marketingViewM.LoadURL(MARKETING_VEIW_URL);

        /*표시 위치를 결정*/
        marketingViewM.SetMargins(margins[0], margins[1], margins[2], margins[3]);
        marketingViewM.SetVisibility(true);
    }

}
