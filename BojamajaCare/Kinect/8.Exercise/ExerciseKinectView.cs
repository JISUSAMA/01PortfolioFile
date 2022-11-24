
using Quaternion = UnityEngine.Quaternion;
using UnityEngine;
using UnityEngine.UI;
using LightBuzz.Vitruvius;
using LightBuzz;



public class ExerciseKinectView : MonoBehaviour
{
    SensorAdapter adapter = null;

    public SensorType sensorType = SensorType.Kinect2;

    Texture2D imageViewTexture = null;
    public RawImage imageViewRawImage = null;
    public Transform imageViewTransform = null;

    public ScreenViewStickman screenViewStickman = null;


    public Material imageViewMaterial = null;
    public bool flipView = false;

    [Space(5)]

    public Transform leftShoulderParent;
    public AngleArc leftShoulderArc;
    public TextMesh leftShoulderAngleText;

    [Space(5)]

    public Transform leftElbowParent;
    public AngleArc leftElbowArc;
    public TextMesh leftElbowAngleText;

    [Space(5)]

    public Transform leftHipParent;
    public AngleArc leftHipArc;
    public TextMesh leftHipAngleText;

    [Space(5)]

    public Transform leftKneeParent;
    public AngleArc leftKneeArc;
    public TextMesh leftKneeAngleText;

    [Space(5)]

    public Transform leftAnkleParent;
    public AngleArc leftAnkleArc;
    public TextMesh leftAnkleAngleText;

    [Space(5)]

    public Transform rightShoulderParent;
    public AngleArc rightShoulderArc;
    public TextMesh rightShoulderAngleText;

    [Space(5)]

    public Transform rightElbowParent;
    public AngleArc rightElbowArc;
    public TextMesh rightElbowAngleText;

    [Space(5)]

    public Transform rightHipParent;
    public AngleArc rightHipArc;
    public TextMesh rightHipAngleText;

    [Space(5)]

    public Transform rightKneeParent;
    public AngleArc rightKneeArc;
    public TextMesh rightKneeAngleText;

    [Space(5)]

    public Transform rightAnkleParent;
    public AngleArc rightAnkleArc;
    public TextMesh rightAnkleAngleText;


    void OnEnable()
    {
        if (GlobalSensorController.WasSetFromLoader)
        {
            sensorType = GlobalSensorController.StartWithSensor;
        }

        adapter = new SensorAdapter(sensorType)
        {
            OnChangedAvailabilityEventHandler = (sender, args) =>
            {
                Debug.Log(args.SensorType + " is connected: " + args.IsConnected);
            }
        };
    }

    void OnDisable()
    {
        if (adapter != null)
        {
            adapter.Close();
            adapter = null;
        }

        Destroy(imageViewTexture);
        imageViewMaterial.mainTexture = null;
    }



    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);

            return;
        }

        if (adapter == null) return;

        if (adapter.SensorType != sensorType)
        {
            adapter.SensorType = sensorType;
        }

        Frame frame = adapter.UpdateFrame();

        if (frame != null)
        {
            if (frame.ImageData != null)
            {
                imageViewTexture = ValidateTexture(imageViewTexture, frame.ImageWidth, frame.ImageHeight, imageViewRawImage);

                if (imageViewTexture != null)
                {
                    imageViewTexture.LoadRawTextureData(frame.ImageData);
                    imageViewTexture.Apply(false);
                }
            }

            
            

            Body body = frame.GetClosestBody();

            if (body != null)
            {
                screenViewStickman.UpdateStickman(adapter, frame, body, imageViewTransform, Visualization.Image);

                //내가한거
                //키넥트에서 내 거리
                //var distacne = body.Joints[JointType.SpineBase].WorldPosition.Z;
                //Debug.Log(distacne);

                //바닥에서 내머리까지 거리
                Floor floor = frame.FloorData;
                float height = floor.Height;    //키넥트 높이
                //Debug.Log(height);

                //바닥에서 오른쪽 손높이 측정 +11해서 측정하면 키가 나옴
                double floorDis = floor.DistanceFrom(body.Joints[JointType.Head].WorldPosition);
                //Debug.Log((float)(floorDis + 11f));

                // Left
                UpdateArc(
                    screenViewStickman.jointPoints[13].position,
                    screenViewStickman.jointPoints[5].position,
                    screenViewStickman.jointPoints[6].position,
                    body.Joints[JointType.HipLeft].WorldPosition,
                    body.Joints[JointType.ShoulderLeft].WorldPosition,
                    body.Joints[JointType.ElbowLeft].WorldPosition,
                   leftShoulderParent, leftShoulderArc, leftShoulderAngleText, "LeftShoulder");

                UpdateArc(
                    screenViewStickman.jointPoints[5].position,
                    screenViewStickman.jointPoints[6].position,
                    screenViewStickman.jointPoints[7].position,
                    body.Joints[JointType.ShoulderLeft].WorldPosition,
                    body.Joints[JointType.ElbowLeft].WorldPosition,
                    body.Joints[JointType.WristLeft].WorldPosition,
                   leftElbowParent, leftElbowArc, leftElbowAngleText, "LeftElbow");

                UpdateArc(
                    screenViewStickman.jointPoints[3].position,
                    screenViewStickman.jointPoints[13].position,
                    screenViewStickman.jointPoints[14].position,
                    body.Joints[JointType.SpineMid].WorldPosition,
                    body.Joints[JointType.HipLeft].WorldPosition,
                    body.Joints[JointType.KneeLeft].WorldPosition,
                   leftHipParent, leftHipArc, leftHipAngleText, "LeftHip");

                UpdateArc(
                    screenViewStickman.jointPoints[13].position,
                    screenViewStickman.jointPoints[14].position,
                    screenViewStickman.jointPoints[15].position,
                    body.Joints[JointType.HipLeft].WorldPosition,
                    body.Joints[JointType.KneeLeft].WorldPosition,
                    body.Joints[JointType.AnkleLeft].WorldPosition,
                   leftKneeParent, leftKneeArc, leftKneeAngleText, "LeftKnee");

                UpdateArc(
                    screenViewStickman.jointPoints[14].position,
                    screenViewStickman.jointPoints[15].position,
                    screenViewStickman.jointPoints[16].position,
                    body.Joints[JointType.KneeLeft].WorldPosition,
                    body.Joints[JointType.AnkleLeft].WorldPosition,
                    body.Joints[JointType.FootLeft].WorldPosition,
                   leftAnkleParent, leftAnkleArc, leftAnkleAngleText, "LeftAnkle");

                // Right

                UpdateArc(
                    screenViewStickman.jointPoints[17].position,
                    screenViewStickman.jointPoints[9].position,
                    screenViewStickman.jointPoints[10].position,
                    body.Joints[JointType.HipRight].WorldPosition,
                    body.Joints[JointType.ShoulderRight].WorldPosition,
                    body.Joints[JointType.ElbowRight].WorldPosition,
                   rightShoulderParent, rightShoulderArc, rightShoulderAngleText, "RightShoulder");

                UpdateArc(
                    screenViewStickman.jointPoints[9].position,
                    screenViewStickman.jointPoints[10].position,
                    screenViewStickman.jointPoints[11].position,
                    body.Joints[JointType.ShoulderRight].WorldPosition,
                    body.Joints[JointType.ElbowRight].WorldPosition,
                    body.Joints[JointType.WristRight].WorldPosition,
                   rightElbowParent, rightElbowArc, rightElbowAngleText, "RightElbow");

                UpdateArc(
                    screenViewStickman.jointPoints[3].position,
                    screenViewStickman.jointPoints[17].position,
                    screenViewStickman.jointPoints[18].position,
                    body.Joints[JointType.SpineMid].WorldPosition,
                    body.Joints[JointType.HipRight].WorldPosition,
                    body.Joints[JointType.KneeRight].WorldPosition,
                   rightHipParent, rightHipArc, rightHipAngleText, "RightHip");

                UpdateArc(
                    screenViewStickman.jointPoints[17].position,
                    screenViewStickman.jointPoints[18].position,
                    screenViewStickman.jointPoints[19].position,
                    body.Joints[JointType.HipRight].WorldPosition,
                    body.Joints[JointType.KneeRight].WorldPosition,
                    body.Joints[JointType.AnkleRight].WorldPosition,
                   rightKneeParent, rightKneeArc, rightKneeAngleText, "RightKnee");

                UpdateArc(
                    screenViewStickman.jointPoints[18].position,
                    screenViewStickman.jointPoints[19].position,
                    screenViewStickman.jointPoints[20].position,
                    body.Joints[JointType.KneeRight].WorldPosition,
                    body.Joints[JointType.AnkleRight].WorldPosition,
                    body.Joints[JointType.FootRight].WorldPosition,
                   rightAnkleParent, rightAnkleArc, rightAnkleAngleText, "RightAnkle");
            }
        }
    }

    void UpdateArc(
        Vector2 start2D, Vector2 center2D, Vector2 end2D,
        Vector3D start3D, Vector3D center3D, Vector3D end3D,
        Transform arcParent, AngleArc arc, TextMesh arcText, string angleName)
    {
        Vector2 direction1 = (start2D - center2D).normalized;
        Vector2 direction2 = (end2D - center2D).normalized;

        float angle = Vector2.Angle(direction1, direction2);
        arc.Angle = angle;
        arc.transform.up = Quaternion.Euler(0, 0, angle) *
            (Vector2.Dot(Quaternion.Euler(0, 0, 90) * direction1, direction2) > 0 ? direction1 : direction2);

        angle = Calculations.Angle(start3D, center3D, end3D);
        arcText.text = angle.ToString("N0") + '°';

        //운동체크 시작!!
        if(Exercise_UIManager.instance.execiseCheckStart.Equals(true))
            Exercise_DataManager.instance.ExerciseCheck(angleName, angle);

        arcParent.position = center2D;
    }


    Texture2D ValidateTexture(Texture2D texture, int width, int height, RawImage rawImage)
    {
        if (width == 0 || height == 0) return texture;

        if (texture == null)
        {
            texture = new Texture2D(width, height, TextureFormat.RGBA32, false);
        }
        else if (texture.width != width || texture.height != height)
        {
            texture.Resize(width, height, TextureFormat.RGBA32, false);
        }

        rawImage.texture = texture;

        return texture;
    }

    public void OnLeftCheck(bool isOn)
    {
        leftAnkleArc.gameObject.SetActive(isOn);
        leftElbowArc.gameObject.SetActive(isOn);
        leftHipArc.gameObject.SetActive(isOn);
        leftKneeArc.gameObject.SetActive(isOn);
        leftShoulderArc.gameObject.SetActive(isOn);

        leftAnkleAngleText.gameObject.SetActive(isOn);
        leftElbowAngleText.gameObject.SetActive(isOn);
        leftHipAngleText.gameObject.SetActive(isOn);
        leftKneeAngleText.gameObject.SetActive(isOn);
        leftShoulderAngleText.gameObject.SetActive(isOn);
    }

    public void OnRightCheck(bool isOn)
    {
        rightAnkleArc.gameObject.SetActive(isOn);
        rightElbowArc.gameObject.SetActive(isOn);
        rightHipArc.gameObject.SetActive(isOn);
        rightKneeArc.gameObject.SetActive(isOn);
        leftShoulderArc.gameObject.SetActive(isOn);

        rightAnkleAngleText.gameObject.SetActive(isOn);
        rightElbowAngleText.gameObject.SetActive(isOn);
        rightHipAngleText.gameObject.SetActive(isOn);
        rightKneeAngleText.gameObject.SetActive(isOn);
        rightShoulderAngleText.gameObject.SetActive(isOn);
    }
}
