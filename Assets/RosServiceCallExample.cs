using RosMessageTypes.UnityRoboticsDemo;
using UnityEngine;
using Unity.Robotics.ROSTCPConnector;
using RosColor = RosMessageTypes.UnityRoboticsDemo.UnityColorMsg;

public class RosServiceCallExample : MonoBehaviour
{
    ROSConnection ros;

    public string serviceName = "pos_srv";
    public string serviceName4 = "pos_srv4";

    public GameObject cube;

    // Cube movement conditions
    public float delta = 20f;
    public float speed = 10f;
    private Quaternion destinationElbow;
    private Quaternion destinationArm;
    private Quaternion destinationWrist;
    private Vector3 destinationx;
    public Quaternion originElbow;
    public Quaternion originArm;
    public Quaternion originWrist;
    //public Vector3 originTest;

    float awaitingResponseUntilTimestamp = -1;

    void Start()
    {
        ROSConnection.GetOrCreateInstance().Subscribe<RosColor>("color", ColorChange);
        ros = ROSConnection.GetOrCreateInstance();
        ros.RegisterRosService<PositionServiceRequest, PositionServiceResponse>(serviceName);
        ros.RegisterRosService<PositionService4Request, PositionService4Response>(serviceName4);

        //destinationElbow = GameObject.Find("elbow_link").transform.localRotation;
        //destinationArm = GameObject.Find("arm_link").transform.localRotation;
        //originElbow.eulerAngles = new Vector3(0, 90, 0);
        //originArm.eulerAngles = new Vector3(-50, 0, 0);
        //originWrist.eulerAngles = new Vector3(0, 0, 50);
       
    }

    private void Update()
    {
        // Move our position a step closer to the target.
        float step = speed * Time.deltaTime; // calculate distance to move

        GameObject.Find("elbow_link").transform.localRotation = Quaternion.RotateTowards(GameObject.Find("elbow_link").transform.localRotation,
            destinationElbow, step);
        GameObject.Find("arm_linkC").transform.localRotation = Quaternion.RotateTowards(GameObject.Find("arm_linkC").transform.localRotation,
            destinationArm, step);
        GameObject.Find("wrist_linkC").transform.localRotation = Quaternion.RotateTowards(GameObject.Find("wrist_linkC").transform.localRotation,
            destinationWrist, step);
        //print(destinationArm.eulerAngles);

        if (Vector3.Distance(cube.transform.position, destinationx) < delta && Time.time > awaitingResponseUntilTimestamp)
        {
            //Debug.Log("Destination reached.");

            PosRotMsg cubePos = new PosRotMsg(
                cube.transform.position.x,
                cube.transform.position.y,
                cube.transform.position.z,
                cube.transform.rotation.x,
                cube.transform.rotation.y,
                cube.transform.rotation.z,
                cube.transform.rotation.w
            );

            PositionServiceRequest positionServiceRequest = new PositionServiceRequest(cubePos);
            PositionService4Request positionService4Request = new PositionService4Request(cubePos);

            // Send message to ROS and return the response
            ros.SendServiceMessage<PositionServiceResponse>(serviceName, positionServiceRequest, Callback_Destination);
            ros.SendServiceMessage<PositionService4Response>(serviceName4, positionService4Request, Callback_Destination4);

            awaitingResponseUntilTimestamp = Time.time + 1f; // don't send again for 1 second, or until we receive a response
        }
    }

    void Callback_Destination(PositionServiceResponse response)
    {
        //GameObject.Find("elbow_link").transform.localRotation = Quaternion.RotateTowards(GameObject.Find("elbow_link").transform.localRotation,
        //        originElbow, speed * Time.deltaTime);
        //GameObject.Find("arm_link").transform.localRotation = Quaternion.RotateTowards(GameObject.Find("arm_link").transform.localRotation,
        //        originArm, speed * Time.deltaTime);
        //GameObject.Find("Sphere").transform.position = Vector3.MoveTowards(GameObject.Find("Sphere").transform.position,
        //       originTest, speed * Time.deltaTime);

        awaitingResponseUntilTimestamp = -1;
        destinationElbow.eulerAngles = new Vector3(0f, 90f, 0f);
        destinationArm.eulerAngles = new Vector3(-50, 0, 0);
        destinationWrist.eulerAngles = new Vector3(0, 0, 50);
        //Debug.Log("New Destination: " + destinationArm);
        //Debug.Log("New Destination: " + destinationElbow);
    }

    void Callback_Destination4(PositionService4Response response)
    {
        awaitingResponseUntilTimestamp = -1;
        destinationElbow.eulerAngles = new Vector3(0f, -5f, 0f);
        destinationArm.eulerAngles = new Vector3(5, 0, 0);
        destinationWrist.eulerAngles = new Vector3(0, 0, 0);
    }


void ColorChange(RosColor colorMessage)
    {
        destinationElbow.eulerAngles = new Vector3(0f, 0f, 0f);
        destinationArm.eulerAngles = new Vector3(0, 0, 0);
        destinationWrist.eulerAngles = new Vector3(0, 0, 0);


        //GameObject.Find("servo_head").transform.localScale = new Vector3(1, 1, 2.5f);
        //GameObject.Find("servo_head2").transform.localScale = new Vector3(1, 1, 2.5f);
        //cube.GetComponent<Renderer>().material.color = new Color32((byte)colorMessage.r, (byte)colorMessage.g, (byte)colorMessage.b, (byte)colorMessage.a);
    }

}