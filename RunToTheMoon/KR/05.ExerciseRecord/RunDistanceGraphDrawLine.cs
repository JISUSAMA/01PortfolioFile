using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RunDistanceGraphDrawLine : MonoBehaviour
{
    public static RunDistanceGraphDrawLine instance { get; private set; }

    public LineRenderer lr;
    public GameObject[] parants;
    public GameObject[] graph;

    [SerializeField]
    ContentSizeFitter csf;


    int graphPosCount;  //그려야할 라인 객체 수
    int[] graphIndex;   //라인 갯수
    int index = 0;  //그래프 포지션 인덱스
    Vector3[] graphPos; //라인 그릴 위치

    private void Awake()
    {
        if (instance != null)
            Destroy(this);
        else instance = this;
    }

    public void GraphDrawLineMake()
    {
        StartCoroutine(_GraphDrawLineMake());
    }


    IEnumerator _GraphDrawLineMake()
    {
        yield return new WaitForSeconds(0.1f);

        lr.startWidth = 0.5f;
        lr.endWidth = 0.5f;

        //렌더러 라인 그리는 pos 갯수 측정
        for (int k = 0; k < graph.Length; k++)
        {
            if (parants[k].activeSelf == true)
            {
                if (graph[k].activeSelf == true)
                {
                    graphPosCount++;
                }
            }
        }

        graphIndex = new int[graphPosCount];
        //Debug.Log("graphPosCount " + graphPosCount);

        //그려야할 객체번호(몇번째인지-인덱스) 구하기
        for (int i = 0; i < graph.Length; i++)
        {
            if (parants[i].activeSelf == true)
            {
                if (graph[i].activeSelf == true)
                {
                    //Debug.LogWarning("이름 " + graph[i].name);
                    graphIndex[index++] = i;    //그래프 포지션 그릴 위치값이 어떤 객체의 번호를 가지고 있는지 저장(객체 번호)
                }
            }
        }

        graphPos = new Vector3[graphPosCount];  //그릴 포지션 객체 생성

        for (int i = 0; i < graphPos.Length; i++)
        {
            graphPos[i] = new Vector3(graph[graphIndex[i]].transform.position.x, graph[graphIndex[i]].transform.position.y + 2f, 90f);
            //graphPos[i] = new Vector3(graph[graphIndex[i]].transform.position.x + (-860.89f + (graphIndex[i] * 158.03f)), graph[graphIndex[i]].transform.position.y + 1f, -90f);
        }

        lr.positionCount = graphPosCount;   //그릴 포지션 객수를 생성


        lr.SetPositions(graphPos);
        //for (int i = 0; i < graphPos.Length; i++)
        //lr.SetPosition(i, graphPos[i]);

        //컨텐츠필드 사이즈 리프레쉬
        LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)csf.transform);
    }

    public void DeleteLineDraw()
    {
        lr.positionCount = 0;
        graphPosCount = 0;
        index = 0;
    }
}
