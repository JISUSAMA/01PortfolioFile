using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoticeContentCtrl : MonoBehaviour
{
    public static NoticeContentCtrl intance { get; private set; }

    public Text titleText; //제목
    public Text contentText;   //내용

    public uint num;



    private void Awake()
    {
        if (intance != null)
            Destroy(this);
        else intance = this;
    }

    //내용보기에서 뒤로 버튼 눌렀을 때 이벤트
    public void BackButtonOn()
    {
        //자기 비활성화
        this.gameObject.SetActive(false);
    }

    //내용보기에서 삭제버튼 눌렀을 때
    public void DeletionButtonOn()
    {
        //팝업에서 번호를 넘겨줘서 이름이 해당되는 애를 삭제(순서를 이름으로 지정)
        MailBoxPopupCtrl.intance.DeleteNoticeContent(num);
    }
}