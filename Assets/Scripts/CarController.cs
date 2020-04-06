using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(InputManager))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(LightingManager))]
public class CarController : MonoBehaviour
{
    public InputManager inputManager;
    public LightingManager lightingManager;
    public List<WheelCollider> throttleWheelsCollider;
    public List<GameObject> steerWheelsCollider;
    public List<GameObject> meshes;
    public float strengthCoefficient = 10000f;
    public float maxTurn = 20f;
    public Transform centerMass;
    public Rigidbody rigidBody;
    public float brakeStrength;
    public List<GameObject> tailLights;
    public FuelSystem fuelSystem;
    public SpeedometerUIManager speedUI;
    // Start is called before the first frame update
    void Start() {
        inputManager = GetComponent<InputManager>();
        rigidBody = GetComponent<Rigidbody>();

        if (centerMass) {
            rigidBody.centerOfMass = centerMass.localPosition;
        }

        fuelSystem = GetComponent<FuelSystem>();
    }

    void Update() {

        if (inputManager.lightON) {

            lightingManager.toggleHeadlights();
          
        }
            
        foreach(GameObject tailLight in tailLights) {
            tailLight.GetComponent<Renderer>().material.SetColor("_EmissionColor", inputManager.brake ? new Color(0.5f, 0.111f, 0.111f) : Color.black);
        }

        speedUI.setCarCpeed(transform.InverseTransformVector(rigidBody.velocity).z);
    }


    // Update is called once per frame
    void FixedUpdate() {
        foreach(WheelCollider wheel in throttleWheelsCollider) {

            if (inputManager.brake || fuelSystem.startFuel <= 0 ) {

                wheel.motorTorque = 0f;
                wheel.brakeTorque = brakeStrength * Time.deltaTime;
            }
            else {
                wheel.motorTorque = strengthCoefficient * Time.deltaTime * inputManager.throttle;
                wheel.brakeTorque = 0f;

          
            }

            fuelSystem.fuelConsumptionRate =  wheel.motorTorque >= 0f ? wheel.motorTorque * 0.0003f: Mathf.Abs(wheel.motorTorque) * 0.0001f;
            fuelSystem.ReduceFuel();
        }

        foreach (GameObject wheel in steerWheelsCollider) {
            wheel.GetComponent<WheelCollider>().steerAngle = maxTurn * inputManager.steer;
            wheel.transform.localEulerAngles = new Vector3(0f,inputManager.steer * maxTurn,0f);
        }

        foreach(GameObject mesh in meshes) {
            mesh.transform.Rotate(rigidBody.velocity.magnitude  * (transform.InverseTransformDirection(rigidBody.velocity).z >= 0 ? 1 : -1) / ( 2 * Mathf.PI * 0.33f),0f,0f);
        }

    }
}
