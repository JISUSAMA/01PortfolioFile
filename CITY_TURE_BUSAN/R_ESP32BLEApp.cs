using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class R_ESP32BLEApp : MonoBehaviour
{

    public static R_ESP32BLEApp instance { get; private set; }


    private string DeviceName = "ESP32 FITNESS LEFT";
    private string ServiceUUID = "FFE0";
    private string Characteristic = "FFE1";

    float qx, qy, qz, qw;
    float ax, ay, az;
    enum States
    {
        None,
        Scan,
        Connect,
        Subscribe,
        Unsubscribe,
        Disconnect,
        Communication,
    }

    private bool _workingFoundDevice = true;
    private bool _connected = false;
    private float _timeout = 0f;
    private States _state = States.None;
    private bool _foundID = false;
    private string _deviceAddress;

    [SerializeField] private TMP_Text stateText;
    [SerializeField] private TMP_Text stateTextAcc;
    [SerializeField] private Transform man;
    [SerializeField] private Transform woman;


    //[SerializeField] private Transform player_Woman;
    private Quaternion inverseQt;
    private Quaternion rawQt;

    //센서 연결 팝업
    public GameObject popup;
    public Text popupText;
    public Text connectBtn;
    public float timer;


    void Reset()
    {
        _workingFoundDevice =
            false; // used to guard against trying to connect to a second device while still connecting to the first
        _connected = false;
        _timeout = 0f;
        _state = States.None;
        _foundID = false;
        _deviceAddress = null;
    }

    void SetState(States newState, float timeout)
    {
        _state = newState;
        _timeout = timeout;
    }

    void setStateText(string text)
    {
        if (stateText == null) return;
        stateText.text = text;
    }

    void setStateTextAcc(string text)
    {
        if (stateTextAcc == null) return;
        stateTextAcc.text = text;
    }

    public void StartProcess()
    {
        //setStateText("Initializing...");
        Reset();
        BluetoothLEHardwareInterface.Initialize(true, false, () =>
        {
            SetState(States.Scan, 0.1f);
            //setStateText("Initialized");
        }, (error) => {  BluetoothLEHardwareInterface.Log("Error: " + error); });
    }

    public void Popup_SenSor_State_Show()
    {
        popup.SetActive(true);
        popupText.text = "<b><color=#ff0000>센서가 연결이 되지않았습니다.</color></b>" + "\n" +
            "센서의 전원이 " + "\n" + "켜져있는지 확인해주시고," + "\n" +
            "GPS(위치)와 블루투스가" + "\n" + "켜져있는지 확인해주세요.";
    }

    public void Popup_GPS_Show()
    {
        popup.SetActive(true);
        popupText.text = "<b><color=#ff0000>센서가 연결이 되지않았습니다.</color></b>" + "\n" +
            "GPS(위치)와 블루투스가" + "\n" + "켜져있는지 확인해주세요.";
    }

    // Use this for initialization
    void Start()
    {
        StartProcess();
        inverseQt = Quaternion.identity;

    }


    // Update is called once per frame
    void Update()
    {
        if (_timeout > 0f)
        {
            _timeout -= Time.deltaTime;
            if (_timeout <= 0f)
            {
                _timeout = 0f;

                switch (_state)
                {
                    case States.None:
                        break;

                    case States.Scan:
                        //setStateText("Scanning for ESP32 devices...");

                        BluetoothLEHardwareInterface.ScanForPeripheralsWithServices(null, (address, name) =>
                        {
                            
                            Debug.Log("Address : " + address + "/ Name : " + name);
                            // we only want to look at devices that have the name we are looking for
                            // this is the best way to filter out devices

                            
                            if (name.Contains(DeviceName))
                            {
                                Text text_1 = GameObject.Find("Text (1)").GetComponent<Text>();
                                text_1.text = "SensorAddr 1: " + name + " 'address' " + address;

                                _workingFoundDevice = true;

                                // it is always a good idea to stop scanning while you connect to a device
                                // and get things set up
                                BluetoothLEHardwareInterface.StopScan();

                                // add it to the list and set to connect to it
                                _deviceAddress = address;
                                SetState(States.Connect, 0.5f);

                                _workingFoundDevice = false;
                            }
                        }, null, false, false);
                        break;

                    case States.Connect:
                        // set these flags
                        _foundID = false;

                        //setStateText("Connecting to ESP32");
                        timer = 0;

                        // note that the first parameter is the address, not the name. I have not fixed this because
                        // of backwards compatiblity.
                        // also note that I am note using the first 2 callbacks. If you are not looking for specific characteristics you can use one of
                        // the first 2, but keep in mind that the device will enumerate everything and so you will want to have a timeout
                        // large enough that it will be finished enumerating before you try to subscribe or do any other operations.
                        BluetoothLEHardwareInterface.ConnectToPeripheral(_deviceAddress, null, null,
                            (address, serviceUUID, characteristicUUID) =>
                            {
                                Text text_2 = GameObject.Find("Text (2)").GetComponent<Text>();
                                text_2.text = "connecting___-__- : " + _connected;
                                if (IsEqual(serviceUUID, ServiceUUID))
                                {
                                    // if we have found the characteristic that we are waiting for
                                    // set the state. make sure there is enough timeout that if the
                                    // device is still enumerating other characteristics it finishes
                                    // before we try to subscribe
                                    if (IsEqual(characteristicUUID, Characteristic))
                                    {
                                        _connected = true;
                                        SetState(States.Subscribe, 2f);

                                        //setStateText("Connected to ESP32");
                                    }
                                }
                            }, (disconnectedAddress) =>
                            {
                                BluetoothLEHardwareInterface.Log("Device disconnected: " + disconnectedAddress);

                                
                                {
                                    //센서가 연결되지 않았을 때 팝업 띄움
                                    //Popup_SenSor_State_Show(); 
                                    //setStateText("Disconnected");
                                }

                            });
                        break;

                    case States.Subscribe:
                        //setStateText("Subscribing to ESP32");

                        Text text_4 = GameObject.Find("Text (6)").GetComponent<Text>();
                        text_4.text = "연결됨: ";

                        BluetoothLEHardwareInterface.SubscribeCharacteristicWithDeviceAddress(_deviceAddress,
                        ServiceUUID,
                        Characteristic, null,
                        (address, characteristicUUID, bytes) =>
                        {
                            if (bytes.Length == 12)
                            {
                                
                                ax = BitConverter.ToSingle(bytes, 0);
                                ay = BitConverter.ToSingle(bytes, 4);
                                az = BitConverter.ToSingle(bytes, 8);
                                //setStateTextAcc("ax " + ax);

                                if (PlayerPrefs.GetString("Player_Sex").Equals("Man"))
                                {
                                    Man_Move.instance.mpu_value[0] = ax;
                                    Man_Move.instance.mpu_value[1] = ay;
                                    Man_Move.instance.mpu_value[2] = az;
                                }
                                else if (PlayerPrefs.GetString("Player_Sex").Equals("Woman"))
                                {
                                    Woman_Move.instance.mpu_value[0] = ax;
                                    Woman_Move.instance.mpu_value[1] = ay;
                                    Woman_Move.instance.mpu_value[2] = az;
                                }

                                Text text2 = GameObject.Find("Text (7)").GetComponent<Text>();
                                text2.text = "좌표 : "+  ax.ToString("0.00000") +
                                        ", ay: " + ay.ToString("0.00000") + ", az: " + az.ToString("0.00000");

                                //setStateTextAcc("byte length: " + bytes.Length + ", ax: " + ax.ToString("0.00000") +
                                //            ", ay: " + ay.ToString("0.00000") + ", az: " + az.ToString("0.00000"));// ", speed : " + PlayerCtrl_v1.instance.speed);


                                //setStateTextAcc("byte length: " + bytes.Length + ", ax: " + ax.ToString("0.00000") +
                                //            ", ay: " + ay.ToString("0.00000") + ", az: " + az.ToString("0.00000"));// ", speed : " + PlayerCtrl_v1.instance.speed);
                            }
                            else if (bytes.Length == 16)
                            {
                                qx = BitConverter.ToSingle(bytes, 0);
                                qy = BitConverter.ToSingle(bytes, 4);
                                qz = BitConverter.ToSingle(bytes, 8);
                                qw = BitConverter.ToSingle(bytes, 12);
                            }
                         });

                        // set to the none state and the user can start sending and receiving data
                        _state = States.None;
                        break;

                    case States.Unsubscribe:
                        BluetoothLEHardwareInterface.UnSubscribeCharacteristic(_deviceAddress, ServiceUUID,
                            Characteristic,
                            null);
                        SetState(States.Disconnect, 4f);
                        break;

                    case States.Disconnect:
                        if (_connected)
                        {
                            BluetoothLEHardwareInterface.DisconnectPeripheral(_deviceAddress, (address) =>
                            {
                                BluetoothLEHardwareInterface.DeInitialize(() =>
                                {
                                    _connected = false;
                                    _state = States.None;
                                });
                            });
                        }
                        else
                        {
                            BluetoothLEHardwareInterface.DeInitialize(() => { _state = States.None; });
                        }

                        break;
                }
            }
        }
    }

    string FullUUID(string uuid)
    {
        return "0000" + uuid + "-0000-1000-8000-00805F9B34FB";
    }

    bool IsEqual(string uuid1, string uuid2)
    {
        if (uuid1.Length == 4)
            uuid1 = FullUUID(uuid1);
        if (uuid2.Length == 4)
            uuid2 = FullUUID(uuid2);

        return (uuid1.ToUpper().Equals(uuid2.ToUpper()));
    }

    public void resetRotation()
    {
        inverseQt = Quaternion.Inverse(rawQt);
    }
}