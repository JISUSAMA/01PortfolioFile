using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InternetState : MonoBehaviour
{
    private void Update()
    {
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            // 인터넷 연결이 안되었을 때 행동
            // 인터넷 연결 상태가 안좋습니다. or 인터넷 연결 로딩 패널
            // 일정시간 지나면 - 네트워크의 상태가 불안합니다. 다시 접속합니다.
        }
        else if (Application.internetReachability == NetworkReachability.ReachableViaCarrierDataNetwork)
        {
            // 데이터로 연결이 되었을 때 행동
            // 모바일 데이터 접속을 허용합니다. 가입한 요금제에 따라 데이터 이용료가 발생할 수 있습니다.
        }
        else
        {
            // 와이파이로 연결이 되었을 때 행동
        }
    }
}
