using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedometerUIManager : MonoBehaviour
{
    public GameObject needle;
    private float startPosition = 220f, endPosition = -49f;
    private float desirePosition;

    private float vehicleSpeed;

    public virtual void setCarCpeed(float speed) {

        vehicleSpeed = Mathf.Clamp(Mathf.Round(speed * 5f), 0f, 1000);
    }
      
   private void Update()
    {
        updateNeedle();
    }

    public void updateNeedle() {
        desirePosition = startPosition - endPosition;
        float temp = Mathf.Abs(vehicleSpeed) * 0.00555f;
        needle.transform.eulerAngles = new Vector3(0, 0,(startPosition - temp * desirePosition));
    }


}
