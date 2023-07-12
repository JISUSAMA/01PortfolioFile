using TMPro;        
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class GetVersion : MonoBehaviour
{
    // ���� �ؽ�Ʈ
    [SerializeField] private TMP_Text version;

    private void OnValidate()
    {
#if UNITY_EDITOR
        version.text = $"CITY TOUR VER {Application.version}.{PlayerSettings.Android.bundleVersionCode}";
#endif
    }
}
