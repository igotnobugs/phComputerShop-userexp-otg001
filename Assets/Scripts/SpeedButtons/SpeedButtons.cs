using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeedButtons : MonoBehaviour
{
   public void ChangeTimeScale(float timeSpeed)
    {
        Time.timeScale = timeSpeed; 
        Debug.Log("TimeScale Set to: " + timeSpeed.ToString());
    }
}