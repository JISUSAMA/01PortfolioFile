using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GainValueAnim : MonoBehaviour
{
    [SerializeField] Vector3 OriginalPos;
    [SerializeField] Vector3 dir;
    bool Completion = false; 
    // Update is called once per frame
    private void Start()
    {
        OriginalPos = this.gameObject.transform.position;
    }
    private void OnEnable()
    {
        OriginalPos = this.gameObject.transform.position;
        MoveRandomDirection();
    }
    private void OnDisable()
    {
        this.gameObject.GetComponent<Text>().color = new Color(1, 0, 0, 1f); //컬러 초기화
        this.gameObject.transform.position = OriginalPos;
        Completion = false;
    }
    public void MoveRandomDirection()
    {
        StartCoroutine(_Gain_Fade_out());
        StartCoroutine(_DisplayScore());
    }
    IEnumerator _Gain_Fade_out()
    {
        while (!Completion)
        {
            Vector3 pos = this.gameObject.transform.position;
            pos.y += 0.01f;
            transform.position = pos;
            Debug.Log("Pos  :" + pos);
            yield return null;
        }
        yield return null;
    }
    IEnumerator _DisplayScore()
    {
        yield return new WaitForSeconds(2f);

        for (float a = 1; a >= 0; a -= 0.15f)
        {
            this.gameObject.GetComponent<Text>().color = new Vector4(1, 0, 0, a);
            yield return new WaitForFixedUpdate();
        }

        Completion = true;
        this.gameObject.SetActive(false);
    }

}
