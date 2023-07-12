using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class L_ESP32BLEApp : MonoBehaviour
{
    public static L_ESP32BLEApp instance { get; private set; }

    private string DeviceName = "FITTAG";// "ESP32 FITNESS LEFT";
    private string ServiceUUID = "c560";
    private string Characteristic = "c561";

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

    //[SerializeField] private TMP_Text stateText;
    [SerializeField] private TMP_Text stateTextAcc;
    //TMP_Text stateTextAcc;
    // [SerializeField] private TMP_Text stateTextAcc2;
    // [SerializeField] private TMP_Text stateTextAcc3;
    [SerializeField] private Transform woman;
    [SerializeField] private Transform man;


    //[SerializeField] private Transform player_Woman;
    private Quaternion inverseQt;
    private Quaternion rawQt;

    //센서 연결 팝업 
    public Sprite[] connectSprite;    //연결 이미지 0: 빨강, 1: 초록
    GameObject parentsObj_Canvas;   //캔버스 부모역할
    bool connecting;    //연결 상태

    

    private void Awake()
    {
        if (instance != null)
            Destroy(this.gameObject);
        else instance = this;

        DontDestroyOnLoad(this.gameObject);

    }


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
        //if (stateText == null) return;
        //stateText.text = text;
    }

    void setStateTextAcc(string text)
    {
        if (stateTextAcc == null) return;
        stateTextAcc.text = text;
    }

    public void StartProcess()
    {
        //setStateText("Initializing...");
        //stateTextAcc = GameObject.Find("Text (TMP)").GetComponent<TMP_Text>();
        parentsObj_Canvas = GameObject.Find("UICanvas");
        //Debug.Log("??????????");
        Reset();
        BluetoothLEHardwareInterface.Initialize(true, false, () =>
        {
            SetState(States.Scan, 0.1f);
        }, (error) =>
        {
            //Popup_SenSor_State_Show();
            BluetoothLEHardwareInterface.Log("Error: " + error);
        });

        //Text text_2 = GameObject.Find("Text (2)").GetComponent<Text>();
        //Text text_3 = GameObject.Find("Text (3)").GetComponent<Text>();
        //Text text_1 = GameObject.Find("Text (1)").GetComponent<Text>();
        //Text text_4 = GameObject.Find("Text (6)").GetComponent<Text>();
        //text_4.text = "";
        //text_1.text = "";
        //text_3.text = "";
        //text_2.text = "";
    }

    //센서 연결 상태에 따른 센서팝업 텍스트 글자 변경
    public void ConnectStateTextShow()
    {
        //stateTextAcc = GameObject.Find("Text (TMP)").GetComponent<TMP_Text>();
        if (SceneManager.GetActiveScene().name != "Loading" && SceneManager.GetActiveScene().name != "GameFinish")
        {
            parentsObj_Canvas = GameObject.Find("UICanvas");
            GameObject fit_tagPopup = parentsObj_Canvas.transform.Find("PopupGroup").transform.GetChild(1).gameObject;

            //팝업이 열려있으면
            if (fit_tagPopup.activeSelf.Equals(true))
            {
                Image connectImg = parentsObj_Canvas.transform.GetChild(1).transform.GetChild(1).GetComponent<Image>();
                //GameObject connectImage = fit_tagPopup.transform.GetChild(0).gameObject;
                Button fit_tagBtn = fit_tagPopup.transform.Find("ConnectButton").GetComponent<Button>();
                GameObject connectText = fit_tagPopup.transform.Find("ConnectText").gameObject;

                //연결 놉 빨강
                if (connectImg.sprite.name.Equals("gameing_Connect off_icon"))
                {
                    //stateTextAcc2.text = "1번 들어왓음";
                    //Debug.Log("1번 로고는 찍힘?");
                    //connectText.text = "센서의 전원이 켜져있는지 확인해주세요.";
                    connectText.SetActive(false);
                    fit_tagBtn.interactable = true; //클릭할수 있게
                    _connected = false; //커넥트 끊기
                }
                //연결 옙 초록
                else if (connectImg.sprite.name.Equals("gameing_Connect on_icon"))
                {
                    //stateTextAcc2.text = "2번 들어왓음";
                    //Debug.Log("2번 로고는 찍힘?");
                    //connectText.text = "기기와 연결되어 있습니다.";
                    connectText.SetActive(true);
                    fit_tagBtn.interactable = false;    //클릭할수 없게
                    _connected = true;  //커넥트 연결
                }
            }
            else
            {
                //Debug.Log("여긴 들어오쥬");
                if (_connected.Equals(false))
                {
                    //Debug.Log("1 여긴 들어오쥬");
                    //커넥트 연결 빨강
                    parentsObj_Canvas.transform.GetChild(1).transform.GetChild(1).GetComponent<Image>().sprite = connectSprite[0];
                }
                else
                {
                    //Debug.Log("2 여긴 들어오쥬");
                    //커넥트 연결 초록
                    parentsObj_Canvas.transform.GetChild(1).transform.GetChild(1).GetComponent<Image>().sprite = connectSprite[1];
                }
            }
        }
    }

    public void Popup_SenSor_State_Show()
    {
        parentsObj_Canvas = GameObject.Find("UICanvas");

        //커넥트 연결 빨강
        parentsObj_Canvas.transform.GetChild(1).transform.GetChild(1).GetComponent<Image>().sprite = connectSprite[0];

        if(SceneManager.GetActiveScene().name != "Loading")
        {
            if (SceneManager.GetActiveScene().name != "BusanMapMorning" && SceneManager.GetActiveScene().name != "BusanMapNight")
            {
                //셋업팝업창이 열려 있지 않을 경우
                if (parentsObj_Canvas.transform.Find("PopupGroup").transform.GetChild(0).gameObject.activeSelf.Equals(false))
                {
                    //커넥트 연결 상단 버튼
                    Image connectImg = parentsObj_Canvas.transform.GetChild(1).transform.GetChild(1).GetComponent<Image>();

                    GameObject fit_tagPopup = parentsObj_Canvas.transform.Find("PopupGroup").transform.GetChild(1).gameObject;
                    fit_tagPopup.SetActive(true);

                    //GameObject connectImage = fit_tagPopup.transform.GetChild(0).gameObject;
                    //connectImage.SetActive(true);

                    Button fit_tagBtn = fit_tagPopup.transform.Find("ConnectButton").GetComponent<Button>();
                    GameObject connectText = fit_tagPopup.transform.Find("ConnectText").gameObject;


                    //센서 연결 상태에 따른 센서팝업 텍스트 글자 변경
                    //연결 놉 빨강
                    if (connectImg.sprite.name.Equals("gameing_Connect off_icon"))
                    {
                        //connectText.text = "센서의 전원이 켜져있는지 확인해주세요.";
                        connectText.SetActive(false);
                        fit_tagBtn.interactable = true; //클릭할수 있게
                    }
                    //연결 옙 초록
                    else if (connectImg.sprite.name.Equals("gameing_Connect on_icon"))
                    {
                        //connectText.text = "기기와 연결되어 있습니다.";
                        connectText.SetActive(true);
                        fit_tagBtn.interactable = false;    //클릭할수 없게
                    }
                }
            }
            else
            {
                //커넥트 연결 상단 버튼
                Image connectImg = parentsObj_Canvas.transform.GetChild(1).transform.GetChild(1).GetComponent<Image>();

                GameObject fit_tagPopup = parentsObj_Canvas.transform.Find("PopupGroup").transform.GetChild(1).gameObject;
                fit_tagPopup.SetActive(true);

                //GameObject connectImage = fit_tagPopup.transform.GetChild(0).gameObject;
                //connectImage.SetActive(true);

                Button fit_tagBtn = fit_tagPopup.transform.Find("ConnectButton").GetComponent<Button>();
                GameObject connectText = fit_tagPopup.transform.Find("ConnectText").gameObject;


                //센서 연결 상태에 따른 센서팝업 텍스트 글자 변경
                //연결 놉 빨강
                if (connectImg.sprite.name.Equals("gameing_Connect off_icon"))
                {
                    //connectText.text = "센서의 전원이 켜져있는지 확인해주세요.";
                    connectText.SetActive(false);
                    fit_tagBtn.interactable = true; //클릭할수 있게
                }
                //연결 옙 초록
                else if (connectImg.sprite.name.Equals("gameing_Connect on_icon"))
                {
                    //connectText.text = "기기와 연결되어 있습니다.";
                    connectText.SetActive(true);
                    fit_tagBtn.interactable = false;    //클릭할수 없게
                }
            }
        }
    }

    public void Popup_GPS_Show()
    {
        parentsObj_Canvas = GameObject.Find("UICanvas");
        //커넥트 연결 빨강
        parentsObj_Canvas.transform.GetChild(1).transform.GetChild(1).GetComponent<Image>().sprite = connectSprite[0];

        GameObject fit_tagPopup = parentsObj_Canvas.transform.Find("PopupGroup").transform.GetChild(1).gameObject;
        fit_tagPopup.SetActive(true);
        GameObject connectImage = fit_tagPopup.transform.GetChild(0).gameObject;
        connectImage.SetActive(true);
        Text connectText = connectImage.transform.GetChild(0).GetComponent<Text>();
        connectText.text = "GPS(위치)와 블루투스가 켜져있는지 확인해주세요." + "\n" +
            "페어링이 되어 있다면 페어링을 해제해주세요.";
    }

    // Use this for initialization
    void Start()
    {
        StartProcess();
        inverseQt = Quaternion.identity;
    }

    string AddStr(string _data)
    {
        string returnStr = "";

        if(_data != "")
        {
            string[] addArr;

            addArr = _data.Split(':');

            returnStr = "FITTAG(" + addArr[0] + addArr[1] + addArr[2] + addArr[3] + addArr[4] + addArr[5] + ")";
        }

        return returnStr;
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
                        BluetoothLEHardwareInterface.ScanForPeripheralsWithServices(null, (address, name) =>
                        {
                            //setStateTextAcc("OKAddress : " + address + "/ Name : " + name);

                            if(PlayerPrefs.GetString("Busan_SensorAddr").Equals(""))
                            {
                                
                                //찾고자 하는 것에 포함되어 있는지
                                if (name.Contains(DeviceName))
                                {
                                    

                                    _workingFoundDevice = true;
                                    //connecting = true;  //연결 성공

                                    // it is always a good idea to stop scanning while you connect to a device
                                    // and get things set up
                                    BluetoothLEHardwareInterface.StopScan();

                                    // add it to the list and set to connect to it
                                    _deviceAddress = address;

                                    //처음 등록한 장비가 없다면 현재 센서주소를 저장한다.
                                    if (PlayerPrefs.GetString("Busan_SensorAddr").Equals(""))
                                        PlayerPrefs.SetString("Busan_SensorAddr", address);

                                    SetState(States.Connect, 0.5f);

                                    _workingFoundDevice = false;
                                }
                            }
                            else
                            {
                                //Text text_2 = GameObject.Find("Text (2)").GetComponent<Text>();
                                //text_2.text = "connecting___-__- : " + connecting + " name " + name;

                                // we only want to look at devices that have the name we are looking for
                                // this is the best way to filter out devices
                                if (name.Contains(AddStr(PlayerPrefs.GetString("Busan_SensorAddr"))))
                                {
                                    _workingFoundDevice = true;
                                    //connecting = true;  //연결 성공

                                    // it is always a good idea to stop scanning while you connect to a device
                                    // and get things set up
                                    BluetoothLEHardwareInterface.StopScan();

                                    // add it to the list and set to connect to it
                                    _deviceAddress = address;


                                    SetState(States.Connect, 0.5f);

                                    _workingFoundDevice = false;

                                    //Text text_3 = GameObject.Find("Text (3)").GetComponent<Text>();
                                    //text_3.text = "SensorAddr 2: " + name + " connecting " + connecting;
                                }
                                else
                                {
                                    //Text text_1 = GameObject.Find("Text (1)").GetComponent<Text>();
                                    //text_1.text = "SensorAddr 1: " + AddStr(PlayerPrefs.GetString("Busan_SensorAddr")) + " connecting " + connecting + " 'name' " + name + " 'address' " + address;

                                    //connecting = false;  //연결 실패
                                                         //커넥트 연결 빨강
                                    parentsObj_Canvas.transform.GetChild(1).transform.GetChild(1).GetComponent<Image>().sprite = connectSprite[0];
                                    //센서가 연결되지 않았을 때 팝업 띄움
                                    //Popup_SenSor_State_Show();
                                }
                                
                            }


                        }, null, false, false);
                        break;

                    case States.Connect:
                        // set these flags
                        _foundID = false;
                        //커넥트 연결 초록
                        //parentsObj_Canvas.transform.GetChild(1).transform.GetChild(1).GetComponent<Image>().sprite = connectSprite[0];
                        //setStateText("Connecting to ESP32");

                        // note that the first parameter is the address, not the name. I have not fixed this because
                        // of backwards compatiblity.
                        // also note that I am note using the first 2 callbacks. If you are not looking for specific characteristics you can use one of
                        // the first 2, but keep in mind that the device will enumerate everything and so you will want to have a timeout
                        // large enough that it will be finished enumerating before you try to subscribe or do any other operations.
                        //Text text_4 = GameObject.Find("Text (6)").GetComponent<Text>();
                        //text_4.text = "SensorAddr__: " + PlayerPrefs.GetString("Busan_SensorAddr") + " _deviceAddress " + _deviceAddress;

                        //addressStr = PlayerPrefs.GetString("SensorAddr");
                        BluetoothLEHardwareInterface.ConnectToPeripheral(_deviceAddress, null, null,
                            (address, serviceUUID, characteristicUUID) =>
                            {
                                //setStateTextAcc("_deviceAddress " + _deviceAddress + "  address " + address);
                                //text_4.text = "SensorAddr__: " + PlayerPrefs.GetString("Busan_SensorAddr") + " 'address' " + address + " 'serviceUUID' " + serviceUUID + " " + characteristicUUID;
                                //처음 등록한 센서의 주소랑 현재 접속한 센서의 주소가 동일하면
                                if (PlayerPrefs.GetString("Busan_SensorAddr").Equals(address))
                                {
                                    if (IsEqual(serviceUUID, FullUUID(ServiceUUID)))
                                    {
                                        // if we have found the characteristic that we are waiting for
                                        // set the state. make sure there is enough timeout that if the
                                        // device is still enumerating other characteristics it finishes
                                        // before we try to subscribe
                                        if (IsEqual(characteristicUUID, FullUUID(Characteristic)))
                                        {
                                            _connected = true;
                                            SetState(States.Subscribe, 2f);
                                            //setStateText("Connected to ESP32");

                                            //커넥트 연결 초록
                                            parentsObj_Canvas.transform.GetChild(1).transform.GetChild(1).GetComponent<Image>().sprite = connectSprite[1];

                                            if (PlayerPrefs.GetString("Busan_Player_Sex").Equals("Man"))
                                            {
                                                NewManMove.instance.connetState = true;
                                            }
                                            else if (PlayerPrefs.GetString("Busan_Player_Sex").Equals("Woman"))
                                            {
                                                NewWomanMove.instance.connetState = true;
                                            }

                                            //connecting = true;  //연결 중
                                        }
                                    }
                                }
                                else
                                {
                                    //connecting = false;  //연결 실패
                                                         //커넥트 연결 빨강
                                    parentsObj_Canvas.transform.GetChild(1).transform.GetChild(1).GetComponent<Image>().sprite = connectSprite[0];
                                    //센서가 연결되지 않았을 때 팝업 띄움
                                    Popup_SenSor_State_Show();
                                }


                            }, (disconnectedAddress) =>
                            {
                                BluetoothLEHardwareInterface.Log("Device disconnected: " + disconnectedAddress);

                                //connecting = false;  //연결 실패

                                //커넥트 연결 빨강
                                parentsObj_Canvas.transform.GetChild(1).transform.GetChild(1).GetComponent<Image>().sprite = connectSprite[0];

                                //센서가 연결되지 않았을 때 팝업 띄움
                                Popup_SenSor_State_Show();
                            });
                        break;

                    case States.Subscribe:
                        //setStateText("Subscribing to ESP32");
                        //connecting = true;  //연결 중

                        BluetoothLEHardwareInterface.SubscribeCharacteristicWithDeviceAddress(_deviceAddress,
                        FullUUID(ServiceUUID),
                        FullUUID(Characteristic), null,
                        (address, characteristicUUID, bytes) =>
                        {

                            if (bytes.Length == 12)
                            {
                                ax = BitConverter.ToSingle(bytes, 0);
                                ay = BitConverter.ToSingle(bytes, 4);
                                az = BitConverter.ToSingle(bytes, 8);

                                if (PlayerPrefs.GetString("Busan_Player_Sex").Equals("Man"))
                                {
                                    NewManMove.instance.mpu_value[0] = ax;
                                    NewManMove.instance.mpu_value[1] = ay;
                                    NewManMove.instance.mpu_value[2] = az;
                                }
                                else if (PlayerPrefs.GetString("Busan_Player_Sex").Equals("Woman"))
                                {
                                    NewWomanMove.instance.mpu_value[0] = ax;
                                    NewWomanMove.instance.mpu_value[1] = ay;
                                    NewWomanMove.instance.mpu_value[2] = az;
                                }

                                //setStateTextAcc("byte length: " + bytes.Length + ", ax: " + ax.ToString("0.00000") +
                                //            ", ay: " + ay.ToString("0.00000") + ", az: " + az.ToString("0.00000"));// ", speed : " + PlayerCtrl_v1.instance.speed);

                                //if(SceneManager.GetActiveScene().name.Equals("BusanMapMorning") || SceneManager.GetActiveScene().name.Equals("BusanMapNight"))
                                //{
                                    //Text text = GameObject.Find("Text (1)").GetComponent<Text>();
                                    //text.text = ax.ToString("0.00000") +
                                    //        ", ay: " + ay.ToString("0.00000") + ", az: " + az.ToString("0.00000");
                                //}
                            }
                            else if (bytes.Length == 16)
                            {
                                qx = BitConverter.ToSingle(bytes, 0);
                                qy = BitConverter.ToSingle(bytes, 4);
                                qz = BitConverter.ToSingle(bytes, 8);
                                qw = BitConverter.ToSingle(bytes, 12);
                            }
                        });


                        //stateTextAcc3.text = "_connected == " + _connected;
                        // set to the none state and the user can start sending and receiving data
                        _state = States.None;

                        break;

                    case States.Unsubscribe:
                        BluetoothLEHardwareInterface.UnSubscribeCharacteristic(_deviceAddress, FullUUID(ServiceUUID),
                            FullUUID(Characteristic),
                            null);
                        SetState(States.Disconnect, 2f);
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
                                    //커넥트 연결 빨강
                                    parentsObj_Canvas.transform.GetChild(1).transform.GetChild(1).GetComponent<Image>().sprite = connectSprite[0];
                                    //센서가 연결되지 않았을 때 팝업 띄움
                                    Popup_SenSor_State_Show();
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

        //센서 연결 상태에 따른 센서팝업 텍스트 글자 변경
        ConnectStateTextShow();
    }

    string FullUUID(string uuid)
    {
        return "389b" + uuid + "-5aab-4ee3-9f3d-d847c3b1a4e9";
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