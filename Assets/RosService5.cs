//using RosMessageTypes.UnityRoboticsDemo;
//using UnityEngine;
//using Unity.Robotics.ROSTCPConnector;

//public class RosService5 : MonoBehaviour
//{
//    ROSConnection ros;

//    public string serviceName = "pos_srv5";

//    public GameObject cube;

//    // Cube movement conditions
//    public float delta = 1.0f;
//    public float speed = 200f;
//    public float growFactor =1f;
//    private Vector3 destinationDelay1;
//    private Vector3 destination;
//    //public Quaternion destination0;
//    private Quaternion destinationElbow;
//    private Quaternion destinationArm;
//    private Quaternion destinationWrist;
//    private Quaternion destinationShoulderPlus;
//    private Quaternion destinationShoulderMinus;
//    private Quaternion destinationUp;
//    private bool Move1 = false;
//    private bool Move2 = false;
//    private bool Move3 = false;
//    private bool Move4 = false;
//    private bool Move5 = false;
//    private bool Move6 = false;
//    private CubeState cubeState;
//    private ReadCube readCube;
    


//    float awaitingResponseUntilTimestamp = -1;

//    void Start()
//    {
//        cubeState = FindObjectOfType<CubeState>();
//        readCube = FindObjectOfType<ReadCube>();
//        ros = ROSConnection.GetOrCreateInstance();
//        ros.RegisterRosService<PositionService5Request, PositionService5Response>(serviceName);
//        //destination = cube.transform.position;
//        //destinationElbow.eulerAngles = new Vector3(0f, 90f, 0f);
//        //destinationArm.eulerAngles = new Vector3(-50, 0, 0);
//        //destinationWrist.eulerAngles = new Vector3(0, 0, 50);
//        Move1 = false;
//        destinationShoulderMinus.eulerAngles = new Vector3(0, 0, 0);
//        destinationDelay1 = new Vector3(0, 0, 0);
//    }

//    private void Update()
//    {
//        // Move our position a step closer to the target.
//        float step = speed * Time.deltaTime; // calculate distance to move
//                                             //cube.transform.position = Vector3.MoveTowards(cube.transform.position, destination, step);

//        readCube.ReadState();
//        MoveUp();

//        //if (Move1 == true)
//        //{
//        //    destinationElbow.eulerAngles = new Vector3(0f, -7f, 0f);
//        //    destinationArm.eulerAngles = new Vector3(7, 0, 0);
//        //    destinationWrist.eulerAngles = new Vector3(0, 0, 0);
//        //    Move1 = false;
//        //    Move2 = true;
//        //}
//        //if (Move2 == true)
//        //{
//        //    GameObject.Find("servo_head").transform.localScale = new Vector3(1, 1, 2.5f);
//        //}

//        GameObject.Find("elbow_link").transform.localRotation = Quaternion.RotateTowards(GameObject.Find("elbow_link").transform.localRotation,
//            destinationElbow, step);
//        GameObject.Find("arm_linkC").transform.localRotation = Quaternion.RotateTowards(GameObject.Find("arm_linkC").transform.localRotation,
//            destinationArm, step);
//        GameObject.Find("wrist_linkC").transform.localRotation = Quaternion.RotateTowards(GameObject.Find("wrist_linkC").transform.localRotation,
//            destinationWrist, step);
//        GameObject.Find("shoulder_linkC").transform.localRotation = Quaternion.RotateTowards(GameObject.Find("shoulder_linkC").transform.localRotation,
//            destinationShoulderPlus, step);
//        GameObject.Find("Niryo_assembly").transform.localRotation = Quaternion.RotateTowards(GameObject.Find("Niryo_assembly").transform.localRotation,
//            destinationUp, step);
//        GameObject.Find("DelayObj").transform.position = Vector3.MoveTowards(GameObject.Find("DelayObj").transform.position,
//            destinationDelay1, step);

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

//            PositionService5Request positionService5Request = new PositionService5Request(cubePos);

//            // Send message to ROS and return the response
//            ros.SendServiceMessage<PositionService5Response>(serviceName, positionService5Request, Callback_Destination5);
//            awaitingResponseUntilTimestamp = Time.time + 1.0f; // don't send again for 1 second, or until we receive a response

            

//        }
//    }

//    void Callback_Destination5(PositionService5Response response)
//    {
//        awaitingResponseUntilTimestamp = -1;
//        Debug.Log("New Destination: " + destination);
//        //Move1 = true;
//        destinationDelay1 = new Vector3(5, 5, 5);
//        destinationUp.eulerAngles = new Vector3(0, 0, 0);
//    }
//    void MoveUp()
//    {
//        if (destinationUp.eulerAngles == new Vector3(0,0,0) && GameObject.Find("DelayObj").transform.position == new Vector3(5,5,5))
//        {
//            Move1 = true;
//            destinationDelay1 = new Vector3(0, 0, 0);
//        }


//        if (Move1 == true && Move4 == false && GameObject.Find("shoulder_linkC").transform.localRotation.eulerAngles == destinationShoulderMinus.eulerAngles)
//        {
//            destinationElbow.eulerAngles = new Vector3(0f, -7f, 0f);
//            destinationArm.eulerAngles = new Vector3(7, 0, 0);
//            destinationWrist.eulerAngles = new Vector3(0, 0, 0);
//            Move1 = false;
//            Move2 = true;
//        }
//        if (Move2 == true && GameObject.Find("elbow_link").transform.localRotation.eulerAngles == destinationElbow.eulerAngles)
//        {
//            GameObject.Find("servo_head").transform.localScale = new Vector3(1f, 1f, 2.5f);
//            Move2 = false;
//            Move3 = true;
//        }
//        if (Move3 == true && GameObject.Find("servo_head").transform.localScale == new Vector3(1, 1, 2.5f))
//        {
//            destinationShoulderPlus.eulerAngles = new Vector3(0, 90, 0);
//            //RotateSide(cubeState.up, 90);
//            Move3 = false;
//            Move4 = true;
//        }
//        if (Move4 == true && GameObject.Find("shoulder_linkC").transform.localRotation.eulerAngles == destinationShoulderPlus.eulerAngles)
//        {
//            destinationElbow.eulerAngles = new Vector3(0f, 90f, 0f);
//            destinationArm.eulerAngles = new Vector3(-50, 0, 0);
//            destinationWrist.eulerAngles = new Vector3(0, 0, 50);
//            GameObject.Find("servo_head").transform.localScale = new Vector3(1, 1, 3f);
//            Move4 = false;
//            Move5 = true;
//        }
//        if (Move5 == true && GameObject.Find("elbow_link").transform.localRotation.eulerAngles == new Vector3(0f, 90f, 0f))
//        {
//            destinationShoulderPlus.eulerAngles = new Vector3(0, 0, 0);
//            Move5 = false;
//            Move6 = true;
//        }
//    }
//}