using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TunnelEffect;

public class TunnelController : MonoBehaviour
{
   public TunnelFX2 fx;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(_Test());

        //Debug.Log("int_Temp : " + int_Temp);
    }

    IEnumerator _Test()
    {
        fx = TunnelFX2.instance;

        Debug.Log("fx.name : " + fx.name);

        fx.preset = TUNNEL_PRESET.MysticTravel;
        Debug.Log("fx.layersSpeed : " + fx.layersSpeed);

        //while (fx.layersSpeed > 0 && fx.layersSpeed < 20)
        //{
        //    fx.layersSpeed += Time.deltaTime * 0.03f;

        //    yield return null;
        //}

        yield return null;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
