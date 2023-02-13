using RosMessageTypes.UnityRoboticsDemo;
using UnityEngine;
using Unity.Robotics.ROSTCPConnector;

public class RosService2 : MonoBehaviour
{
    ROSConnection ros;

    public string serviceName2 = "pos_srv2";
    public string serviceName3 = "pos_srv3";

    public GameObject cube;

    // Cube movement conditions
    public float delta = 1.0f;
    public float speed = 200f;
    private Vector3 destination;
    public Quaternion destination90;

    float awaitingResponseUntilTimestamp = -1;

    void Start()
    {
        
        ros = ROSConnection.GetOrCreateInstance();
        ros.RegisterRosService<PositionService2Request, PositionService2Response>(serviceName2);
        ros.RegisterRosService<PositionService3Request, PositionService3Response>(serviceName3);
        destination = cube.transform.position;
    }

    private void Update()
    {
        // Move our position a step closer to the target.
        float step = speed * Time.deltaTime; // calculate distance to move
        cube.transform.position = Vector3.MoveTowards(cube.transform.position, destination, step);

        if (Vector3.Distance(cube.transform.position, destination) < delta && Time.time > awaitingResponseUntilTimestamp)
        {
            Debug.Log("Destination reached.");

            PosRotMsg cubePos = new PosRotMsg(
                cube.transform.position.x,
                cube.transform.position.y,
                cube.transform.position.z,
                cube.transform.rotation.x,
                cube.transform.rotation.y,
                cube.transform.rotation.z,
                cube.transform.rotation.w
            );

            PositionService2Request positionService2Request = new PositionService2Request(cubePos);
            PositionService3Request positionService3Request = new PositionService3Request(cubePos);

            // Send message to ROS and return the response
            ros.SendServiceMessage<PositionService2Response>(serviceName2, positionService2Request, Callback_Destination2);
            ros.SendServiceMessage<PositionService3Response>(serviceName3, positionService3Request, Callback_Destination3);

            awaitingResponseUntilTimestamp = Time.time + 1.0f; // don't send again for 1 second, or until we receive a response

            GameObject.Find("forearm_linkC").transform.localRotation = Quaternion.RotateTowards(GameObject.Find("forearm_linkC").transform.localRotation,
            destination90, step);

        }
    }

    void Callback_Destination2(PositionService2Response response)
    {
        awaitingResponseUntilTimestamp = -1;
        destination90.eulerAngles = new Vector3(0f, 0f, 90f);
        Debug.Log("New Destination: " + destination);
        print(destination90.eulerAngles);
    }
    void Callback_Destination3(PositionService3Response response)
    {
        awaitingResponseUntilTimestamp = -1;
        destination90.eulerAngles = new Vector3(0f, 0f, 0f);
        Debug.Log("New Destination: " + destination);
        print(speed);
    }
}