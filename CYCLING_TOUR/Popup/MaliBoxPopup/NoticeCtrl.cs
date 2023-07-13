using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoticeCtrl : MonoBehaviour
{

    GameObject mailContnetPopup;

    public uint num; //순서
    public string titleStr; //제목
    public string contentStr;   //내용
    //public bool deleteState;    //삭제여부
    public bool readState;  //읽음 여부

    public Text titleText;  //내가 가지고 있는 제목텍스트
    public Text contentText;    //내가 가지고 있는 내용텍스트

    public Sprite sprite;   //읽었으면 배경 변경을 위한 이미지

    void Start()
    {
        mailContnetPopup = GameObject.Find("PopupGroup").transform.Find("MailBoxContentPopup").gameObject;
    }

    public void MyDataSet(uint _num, string _titleStr, string _contentStr, bool _readState)
    {
        num = _num;
        titleStr = _titleStr;
        contentStr = _contentStr;
        readState = _readState;

        titleText.text = _titleStr;
        //contentText.text = _contentStr;
    }

    //리스트에 있을 때 삭제 버튼 눌렀을 때 이벤트
    public void DeleteButtonOn()
    {
        //deleteState = true;    //삭제했다고 서버에 저장해야함
        ServerManager.Instance.Update_NoticeDeleteStateContents(num);

        GameObject sound = GameObject.Find("SoundManager");
        SoundMaixer sound_s = sound.GetComponent<SoundMaixer>();
        sound_s.PopupClickSoundPlay();   //클릭사운드
        // index 가 몇번인지만 확인하고 서버에서 지워주면됨
        Destroy(this.gameObject);
    }

    //리스트에 있을 때 내용보기 이벤트
    public void ContentLookButtonOn()
    {
        this.gameObject.GetComponent<Image>().sprite = sprite;
        mailContnetPopup.SetActive(true);   //내용팝업 활성화
        NoticeContentCtrl.intance.num = num;
        NoticeContentCtrl.intance.titleText.text = titleStr;
        NoticeContentCtrl.intance.contentText.text = contentStr;
        readState = true;

        // 서버에 읽은 상태 업데이트
        ServerManager.Instance.UpdateNoticeContentsReadingState(num);

        // 읽은상태 : UI에 바로 적용하기위해서 서버에 안들르고 값 바꿔줌
        ServerManager.Instance.notice[(int)num - 1].reading_State = 1;

        GameObject sound = GameObject.Find("SoundManager");
        SoundMaixer sound_s = sound.GetComponent<SoundMaixer>();
        sound_s.ClickSoundPlay();   //클릭사운드
    }

    //읽은 메일 백 배경 변경 함수
    public void ReadMailChangeBackImage()
    {
        this.gameObject.GetComponent<Image>().sprite = sprite;
    }
}