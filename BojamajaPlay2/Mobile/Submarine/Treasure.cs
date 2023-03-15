using UnityEngine;

namespace Submarine
{
  public class Treasure : MonoBehaviour
  {
    public int points; // ���� ����
    public float rotationSpeed;


    void OnTriggerEnter(Collider col)
    {
            //�÷��̾ ������ �浹 ���� ��
      if (col.gameObject.layer == LayerMask.NameToLayer("Player"))
      {
        DataManager.Instance.scoreManager.Add(points);
        SoundManager.Instance.PlaySFX("13 Get jewelry");

        Destroy(this.gameObject);
      }
    }
    void Update()
    {
      transform.Rotate(transform.forward, rotationSpeed * Time.deltaTime, Space.World);
    }
  }
}