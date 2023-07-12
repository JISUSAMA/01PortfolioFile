using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Customer : MonoBehaviour
{
    CustomerQueueManager _queueManager;
    public Customer thisCustomer;
    public SkinnedMeshRenderer _animal;
    public Texture _angryImg;
    public int _currentSpot = 4;


    void Awake()
    {
        _queueManager = FindObjectOfType<CustomerQueueManager>();
        thisCustomer = this.gameObject.GetComponent<Customer>();
        OrderManager.angry = false;  //���� �� ��, �ʱ�ȭ
    }
    public void Update()
    {
        if (!this.gameObject.Equals("Timeguy(Clone)") && OrderManager.angry.Equals(true))
        {
            Angry();
        }
    }
    public void Angry()
    {
        if (thisCustomer._currentSpot.Equals(1))
        {                
                _animal.material.SetTexture("_MainTex", _angryImg); 
        }
    }

    public void MoveTo(int spot)
    {
        StopCoroutine("_MoveTo");
        StartCoroutine("_MoveTo", spot);
    }
    //�ֹ� �Ϸ� �Ǿ��� �� ����
    public void MoveUpOneSpot()
    {
        StopCoroutine("_MoveTo");
        StartCoroutine("_MoveTo", -1);
    }
    //�մ��� spot�� ������
    IEnumerator _MoveTo(int spot)
    {
        float time = 0f;
        float duration = 1f;
        _currentSpot = spot > 0 ? spot : _currentSpot - 1;
        //spot���� �̵�
        while (time < duration)
        {
            if (time < duration * 0.8f) time += Time.deltaTime;
            else time += 0.5f * Time.deltaTime;

            transform.position = Vector3.Lerp(transform.position, _queueManager.QueueSpots[_currentSpot], time / duration);
            yield return null;
        }
        if (_currentSpot == 0) Destroy(gameObject);
    }

}