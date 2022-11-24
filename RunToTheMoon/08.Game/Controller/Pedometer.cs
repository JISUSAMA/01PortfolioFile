using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Pedometer : MonoBehaviour
{
    [Header("Pedometer")]
    public float lowLimit = 0.005F; // Level to fall to the low state. 
    public float highLimit = 0.1F; // Level to go to high state (and detect steps).
    private bool stateHigh = false; // Comparator state.

    public float filterHigh = 10.0F; // Noise filter control. Reduces frequencies above filterHigh private . 
    public float filterLow = 0.1F; // Average gravity filter control. Time constant about 1/filterLow.
    public float currentAcceleration = 0F; // Noise filter.
    float averageAcceleration = 0F;

    public int steps = 0; // Step counter. Counts when comparator state goes to high.
    public float waitCounter = 0F;
    public float timeElapsedWalking = 0F;
    public float timeElapsedStandingStill = 0F;
    public bool isWalking = false;

    void Awake()
    {
        averageAcceleration = Input.acceleration.magnitude; // Initialize average filter.
    }

    void FixedUpdate()
    {
        if (Game_DataManager.instance.gamePlaying)
        {
            // Filter Input.acceleration using Math.Lerp.
            currentAcceleration = Mathf.Lerp(currentAcceleration, Input.acceleration.magnitude, Time.deltaTime * filterHigh);
            averageAcceleration = Mathf.Lerp(averageAcceleration, Input.acceleration.magnitude, Time.deltaTime * filterLow);

            float delta = currentAcceleration - averageAcceleration; // Gets the acceleration pulses.

            if (!stateHigh)
            {
                // If the state is low.
                if (delta > highLimit)
                {
                    // Only goes to high, if the Input is higher than the highLimit.
                    stateHigh = true;
                    steps++; // Counts the steps when the comparator goes to high.
                    Game_DataManager.instance.once_stepCount = steps;
                    //stepsText.text = "Steps: " + steps;
                }
            }
            else
            {
                if (delta < lowLimit)
                {
                    // Only goes to low, if the Input is lower than the lowLimit.
                    stateHigh = false;
                }
            }
        }
    }
}