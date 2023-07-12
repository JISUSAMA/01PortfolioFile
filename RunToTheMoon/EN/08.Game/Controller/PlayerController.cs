using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;


public class PlayerController : MonoBehaviour
{
    public float speed;      // 캐릭터 움직임 스피드.
    public float jumpSpeed; // 캐릭터 점프 힘.
    public float gravity;    // 캐릭터에게 작용하는 중력.

    private CharacterController controller; // 현재 캐릭터가 가지고있는 캐릭터 컨트롤러 콜라이더.
    private Vector3 MoveDir;                // 캐릭터의 움직이는 방향.
    public bool isEnableStairMove;
    public float starfieldSpeed = 0f;
 
    #region Singleton
    public static PlayerController Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(this);
        else Instance = this;
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        //speed = 6.0f;
        //jumpSpeed = 8.0f;
        //gravity = 20.0f;
    
        MoveDir = Vector3.zero;
        controller = GetComponent<CharacterController>();
 
    }
 
    // Update is called once per frame
    void Update()
    {
      //  bl.intensity = new MinFloatParameter(400, 0, true);
        if (controller.isGrounded)
        {
            isEnableStairMove = false;
            // 위, 아래 움직임 셋팅. 
            MoveDir = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            //MoveDir = new Vector3(0, 0, 0);

            // 벡터를 로컬 좌표계 기준에서 월드 좌표계 기준으로 변환한다.
            MoveDir = transform.TransformDirection(MoveDir);

            // 스피드 증가.
            MoveDir *= speed;

            // 별 스피드
            starfieldSpeed = 0f;

            // 캐릭터 점프
            if (Input.GetButton("Jump"))
            {
                MoveDir.y = jumpSpeed;

                // 별 스피드
                starfieldSpeed = 0.0008f;

                // 계단이 움직
                isEnableStairMove = true;
            }
            if (MoveDir.z > 0)
            {/*
                Debug.Log("움직여용! 앞으로!");
                Game_DataManager.instance.todayKcal += 0.1f;
                Game_DataManager.instance.spaceStationDis -= 0.1f;
                Game_DataManager.instance.moonDis -= 0.1f;
               */
            }

        }


        // 캐릭터에 중력 적용.
        MoveDir.y -= gravity * Time.deltaTime;

        // 캐릭터 움직임.
        controller.Move(MoveDir * Time.deltaTime);
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.CompareTag("Stair"))
        {
            
        }
    }
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        GameObject col = hit.gameObject;
        //Debug.Log(col.name);
        if (col.name.Equals("Portal"))
        {
            controller.enabled = false;
            Game_UIManager.instance.Potal_Bloom();
        }
    }
 
}
