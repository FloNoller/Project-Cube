//using RosMessageTypes.UnityRoboticsDemo;
//using UnityEngine;
//using Unity.Robotics.ROSTCPConnector;

//public class RosService4 : MonoBehaviour
//{
//    ROSConnection ros;

//    public string serviceName = "pos_srv4";

//    public GameObject cube;

//    // Cube movement conditions
//    public float delta = 1.0f;
//    public float speed = 2.0f;
//    private Vector3 destination;
//    public Quaternion destination270;

//    float awaitingResponseUntilTimestamp = -1;

//    void Start()
//    {

//        ros = ROSConnection.GetOrCreateInstance();
//        ros.RegisterRosService<PositionService4Request, PositionService4Response>(serviceName);
//        destination = cube.transform.position;
//    }

//    private void Update()
//    {
//        // Move our position a step closer to the target.
//        float step = speed * Time.deltaTime; // calculate distance to move
//        cube.transform.position = Vector3.MoveTowards(cube.transform.position, destination, step);

//        if (Vector3.Distance(cube.transform.position, destination) < delta && Time.time > awaitingResponseUntilTimestamp)
//        {
//            Debug.Log("Destination reached.");

//            PosRotMsg cubePos = new PosRotMsg(
//                cube.transform.position.x,
//                cube.transform.position.y,
//                cube.transform.position.z,
//                cube.transform.rotation.x,
//                cube.transform.rotation.y,
//                cube.transform.rotation.z,
//                cube.transform.rotation.w
//            );

//            PositionService4Request positionService4Request = new PositionService4Request(cubePos);

//            // Send message to ROS and return the response
//            ros.SendServiceMessage<PositionService4Response>(serviceName, positionService4Request, Callback_Destination4);
//            awaitingResponseUntilTimestamp = Time.time + 1.0f; // don't send again for 1 second, or until we receive a response

//            GameObject.Find("shoulder_link").transform.localRotation = Quaternion.RotateTowards(GameObject.Find("shoulder_link").transform.localRotation,
//            destination270, step);

//        }
//    }

//    void Callback_Destination4(PositionService4Response response)
//    {
//        awaitingResponseUntilTimestamp = -1;
//        destination270.eulerAngles = new Vector3(0f, -90f, 0f);
//        Debug.Log("New Destination: " + destination);
//        print(speed);
//    }

//}