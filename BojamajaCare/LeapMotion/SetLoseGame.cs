using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SetLoseGame : MonoBehaviour
{
    private BoxCollider boxCollider;
    public TMP_Text guidlineText;
    private void Start()
    {
        boxCollider = this.GetComponent<BoxCollider>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("R_Hand"))
        {
            boxCollider.isTrigger = false;
            StartCoroutine(BlinkText());
        }
        else if (other.CompareTag("L_Hand"))
        {
            boxCollider.isTrigger = false;
            StartCoroutine(BlinkText());
        }
    }

    IEnumerator BlinkText()
    {
        guidlineText.text = "���� ������ ���� �Ͽ����ϴ�.";
        int i = 10;
        Color c;

        yield return new WaitForSeconds(2f);

        while (i > 0f)
        {
            i -= 1;
            float f = i / 10.0f;
            c = guidlineText.color;
            c.a = f;
            guidlineText.color = c;
            yield return new WaitForSeconds(0.02f);
        }

        guidlineText.text = "";
        c = guidlineText.color;
        c.a = 1f;
        guidlineText.color = c;

        //GestureManager.target_set_game("����");

        boxCollider.isTrigger = true;
        //guidlineText.text = "������ �����ϰڽ��ϴ�.";
        //c = guidlineText.color;
        //c.a = 1f;
        //guidlineText.color = c;
        //i = 10;

        //while (i > 0f)
        //{
        //    i -= 1;
        //    float f = i / 10.0f;
        //    c = guidlineText.color;
        //    c.a = f;
        //    guidlineText.color = c;
        //    yield return new WaitForSeconds(0.02f);
        //}

        yield return null;
    }
}
