using UnityEngine;
using UnityEngine.UI;

public class OnConnect : MonoBehaviour
{
    public GameObject onConnecting;
    public GameObject onConnected;
    public GameObject onSkip;

    public void OnEnable()
    {
        SensorManager.instance.connectState += ConnectState;
    }

    private void OnDisable()
    {
        SensorManager.instance.connectState -= ConnectState;
    }

    private void ConnectState(SensorManager.States reciever)
    {
        Debug.Log($"ConnectState is {reciever} in OnConnect");

        if (SensorManager.States.None == reciever)
        {
            // ��ĵ�߿� ���ῡ ������ 2022 11 01            
            onConnecting.SetActive(false);
            onSkip.GetComponent<Button>().enabled = true;
        }
        else if (SensorManager.States.Scan == reciever || SensorManager.States.Connect == reciever)
        {
            // ������
            onConnecting.SetActive(true);
            onSkip.GetComponent<Button>().enabled = false;
        }
        else if (SensorManager.States.Subscribe == reciever)
        {
            onConnecting.SetActive(false);
            onConnected.SetActive(true);
            onSkip.GetComponent<Button>().enabled = true;
        }
    }

    public void OnClickConnect()
    {
        if (SensorManager.instance._connected)
        {
            Debug.Log("_connected is true in OnClickConnect");
            // ���� �̹� ������ �Ǿ������� ���� ���·� �Ѿ
            onConnecting.SetActive(true);
            Invoke("Delay", 1);
        }
        else
        {
            Debug.Log("_connected is false in OnClickConnect");
            //onConnecting.SetActive(true);
            SensorManager.instance.StartProcess();
        }
    }

    private void Delay()
    {
        onConnecting.SetActive(false);
        onConnected.SetActive(true);
        onSkip.GetComponent<Button>().enabled = true;
    }
}
