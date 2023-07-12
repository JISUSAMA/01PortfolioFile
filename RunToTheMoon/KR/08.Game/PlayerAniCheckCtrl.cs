using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerAniCheckCtrl : MonoBehaviour
{
    private int steps = 0;
    public void GetStarBoolCheck()
    {
        Chapter19_Manager.instance.getStarCheck = true;
    }

    public void LeftFootSFX(float targetSpeed)
    {
        if (!SceneManager.GetActiveScene().name.Equals("HallofFame") &&getMovementState(RunnerPlayer1.instance.speed) == getMovementState(targetSpeed))
        {
            steps++; // Counts the steps when the comparator goes to high.
            Game_DataManager.instance.once_stepCount = steps;
            SoundManager.Instance.PlaySFX("Landing_Character_SFX");
            //SoundManager.Instance.PlaySFX("Run_Character_1_SFX");
            //SoundManager.Instance.PlaySFX("Run_Character_2_SFX");
            //SoundManager.Instance.PlaySFX("Run_Character_3_SFX");
        }
    }

    public void RightFootSFX(float targetSpeed)
    {
        if (!SceneManager.GetActiveScene().name.Equals("HallofFame")&& getMovementState(RunnerPlayer1.instance.speed) == getMovementState(targetSpeed))
        {
            steps++; // Counts the steps when the comparator goes to high.
            Game_DataManager.instance.once_stepCount = steps;
            SoundManager.Instance.PlaySFX("Landing_Character_SFX");
            //SoundManager.Instance.PlaySFX("Run_Character_1_SFX");
            //SoundManager.Instance.PlaySFX("Run_Character_2_SFX");
            //SoundManager.Instance.PlaySFX("Run_Character_3_SFX");
        }
    }

    public void JumpSFX()
    {
        SoundManager.Instance.PlaySFX("Jump_Character_SFX");
    }

    private float getMovementState(float speed)
    {
        if (speed < 0.5f)
        {
            return 0;
        }

        if (speed <= 0.8f)
        {
            return 1;
        }

        if (speed <= 1.1f)
        {
            return 2;
        }

        return 3;
    }
}
