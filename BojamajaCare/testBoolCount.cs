using System.Linq; //선언
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;

public class testBoolCount : MonoBehaviour
{
    public Text[] TextBool; 
    public bool[] boolList;
    public Text total;

    private void Update()
    {
        int Temp_finger = boolList.Count(c => c);
        total.text = Temp_finger.ToString();
        Debug.Log(Temp_finger);
    }

    public void OnClick_ChangeBool()
    {
        string ButtonName = EventSystem.current.currentSelectedGameObject.name;
        int number = int.Parse(ButtonName)-1;
            if (boolList[number])
            {
                TextBool[number].text = "false"; boolList[number] = false;
            }
            else if (!boolList[number])
            {
                TextBool[number].text = "true"; boolList[number] = true;
            }
    }
}
