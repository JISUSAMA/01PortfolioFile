using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMoveDirection : MonoBehaviour
{
	public float speed = 1;
	Rigidbody rbody;

	// Start is called before the first frame update
	void Start()
	{
		rbody = GetComponent<Rigidbody>();
	//	StartCoroutine(RandomizeDirection());
	}
    private void OnTriggerEnter(Collider other)
    {
		Debug.Log(other.name);
    }
    // Update is called once per frame
    void FixedUpdate()
	{
		//rbody.velocity = new Vector3(Random.Range(1, 1000), Random.Range(1, 1000), Random.Range(1, 1000));
		rbody.AddForce(transform.forward *speed);

	}
	public Vector3 direction;
 
	public float velocity ;
	private IEnumerator RandomizeDirection()
	{
		float min = 1f;
		float max = 9f;
		float interval = UnityEngine.Random.Range(min, max);
		float time = 0f;
		float rotationX = 0.2f;
		float rotationY = 1f;
		velocity = 2;
		while (true)
		{
			time += Time.deltaTime;
			transform.Rotate(rotationX, 0f, 0f);
			transform.Rotate(0f, rotationY, 0f);

			if (time >= interval)
			{
				time = 0f;
				rotationX = -rotationX;
				rotationY = -rotationY;
				interval = UnityEngine.Random.Range(min, max);
			}
			 /*direction = (this.gameObject.transform.position - transform.position).normalized;
			      // 초가 아닌 한 프레임으로 가속도 계산하여 속도 증가
			      velocity = (velocity  * Time.deltaTime);
			this.gameObject.transform.position = new Vector3(transform.position.x + (direction.x * velocity),
			                                             transform.position.y + (direction.y * velocity),
			                                                transform.position.z);
			*/
			rbody.AddForce(transform.forward);
			yield return null;
		}
	}
}
	//  public float collisionInterval = 1f;
	//  public float maxSpeed = 50f;
	//  public float acceleration = 100f;
	//  public float rotation_time = 10f;
	//
	//  Rigidbody _rigidbody;
	//  Collider _collider;
	//  float _timeSinceContact;
	//  float _myTime;
	//
	//  public Transform target;
	//  public Vector3 direction;
	//  public float velocity ;
	//  public float default_velocity;
	//  public float accelaration;
	//  public Vector3 default_direction;
	//
	//  public static int m_Boundary = 7;
	//  public static GameObject[] m_Fishes;
	//  public static Vector3 m_TargetPosition = Vector3.zero;
	//  void Start()
	//  {
	//      _rigidbody = GetComponent<Rigidbody>();
	//      _collider = GetComponent<Collider>();
	//      // 자동으로 움직일 방향 벡터
	//      default_direction.x = Random.Range(-1.0f, 1.0f);
	//      default_direction.y = Random.Range(-1.0f, 1.0f);
	//      // 가속도 지정 (추후 힘과 질량, 거리 등 계산해서 수정할 것)
	//      accelaration = 0.1f;
	//      default_velocity = 0.1f;
	//      velocity = 2f;
	//     StartCoroutine(ChangeDirection());
	//  }
	//  private IEnumerator RandomizeDirection()
	//  {
	//      float min = 1f;
	//      float max = 9f;
	//      float interval = UnityEngine.Random.Range(min, max);
	//      float time = 0f;
	//      float rotationX = 0.2f;
	//      float rotationY = 1f;
	//
	//      while (true)
	//      {
	//          time += Time.deltaTime;
	//          transform.Rotate(rotationX, 0f, 0f);
	//          transform.Rotate(0f, rotationY, 0f);
	//
	//          if (time >= interval)
	//          {
	//              time = 0f;
	//              rotationX = -rotationX;
	//              rotationY = -rotationY;
	//              interval = UnityEngine.Random.Range(min, max);
	//          }
	//
	//          yield return null;
	//      }
	//  }
	//  private void Update()
	//  {
	//      MoveToTarget();
	//      //   _timeSinceContact += Time.deltaTime;
	//      //  _myTime += Time.deltaTime;
	//      //   MoveToTarget();
	//  }
	//  public void MoveToTarget()
	//  {
	//      // Player의 위치와 이 객체의 위치를 빼고 단위 벡터화 한다.
	//     // direction = (target.position - transform.position).normalized;
	//      // 초가 아닌 한 프레임으로 가속도 계산하여 속도 증가
	//      velocity = (velocity + accelaration * Time.deltaTime);
	//      // 해당 방향으로 무빙
	//      this.transform.position = new Vector3(transform.position.x + (direction.x * velocity),
	//                                             transform.position.y + (direction.y * velocity),
	//                                                transform.position.z);
	//
	//  }
	//  private void FixedUpdate()
	//  {
	//      /*  rotation_time -= Time.deltaTime;
	//        if (rotation_time < 0)
	//        {
	//            Move();
	//            rotation_time = 10f;
	//        }*/
	//     // GetTargetPosition();
	//  }
	//  private void OnCollisionExit(Collision collision)
	//  {
	//   /*   if (collision.gameObject.name.Equals("SporeSpace"))
	//      {
	//          StartCoroutine(RandomizeDirection());
	//      }*/
	//  }
	//  void GetTargetPosition()
	//  {
	//      if (Random.Range(1, 100) < 50)
	//      {
	//          m_TargetPosition = new Vector3(
	//              Random.Range(-m_Boundary, m_Boundary),
	//              Random.Range(-m_Boundary, m_Boundary),
	//              Random.Range(-m_Boundary, m_Boundary)
	//          );
	//          _rigidbody.AddForce(transform.forward);
	//      } 
	//
	//  }
	//  private void Move()
	//  {
	//      //   if (_rigidbody.velocity.magnitude < maxSpeed)
	//      //   {
	//      //       _rigidbody.AddForce(transform.forward * acceleration);
	//      //   }
	//      
	//      _rigidbody.angularVelocity = Random.insideUnitSphere * maxSpeed;
	//      _rigidbody.AddForce(transform.forward * acceleration);
	//  }
	//
	//  //방향 전환 하기 
	//  private IEnumerator ChangeDirection(Collision other)
	//  {
	//      float time = 0f;
	//      var targetQuat = Quaternion.LookRotation(other.contacts[0].normal, Vector3.up);
	//      while (time < 2f)
	//      {
	//          transform.rotation = Quaternion.Lerp(transform.rotation, targetQuat, 2f * Time.deltaTime);
	//          time += Time.deltaTime;
	//          yield return null;
	//      }
	//  }
