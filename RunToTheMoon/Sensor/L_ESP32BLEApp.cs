using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;


public class L_ESP32BLEApp : MonoBehaviour
{
    public static L_ESP32BLEApp instance { get; private set; }

    private string DeviceName = "FITTAG";// "ESP32 FITNESS LEFT";
    private string ServiceUUID = "c560";
    private string Characteristic = "c561";

    //float gx, gy, gz, gch;
    float ax, ay, az;
    float qx, qy, qz, qw;
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
    //[SerializeField] private TMP_Text stateText1;
    //[SerializeField] private TMP_Text stateText2;
    //[SerializeField] private TMP_Text stateText3;
    //[SerializeField] private TMP_Text stateText4;
    //[SerializeField] private TMP_Text stateText5;

    //[SerializeField] private TMP_Text stateText6;
    //[SerializeField] private TMP_Text stateText7;
    //[SerializeField] private TMP_Text stateText8;

    //[SerializeField] private TMP_Text stateTextAcc;
    //[SerializeField] private TMP_Text stateTextAcc1;
    //TMP_Text stateTextAcc;
    // [SerializeField] private TMP_Text stateTextAcc2;
    // [SerializeField] private TMP_Text stateTextAcc3;
    //[SerializeField] private Transform woman;
    //[SerializeField] private Transform man;


    //[SerializeField] private Transform player_Woman;


    //private Quaternion inverseQt;
    //private Quaternion rawQt;
    //public Transform cube;


    //센서 연결 팝업 
    public Sprite[] connectSprite;    //연결 이미지 0: 끊김(빨강) , 1: 연결(초록)
    GameObject ui_parentsObj_Canvas;   //UI 캔버스 부모역할
    GameObject popup_parentsObj_Canvas;   //Popup 캔버스 부모역할
    public bool connecting;    //연결 상태

    //public bool logOn = false;

    private void Awake()
    {
        if (instance != null)
            Destroy(this.gameObject);
        else instance = this;
        //QualitySettings.vSyncCount = 0;
        //Application.targetFrameRate = 60;
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

    public void NonState()
    {
        _state = States.None;
        _timeout = 0.5f;
        BluetoothLEHardwareInterface.StopScan();
    }
    //void setStateText2(string text)
    //{
    //    if (stateText1 == null) return;
    //    stateText1.text = text;
    //}

    //void setStateText(string text)
    //{
    //    if (stateText == null) return;
    //    stateText.text = text;
    //}

    public void StartProcess()
    {
        // 1. 모든씬의 센서팝업이 포함된부모의 캔버스 이름 고정하기
        popup_parentsObj_Canvas = GameObject.Find("PopupCanvas");
        ui_parentsObj_Canvas = GameObject.Find("UICanvas");

        Reset();
        BluetoothLEHardwareInterface.Initialize(true, false, () =>
        {
            SetState(States.Scan, 0.5f);
        }, (error) =>
        {
            // 에러가 나면
            SetState(States.None, 0.5f);
            Popup_SenSor_State_Show();
            BluetoothLEHardwareInterface.Log("Error: " + error);
        });
    }

    //센서 연결 상태에 따른 센서팝업 텍스트 글자 변경
    public void ConnectStateTextShow()
    {

        //팝업이 나와야 하는 씬 이름을 조건식으로 주시오.
        if (SceneManager.GetActiveScene().name != "Loading"
            && SceneManager.GetActiveScene().name != "HallofFame"
            && SceneManager.GetActiveScene().name != "Join"
            && SceneManager.GetActiveScene().name != "Login"
            && SceneManager.GetActiveScene().name != "NickName"
            && SceneManager.GetActiveScene().name != "ExerciseRecord")
        {

            popup_parentsObj_Canvas = GameObject.Find("PopupCanvas");   // 모든씬에 살아있으므로 그때그때 캔버스를 받아야함
            ui_parentsObj_Canvas = GameObject.Find("UICanvas");

            GameObject fit_tagPopup = popup_parentsObj_Canvas.transform.Find("PopupGroup").transform.GetChild(6).gameObject;  // 연결상태 팝업

            if (fit_tagPopup.activeSelf.Equals(true))
            {
                Image connectStateImg = ui_parentsObj_Canvas.transform.Find("BackImage").transform.GetChild(1).GetComponent<Image>();    // 연결상태 이미지
                //커넥트 연결 상단 버튼
                GameObject popUp_UnConnectObj = fit_tagPopup.transform.GetChild(1).gameObject;  // UnConnect 오브젝트 (배열상 2번째 존재)
                GameObject popUp_ConnectObj = fit_tagPopup.transform.GetChild(2).gameObject;  // Connect 오브젝트 (배열상 3번째 존재)


                if (connectStateImg.sprite.name.Equals("Top Bar_OnDisConnected"))
                {
                    popUp_ConnectObj.SetActive(false);
                    popUp_UnConnectObj.SetActive(true);
                }
                else if (connectStateImg.sprite.name.Equals("Top Bar_OnConnected"))
                {
                    popUp_ConnectObj.SetActive(true);
                    popUp_UnConnectObj.SetActive(false);
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
            //셋업팝업창이 열려 있지 않을 경우
            if (popup_parentsObj_Canvas.transform.Find("PopupGroup").transform.GetChild(3).gameObject.activeSelf.Equals(false))
            {
                // 한번만 띄워야함

                Image connectStateImg = ui_parentsObj_Canvas.transform.Find("BackImage").transform.GetChild(1).GetComponent<Image>();    // 연결상태 이미지

                GameObject fit_tagPopup = popup_parentsObj_Canvas.transform.Find("PopupGroup").transform.GetChild(6).gameObject;  // 연결상태 팝업

                fit_tagPopup.SetActive(true);   // 팝업 열기

                //커넥트 연결 상단 버튼
                GameObject popUp_UnConnectObj = fit_tagPopup.transform.GetChild(1).gameObject;  // UnConnect 오브젝트 (배열상 2번째 존재)
                GameObject popUp_ConnectObj = fit_tagPopup.transform.GetChild(2).gameObject;  // Connect 오브젝트 (배열상 3번째 존재)

                if (connectStateImg.sprite.name.Equals("Top Bar_OnDisConnected"))
                {
                    popUp_ConnectObj.SetActive(true);
                    popUp_UnConnectObj.SetActive(false);
                }
                else if (connectStateImg.sprite.name.Equals("Top Bar_OnConnected"))
                {
                    popUp_ConnectObj.SetActive(false);
                    popUp_UnConnectObj.SetActive(true);
                }

                //stateText2.text = accessCount.ToString();
            }
        }
    }

    // Use this for initialization
    void Start()
    {
        //StartProcess();

        //inverseQt = Quaternion.identity;
    }
    //float timedelta = 0f;
    // Update is called once per frame
    void Update()
    {
        //timedelta += Time.deltaTime;
        //stateText1.text = "data transfer rate : " + timedelta.ToString();
        //stateText2.text = "x : " + cube.rotation.x + "/ y : " + cube.rotation.y + "/ z : " + cube.rotation.z;

        //if (rowData.Count < 802 && logOn)
        //{
        //    rowData.Add(timedelta.ToString());
        //    rowData.Add(stateText2.text);
        //}
        //stateText.text = _timeout.ToString();
        //stateText1.text = _state.ToString();
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
                        SetState(States.None, 0.5f);
                        break;
                    case States.Scan:
                        //setStateText("Scanning for ESP32 devices...");                        
                        //GPS연결 여부 확인
                        //if (!Input.location.isEnabledByUser)
                        //{
                        //Debug.Log("non Gps");
                        //}
                        BluetoothLEHardwareInterface.ScanForPeripheralsWithServices(null, (address, name) =>
                        {
                            //setStateText("OKAddress : " + address + "/ Name : " + name);
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
                                SetState(States.None, 0.5f);

                                BluetoothLEHardwareInterface.StopScan();

                                Popup_SenSor_State_Show();
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
                        BluetoothLEHardwareInterface.ConnectToPeripheral(_deviceAddress, null, null,
                        (address, serviceUUID, characteristicUUID) =>
                        {
                                //setStateTextAcc("_deviceAddress " + _deviceAddress + "  address " + address);

                                //처음 등록한 센서의 주소랑 현재 접속한 센서의 주소가 동일하면
                                if (PlayerPrefs.GetString("SensorAddr").Equals(address))
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
                                            RunnerPlayer1.instance.connetState = true;

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
                                    SetState(States.None, 0.5f);
                                    //BluetoothLEHardwareInterface.StopScan();
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
                                SetState(States.None, 0.5f);
                                //BluetoothLEHardwareInterface.StopScan();
                                Popup_SenSor_State_Show();
                        });
                        break;

                    case States.Subscribe:
                        //setStateText("Subscribing to ESP32");

                        connecting = true;  //연결 중

                        BluetoothLEHardwareInterface.SubscribeCharacteristicWithDeviceAddress(_deviceAddress,
                        FullUUID(ServiceUUID),
                        FullUUID(Characteristic), null,
                        (address, characteristicUUID, bytes) =>
                        {
                            if (bytes.Length == 16)
                            {
                                //float timedelta = 0f;
                                //timedelta += Time.deltaTime;
                                //stateText1.text = "data transfer rate : " + timedelta.ToString();

                                //rawQt = new Quaternion(qx, qy, -qz, qw);
                                //cube.rotation = rawQt * inverseQt;

                                //stateText2.text = "x : " + cube.rotation.x + " / y : " + cube.rotation.y + " / z : " + cube.rotation.z;

                                //if (rowData.Count < 802 && logOn)
                                //{
                                //    rowData.Add(timedelta.ToString());
                                //    rowData.Add(stateText2.text);
                                //}

                                if (Game_DataManager.instance.gamePlaying)
                                {
                                    qx = BitConverter.ToSingle(bytes, 0);
                                    qy = BitConverter.ToSingle(bytes, 4);
                                    qz = BitConverter.ToSingle(bytes, 8);
                                    qw = BitConverter.ToSingle(bytes, 12);

                                    RunnerPlayer1.instance.mpu_q_value[0] = qx;
                                    RunnerPlayer1.instance.mpu_q_value[1] = qy;
                                    RunnerPlayer1.instance.mpu_q_value[2] = -qz;
                                    RunnerPlayer1.instance.mpu_q_value[3] = qw;
                                }
                            }
                            else if (bytes.Length == 12)
                            {
                                //ax = BitConverter.ToSingle(bytes, 0);
                                //ay = BitConverter.ToSingle(bytes, 4);
                                //az = BitConverter.ToSingle(bytes, 8);

                                //RunnerPlayer1.instance.mpu_a_value[0] = ax;
                                //RunnerPlayer1.instance.mpu_a_value[1] = ay;
                                //RunnerPlayer1.instance.mpu_a_value[2] = az;
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
                                    //parentsObj_Canvas.transform.GetChild(1).transform.GetChild(1).GetComponent<Image>().sprite = connectSprite[0];
                                    Image connectStateImg = ui_parentsObj_Canvas.transform.Find("BackImage").transform.GetChild(1).GetComponent<Image>();
                                    connectStateImg.sprite = connectSprite[0];
                                    //센서가 연결되지 않았을 때 팝업 띄움
                                    SetState(States.None, 0.5f);
                                    BluetoothLEHardwareInterface.StopScan();
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

    //    public void resetRotation()
    //    {
    //        inverseQt = Quaternion.Inverse(rawQt);
    //    }

    //    public List<string> rowData = new List<string>();

    //    [System.Serializable]
    //    public class RowData
    //    {
    //        public string accessSeconds;
    //        public string values;
    //    }
    //    [System.Serializable]
    //    public class RowDataList
    //    {
    //        public RowData[] rowdata;
    //    }

    //    public RowDataList myrowdataList = new RowDataList();
    //    public void Write()
    //    {
    //        if (logOn)
    //        {
    //            stateText.text = getPath();
    //            TextWriter tw = new StreamWriter(getPath(), false);
    //            tw.WriteLine("Access Count, Values");
    //            tw.Close();

    //            tw = new StreamWriter(getPath(), true);

    //            for (int i = 0; i < 400; i++)
    //            {
    //                tw.WriteLine(rowData[i*2] + "," + rowData[(i*2)+1]);
    //            }

    //            tw.Close();
    //        }
    //    }

    //    public void Clear()
    //    {
    //        rowData.Clear();
    //    }

    //    public void LogOn(Button button)
    //    {
    //        if (logOn)
    //        {
    //            var colors = button.colors;
    //            colors.normalColor = Color.white;
    //            button.colors = colors;
    //            logOn = false;
    //        }
    //        else
    //        {
    //            var colors = button.colors;
    //            colors.normalColor = Color.red;
    //            button.colors = colors;
    //            logOn = true;
    //        }
    //    }

    //    private string getPath()
    //    {
    //#if UNITY_EDITOR
    //        return Application.dataPath + "/LogData.csv";
    //#elif UNITY_ANDROID
    //        return Application.persistentDataPath+"Log Data.csv";
    //#elif UNITY_IPHONE
    //        return Application.persistentDataPath+"/"+"Student Data.csv";
    //#else
    //        return Application.dataPath +"/"+"Student Data.csv";
    //#endif
    //    }

}