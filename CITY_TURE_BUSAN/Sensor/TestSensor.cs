using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TestSensor : MonoBehaviour
{
    public static TestSensor instance { get; private set; }

    private string DeviceName = "FITTAG";// "ESP32 FITNESS LEFT";
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
    [SerializeField] private TMP_Text stateText1;
    //[SerializeField] private TMP_Text stateTextAcc;
    //[SerializeField] private TMP_Text stateTextAcc1;
    //TMP_Text stateTextAcc;
    // [SerializeField] private TMP_Text stateTextAcc2;
    // [SerializeField] private TMP_Text stateTextAcc3;
    //[SerializeField] private Transform woman;
    //[SerializeField] private Transform man;


    //[SerializeField] private Transform player_Woman;
    private Quaternion inverseQt;
    private Quaternion rawQt;

    //센서 연결 팝업 
    public Sprite[] connectSprite;    //연결 이미지 0: 끊김(빨강) , 1: 연결(초록)
    GameObject ui_parentsObj_Canvas;   //UI 캔버스 부모역할
    GameObject popup_parentsObj_Canvas;   //Popup 캔버스 부모역할
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

    void setStateText2(string text)
    {
        if (stateText1 == null) return;
        stateText1.text = text;
    }

    void setStateText(string text)
    {
        if (stateText == null) return;
        stateText.text = text;
    }

    public void StartProcess()
    {
        // 1. 모든씬의 센서팝업이 포함된부모의 캔버스 이름 고정하기
        popup_parentsObj_Canvas = GameObject.Find("PopupCanvas");
        ui_parentsObj_Canvas = GameObject.Find("UICanvas");

        Reset();
        BluetoothLEHardwareInterface.Initialize(true, false, () =>
        {
            SetState(States.Scan, 0.1f);
        }, (error) =>
        {
            // 에러가 나면
            Popup_SenSor_State_Show();
            BluetoothLEHardwareInterface.Log("Error: " + error);
        });
    }

    //센서 연결 상태에 따른 센서팝업 텍스트 글자 변경
    public void ConnectStateTextShow()
    {
        if (SceneManager.GetActiveScene().name != "Loading")    // 로딩상태를 제외한 나머지
        {
            popup_parentsObj_Canvas = GameObject.Find("PopupCanvas");   // 모든씬에 살아있으므로 그때그때 캔버스를 받아야함

            GameObject fit_tagPopup = popup_parentsObj_Canvas.transform.Find("PopupGroup").transform.GetChild(6).gameObject;  // 연결상태 팝업


            if (fit_tagPopup.activeSelf.Equals(true))
            {
                Image connectStateImg = ui_parentsObj_Canvas.transform.Find("BackImage").transform.GetChild(1).GetComponent<Image>();    // 연결상태 이미지
                //커넥트 연결 상단 버튼
                GameObject popUp_UnConnectObj = fit_tagPopup.transform.GetChild(1).gameObject;  // UnConnect 오브젝트 (배열상 2번째 존재)
                GameObject popUp_ConnectObj = fit_tagPopup.transform.GetChild(2).gameObject;  // Connect 오브젝트 (배열상 3번째 존재)


                if (connectStateImg.sprite.name.Equals(""))
                {
                    popUp_ConnectObj.SetActive(true);
                    popUp_UnConnectObj.SetActive(false);
                }
                else if (connectStateImg.sprite.name.Equals(""))
                {
                    popUp_ConnectObj.SetActive(false);
                    popUp_UnConnectObj.SetActive(true);
                }
            }
            else
            {
                if (connecting.Equals(false))
                {
                    ui_parentsObj_Canvas.transform.Find("BackImage").transform.GetChild(1).GetComponent<Image>().sprite = connectSprite[0];
                }
                else
                {
                    ui_parentsObj_Canvas.transform.Find("BackImage").transform.GetChild(1).GetComponent<Image>().sprite = connectSprite[1];
                }
            }
        }
    }

    // 연결상태에 따라서 팝업 띄움
    public void Popup_SenSor_State_Show()
    {
        popup_parentsObj_Canvas = GameObject.Find("PopupCanvas");
        ui_parentsObj_Canvas.transform.Find("BackImage").transform.GetChild(1).GetComponent<Image>().sprite = connectSprite[0];

        if (SceneManager.GetActiveScene().name != "Loading")
        {
            if (SceneManager.GetActiveScene().name.Equals("Lobby"))
            {
                //셋업팝업창이 열려 있지 않을 경우
                if (popup_parentsObj_Canvas.transform.Find("PopupGroup").transform.GetChild(3).gameObject.activeSelf.Equals(false))
                {
                    Image connectStateImg = ui_parentsObj_Canvas.transform.Find("BackImage").transform.GetChild(1).GetComponent<Image>();    // 연결상태 이미지

                    GameObject fit_tagPopup = popup_parentsObj_Canvas.transform.Find("PopupGroup").transform.GetChild(6).gameObject;  // 연결상태 팝업
                    fit_tagPopup.SetActive(true);   // 팝업 열기

                    //커넥트 연결 상단 버튼
                    GameObject popUp_UnConnectObj = fit_tagPopup.transform.GetChild(1).gameObject;  // UnConnect 오브젝트 (배열상 2번째 존재)
                    GameObject popUp_ConnectObj = fit_tagPopup.transform.GetChild(2).gameObject;  // Connect 오브젝트 (배열상 3번째 존재)

                    if(connectStateImg.sprite.name.Equals(""))
                    {
                        popUp_ConnectObj.SetActive(true);
                        popUp_UnConnectObj.SetActive(false);
                    }
                    else if(connectStateImg.sprite.name.Equals(""))
                    {
                        popUp_ConnectObj.SetActive(false);
                        popUp_UnConnectObj.SetActive(true);
                    }

                }
            }
            else
            {
                Image connectStateImg = ui_parentsObj_Canvas.transform.Find("BackImage").transform.GetChild(1).GetComponent<Image>();    // 연결상태 이미지

                GameObject fit_tagPopup = popup_parentsObj_Canvas.transform.Find("PopupGroup").transform.GetChild(6).gameObject;  // 연결상태 팝업
                fit_tagPopup.SetActive(true);   // 팝업 열기

                //커넥트 연결 상단 버튼
                GameObject popUp_UnConnectObj = fit_tagPopup.transform.GetChild(1).gameObject;  // UnConnect 오브젝트 (배열상 2번째 존재)
                GameObject popUp_ConnectObj = fit_tagPopup.transform.GetChild(2).gameObject;  // Connect 오브젝트 (배열상 3번째 존재)

                if (connectStateImg.sprite.name.Equals(""))
                {
                    popUp_ConnectObj.SetActive(true);
                    popUp_UnConnectObj.SetActive(false);
                }
                else if (connectStateImg.sprite.name.Equals(""))
                {
                    popUp_ConnectObj.SetActive(false);
                    popUp_UnConnectObj.SetActive(true);
                }
            }
        }
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
        //센서 연결 상태에 따른 센서팝업 텍스트 글자 변경
        ConnectStateTextShow();

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
                        setStateText("Scanning for ESP32 devices...");

                        //GPS연결 여부 확인
                        //if (!Input.location.isEnabledByUser)
                        //{
                        //Debug.Log("non Gps");
                        //}

                        BluetoothLEHardwareInterface.ScanForPeripheralsWithServices(null, (address, name) =>
                        {
                            setStateText("OKAddress : " + address + "/ Name : " + name);
                            // we only want to look at devices that have the name we are looking for
                            // this is the best way to filter out devices
                            if (name.Contains(DeviceName))
                            {
                                //stateTextAcc2.text = "Address : " + address + "/ Name : " + name;
                                _workingFoundDevice = true;
                                connecting = true;  //연결 성공

                                // it is always a good idea to stop scanning while you connect to a device
                                // and get things set up
                                BluetoothLEHardwareInterface.StopScan();

                                // add it to the list and set to connect to it
                                _deviceAddress = address;

                                //처음 등록한 장비가 없다면 현재 센서주소를 저장한다.
                                if (PlayerPrefs.GetString("SensorAddr").Equals(""))
                                    PlayerPrefs.SetString("SensorAddr", address);

                                SetState(States.Connect, 0.5f);

                                _workingFoundDevice = false;

                            }
                            else
                            {
                                connecting = false;  //연결 실패
                                                     //커넥트 연결 빨강
                                                     //parentsObj_Canvas.transform.GetChild(1).transform.GetChild(1).GetComponent<Image>().sprite = connectSprite[0];
                                Image connectStateImg = ui_parentsObj_Canvas.transform.Find("BackImage").transform.GetChild(1).GetComponent<Image>();
                                connectStateImg.sprite = connectSprite[0];
                                //센서가 연결되지 않았을 때 팝업 띄움
                                Popup_SenSor_State_Show();
                            }

                        }, null, false, false);
                        break;

                    case States.Connect:
                        // set these flags
                        _foundID = false;
                        //커넥트 연결 초록
                        //parentsObj_Canvas.transform.GetChild(1).transform.GetChild(1).GetComponent<Image>().sprite = connectSprite[0];
                        setStateText("Connecting to ESP32");

                        // note that the first parameter is the address, not the name. I have not fixed this because
                        // of backwards compatiblity.
                        // also note that I am note using the first 2 callbacks. If you are not looking for specific characteristics you can use one of
                        // the first 2, but keep in mind that the device will enumerate everything and so you will want to have a timeout
                        // large enough that it will be finished enumerating before you try to subscribe or do any other operations.
                        BluetoothLEHardwareInterface.ConnectToPeripheral(_deviceAddress, null, null,
                            (address, serviceUUID, characteristicUUID) =>
                            {
                                //setStateTextAcc("_deviceAddress " + _deviceAddress + "  address " + address);

                                //처음 등록한 센서의 주소랑 현재 접속한 센서의 주소가 동일하면
                                if (PlayerPrefs.GetString("SensorAddr").Equals(address))
                                {
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
                                            setStateText("Connected to ESP32");

                                            //커넥트 연결 초록
                                            //parentsObj_Canvas.transform.GetChild(1).transform.GetChild(1).GetComponent<Image>().sprite = connectSprite[1];
                                            Image connectStateImg = ui_parentsObj_Canvas.transform.Find("BackImage").transform.GetChild(1).GetComponent<Image>();
                                            connectStateImg.sprite = connectSprite[1];

                                            //if (PlayerPrefs.GetString("Player_Sex").Equals("Man"))
                                            //{
                                            //    Man_Move.instance.connetState = true;
                                            //}
                                            //else if (PlayerPrefs.GetString("Player_Sex").Equals("Woman"))
                                            //{
                                            //    Woman_Move.instance.connetState = true;
                                            //}
                                            //RunnerPlayer1.instance.connetState = true;


                                            connecting = true;  //연결 중
                                        }
                                    }
                                }
                                else
                                {
                                    connecting = false;  //연결 실패
                                                         //커넥트 연결 빨강
                                                         //parentsObj_Canvas.transform.GetChild(1).transform.GetChild(1).GetComponent<Image>().sprite = connectSprite[0];
                                    Image connectStateImg = ui_parentsObj_Canvas.transform.Find("BackImage").transform.GetChild(1).GetComponent<Image>();
                                    connectStateImg.sprite = connectSprite[0];
                                    //센서가 연결되지 않았을 때 팝업 띄움
                                    Popup_SenSor_State_Show();
                                }


                            }, (disconnectedAddress) =>
                            {
                                BluetoothLEHardwareInterface.Log("Device disconnected: " + disconnectedAddress);

                                connecting = false;  //연결 실패

                                //커넥트 연결 빨강
                                //parentsObj_Canvas.transform.GetChild(1).transform.GetChild(1).GetComponent<Image>().sprite = connectSprite[0];
                                Image connectStateImg = ui_parentsObj_Canvas.transform.Find("BackImage").transform.GetChild(1).GetComponent<Image>();
                                connectStateImg.sprite = connectSprite[0];

                                //센서가 연결되지 않았을 때 팝업 띄움
                                Popup_SenSor_State_Show();
                            });
                        break;

                    case States.Subscribe:
                        setStateText("Subscribing to ESP32");
                        connecting = true;  //연결 중

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

                                //if (PlayerPrefs.GetString("Player_Sex").Equals("Man"))
                                //{
                                //    Man_Move.instance.mpu_value[0] = ax;
                                //    Man_Move.instance.mpu_value[1] = ay;
                                //    Man_Move.instance.mpu_value[2] = az;
                                //}
                                //else if (PlayerPrefs.GetString("Player_Sex").Equals("Woman"))
                                //{
                                //    Woman_Move.instance.mpu_value[0] = ax;
                                //    Woman_Move.instance.mpu_value[1] = ay;
                                //    Woman_Move.instance.mpu_value[2] = az;
                                //}

                                // 2021.08.10
                                // 이거도 로비씬에서 존재하지않아서 에러뜸
                                //RunnerPlayer1.instance.mpu_value[0] = ax;
                                //RunnerPlayer1.instance.mpu_value[1] = ay;
                                //RunnerPlayer1.instance.mpu_value[2] = az;

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


                        //stateTextAcc3.text = "_connected == " + _connected;
                        // set to the none state and the user can start sending and receiving data
                        _state = States.None;

                        break;

                    case States.Unsubscribe:
                        BluetoothLEHardwareInterface.UnSubscribeCharacteristic(_deviceAddress, ServiceUUID,
                            Characteristic,
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
                                    //parentsObj_Canvas.transform.GetChild(1).transform.GetChild(1).GetComponent<Image>().sprite = connectSprite[0];
                                    Image connectStateImg = ui_parentsObj_Canvas.transform.Find("BackImage").transform.GetChild(1).GetComponent<Image>();
                                    connectStateImg.sprite = connectSprite[0];
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