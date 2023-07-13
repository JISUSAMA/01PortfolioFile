using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MailBoxPopupCtrl : MonoBehaviour
{
    public static MailBoxPopupCtrl intance { get; private set; }

    public GameObject mailListPref; //리스트생길 공지
    public GameObject mailParent;   //위치

    public Image mailIcon;  //메일 이미지

    GameObject mail_Instan; //복사본
    NoticeCtrl noticeCtrt_scip; //스크립트

    private void Awake()
    {
        if (intance != null)
            Destroy(this);
        else intance = this;


        Initialization();
        //Invoke("MailLightExistent", 1f);
    }

    //초기화
    void Initialization()
    {
        StartCoroutine(_Initialization());
    }

    IEnumerator _Initialization()
    {
        ServerManager.Instance.Get_Notice();

        yield return new WaitUntil(() => ServerManager.Instance.isNoticeSearchCompleted);

        ServerManager.Instance.isNoticeSearchCompleted = false;

        //noticeNum -> 서버에서 공지 테이블에 잇는 갯수
        for (int i = 0; i < ServerManager.Instance.notice.Count; i++)
        {
            // Delete 상태일 때 넘김
            if (ServerManager.Instance.notice[i].delete_State == 1) { continue; }

            mail_Instan = Instantiate(mailListPref, mailParent.transform);
            mail_Instan.transform.parent = mailParent.transform;
            noticeCtrt_scip = mail_Instan.GetComponent<NoticeCtrl>();
            mail_Instan.name = (i + 1).ToString();  //알림 순서를 이름으로 지정(추후 서버에 등록되는 순서번호로 지정)

            //서버에서 순서 제목 내용 삭제 여부 들고와야함 --- 서버에서 내용 들고 와야함 !!! 성엽이부분
            //if(삭제여부 == false)
            //int index = i + 1; //(추후 서버에 등록되는 순서번호로 지정)
            //string title = "안녕하세요 __ " + (i + 1);
            //string content = "후후후후 젠장 어렵다...." + (i + 1);

            uint index = ServerManager.Instance.notice[i].index; //(추후 서버에 등록되는 순서번호로 지정)
            string title = ServerManager.Instance.notice[i].title;
            string content = ServerManager.Instance.notice[i].contents;

            //bool daletaState = false;  //삭제하지않음- 테스트용
            bool readState = ServerManager.Instance.notice[i].reading_State == 1 ? true : false; //읽지 않음 - 테스트용
            noticeCtrt_scip.MyDataSet(index, title, content, readState);

            //읽은 메일 백 배경 변경 (읽었다는 표시 - 회색이미지)
            if (readState == true)
                noticeCtrt_scip.ReadMailChangeBackImage();

            //삭제 된건 안보이기 위해 //요것도 테스트 용 난쥬 지워야함
            //if (daletaState == true)
            //    Destroy(mail_Instan);
        }

        MailLightExistent();
    }

    //읽지 않은 메일이 있는지 확인 함수
    public void MailLightExistent()
    {
        int noReadCount = 0;
        //읽지 않은 메일 있는 확인 noticeNum -> 서버에서 들고오숑
        for (int i = 0; i < ServerManager.Instance.notice.Count; i++)
        {
            //noticeCtrt_scip = mailParent.transform.GetChild(i).GetComponent<NoticeCtrl>();

            //if (noticeCtrt_scip.readState == false)
            //    noReadCount++;  //읽지 않은 메일 있으면 사운트
            if (ServerManager.Instance.notice[i].reading_State == 0) { noReadCount++; }
        }

        if (noReadCount >= 1)
            StartCoroutine(ImageLight());
        else
        {
            StopAllCoroutines();    //발광 멈춤
            mailIcon.color = Color.white;   //메일 이미지 화이트
        }
    }

    //삭제 버튼 눌렀을 때 함수
    public void DeleteNoticeContent(uint _num)
    {
        StartCoroutine(_DeleteNoticeContent(_num));
    }

    IEnumerator _DeleteNoticeContent(uint _num)
    {
        //생성된 갯수만큼 돌린다.
        for (int i = 0; i < mailParent.transform.childCount; i++)
        {
            noticeCtrt_scip = mailParent.transform.GetChild(i).GetComponent<NoticeCtrl>();
            //삭제 요청한 번호와 동일한 이름을 가진 애를 삭제
            if (_num.ToString() == mailParent.transform.GetChild(i).name)
            {
                // 서버에서 공지 지운상태로 업데이트
                ServerManager.Instance.Update_NoticeDeleteStateContents(_num);

                yield return new WaitUntil(() => ServerManager.Instance.isDeleteNoticeContentsCompleted);

                ServerManager.Instance.isDeleteNoticeContentsCompleted = false;

                Destroy(mailParent.transform.GetChild(i).gameObject);
            }
        }

        // 서버에서 다시 가져오기
        ServerManager.Instance.Get_Notice();

        yield return new WaitUntil(() => ServerManager.Instance.isNoticeSearchCompleted);

        ServerManager.Instance.isNoticeSearchCompleted = false;
    }

    //새로운 메일이 있다고 메일 이미지 발광함수
    IEnumerator ImageLight()
    {
        int count = 0;

        while (count < 10)
        {
            mailIcon.color = Color.white;
            yield return new WaitForSeconds(0.5f);
            mailIcon.color = Color.yellow;
            yield return new WaitForSeconds(0.5f);
            count++;
        }
        mailIcon.color = Color.white;
        StartCoroutine(ImageLight());
    }

}