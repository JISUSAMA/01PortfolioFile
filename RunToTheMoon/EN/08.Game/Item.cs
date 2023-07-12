using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public enum Type { ColorlessStar, StarDust, EmptyStarDust, Thread, Necklace, JewlBox, Sculpture, SpaceFlower, OxygenTank_small, OxygenTank_big, Radio, 
                       YELLOW_Snack, BLUE_Snack, GREEN_Snack, ORANGE_Snack, PURPLE_Snack, Old_OxygenTank_small, Old_OxygenTank_big,
                       Canned_YELLOW, Canned_BLUE, Canned_GREEN, Canned_ORANGE, Canned_PURPLE , ItemKind , MoonPowder};
    public Type type;
    public int value;

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("Game_TypeWriterEffect.instance.Event_ing " + Game_TypeWriterEffect.instance.Event_ing);
        if (type.Equals(Type.Radio) || type.Equals(Type.Thread) ||type.Equals(Type.MoonPowder))
        {
            transform.Rotate(new Vector3(0f, 30f, 0f) * Time.deltaTime, Space.World);
        }
        else if (type.Equals(Type.JewlBox))
        {
            transform.Rotate(new Vector3(0f, 30f, 0f) * Time.deltaTime * 2f, Space.World);
        }
        else if (type.Equals(Type.YELLOW_Snack) || type.Equals(Type.BLUE_Snack) || type.Equals(Type.GREEN_Snack) || type.Equals(Type.ORANGE_Snack) || type.Equals(Type.PURPLE_Snack))
        {
            transform.Rotate(new Vector3(0f, 30f, 0f) * Time.deltaTime, Space.World);
        }
        else if (type.Equals(Type.Canned_YELLOW) || type.Equals(Type.Canned_BLUE) || type.Equals(Type.Canned_GREEN) || type.Equals(Type.Canned_ORANGE) || type.Equals(Type.Canned_PURPLE))
        {
            transform.Rotate(new Vector3(0f, 30f, 0f) * Time.deltaTime, Space.World);
        }
        else if (type.Equals(Type.OxygenTank_small) || type.Equals(Type.OxygenTank_big))
        {
            transform.Rotate(new Vector3(0f, 10f, 0f) * Time.deltaTime, Space.World);
        }
        else if (type.Equals(Type.Old_OxygenTank_small) || type.Equals(Type.Old_OxygenTank_big))
        {
            transform.Rotate(new Vector3(0f, 10f, 0f) * Time.deltaTime, Space.World);
        }
        else if (type.Equals(Type.SpaceFlower))
        {
            //transform.Rotate(new Vector3(0f, 10f, 0f) * Time.deltaTime, Space.World);
        }
        else if (type.Equals(Type.StarDust) || type.Equals(Type.EmptyStarDust))
        {
            transform.Rotate(new Vector3(0f, 10f, 0f) * Time.deltaTime, Space.World);
        }
        //아이템 설명나오는 오브젝트 회전시키기 위해서 만들어진 타입
        else if (type.Equals(Type.ItemKind))
        {
            transform.Rotate(new Vector3(0f, 30f, 0f) * Time.deltaTime, Space.World);
            
        }
    }

    // 현재 먹은 아이템에 대한 텍스트 뿌리기
    public void GetText()
    {
        // 텍스트 뿌리기
        // 현재 타입은 알고있음.
        if (type.Equals(Type.Canned_YELLOW))  // 번개
        {
            // 현재 wayPoint : 시작할 때 Game_TypeWriterEffect 에서 초기화해서 쌓아둠
            if (Game_TypeWriterEffect.instance.wayPoint == 1) { Game_TypeWriterEffect.instance.Show_EventStoryText(11); }    // 공통
            if (Game_TypeWriterEffect.instance.wayPoint == 2) { Game_TypeWriterEffect.instance.Show_EventStoryText(6); }     // 공통
            if (Game_TypeWriterEffect.instance.wayPoint == 3) { Game_TypeWriterEffect.instance.Show_EventStoryText(1); }     // 번개 노란색 통조림
            if (Game_TypeWriterEffect.instance.wayPoint == 4) { Game_TypeWriterEffect.instance.Show_EventStoryText(1); } // 스낵 오렌지
           
            if (Game_TypeWriterEffect.instance.wayPoint == 7) { Game_TypeWriterEffect.instance.Show_EventStoryText(2); } // 공통
            if (Game_TypeWriterEffect.instance.wayPoint == 8) { Game_TypeWriterEffect.instance.Show_EventStoryText(4); } // 공통
            if (Game_TypeWriterEffect.instance.wayPoint == 9) { Game_TypeWriterEffect.instance.Show_EventStoryText(1); } // 공통
            if (Game_TypeWriterEffect.instance.wayPoint == 10) { Game_TypeWriterEffect.instance.Show_EventStoryText(5); } // 공통
            //if(Game_TypeWriterEffect.instance.wayPoint == 11) { Game_TypeWriterEffect.instance.Show_EventStoryText(1); } // 들어가야하낭?.. 아무거나라도
            if (Game_TypeWriterEffect.instance.wayPoint == 12) { Game_TypeWriterEffect.instance.Show_EventStoryText(3); }    // 공통
            if (Game_TypeWriterEffect.instance.wayPoint == 13) { Game_TypeWriterEffect.instance.Show_EventStoryText(3); }    // 공통
            if (Game_TypeWriterEffect.instance.wayPoint == 14) { Game_TypeWriterEffect.instance.Show_EventStoryText(1); }    // 공통
          
            if (Game_TypeWriterEffect.instance.wayPoint == 18) { Game_TypeWriterEffect.instance.Show_EventStoryText(3); }      // 통조림
            if (Game_TypeWriterEffect.instance.wayPoint == 19) { Game_TypeWriterEffect.instance.Show_EventStoryText(2); }        // 공통

         //   StartCoroutine(_ItemDescription(Type.Canned_YELLOW));
        }
        else if (type.Equals(Type.Canned_BLUE))
        {
            // 현재 wayPoint : 시작할 때 Game_TypeWriterEffect 에서 초기화해서 쌓아둠
            if (Game_TypeWriterEffect.instance.wayPoint == 1) { Game_TypeWriterEffect.instance.Show_EventStoryText(11); }    // 공통
            if (Game_TypeWriterEffect.instance.wayPoint == 2) { Game_TypeWriterEffect.instance.Show_EventStoryText(6); }     // 공통
            //if (Game_TypeWriterEffect.instance.wayPoint == 3) { Game_TypeWriterEffect.instance.Show_EventStoryText(1); }     // 번개 노란색 통조림
            //if(Game_TypeWriterEffect.instance.wayPoint == 4) { Game_TypeWriterEffect.instance.Show_EventStoryText(1); } // 스낵 오렌지
            if (Game_TypeWriterEffect.instance.wayPoint == 5) { Game_TypeWriterEffect.instance.Show_EventStoryText(4); } // 행성
            //if(Game_TypeWriterEffect.instance.wayPoint == 6) { Game_TypeWriterEffect.instance.Show_EventStoryText(1); } // 달과 별이 그려진 스낵
            if (Game_TypeWriterEffect.instance.wayPoint == 7) { Game_TypeWriterEffect.instance.Show_EventStoryText(2); } // 공통
            if (Game_TypeWriterEffect.instance.wayPoint == 8) { Game_TypeWriterEffect.instance.Show_EventStoryText(4); } // 공통
            if (Game_TypeWriterEffect.instance.wayPoint == 9) { Game_TypeWriterEffect.instance.Show_EventStoryText(1); } // 공통
            if (Game_TypeWriterEffect.instance.wayPoint == 10) { Game_TypeWriterEffect.instance.Show_EventStoryText(5); } // 공통
            //if(Game_TypeWriterEffect.instance.wayPoint == 11) { Game_TypeWriterEffect.instance.Show_EventStoryText(1); }  // 나래이션 존재 X
            if (Game_TypeWriterEffect.instance.wayPoint == 12) { Game_TypeWriterEffect.instance.Show_EventStoryText(3); }    // 공통
            if (Game_TypeWriterEffect.instance.wayPoint == 13) { Game_TypeWriterEffect.instance.Show_EventStoryText(3); }    // 공통
            if (Game_TypeWriterEffect.instance.wayPoint == 14) { Game_TypeWriterEffect.instance.Show_EventStoryText(1); }    // 공통

            if (Game_TypeWriterEffect.instance.wayPoint == 18) { Game_TypeWriterEffect.instance.Show_EventStoryText(3); }      // 통조림
            if (Game_TypeWriterEffect.instance.wayPoint == 19) { Game_TypeWriterEffect.instance.Show_EventStoryText(2); }        // 공통
           // StartCoroutine(_ItemDescription(Type.Canned_BLUE));
        }
        else if (type.Equals(Type.Canned_GREEN))
        {
            // 현재 wayPoint : 시작할 때 Game_TypeWriterEffect 에서 초기화해서 쌓아둠
            if (Game_TypeWriterEffect.instance.wayPoint == 1) { Game_TypeWriterEffect.instance.Show_EventStoryText(11); }    // 공통
            if (Game_TypeWriterEffect.instance.wayPoint == 2) { Game_TypeWriterEffect.instance.Show_EventStoryText(6); }     // 공통
            //if (Game_TypeWriterEffect.instance.wayPoint == 3) { Game_TypeWriterEffect.instance.Show_EventStoryText(1); }     // 번개 노란색 통조림
            //if(Game_TypeWriterEffect.instance.wayPoint == 4) { Game_TypeWriterEffect.instance.Show_EventStoryText(1); } // 스낵 오렌지
            //if(Game_TypeWriterEffect.instance.wayPoint == 5) { Game_TypeWriterEffect.instance.Show_EventStoryText(4); } // 행성
            if (Game_TypeWriterEffect.instance.wayPoint == 6) { Game_TypeWriterEffect.instance.Show_EventStoryText(1); } // 달과 별이 그려진 스낵
            if (Game_TypeWriterEffect.instance.wayPoint == 7) { Game_TypeWriterEffect.instance.Show_EventStoryText(2); } // 공통
            if (Game_TypeWriterEffect.instance.wayPoint == 8) { Game_TypeWriterEffect.instance.Show_EventStoryText(4); } // 공통
            if (Game_TypeWriterEffect.instance.wayPoint == 9) { Game_TypeWriterEffect.instance.Show_EventStoryText(1); } // 공통
            if (Game_TypeWriterEffect.instance.wayPoint == 10) { Game_TypeWriterEffect.instance.Show_EventStoryText(5); } // 공통
            //if(Game_TypeWriterEffect.instance.wayPoint == 11) { Game_TypeWriterEffect.instance.Show_EventStoryText(1); }  // 나래이션 존재 X
            if (Game_TypeWriterEffect.instance.wayPoint == 12) { Game_TypeWriterEffect.instance.Show_EventStoryText(3); }    // 공통
            if (Game_TypeWriterEffect.instance.wayPoint == 13) { Game_TypeWriterEffect.instance.Show_EventStoryText(3); }    // 공통
            if (Game_TypeWriterEffect.instance.wayPoint == 14) { Game_TypeWriterEffect.instance.Show_EventStoryText(1); }    // 공통
            //if(Game_TypeWriterEffect.instance.wayPoint == 15) { Game_TypeWriterEffect.instance.Show_EventStoryText(3); }    // 별그림 스낵
            //if(Game_TypeWriterEffect.instance.wayPoint == 16) { Game_TypeWriterEffect.instance.Show_EventStoryText(1); }  // 나래이션 존재 X
            //if(Game_TypeWriterEffect.instance.wayPoint == 17) { Game_TypeWriterEffect.instance.Show_EventStoryText(1); }  // 나래이션 존재 X
            if (Game_TypeWriterEffect.instance.wayPoint == 18) { Game_TypeWriterEffect.instance.Show_EventStoryText(3); }      // 통조림
            if (Game_TypeWriterEffect.instance.wayPoint == 19) { Game_TypeWriterEffect.instance.Show_EventStoryText(2); }        // 공통
                                                                                                                                 //if(Game_TypeWriterEffect.instance.wayPoint == 20) { Game_TypeWriterEffect.instance.Show_EventStoryText(1); }    // 퍼플 통조림
           // StartCoroutine(_ItemDescription(Type.Canned_GREEN));
        }
        else if (type.Equals(Type.Canned_ORANGE))
        {
            // 현재 wayPoint : 시작할 때 Game_TypeWriterEffect 에서 초기화해서 쌓아둠
            if (Game_TypeWriterEffect.instance.wayPoint == 1) { Game_TypeWriterEffect.instance.Show_EventStoryText(11); }    // 공통
            if (Game_TypeWriterEffect.instance.wayPoint == 2) { Game_TypeWriterEffect.instance.Show_EventStoryText(6); }     // 공통
            //if (Game_TypeWriterEffect.instance.wayPoint == 3) { Game_TypeWriterEffect.instance.Show_EventStoryText(1); }     // 번개 노란색 통조림
            //if(Game_TypeWriterEffect.instance.wayPoint == 4) { Game_TypeWriterEffect.instance.Show_EventStoryText(1); } // 스낵 오렌지
            //if(Game_TypeWriterEffect.instance.wayPoint == 5) { Game_TypeWriterEffect.instance.Show_EventStoryText(4); } // 행성
            //if(Game_TypeWriterEffect.instance.wayPoint == 6) { Game_TypeWriterEffect.instance.Show_EventStoryText(1); } // 달과 별이 그려진 스낵
            if (Game_TypeWriterEffect.instance.wayPoint == 7) { Game_TypeWriterEffect.instance.Show_EventStoryText(2); } // 공통
            if (Game_TypeWriterEffect.instance.wayPoint == 8) { Game_TypeWriterEffect.instance.Show_EventStoryText(4); } // 공통
            if (Game_TypeWriterEffect.instance.wayPoint == 9) { Game_TypeWriterEffect.instance.Show_EventStoryText(1); } // 공통
            if (Game_TypeWriterEffect.instance.wayPoint == 10) { Game_TypeWriterEffect.instance.Show_EventStoryText(5); } // 공통
            //if(Game_TypeWriterEffect.instance.wayPoint == 11) { Game_TypeWriterEffect.instance.Show_EventStoryText(1); }  // 나래이션 존재 X
            if (Game_TypeWriterEffect.instance.wayPoint == 12) { Game_TypeWriterEffect.instance.Show_EventStoryText(3); }    // 공통
            if (Game_TypeWriterEffect.instance.wayPoint == 13) { Game_TypeWriterEffect.instance.Show_EventStoryText(3); }    // 공통
            if (Game_TypeWriterEffect.instance.wayPoint == 14) { Game_TypeWriterEffect.instance.Show_EventStoryText(1); }    // 공통
            //if(Game_TypeWriterEffect.instance.wayPoint == 15) { Game_TypeWriterEffect.instance.Show_EventStoryText(3); }    // 별그림 스낵
            //if(Game_TypeWriterEffect.instance.wayPoint == 16) { Game_TypeWriterEffect.instance.Show_EventStoryText(1); }  // 나래이션 존재 X
            //if(Game_TypeWriterEffect.instance.wayPoint == 17) { Game_TypeWriterEffect.instance.Show_EventStoryText(1); }  // 나래이션 존재 X
            if (Game_TypeWriterEffect.instance.wayPoint == 18) { Game_TypeWriterEffect.instance.Show_EventStoryText(3); }      // 통조림
            if (Game_TypeWriterEffect.instance.wayPoint == 19) { Game_TypeWriterEffect.instance.Show_EventStoryText(2); }        // 공통
            //if(Game_TypeWriterEffect.instance.wayPoint == 20) { Game_TypeWriterEffect.instance.Show_EventStoryText(1); }    // 퍼플 통조림
                    
            //StartCoroutine(_ItemDescription(Type.Canned_ORANGE));
        }
        else if (type.Equals(Type.Canned_PURPLE))
        {
            // 현재 wayPoint : 시작할 때 Game_TypeWriterEffect 에서 초기화해서 쌓아둠
            if (Game_TypeWriterEffect.instance.wayPoint == 1) { Game_TypeWriterEffect.instance.Show_EventStoryText(11); }    // 공통
            if (Game_TypeWriterEffect.instance.wayPoint == 2) { Game_TypeWriterEffect.instance.Show_EventStoryText(6); }     // 공통
            //if (Game_TypeWriterEffect.instance.wayPoint == 3) { Game_TypeWriterEffect.instance.Show_EventStoryText(1); }     // 번개 노란색 통조림
            //if(Game_TypeWriterEffect.instance.wayPoint == 4) { Game_TypeWriterEffect.instance.Show_EventStoryText(1); } // 스낵 오렌지
            //if(Game_TypeWriterEffect.instance.wayPoint == 5) { Game_TypeWriterEffect.instance.Show_EventStoryText(4); } // 행성
            //if(Game_TypeWriterEffect.instance.wayPoint == 6) { Game_TypeWriterEffect.instance.Show_EventStoryText(1); } // 달과 별이 그려진 스낵
            if (Game_TypeWriterEffect.instance.wayPoint == 7) { Game_TypeWriterEffect.instance.Show_EventStoryText(2); } // 공통
            if (Game_TypeWriterEffect.instance.wayPoint == 8) { Game_TypeWriterEffect.instance.Show_EventStoryText(4); } // 공통
            if (Game_TypeWriterEffect.instance.wayPoint == 9) { Game_TypeWriterEffect.instance.Show_EventStoryText(1); } // 공통
            if (Game_TypeWriterEffect.instance.wayPoint == 10) { Game_TypeWriterEffect.instance.Show_EventStoryText(5); } // 공통
            //if(Game_TypeWriterEffect.instance.wayPoint == 11) { Game_TypeWriterEffect.instance.Show_EventStoryText(1); }  // 나래이션 존재 X
            if (Game_TypeWriterEffect.instance.wayPoint == 12) { Game_TypeWriterEffect.instance.Show_EventStoryText(3); }    // 공통
            if (Game_TypeWriterEffect.instance.wayPoint == 13) { Game_TypeWriterEffect.instance.Show_EventStoryText(3); }    // 공통
            if (Game_TypeWriterEffect.instance.wayPoint == 14) { Game_TypeWriterEffect.instance.Show_EventStoryText(1); }    // 공통
            //if(Game_TypeWriterEffect.instance.wayPoint == 15) { Game_TypeWriterEffect.instance.Show_EventStoryText(3); }    // 별그림 스낵
            //if(Game_TypeWriterEffect.instance.wayPoint == 16) { Game_TypeWriterEffect.instance.Show_EventStoryText(1); }  // 나래이션 존재 X
            //if(Game_TypeWriterEffect.instance.wayPoint == 17) { Game_TypeWriterEffect.instance.Show_EventStoryText(1); }  // 나래이션 존재 X
            if (Game_TypeWriterEffect.instance.wayPoint == 18) { Game_TypeWriterEffect.instance.Show_EventStoryText(3); }      // 통조림
            if (Game_TypeWriterEffect.instance.wayPoint == 19) { Game_TypeWriterEffect.instance.Show_EventStoryText(2); }        // 공통
            //if(Game_TypeWriterEffect.instance.wayPoint == 20) { Game_TypeWriterEffect.instance.Show_EventStoryText(1); }    // 퍼플 통조림
           // StartCoroutine(_ItemDescription(Type.Canned_PURPLE));
        }

        else if (type.Equals(Type.YELLOW_Snack))
        {
            // 현재 wayPoint : 시작할 때 Game_TypeWriterEffect 에서 초기화해서 쌓아둠
            if (Game_TypeWriterEffect.instance.wayPoint == 1) { Game_TypeWriterEffect.instance.Show_EventStoryText(11); }    // 공통
            if (Game_TypeWriterEffect.instance.wayPoint == 2) { Game_TypeWriterEffect.instance.Show_EventStoryText(6); }     // 공통
            //if (Game_TypeWriterEffect.instance.wayPoint == 3) { Game_TypeWriterEffect.instance.Show_EventStoryText(1); }     // 번개 노란색 통조림
            //if(Game_TypeWriterEffect.instance.wayPoint == 4) { Game_TypeWriterEffect.instance.Show_EventStoryText(1); } // 스낵 오렌지
            //if(Game_TypeWriterEffect.instance.wayPoint == 5) { Game_TypeWriterEffect.instance.Show_EventStoryText(4); } // 행성
            if (Game_TypeWriterEffect.instance.wayPoint == 6) { Game_TypeWriterEffect.instance.Show_EventStoryText(1); } // 달과 별이 그려진 스낵
            if (Game_TypeWriterEffect.instance.wayPoint == 7) { Game_TypeWriterEffect.instance.Show_EventStoryText(2); } // 공통
            if (Game_TypeWriterEffect.instance.wayPoint == 8) { Game_TypeWriterEffect.instance.Show_EventStoryText(4); } // 공통
            if (Game_TypeWriterEffect.instance.wayPoint == 9) { Game_TypeWriterEffect.instance.Show_EventStoryText(1); } // 공통
            if (Game_TypeWriterEffect.instance.wayPoint == 10) { Game_TypeWriterEffect.instance.Show_EventStoryText(5); } // 공통
            //if(Game_TypeWriterEffect.instance.wayPoint == 11) { Game_TypeWriterEffect.instance.Show_EventStoryText(1); }  // 나래이션 존재 X
            if (Game_TypeWriterEffect.instance.wayPoint == 12) { Game_TypeWriterEffect.instance.Show_EventStoryText(3); }    // 공통
            if (Game_TypeWriterEffect.instance.wayPoint == 13) { Game_TypeWriterEffect.instance.Show_EventStoryText(3); }    // 공통
            if (Game_TypeWriterEffect.instance.wayPoint == 14) { Game_TypeWriterEffect.instance.Show_EventStoryText(1); }    // 공통
            //if(Game_TypeWriterEffect.instance.wayPoint == 15) { Game_TypeWriterEffect.instance.Show_EventStoryText(3); }    // 별그림 그린 스낵
            //if(Game_TypeWriterEffect.instance.wayPoint == 16) { Game_TypeWriterEffect.instance.Show_EventStoryText(1); }  // 나래이션 존재 X
            //if(Game_TypeWriterEffect.instance.wayPoint == 17) { Game_TypeWriterEffect.instance.Show_EventStoryText(1); }  // 나래이션 존재 X
            //if (Game_TypeWriterEffect.instance.wayPoint == 18) { Game_TypeWriterEffect.instance.Show_EventStoryText(3); }      // 통조림
            if (Game_TypeWriterEffect.instance.wayPoint == 19) { Game_TypeWriterEffect.instance.Show_EventStoryText(2); }        // 공통
            //if(Game_TypeWriterEffect.instance.wayPoint == 20) { Game_TypeWriterEffect.instance.Show_EventStoryText(1); }    // 퍼플 통조림
           //  StartCoroutine(_ItemDescription(Type.YELLOW_Snack));
        }
        else if (type.Equals(Type.BLUE_Snack))
        {
            if (Game_TypeWriterEffect.instance.wayPoint == 1) { Game_TypeWriterEffect.instance.Show_EventStoryText(11); }    // 공통
            if (Game_TypeWriterEffect.instance.wayPoint == 2) { Game_TypeWriterEffect.instance.Show_EventStoryText(6); }     // 공통
            //if (Game_TypeWriterEffect.instance.wayPoint == 3) { Game_TypeWriterEffect.instance.Show_EventStoryText(1); }     // 번개 노란색 통조림
            //if(Game_TypeWriterEffect.instance.wayPoint == 4) { Game_TypeWriterEffect.instance.Show_EventStoryText(1); } // 스낵 오렌지
            //if(Game_TypeWriterEffect.instance.wayPoint == 5) { Game_TypeWriterEffect.instance.Show_EventStoryText(4); } // 행성
            //if (Game_TypeWriterEffect.instance.wayPoint == 6) { Game_TypeWriterEffect.instance.Show_EventStoryText(1); } // 달과 별이 그려진 스낵
            if (Game_TypeWriterEffect.instance.wayPoint == 7) { Game_TypeWriterEffect.instance.Show_EventStoryText(2); } // 공통
            if (Game_TypeWriterEffect.instance.wayPoint == 8) { Game_TypeWriterEffect.instance.Show_EventStoryText(4); } // 공통
            if (Game_TypeWriterEffect.instance.wayPoint == 9) { Game_TypeWriterEffect.instance.Show_EventStoryText(1); } // 공통
            if (Game_TypeWriterEffect.instance.wayPoint == 10) { Game_TypeWriterEffect.instance.Show_EventStoryText(5); } // 공통
            //if(Game_TypeWriterEffect.instance.wayPoint == 11) { Game_TypeWriterEffect.instance.Show_EventStoryText(1); }  // 나래이션 존재 X
            if (Game_TypeWriterEffect.instance.wayPoint == 12) { Game_TypeWriterEffect.instance.Show_EventStoryText(3); }    // 공통
            if (Game_TypeWriterEffect.instance.wayPoint == 13) { Game_TypeWriterEffect.instance.Show_EventStoryText(3); }    // 공통
            if (Game_TypeWriterEffect.instance.wayPoint == 14) { Game_TypeWriterEffect.instance.Show_EventStoryText(1); }    // 공통
            //if(Game_TypeWriterEffect.instance.wayPoint == 15) { Game_TypeWriterEffect.instance.Show_EventStoryText(3); }    // 별그림 그린 스낵
            //if(Game_TypeWriterEffect.instance.wayPoint == 16) { Game_TypeWriterEffect.instance.Show_EventStoryText(1); }  // 나래이션 존재 X
            //if(Game_TypeWriterEffect.instance.wayPoint == 17) { Game_TypeWriterEffect.instance.Show_EventStoryText(1); }  // 나래이션 존재 X
            //if (Game_TypeWriterEffect.instance.wayPoint == 18) { Game_TypeWriterEffect.instance.Show_EventStoryText(3); }      // 통조림
            if (Game_TypeWriterEffect.instance.wayPoint == 19) { Game_TypeWriterEffect.instance.Show_EventStoryText(2); }        // 공통
            //if(Game_TypeWriterEffect.instance.wayPoint == 20) { Game_TypeWriterEffect.instance.Show_EventStoryText(1); }    // 퍼플 통조림

            // StartCoroutine(_ItemDescription(Type.BLUE_Snack));
        }
        else if (type.Equals(Type.ORANGE_Snack))
        {
            if (Game_TypeWriterEffect.instance.wayPoint == 1) { Game_TypeWriterEffect.instance.Show_EventStoryText(11); }    // 공통
            if (Game_TypeWriterEffect.instance.wayPoint == 2) { Game_TypeWriterEffect.instance.Show_EventStoryText(6); }     // 공통
            //if (Game_TypeWriterEffect.instance.wayPoint == 3) { Game_TypeWriterEffect.instance.Show_EventStoryText(1); }     // 번개 노란색 통조림
            if (Game_TypeWriterEffect.instance.wayPoint == 4) { Game_TypeWriterEffect.instance.Show_EventStoryText(1); } // 스낵 오렌지
            //if(Game_TypeWriterEffect.instance.wayPoint == 5) { Game_TypeWriterEffect.instance.Show_EventStoryText(4); } // 행성
            //if (Game_TypeWriterEffect.instance.wayPoint == 6) { Game_TypeWriterEffect.instance.Show_EventStoryText(1); } // 달과 별이 그려진 스낵
            if (Game_TypeWriterEffect.instance.wayPoint == 7) { Game_TypeWriterEffect.instance.Show_EventStoryText(2); } // 공통
            if (Game_TypeWriterEffect.instance.wayPoint == 8) { Game_TypeWriterEffect.instance.Show_EventStoryText(4); } // 공통
            if (Game_TypeWriterEffect.instance.wayPoint == 9) { Game_TypeWriterEffect.instance.Show_EventStoryText(1); } // 공통
            if (Game_TypeWriterEffect.instance.wayPoint == 10) { Game_TypeWriterEffect.instance.Show_EventStoryText(5); } // 공통
            //if(Game_TypeWriterEffect.instance.wayPoint == 11) { Game_TypeWriterEffect.instance.Show_EventStoryText(1); }  // 나래이션 존재 X
            if (Game_TypeWriterEffect.instance.wayPoint == 12) { Game_TypeWriterEffect.instance.Show_EventStoryText(3); }    // 공통
            if (Game_TypeWriterEffect.instance.wayPoint == 13) { Game_TypeWriterEffect.instance.Show_EventStoryText(3); }    // 공통
            if (Game_TypeWriterEffect.instance.wayPoint == 14) { Game_TypeWriterEffect.instance.Show_EventStoryText(1); }    // 공통
            //if(Game_TypeWriterEffect.instance.wayPoint == 15) { Game_TypeWriterEffect.instance.Show_EventStoryText(3); }    // 별그림 그린 스낵
            //if(Game_TypeWriterEffect.instance.wayPoint == 16) { Game_TypeWriterEffect.instance.Show_EventStoryText(1); }  // 나래이션 존재 X
            //if(Game_TypeWriterEffect.instance.wayPoint == 17) { Game_TypeWriterEffect.instance.Show_EventStoryText(1); }  // 나래이션 존재 X
            //if (Game_TypeWriterEffect.instance.wayPoint == 18) { Game_TypeWriterEffect.instance.Show_EventStoryText(3); }      // 통조림
            if (Game_TypeWriterEffect.instance.wayPoint == 19) { Game_TypeWriterEffect.instance.Show_EventStoryText(2); }        // 공통
            //if(Game_TypeWriterEffect.instance.wayPoint == 20) { Game_TypeWriterEffect.instance.Show_EventStoryText(1); }    // 퍼플 통조림

           //  StartCoroutine(_ItemDescription(Type.ORANGE_Snack));
        }
        else if (type.Equals(Type.GREEN_Snack))
        {
            if (Game_TypeWriterEffect.instance.wayPoint == 1) { Game_TypeWriterEffect.instance.Show_EventStoryText(11); }    // 공통
            if (Game_TypeWriterEffect.instance.wayPoint == 2) { Game_TypeWriterEffect.instance.Show_EventStoryText(6); }     // 공통
            //if (Game_TypeWriterEffect.instance.wayPoint == 3) { Game_TypeWriterEffect.instance.Show_EventStoryText(1); }     // 번개 노란색 통조림
            //if(Game_TypeWriterEffect.instance.wayPoint == 4) { Game_TypeWriterEffect.instance.Show_EventStoryText(1); } // 스낵 오렌지
            //if(Game_TypeWriterEffect.instance.wayPoint == 5) { Game_TypeWriterEffect.instance.Show_EventStoryText(4); } // 행성
            //if (Game_TypeWriterEffect.instance.wayPoint == 6) { Game_TypeWriterEffect.instance.Show_EventStoryText(1); } // 달과 별이 그려진 스낵
            if (Game_TypeWriterEffect.instance.wayPoint == 7) { Game_TypeWriterEffect.instance.Show_EventStoryText(2); } // 공통
            if (Game_TypeWriterEffect.instance.wayPoint == 8) { Game_TypeWriterEffect.instance.Show_EventStoryText(4); } // 공통
            if (Game_TypeWriterEffect.instance.wayPoint == 9) { Game_TypeWriterEffect.instance.Show_EventStoryText(1); } // 공통
            if (Game_TypeWriterEffect.instance.wayPoint == 10) { Game_TypeWriterEffect.instance.Show_EventStoryText(5); } // 공통
            //if(Game_TypeWriterEffect.instance.wayPoint == 11) { Game_TypeWriterEffect.instance.Show_EventStoryText(1); }  // 나래이션 존재 X
            if (Game_TypeWriterEffect.instance.wayPoint == 12) { Game_TypeWriterEffect.instance.Show_EventStoryText(3); }    // 공통
            if (Game_TypeWriterEffect.instance.wayPoint == 13) { Game_TypeWriterEffect.instance.Show_EventStoryText(3); }    // 공통
            if (Game_TypeWriterEffect.instance.wayPoint == 14) { Game_TypeWriterEffect.instance.Show_EventStoryText(1); }    // 공통
            if (Game_TypeWriterEffect.instance.wayPoint == 15) { Game_TypeWriterEffect.instance.Show_EventStoryText(3); }    // 별그림 그린 스낵
            //if(Game_TypeWriterEffect.instance.wayPoint == 16) { Game_TypeWriterEffect.instance.Show_EventStoryText(1); }  // 나래이션 존재 X
            //if(Game_TypeWriterEffect.instance.wayPoint == 17) { Game_TypeWriterEffect.instance.Show_EventStoryText(1); }  // 나래이션 존재 X
            //if (Game_TypeWriterEffect.instance.wayPoint == 18) { Game_TypeWriterEffect.instance.Show_EventStoryText(3); }      // 통조림
            if (Game_TypeWriterEffect.instance.wayPoint == 19) { Game_TypeWriterEffect.instance.Show_EventStoryText(2); }        // 공통
            if(Game_TypeWriterEffect.instance.wayPoint == 20) { Game_TypeWriterEffect.instance.Show_EventStoryText(1); }    // 퍼플 통조림
           // StartCoroutine(_ItemDescription(Type.GREEN_Snack));
        }
        else if (type.Equals(Type.PURPLE_Snack))
        {
            if (Game_TypeWriterEffect.instance.wayPoint == 1) { Game_TypeWriterEffect.instance.Show_EventStoryText(11); }    // 공통
            if (Game_TypeWriterEffect.instance.wayPoint == 2) { Game_TypeWriterEffect.instance.Show_EventStoryText(6); }     // 공통
            //if (Game_TypeWriterEffect.instance.wayPoint == 3) { Game_TypeWriterEffect.instance.Show_EventStoryText(1); }     // 번개 노란색 통조림
            //if(Game_TypeWriterEffect.instance.wayPoint == 4) { Game_TypeWriterEffect.instance.Show_EventStoryText(1); } // 스낵 오렌지
            //if(Game_TypeWriterEffect.instance.wayPoint == 5) { Game_TypeWriterEffect.instance.Show_EventStoryText(4); } // 행성
            //if (Game_TypeWriterEffect.instance.wayPoint == 6) { Game_TypeWriterEffect.instance.Show_EventStoryText(1); } // 달과 별이 그려진 스낵
            if (Game_TypeWriterEffect.instance.wayPoint == 7) { Game_TypeWriterEffect.instance.Show_EventStoryText(2); } // 공통
            if (Game_TypeWriterEffect.instance.wayPoint == 8) { Game_TypeWriterEffect.instance.Show_EventStoryText(4); } // 공통
            if (Game_TypeWriterEffect.instance.wayPoint == 9) { Game_TypeWriterEffect.instance.Show_EventStoryText(1); } // 공통
            if (Game_TypeWriterEffect.instance.wayPoint == 10) { Game_TypeWriterEffect.instance.Show_EventStoryText(5); } // 공통
            //if(Game_TypeWriterEffect.instance.wayPoint == 11) { Game_TypeWriterEffect.instance.Show_EventStoryText(1); }  // 나래이션 존재 X
            if (Game_TypeWriterEffect.instance.wayPoint == 12) { Game_TypeWriterEffect.instance.Show_EventStoryText(3); }    // 공통
            if (Game_TypeWriterEffect.instance.wayPoint == 13) { Game_TypeWriterEffect.instance.Show_EventStoryText(3); }    // 공통
            if (Game_TypeWriterEffect.instance.wayPoint == 14) { Game_TypeWriterEffect.instance.Show_EventStoryText(1); }    // 공통
            //if(Game_TypeWriterEffect.instance.wayPoint == 15) { Game_TypeWriterEffect.instance.Show_EventStoryText(3); }    // 별그림 그린 스낵
            //if(Game_TypeWriterEffect.instance.wayPoint == 16) { Game_TypeWriterEffect.instance.Show_EventStoryText(1); }  // 나래이션 존재 X
            //if(Game_TypeWriterEffect.instance.wayPoint == 17) { Game_TypeWriterEffect.instance.Show_EventStoryText(1); }  // 나래이션 존재 X
            //if (Game_TypeWriterEffect.instance.wayPoint == 18) { Game_TypeWriterEffect.instance.Show_EventStoryText(3); }      // 통조림
            if (Game_TypeWriterEffect.instance.wayPoint == 19) { Game_TypeWriterEffect.instance.Show_EventStoryText(2); }        // 공통
            //if(Game_TypeWriterEffect.instance.wayPoint == 20) { Game_TypeWriterEffect.instance.Show_EventStoryText(1); }    // 퍼플 통조림
          //  StartCoroutine(_ItemDescription(Type.PURPLE_Snack));
        }
        //별가루
        else if (type.Equals(Type.StarDust))
        {
            if (Game_TypeWriterEffect.instance.wayPoint == 1) {RunnerPlayer1.instance.Tutorial_ing = true; } //별가루 튜토리얼
            if (Game_TypeWriterEffect.instance.wayPoint == 2) { Game_TypeWriterEffect.instance.Show_EventStoryText(5); }
            if (Game_TypeWriterEffect.instance.wayPoint == 7) { Game_TypeWriterEffect.instance.Show_EventStoryText(3); }
            if (Game_TypeWriterEffect.instance.wayPoint == 9) { Game_TypeWriterEffect.instance.Show_EventStoryText(3); }
            if (Game_TypeWriterEffect.instance.wayPoint == 12) { Game_TypeWriterEffect.instance.Show_EventStoryText(6); }
            if (Game_TypeWriterEffect.instance.wayPoint == 15) { Game_TypeWriterEffect.instance.Show_EventStoryText(4); }
            if (Game_TypeWriterEffect.instance.wayPoint == 19) { Game_TypeWriterEffect.instance.Show_EventStoryText(6); }
        }
        //다쓴 별가루
        else if (type.Equals(Type.EmptyStarDust))
        {
            if (Game_TypeWriterEffect.instance.wayPoint == 8) { Game_TypeWriterEffect.instance.Show_EventStoryText(5); }
       
        }
        //낡은 소형 산소통
        else if (type.Equals(Type.Old_OxygenTank_small))
        {
            if (Game_TypeWriterEffect.instance.wayPoint == 9) { Game_TypeWriterEffect.instance.Show_EventStoryText(6); }
        }
        //낡은 대형 산소통
        else if (type.Equals(Type.Old_OxygenTank_big))
        {
            if (Game_TypeWriterEffect.instance.wayPoint == 7) { Game_TypeWriterEffect.instance.Show_EventStoryText(6); }

        }
        //소형 산소통
        else if (type.Equals(Type.OxygenTank_small))
        {
            if (Game_TypeWriterEffect.instance.wayPoint == 1) { RunnerPlayer1.instance.Tutorial_ing = true; } //산소통 튜토리얼
            if (Game_TypeWriterEffect.instance.wayPoint == 4) { Game_TypeWriterEffect.instance.Show_EventStoryText(3); }
            if (Game_TypeWriterEffect.instance.wayPoint == 7) { Game_TypeWriterEffect.instance.Show_EventStoryText(6); }
        
        }
        //대형 산소통
        else if (type.Equals(Type.OxygenTank_big))
        {
            if (Game_TypeWriterEffect.instance.wayPoint == 8) { Game_TypeWriterEffect.instance.Show_EventStoryText(1); }
            if (Game_TypeWriterEffect.instance.wayPoint == 12) { Game_TypeWriterEffect.instance.Show_EventStoryText(4); }
            if (Game_TypeWriterEffect.instance.wayPoint == 18) { Game_TypeWriterEffect.instance.Show_EventStoryText(5); }
        }
        
        Debug.Log("먹음?");

        gameObject.SetActive(false);
       // Destroy(gameObject);
      
        Debug.Log("지워짐?");
    }
/*
    IEnumerator _ItemDescription(Type type)
    {
        Debug.Log("-------------------------------------------1 ");
     
        yield return new WaitUntil(() => Game_TypeWriterEffect.instance.Event_ing == false);
        Debug.Log("-------------------------------------------3 ");
        if (type.Equals(Type.Canned_BLUE)) { RunnerPlayer1.instance.ItemDescriptionManager.ItemDescription_ob.SetActive(true); RunnerPlayer1.instance.ItemDescriptionManager.Can[0].SetActive(true); }
        else if (type.Equals(Type.Canned_ORANGE)) { RunnerPlayer1.instance.ItemDescriptionManager.ItemDescription_ob.SetActive(true); RunnerPlayer1.instance.ItemDescriptionManager.Can[1].SetActive(true); }
        else if (type.Equals(Type.Canned_GREEN)) { RunnerPlayer1.instance.ItemDescriptionManager.ItemDescription_ob.SetActive(true); RunnerPlayer1.instance.ItemDescriptionManager.Can[2].SetActive(true); }
        else if (type.Equals(Type.Canned_PURPLE)) { RunnerPlayer1.instance.ItemDescriptionManager.ItemDescription_ob.SetActive(true); RunnerPlayer1.instance.ItemDescriptionManager.Can[3].SetActive(true); }
        else if (type.Equals(Type.Canned_YELLOW)) { RunnerPlayer1.instance.ItemDescriptionManager.ItemDescription_ob.SetActive(true); RunnerPlayer1.instance.ItemDescriptionManager.Can[4].SetActive(true); }
       //
        else if (type.Equals(Type.BLUE_Snack)) { RunnerPlayer1.instance.ItemDescriptionManager.ItemDescription_ob.SetActive(true); RunnerPlayer1.instance.ItemDescriptionManager.Snack[0].SetActive(true); }
        else if (type.Equals(Type.ORANGE_Snack)) { RunnerPlayer1.instance.ItemDescriptionManager.ItemDescription_ob.SetActive(true); RunnerPlayer1.instance.ItemDescriptionManager.Snack[1].SetActive(true); }
        else if (type.Equals(Type.GREEN_Snack)) { RunnerPlayer1.instance.ItemDescriptionManager.ItemDescription_ob.SetActive(true); RunnerPlayer1.instance.ItemDescriptionManager.Snack[2].SetActive(true); }
        else if (type.Equals(Type.PURPLE_Snack)) { RunnerPlayer1.instance.ItemDescriptionManager.ItemDescription_ob.SetActive(true); RunnerPlayer1.instance.ItemDescriptionManager.Snack[3].SetActive(true); }
        else if (type.Equals(Type.YELLOW_Snack)) { RunnerPlayer1.instance.ItemDescriptionManager.ItemDescription_ob.SetActive(true); RunnerPlayer1.instance.ItemDescriptionManager.Snack[4].SetActive(true); }
        //작은 산소통
        else if (type.Equals(Type.OxygenTank_small)) { RunnerPlayer1.instance.ItemDescriptionManager.OxygenTank_ob.SetActive(true); RunnerPlayer1.instance.ItemDescriptionManager.OxygenTank_small[0].SetActive(true); }
        else if (type.Equals(Type.Old_OxygenTank_small)) { RunnerPlayer1.instance.ItemDescriptionManager.OxygenTank_ob.SetActive(true); RunnerPlayer1.instance.ItemDescriptionManager.OxygenTank_small[1].SetActive(true); }
        //큰 산소통
        else if (type.Equals(Type.OxygenTank_big)) { RunnerPlayer1.instance.ItemDescriptionManager.OxygenTank_ob.SetActive(true); RunnerPlayer1.instance.ItemDescriptionManager.OxygenTank_big[0].SetActive(true); }
        else if (type.Equals(Type.Old_OxygenTank_big)) { RunnerPlayer1.instance.ItemDescriptionManager.OxygenTank_ob.SetActive(true); RunnerPlayer1.instance.ItemDescriptionManager.OxygenTank_big[1].SetActive(true); }
        //별 가루 
        else if (type.Equals(Type.StarDust)) { RunnerPlayer1.instance.ItemDescriptionManager.StarDust_ob.SetActive(true); RunnerPlayer1.instance.ItemDescriptionManager.StarDust[0].SetActive(true); }
        else if (type.Equals(Type.EmptyStarDust)) { RunnerPlayer1.instance.ItemDescriptionManager.StarDust_ob.SetActive(true); RunnerPlayer1.instance.ItemDescriptionManager.StarDust[1].SetActive(true); }
        Debug.Log("바로들어오는건가??????");
        RunnerPlayer1.instance.ItemDescriptionManager.count_txt.gameObject.SetActive(true);
        for (int i = 5; i > 0; i--)
        {
            RunnerPlayer1.instance.ItemDescriptionManager.count_txt.text = i.ToString();
            yield return new WaitForSeconds(1f);
        }

        if (type.Equals(Type.Canned_BLUE)) { RunnerPlayer1.instance.ItemDescriptionManager.ItemDescription_ob.SetActive(false); RunnerPlayer1.instance.ItemDescriptionManager.Can[0].SetActive(false); }
        else if (type.Equals(Type.Canned_ORANGE)) { RunnerPlayer1.instance.ItemDescriptionManager.ItemDescription_ob.SetActive(false); RunnerPlayer1.instance.ItemDescriptionManager.Can[1].SetActive(false); }
        else if (type.Equals(Type.Canned_GREEN)) { RunnerPlayer1.instance.ItemDescriptionManager.ItemDescription_ob.SetActive(false); RunnerPlayer1.instance.ItemDescriptionManager.Can[2].SetActive(false); }
        else if (type.Equals(Type.Canned_PURPLE)) { RunnerPlayer1.instance.ItemDescriptionManager.ItemDescription_ob.SetActive(false); RunnerPlayer1.instance.ItemDescriptionManager.Can[3].SetActive(false); }
        else if (type.Equals(Type.Canned_YELLOW)) { RunnerPlayer1.instance.ItemDescriptionManager.ItemDescription_ob.SetActive(false); RunnerPlayer1.instance.ItemDescriptionManager.Can[4].SetActive(false); }
        //
        else if (type.Equals(Type.BLUE_Snack)) { RunnerPlayer1.instance.ItemDescriptionManager.ItemDescription_ob.SetActive(false); RunnerPlayer1.instance.ItemDescriptionManager.Snack[0].SetActive(false); }
        else if (type.Equals(Type.ORANGE_Snack)) { RunnerPlayer1.instance.ItemDescriptionManager.ItemDescription_ob.SetActive(false); RunnerPlayer1.instance.ItemDescriptionManager.Snack[1].SetActive(false); }
        else if (type.Equals(Type.GREEN_Snack)) { RunnerPlayer1.instance.ItemDescriptionManager.ItemDescription_ob.SetActive(false); RunnerPlayer1.instance.ItemDescriptionManager.Snack[2].SetActive(false); }
        else if (type.Equals(Type.PURPLE_Snack)) { RunnerPlayer1.instance.ItemDescriptionManager.ItemDescription_ob.SetActive(false); RunnerPlayer1.instance.ItemDescriptionManager.Snack[3].SetActive(false); }
        else if (type.Equals(Type.YELLOW_Snack)) { RunnerPlayer1.instance.ItemDescriptionManager.ItemDescription_ob.SetActive(false); RunnerPlayer1.instance.ItemDescriptionManager.Snack[4].SetActive(false); }
        //작은 산소통
        else if (type.Equals(Type.OxygenTank_small)) { RunnerPlayer1.instance.ItemDescriptionManager.OxygenTank_ob.SetActive(false); RunnerPlayer1.instance.ItemDescriptionManager.OxygenTank_small[0].SetActive(false); }
        else if (type.Equals(Type.Old_OxygenTank_small)) { RunnerPlayer1.instance.ItemDescriptionManager.OxygenTank_ob.SetActive(false); RunnerPlayer1.instance.ItemDescriptionManager.OxygenTank_small[1].SetActive(false); }
        //큰 산소통
        else if (type.Equals(Type.OxygenTank_big)) { RunnerPlayer1.instance.ItemDescriptionManager.OxygenTank_ob.SetActive(false); RunnerPlayer1.instance.ItemDescriptionManager.OxygenTank_big[0].SetActive(false); }
        else if (type.Equals(Type.Old_OxygenTank_big)) { RunnerPlayer1.instance.ItemDescriptionManager.OxygenTank_ob.SetActive(false); RunnerPlayer1.instance.ItemDescriptionManager.OxygenTank_big[1].SetActive(false); }
        //별 가루 
        else if (type.Equals(Type.StarDust)) { RunnerPlayer1.instance.ItemDescriptionManager.StarDust_ob.SetActive(false); RunnerPlayer1.instance.ItemDescriptionManager.StarDust[0].SetActive(false); }
        else if (type.Equals(Type.EmptyStarDust)) { RunnerPlayer1.instance.ItemDescriptionManager.StarDust_ob.SetActive(false); RunnerPlayer1.instance.ItemDescriptionManager.StarDust[1].SetActive(false); }
        yield return null;
    }
*/
}
