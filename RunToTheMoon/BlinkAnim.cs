using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlinkAnim : MonoBehaviour
{
    float time;

    private void OnEnable()
    {
        StopCoroutine(_BlinkImg());
        StartCoroutine(_BlinkImg());
    }
    IEnumerator _BlinkImg()
    {
        yield return new WaitForSeconds(1f);
        while (this.gameObject.activeSelf.Equals(true))
        {
            if (time < 0.5f)
            {
                GetComponent<Image>().color = new Color(1, 1, 1, 1 - time * 1.5f);
            }
            else
            {
                GetComponent<Image>().color = new Color(1, 1, 1, time);
                if (time > 1f)
                {
                    time = 0;
                }
            }
            time += Time.deltaTime;
            yield return null;
        }
        yield return null;
    }

}
