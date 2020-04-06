using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntiRollBar : MonoBehaviour
{
    public WheelCollider WheelL;
    public WheelCollider wheelR;
    private Rigidbody carRigidBody;

    public float AntiRoll = 5000.0f;
    // Start is called before the first frame update
    void Start()
    {
        carRigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        WheelHit hit = new WheelHit();
        float travelL = 1.0f;
        float travelR = 1.0f;


        bool groundedL = WheelL.GetGroundHit(out hit);

        if (groundedL) {
            travelL = (-WheelL.transform.InverseTransformPoint(hit.point).y
                - WheelL.radius) / WheelL.suspensionDistance;
        }

        bool groundedR = wheelR.GetGroundHit(out hit);

        if (groundedR) {

            travelR = (-wheelR.transform.InverseTransformPoint(hit.point).y
                - wheelR.radius) / wheelR.suspensionDistance;
        }

        var antiRollForce = (travelL - travelR) * AntiRoll;

        if (groundedL)
            carRigidBody.AddForceAtPosition(WheelL.transform.up * -antiRollForce,
                WheelL.transform.position);
        if (groundedR)
            carRigidBody.AddForceAtPosition(wheelR.transform.up * antiRollForce,
                wheelR.transform.position);
    }
}
