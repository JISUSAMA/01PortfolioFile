using UnityEngine;
using System.Collections;

//���� �Ǵ� ������
public class BowlingBall : MonoBehaviour
{
    public bool hitPin = false;
    public GameObject confetti;
    public float spinDuration;
    //  public CameraController _camCtrl; 
    Vector3 _referencePos;
    Rigidbody _rigidbody;
    MeshRenderer _meshRenderer;

    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _meshRenderer = GetComponent<MeshRenderer>();
        FindObjectOfType<CameraController>().MissAnyway();

    }
    void Start()
    {
        Destroy(gameObject, 10f); //������ 10�� �ڿ� �����
    }

    void OnCollisionEnter(Collision _col)
    {
        if (_col.gameObject.layer == LayerMask.NameToLayer("Buildings")) //�������� �ɿ� �¾��� �� 
        {
            if (!hitPin)
            {
                hitPin = true; //���� �¾Ҵ�!
                SoundManager.Instance.PlaySFX("bowling-strike");
                Confetti();
                FindObjectOfType<PinSpawner>().EvaluatePins(2f);
                FindObjectOfType<CameraController>().Hit(); //ī�޶��� ��ġ�� hitCameraPosition��ġ���� �̵� ��Ŵ
                StopCoroutine("_ApplyContinuousModifiers");
            }
        }
        //���� �ε����� ������Ʈ ��������� 
        if (_col.gameObject.layer == LayerMask.NameToLayer("wall"))
        {
            FindObjectOfType<CameraController>().MissAnyway();
            Destroy(this.gameObject, 2f);
        }
    }

    //�������� ȸ���� ��ŭ ���� ���� ����
    public void Throw(Vector3 refPos, Vector3 forceVector, float rotValue)
    {
        SoundManager.Instance.PlaySFX("bowling-ball", 2f);

        _referencePos = refPos;

        _rigidbody.AddForce(forceVector, ForceMode.Impulse);

        StartCoroutine("_ApplyContinuousModifiers", rotValue);
    }

    private IEnumerator _ApplyContinuousModifiers(float rotValue)
    {
        float val = rotValue;
        float time = 0f;

        while (time < spinDuration)
        {
            time += Time.deltaTime;
            val = Mathf.Lerp(rotValue, 0f, time / spinDuration);

            _rigidbody.AddForce(_referencePos * val / 2 * Time.deltaTime, ForceMode.Force);
            transform.Rotate(Vector3.up, rotValue * Time.deltaTime, Space.World);

            yield return null;
        }
    }
    //�������� �浹 ������ ��ƼŬ ����
    public void Confetti()
    {
        Instantiate(confetti, transform.position, Quaternion.identity);
    }
}