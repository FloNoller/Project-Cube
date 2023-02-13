using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random=UnityEngine.Random;
using DG.Tweening;
using Unity.Robotics.ROSTCPConnector;
using RosMessageTypes.UnityRoboticsDemo;
using RosShuffle = RosMessageTypes.UnityRoboticsDemo.UnityShuffleMsg;
using System.Runtime.InteropServices;
using RosColor = RosMessageTypes.UnityRoboticsDemo.UnityColorMsg;
using RosSolve = RosMessageTypes.UnityRoboticsDemo.UnitySolveMsg;
using RosUp = RosMessageTypes.UnityRoboticsDemo.UnityUpMsg;
using RosDown = RosMessageTypes.UnityRoboticsDemo.UnityDownMsg;
using RosRight = RosMessageTypes.UnityRoboticsDemo.UnityRightMsg;
using RosLeft = RosMessageTypes.UnityRoboticsDemo.UnityLeftMsg;
using RosBack = RosMessageTypes.UnityRoboticsDemo.UnityBackMsg;
using RosFront = RosMessageTypes.UnityRoboticsDemo.UnityFrontMsg;

public class Automate : MonoBehaviour
{
    ROSConnection ros;

    public string serviceName = "pos_srv";
    public string serviceName2 = "pos_srv2";
    public string serviceName3 = "pos_srv3";
    public string serviceName4 = "pos_srv4";
    public string serviceNameShuffle = "pos_srvs";
    public string serviceNameSolve = "pos_srvsolve";
    public string serviceName5 = "pos_srv5";
    public string serviceName6 = "pos_srv6";
    public string serviceName7 = "pos_srv7";
    public string serviceName8 = "pos_srv8";
    public string serviceName9 = "pos_srv9";
    public string serviceName10 = "pos_srv10";

    public GameObject cube;

    // Cube movement conditions
    public float delta = 1.0f;
    public float speed = 200f;
    public float growFactor = 1f;
    private Vector3 destinationDelay1;
    private Vector3 destinationDelay2;
    private Vector3 destination;
    //public Quaternion destination0;
    private Quaternion destinationElbow;
    private Quaternion destinationArm;
    private Quaternion destinationWrist;
    private Quaternion destinationShoulderPlus;
    private Quaternion destinationShoulderMinus;
    private Quaternion destinationBase;
    private bool Move1 = false;
    private bool Move2 = false;
    private bool Move3 = false;
    private bool Move4 = false;
    private bool Move4S = false;
    private bool Move5 = false;




    //PartMovesUp
    private bool Move1Up90 = false;
    private bool Move2Up90 = false;
    private bool Move3Up90 = false;


    private bool Move1Up180 = false;
    private bool Move2Up180 = false;
    private bool Move3Up180 = false;


    private bool Move1UpN90 = false;
    private bool Move2UpN90 = false;
    private bool Move3UpN90 = false;


    //PartMovesDown
    private bool Move1Down90 = false;
    private bool Move2Down90 = false;
    private bool Move3Down90 = false;


    private bool Move1Down180 = false;
    private bool Move2Down180 = false;
    private bool Move3Down180 = false;


    private bool Move1DownN90 = false;
    private bool Move2DownN90 = false;
    private bool Move3DownN90 = false;


    //PartMovesRight
    private bool Move1Right90 = false;
    private bool Move2Right90 = false;
    private bool Move3Right90 = false;


    private bool Move1Right180 = false;
    private bool Move2Right180 = false;
    private bool Move3Right180 = false;


    private bool Move1RightN90 = false;
    private bool Move2RightN90 = false;
    private bool Move3RightN90 = false;


    //PartMovesLeft
    private bool Move1Left90 = false;
    private bool Move2Left90 = false;
    private bool Move3Left90 = false;


    private bool Move1Left180 = false;
    private bool Move2Left180 = false;
    private bool Move3Left180 = false;


    private bool Move1LeftN90 = false;
    private bool Move2LeftN90 = false;
    private bool Move3LeftN90 = false;


    //PartMovesFront
    private bool Move1Front90 = false;
    private bool Move2Front90 = false;
    private bool Move3Front90 = false;


    private bool Move1Front180 = false;
    private bool Move2Front180 = false;
    private bool Move3Front180 = false;


    private bool Move1FrontN90 = false;
    private bool Move2FrontN90 = false;
    private bool Move3FrontN90 = false;


    //PartMovesBack
    private bool Move1Back90 = false;
    private bool Move2Back90 = false;
    private bool Move3Back90 = false;


    private bool Move1Back180 = false;
    private bool Move2Back180 = false;
    private bool Move3Back180 = false;


    private bool Move1BackN90 = false;
    private bool Move2BackN90 = false;
    private bool Move3BackN90 = false;


    private bool ResetPos = true;
    private bool ResetPosRos = false;
    private bool ShuffleActive = false;
    float awaitingResponseUntilTimestamp = -1;
    private Quaternion destinationBaseUp;
    private Quaternion destinationBaseDown;
    private Quaternion destinationBaseRight;
    private Quaternion destinationBaseLeft;
    private Quaternion destinationBaseFront;
    private Quaternion destinationBaseBack;
    public static List<string> moveList = new List<string>() { };
    private readonly List<string> allMoves = new List<string>()
    {
        "U","D","L","R","F","B"


        ,"U'","U2"
        ,"D'","L'","R'","F'","B'",
        "D2","L2","R2","F2","B2"
    };
    private GameObject Greifer;
    private CubeState cubeState;
    private ReadCube readCube;
    private SolveTwoPhase solver;
    float time;
    float timeDelay;
    
    
    public Vector3 originU;
    public Vector3 originD;
    public Vector3 originR;
    public Vector3 originL;
    public Vector3 originF;
    public Vector3 originB;
    public Vector3 Pivot;


    // Start is called before the first frame update
    void Start()
    {
        originU = new Vector3(0f, 4.8f, 0f);
        originD = new Vector3(0f, -4.8f, 0f);
        originR = new Vector3(0f, 0f, -4.8f);
        originL = new Vector3(0f, 0f, 4.8f);
        originF = new Vector3(-4.8f, 0f, 0f);
        originB = new Vector3(4.8f, 0f, 0f);
        Pivot = new Vector3(0f, 0f, 0f);
        cubeState = FindObjectOfType<CubeState>();
        readCube = FindObjectOfType<ReadCube>();
        solver = FindObjectOfType<SolveTwoPhase>();

        //GameObject.Find("Catcher").transform.position = new Vector3(2, 3, 4);
        //time = 0f;
        //timeDelay = 3f;
        ResetPosRos = true;
        solver.Solver();
        //StartCoroutine(LateStart(5f));

        ros = ROSConnection.GetOrCreateInstance();
        ROSConnection.GetOrCreateInstance().Subscribe<RosSolve>("solve", SolveChange);
        ROSConnection.GetOrCreateInstance().Subscribe<RosColor>("color", ColorChange);
        ROSConnection.GetOrCreateInstance().Subscribe<RosShuffle>("shuffle", ShuffleChange);

        ROSConnection.GetOrCreateInstance().Subscribe<RosUp>("up", UpChange);
        ROSConnection.GetOrCreateInstance().Subscribe<RosDown>("down", DownChange);
        ROSConnection.GetOrCreateInstance().Subscribe<RosFront>("front", FrontChange);
        ROSConnection.GetOrCreateInstance().Subscribe<RosLeft>("left", LeftChange);
        ROSConnection.GetOrCreateInstance().Subscribe<RosBack>("back", BackChange);
        ROSConnection.GetOrCreateInstance().Subscribe<RosRight>("right", RightChange);

        ros.RegisterRosService<ShuffleRequest, ShuffleResponse>(serviceNameShuffle);

        ros.RegisterRosService<SolveRequest, SolveResponse>(serviceNameSolve);

        ros.RegisterRosService<PositionServiceRequest, PositionServiceResponse>(serviceName);
        ros.RegisterRosService<PositionService2Request, PositionService2Response>(serviceName2);
        ros.RegisterRosService<PositionService3Request, PositionService3Response>(serviceName3);
        ros.RegisterRosService<PositionService4Request, PositionService4Response>(serviceName4);
        ros.RegisterRosService<PositionService5Request, PositionService5Response>(serviceName5);
        ros.RegisterRosService<PositionService6Request, PositionService6Response>(serviceName6);
        ros.RegisterRosService<PositionService7Request, PositionService7Response>(serviceName7);
        ros.RegisterRosService<PositionService8Request, PositionService8Response>(serviceName8);
        ros.RegisterRosService<PositionService9Request, PositionService9Response>(serviceName9);
        ros.RegisterRosService<PositionService10Request, PositionService10Response>(serviceName10);
        //destination = cube.transform.position;
        //destinationElbow.eulerAngles = new Vector3(0f, 90f, 0f);
        //destinationArm.eulerAngles = new Vector3(-50, 0, 0);
        //destinationWrist.eulerAngles = new Vector3(0, 0, 50);
    
        destinationShoulderMinus.eulerAngles = new Vector3(0, 0, 0);


        destinationBaseUp.eulerAngles = new Vector3(0, 0, 0);
        destinationBaseDown.eulerAngles = new Vector3(180, 0, 0);
        destinationBaseRight.eulerAngles = new Vector3(270, 0, 0);
        destinationBaseLeft.eulerAngles = new Vector3(90, 0, 0);
        destinationBaseFront.eulerAngles = new Vector3(0, 0, 90);
        destinationBaseBack.eulerAngles = new Vector3(0, 0, 270);
        float step = speed * Time.deltaTime;

    }
    IEnumerator LateStart(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        ResetPosRos = true;
    }





    // Update is called once per frame
    void Update()
    {

        //print(Move1);
        //print(Move2);
        //print(ResetPosRos);

        //time = time + 1f * Time.deltaTime;
        float step = speed * Time.deltaTime;
        
        //print(timeDelay);
        //only call the list when finished rotating
        if (moveList.Count > 0 && !CubeState.autoRotating && CubeState.started && time >= timeDelay)//&& GreGameObject.Find("Niryo_assembly").transform.localRotation.eulerAngles == new Vector3(0, 0, 0)printiferMoving=false;
        {
            DoMove(moveList[0]);
            


        }

        //GameObject.Find("Catcher").transform.LookAt(Pivot);

        if (ShuffleActive == true && ResetPosRos == true)
        {
            Shuffle();
            
        }
        GameObject.Find("elbow_link").transform.localRotation = Quaternion.RotateTowards(GameObject.Find("elbow_link").transform.localRotation,
            destinationElbow, step);
        GameObject.Find("arm_linkC").transform.localRotation = Quaternion.RotateTowards(GameObject.Find("arm_linkC").transform.localRotation,
            destinationArm, step);
        GameObject.Find("wrist_linkC").transform.localRotation = Quaternion.RotateTowards(GameObject.Find("wrist_linkC").transform.localRotation,
            destinationWrist, step);
        GameObject.Find("shoulder_linkC").transform.localRotation = Quaternion.RotateTowards(GameObject.Find("shoulder_linkC").transform.localRotation,
            destinationShoulderPlus, step);
        GameObject.Find("elbow_link2").transform.localRotation = Quaternion.RotateTowards(GameObject.Find("elbow_link").transform.localRotation,
            destinationElbow, step);
        GameObject.Find("arm_linkC2").transform.localRotation = Quaternion.RotateTowards(GameObject.Find("arm_linkC").transform.localRotation,
            destinationArm, step);
        GameObject.Find("wrist_linkC2").transform.localRotation = Quaternion.RotateTowards(GameObject.Find("wrist_linkC").transform.localRotation,
            destinationWrist, step);
        
        GameObject.Find("Niryo_assembly").transform.localRotation = Quaternion.RotateTowards(GameObject.Find("Niryo_assembly").transform.localRotation,
            destinationBase, step);
        GameObject.Find("DelayObj").transform.position = Vector3.MoveTowards(GameObject.Find("DelayObj").transform.position,
            destinationDelay1, step);
        GameObject.Find("DelayObj2").transform.position = Vector3.MoveTowards(GameObject.Find("DelayObj2").transform.position,
            destinationDelay2, step);
        if (Vector3.Distance(cube.transform.position, destination) < delta && Time.time > awaitingResponseUntilTimestamp)
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
            // Send message to ROS and return the response
            ShuffleRequest ShuffleRequest = new ShuffleRequest(cubePos);
            ros.SendServiceMessage<ShuffleResponse>(serviceNameShuffle, ShuffleRequest, Callback_DestinationShuffle);

            SolveRequest SolveRequest = new SolveRequest(cubePos);
            ros.SendServiceMessage<SolveResponse>(serviceNameSolve, SolveRequest, Callback_DestinationSolve);

            PositionServiceRequest positionServiceRequest = new PositionServiceRequest(cubePos);
            PositionService4Request positionService4Request = new PositionService4Request(cubePos);

            PositionService2Request positionService2Request = new PositionService2Request(cubePos);
            PositionService3Request positionService3Request = new PositionService3Request(cubePos);

            ros.SendServiceMessage<PositionService2Response>(serviceName2, positionService2Request, Callback_Destination2);
            ros.SendServiceMessage<PositionService3Response>(serviceName3, positionService3Request, Callback_Destination3);

            ros.SendServiceMessage<PositionServiceResponse>(serviceName, positionServiceRequest, Callback_Destination);
            ros.SendServiceMessage<PositionService4Response>(serviceName4, positionService4Request, Callback_Destination4);

            PositionService5Request positionService5Request = new PositionService5Request(cubePos);
            ros.SendServiceMessage<PositionService5Response>(serviceName5, positionService5Request, Callback_Destination5);

            PositionService6Request positionService6Request = new PositionService6Request(cubePos);
            ros.SendServiceMessage<PositionService6Response>(serviceName6, positionService6Request, Callback_Destination6);

            PositionService7Request positionService7Request = new PositionService7Request(cubePos);
            ros.SendServiceMessage<PositionService7Response>(serviceName7, positionService7Request, Callback_Destination7);

            PositionService8Request positionService8Request = new PositionService8Request(cubePos);
            ros.SendServiceMessage<PositionService8Response>(serviceName8, positionService8Request, Callback_Destination8);

            PositionService9Request positionService9Request = new PositionService9Request(cubePos);
            ros.SendServiceMessage<PositionService9Response>(serviceName9, positionService9Request, Callback_Destination9);

            PositionService10Request positionService10Request = new PositionService10Request(cubePos);
            ros.SendServiceMessage<PositionService10Response>(serviceName10, positionService10Request, Callback_Destination10);
            awaitingResponseUntilTimestamp = Time.time + 1.0f; // don't send again for 1 second, or until we receive a response
        }

        MoveUp();

        if (GameObject.Find("shoulder_linkC").transform.localRotation.eulerAngles == destinationShoulderPlus.eulerAngles && Move4 == true)
        {
            Reset();
        }

        if (GameObject.Find("servo_head").transform.localScale == new Vector3(1, 1, 3) &&
            GameObject.Find("shoulder_linkC").transform.localRotation == Quaternion.Euler(0, 0, 0) &&
            GameObject.Find("wrist_linkC").transform.localRotation == Quaternion.Euler(0, 0, -10) &&
            GameObject.Find("arm_linkC").transform.localRotation == Quaternion.Euler(-60, 0, 0) &&
            GameObject.Find("elbow_link").transform.localRotation == Quaternion.Euler(0, 60, 0) &&
            GameObject.Find("Niryo_assembly").transform.localRotation.eulerAngles == destinationBase.eulerAngles)
        {
            ResetPos = true;
            
        }
        //print(ResetPos);
    }

    void Reset()
    {
        destinationElbow.eulerAngles = new Vector3(0f, 60f, 0f);
        destinationArm.eulerAngles = new Vector3(-60, 0, 0);
        destinationWrist.eulerAngles = new Vector3(0, 0, -10);
        GameObject.Find("servo_head").transform.localScale = new Vector3(1, 1, 3f);
        destinationShoulderPlus.eulerAngles = new Vector3(0, 0, 0);
        destinationDelay2 = new Vector3(0, 0, 0);

        Move1 = false;
        Move2 = false;
        Move3 = false;
        Move4 = false;
        Move4S = false;
        
        //PartMovesUp
        Move1Up90 = false;
        Move2Up90 = false;
        Move3Up90 = false;


        Move1Up180 = false;
        Move2Up180 = false;
        Move3Up180 = false;


        Move1UpN90 = false;
        Move2UpN90 = false;
        Move3UpN90 = false;


    //PartMovesDown
        Move1Down90 = false;
        Move2Down90 = false;
        Move3Down90 = false;


        Move1Down180 = false;
        Move2Down180 = false;
        Move3Down180 = false;


        Move1DownN90 = false;
        Move2DownN90 = false;
        Move3DownN90 = false;


    //PartMovesRight
        Move1Right90 = false;
        Move2Right90 = false;
        Move3Right90 = false;


        Move1Right180 = false;
        Move2Right180 = false;
        Move3Right180 = false;


         Move1RightN90 = false;
         Move2RightN90 = false;
         Move3RightN90 = false;


    //PartMovesLeft
         Move1Left90 = false;
         Move2Left90 = false;
         Move3Left90 = false;


         Move1Left180 = false;
         Move2Left180 = false;
         Move3Left180 = false;


         Move1LeftN90 = false;
         Move2LeftN90 = false;
         Move3LeftN90 = false;


    //PartMovesFront
         Move1Front90 = false;
         Move2Front90 = false;
         Move3Front90 = false;


         Move1Front180 = false;
         Move2Front180 = false;
         Move3Front180 = false;


        Move1FrontN90 = false;
        Move2FrontN90 = false;
        Move3FrontN90 = false;


    //PartMovesBack
         Move1Back90 = false;
         Move2Back90 = false;
         Move3Back90 = false;


         Move1Back180 = false;
         Move2Back180 = false;
         Move3Back180 = false;

         Move1BackN90 = false;
         Move2BackN90 = false;
         Move3BackN90 = false;
    }



    void MoveUp()
    {
        if (GameObject.Find("Niryo_assembly").transform.localRotation.eulerAngles == new Vector3(0, 0, 0) && GameObject.Find("DelayObj").transform.position == new Vector3(5, 5, 5))
        {
            Move1 = true;
            
        }
        if (GameObject.Find("Niryo_assembly").transform.localRotation == Quaternion.Euler(180f, 0f, 0f) && GameObject.Find("DelayObj").transform.position == new Vector3(5, 5, 5))
        {
            Move1 = true;
            
        }
        if (GameObject.Find("Niryo_assembly").transform.localRotation.eulerAngles == new Vector3(270, 0, 0) && GameObject.Find("DelayObj").transform.position == new Vector3(5, 5, 5))
        {
            Move1 = true;
            
        }
        if (GameObject.Find("Niryo_assembly").transform.localRotation.eulerAngles == new Vector3(90, 0, 0) && GameObject.Find("DelayObj").transform.position == new Vector3(5, 5, 5))
        {
            Move1 = true;
          
        }
        if (GameObject.Find("Niryo_assembly").transform.localRotation.eulerAngles == new Vector3(0, 0, 90) && GameObject.Find("DelayObj").transform.position == new Vector3(5, 5, 5))
        {
            Move1 = true;
          
        }
        if (GameObject.Find("Niryo_assembly").transform.localRotation.eulerAngles == new Vector3(0, 0, 270) && GameObject.Find("DelayObj").transform.position == new Vector3(5, 5, 5))
        {
            Move1 = true;
            
        }


        if (Move1 == true && GameObject.Find("Niryo_assembly").transform.localRotation.eulerAngles == destinationBase.eulerAngles &&
            GameObject.Find("shoulder_linkC").transform.localRotation.eulerAngles  == new Vector3(0f, 0f, 0f))
        {
            destinationElbow.eulerAngles = new Vector3(0f, -7f, 0f);
            destinationArm.eulerAngles = new Vector3(7, 0, 0);
            destinationWrist.eulerAngles = new Vector3(0, 0, 0);
           
            destinationDelay1 = new Vector3(0, 0, 0);
            Move1 = false;
            Move2 = true;
        }
        if (Move2 == true && Move3 == false && Move4S == false && Move5 == false && GameObject.Find("elbow_link").transform.localRotation.eulerAngles == destinationElbow.eulerAngles)
        {
            GameObject.Find("servo_head").transform.localScale = new Vector3(1f, 1f, 2.5f);
            Move2 = false;
            Move3 = true;
        }

        //Side Up
        if (Move3 == true  && Move2 == false && GameObject.Find("servo_head").transform.localScale == new Vector3(1, 1, 2.5f) &&
            GameObject.Find("Niryo_assembly").transform.localRotation.eulerAngles == new Vector3(0, 0, 0))
        {
            Move3 = false;
            Move4S = true;
            destinationShoulderPlus.eulerAngles = new Vector3(0, 90, 0);
            RotateSide(cubeState.up, 90);
        }

        //Side Down
        if (Move3 == true && Move2 == false && Move4S == false && Move5 == false && GameObject.Find("servo_head").transform.localScale == new Vector3(1, 1, 2.5f) &&
            GameObject.Find("Niryo_assembly").transform.localRotation == Quaternion.Euler(180f, 0f, 0f))
        {
            Move3 = false;
            Move4S = true;
            destinationShoulderPlus.eulerAngles = new Vector3(0, 90, 0);
            RotateSide(cubeState.down, 90);
        }

        //Side Right
        if (Move3 == true && Move2 == false && Move4S == false && Move5 == false && GameObject.Find("servo_head").transform.localScale == new Vector3(1, 1, 2.5f) &&
            GameObject.Find("Niryo_assembly").transform.localRotation.eulerAngles == new Vector3(270, 0, 0))
        {
            Move3 = false;
            Move4S = true;
            destinationShoulderPlus.eulerAngles = new Vector3(0, 90, 0);
            RotateSide(cubeState.right, 90);
        }

        //Side Left
        if (Move3 == true && Move2 == false && Move4S == false && Move5 == false && GameObject.Find("servo_head").transform.localScale == new Vector3(1, 1, 2.5f) &&
            GameObject.Find("Niryo_assembly").transform.localRotation.eulerAngles == new Vector3(90, 0, 0))
        {
            Move3 = false;
            Move4S = true;
            destinationShoulderPlus.eulerAngles = new Vector3(0, 90, 0);
            RotateSide(cubeState.left, 90);
        }

        //Side Front
        if (Move3 == true && Move2 == false && Move4S == false && Move5 == false && GameObject.Find("servo_head").transform.localScale == new Vector3(1, 1, 2.5f) &&
            GameObject.Find("Niryo_assembly").transform.localRotation.eulerAngles == new Vector3(0, 0, 90))
        {
            Move3 = false;
            Move4S = true;
            destinationShoulderPlus.eulerAngles = new Vector3(0, 90, 0);
            RotateSide(cubeState.front, 90);
        }

        //Side Back
        if (Move3 == true && Move2 == false && Move4S == false && Move5 == false && GameObject.Find("servo_head").transform.localScale == new Vector3(1, 1, 2.5f) &&
            GameObject.Find("Niryo_assembly").transform.localRotation.eulerAngles == new Vector3(0, 0, 270))
        {
            Move3 = false;
            Move4S = true;
            destinationShoulderPlus.eulerAngles = new Vector3(0, 90, 0);
            RotateSide(cubeState.back, 90);
        }


        //Reset
        if (Move4S == true && Move3 == false && Move5 == false && GameObject.Find("shoulder_linkC").transform.localRotation.eulerAngles == destinationShoulderPlus.eulerAngles)
        {
            destinationElbow.eulerAngles = new Vector3(0f, 90f, 0f);
            destinationArm.eulerAngles = new Vector3(-50, 0, 0);
            destinationWrist.eulerAngles = new Vector3(0, 0, 50);
            destinationDelay1 = new Vector3(0, 0, 0);
            GameObject.Find("servo_head").transform.localScale = new Vector3(1, 1, 3f);
            Move4S = false;
            Move5 = true;
        }
        if (Move5 == true && Move2 == false && Move3 == false && Move4S == false && GameObject.Find("elbow_link").transform.localRotation.eulerAngles == new Vector3(0f, 90f, 0f))
        {
            destinationShoulderPlus.eulerAngles = new Vector3(0, 0, 0);
            destinationDelay1 = new Vector3(0, 0, 0);
            Move5 = false;
        }
    }






    /// <summary>
    /// //////////////////////Halbautomatisch/////////////////////
    /// </summary>
    void MoveUp90()
    {
        if (GameObject.Find("Niryo_assembly").transform.localRotation.eulerAngles == new Vector3(0, 0, 0) && ResetPos == true)
        {
            ResetPos = true;
            Move1Up90 = true;
            destinationBase.eulerAngles = new Vector3(0, 0, 0);
            destinationDelay2 = new Vector3(0, 0, 0);
        }
        if (Move1Up90 == true && GameObject.Find("Niryo_assembly").transform.localRotation.eulerAngles == new Vector3(0, 0, 0))
        {
            destinationElbow.eulerAngles = new Vector3(0f, -7f, 0f);
            destinationArm.eulerAngles = new Vector3(7, 0, 0);
            destinationWrist.eulerAngles = new Vector3(0, 0, 0);
            destinationDelay2 = new Vector3(5, 0, 0);
            Move1Up90 = false;
            Move2Up90 = true;
           
        }
        if (Move2Up90 == true && Move3Up90 == false && GameObject.Find("elbow_link").transform.localRotation.eulerAngles == destinationElbow.eulerAngles)
        {
            GameObject.Find("servo_head").transform.localScale = new Vector3(1f, 1f, 2.5f);
            Move2Up90 = false;
            Move3Up90 = true;
            ResetPos = false;
            ResetPosRos = false;
        }
        //Side Up
        if (Move3Up90 == true && GameObject.Find("servo_head").transform.localScale == new Vector3(1, 1, 2.5f) &&
            GameObject.Find("Niryo_assembly").transform.localRotation.eulerAngles == new Vector3(0, 0, 0) && ResetPos == false)
        {
            Move1Up90 = false;
            Move2Up90 = false;
            Move3Up90 = false;
            destinationShoulderPlus.eulerAngles = new Vector3(0, -90, 0);
            RotateSide(cubeState.up, -90);
            Move4 = true;
        }
    }
    void MoveUpN90()
    {
        if (GameObject.Find("Niryo_assembly").transform.localRotation.eulerAngles == new Vector3(0, 0, 0) && ResetPos == true)
        {
            ResetPos = true;
            Move1UpN90 = true;
            destinationBase.eulerAngles = new Vector3(0, 0, 0);
            destinationDelay2 = new Vector3(0, 0, 0);
        }
        if (Move1UpN90 == true && GameObject.Find("Niryo_assembly").transform.localRotation.eulerAngles == new Vector3(0, 0, 0))
        {
            destinationElbow.eulerAngles = new Vector3(0f, -7f, 0f);
            destinationArm.eulerAngles = new Vector3(7, 0, 0);
            destinationWrist.eulerAngles = new Vector3(0, 0, 0);
            destinationDelay2 = new Vector3(5, 0, 0);
            Move1UpN90 = false;
            Move2UpN90 = true;

        }
        if (Move2UpN90 == true && Move3UpN90 == false && GameObject.Find("elbow_link").transform.localRotation.eulerAngles == destinationElbow.eulerAngles)
        {
            GameObject.Find("servo_head").transform.localScale = new Vector3(1f, 1f, 2.5f);
            Move2UpN90 = false;
            Move3UpN90 = true;
            ResetPos = false;
            ResetPosRos = false;
        }
        //Side Up
        if (Move3UpN90 == true && GameObject.Find("servo_head").transform.localScale == new Vector3(1, 1, 2.5f) &&
            GameObject.Find("Niryo_assembly").transform.localRotation.eulerAngles == new Vector3(0, 0, 0) && ResetPos == false)
        {
            Move1UpN90 = false;
            Move2UpN90 = false;
            Move3UpN90 = false;
            destinationShoulderPlus.eulerAngles = new Vector3(0, 90, 0);
            RotateSide(cubeState.up, 90);
            Move4 = true;
        }
    }
    void MoveUp180()
    {
        if (GameObject.Find("Niryo_assembly").transform.localRotation.eulerAngles == new Vector3(0, 0, 0) && ResetPos == true)
        {
            ResetPos = true;
            Move1Up180 = true;
            destinationBase.eulerAngles = new Vector3(0, 0, 0);
            destinationDelay2 = new Vector3(0, 0, 0);
        }
        if (Move1Up180 == true && GameObject.Find("Niryo_assembly").transform.localRotation.eulerAngles == new Vector3(0, 0, 0))
        {
            destinationElbow.eulerAngles = new Vector3(0f, -7f, 0f);
            destinationArm.eulerAngles = new Vector3(7, 0, 0);
            destinationWrist.eulerAngles = new Vector3(0, 0, 0);
            destinationDelay2 = new Vector3(5, 0, 0);
            Move1Up180 = false;
            Move2Up180 = true;

        }
        if (Move2Up180 == true && Move3Up180 == false && GameObject.Find("elbow_link").transform.localRotation.eulerAngles == destinationElbow.eulerAngles)
        {
            GameObject.Find("servo_head").transform.localScale = new Vector3(1f, 1f, 2.5f);
            Move2Up180 = false;
            Move3Up180 = true;
            ResetPos = false;
            ResetPosRos = false;
        }
        //Side Up
        if (Move3Up180 == true && GameObject.Find("servo_head").transform.localScale == new Vector3(1, 1, 2.5f) &&
            GameObject.Find("Niryo_assembly").transform.localRotation.eulerAngles == new Vector3(0, 0, 0) && ResetPos == false)
        {
            Move1Up180 = false;
            Move2Up180 = false;
            Move3Up180 = false;
            destinationShoulderPlus.eulerAngles = new Vector3(0, 180, 0);
            RotateSide(cubeState.up, 180);
            Move4 = true;
        }
    }
    void MoveDown90()
    {
        if (GameObject.Find("Niryo_assembly").transform.localRotation == Quaternion.Euler(180, 0, 0) && ResetPos == true)
        {
            ResetPos = true;
            Move1Down90 = true;
            destinationDelay2 = new Vector3(0, 0, 0);
            destinationBase.eulerAngles = new Vector3(180, 0, 0);
        }
        if (Move1Down90 == true && GameObject.Find("Niryo_assembly").transform.localRotation == Quaternion.Euler(180, 0, 0))
        {
            destinationElbow.eulerAngles = new Vector3(0f, -7f, 0f);
            destinationArm.eulerAngles = new Vector3(7, 0, 0);
            destinationWrist.eulerAngles = new Vector3(0, 0, 0);
            destinationDelay2 = new Vector3(5, 0, 0);
            Move1Down90 = false;
            Move2Down90 = true;
        }
        if (Move2Down90 == true && Move3Down90 == false && GameObject.Find("elbow_link").transform.localRotation.eulerAngles == destinationElbow.eulerAngles)
        {
            GameObject.Find("servo_head").transform.localScale = new Vector3(1f, 1f, 2.5f);
            Move2Down90 = false;
            Move3Down90 = true;
        }
        if (Move3Down90 == true && GameObject.Find("servo_head").transform.localScale == new Vector3(1, 1, 2.5f) &&
            GameObject.Find("Niryo_assembly").transform.localRotation == Quaternion.Euler(180, 0, 0))
        {
            Move1Down90 = false;
            Move2Down90 = false;
            Move3Down90 = false;
            destinationShoulderPlus.eulerAngles = new Vector3(0, 180, 0);
            RotateSide(cubeState.down, 180);
            ResetPos = false;
            ResetPosRos = false;
            Move4 = true;
        }
    }
    void MoveDownN90()
    {
        if (GameObject.Find("Niryo_assembly").transform.localRotation == Quaternion.Euler(180, 0, 0) && ResetPos == true)
        {
            ResetPos = true;
            Move1DownN90 = true;
            destinationDelay2 = new Vector3(0, 0, 0);
            destinationBase.eulerAngles = new Vector3(180, 0, 0);
        }
        if (Move1DownN90 == true && GameObject.Find("Niryo_assembly").transform.localRotation == Quaternion.Euler(180, 0, 0))
        {
            destinationElbow.eulerAngles = new Vector3(0f, -7f, 0f);
            destinationArm.eulerAngles = new Vector3(7, 0, 0);
            destinationWrist.eulerAngles = new Vector3(0, 0, 0);
            destinationDelay2 = new Vector3(5, 0, 0);
            Move1DownN90 = false;
            Move2DownN90 = true;
        }
        if (Move2DownN90 == true && Move3DownN90 == false && GameObject.Find("elbow_link").transform.localRotation.eulerAngles == destinationElbow.eulerAngles)
        {
            GameObject.Find("servo_head").transform.localScale = new Vector3(1f, 1f, 2.5f);
            Move2DownN90 = false;
            Move3DownN90 = true;
        }
        if (Move3DownN90 == true && GameObject.Find("servo_head").transform.localScale == new Vector3(1, 1, 2.5f) &&
            GameObject.Find("Niryo_assembly").transform.localRotation == Quaternion.Euler(180, 0, 0))
        {
            Move1DownN90 = false;
            Move2DownN90 = false;
            Move3DownN90 = false;
            destinationShoulderPlus.eulerAngles = new Vector3(0, 90, 0);
            RotateSide(cubeState.down, 90);
            ResetPosRos = false;
            ResetPos = false;
            Move4 = true;
        }
    }
    void MoveDown180()
    {
        if (GameObject.Find("Niryo_assembly").transform.localRotation == Quaternion.Euler(180, 0, 0) && ResetPos == true)
        {
            ResetPos = true;
            Move1Down180 = true;
            destinationDelay2 = new Vector3(0, 0, 0);
            destinationBase.eulerAngles = new Vector3(180, 0, 0);
        }
        if (Move1Down180 == true && GameObject.Find("Niryo_assembly").transform.localRotation == Quaternion.Euler(180, 0, 0))
        {
            destinationElbow.eulerAngles = new Vector3(0f, -7f, 0f);
            destinationArm.eulerAngles = new Vector3(7, 0, 0);
            destinationWrist.eulerAngles = new Vector3(0, 0, 0);
            destinationDelay2 = new Vector3(5, 0, 0);
            Move1Down180 = false;
            Move2Down180 = true;
        }
        if (Move2Down180 == true && Move3Down180 == false && GameObject.Find("elbow_link").transform.localRotation.eulerAngles == destinationElbow.eulerAngles)
        {
            GameObject.Find("servo_head").transform.localScale = new Vector3(1f, 1f, 2.5f);
            Move2Down180 = false;
            Move3Down180 = true;
        }
        if (Move3Down180 == true && GameObject.Find("servo_head").transform.localScale == new Vector3(1, 1, 2.5f) &&
            GameObject.Find("Niryo_assembly").transform.localRotation == Quaternion.Euler(180, 0, 0))
        {
            Move1Down180 = false;
            Move2Down180 = false;
            Move3Down180 = false;
            destinationShoulderPlus.eulerAngles = new Vector3(0, 180, 0);
            RotateSide(cubeState.down, 180);
            ResetPos = false;
            Move4 = true;
        }
    }

    void MoveRight90()
    {
        if (GameObject.Find("Niryo_assembly").transform.localRotation == Quaternion.Euler(270, 0,0 ) && ResetPos == true)
        {
            ResetPos = true;
            Move1Right90 = true;
            destinationDelay2 = new Vector3(0, 0, 0);
            destinationBase.eulerAngles = new Vector3(270, 0, 0);
        }
        if (Move1Right90 == true && GameObject.Find("Niryo_assembly").transform.localRotation == Quaternion.Euler(270, 0, 0))
        {
            destinationElbow.eulerAngles = new Vector3(0f, -7f, 0f);
            destinationArm.eulerAngles = new Vector3(7, 0, 0);
            destinationWrist.eulerAngles = new Vector3(0, 0, 0);
            destinationDelay2 = new Vector3(5, 0, 0);
            Move1Right90 = false;
            Move2Right90 = true;
        }
        if (Move2Right90 == true && Move3Right90 == false && GameObject.Find("elbow_link").transform.localRotation.eulerAngles == destinationElbow.eulerAngles)
        {
            GameObject.Find("servo_head").transform.localScale = new Vector3(1f, 1f, 2.5f);
            Move2Right90 = false;
            Move3Right90 = true;
        }
        if (Move3Right90 == true && GameObject.Find("servo_head").transform.localScale == new Vector3(1, 1, 2.5f) &&
            GameObject.Find("Niryo_assembly").transform.localRotation == Quaternion.Euler(270, 0, 0))
        {
            Move1Right90 = false;
            Move2Right90 = false;
            Move3Right90 = false;
            destinationShoulderPlus.eulerAngles = new Vector3(0, -90, 0);
            RotateSide(cubeState.right, -90);
            ResetPos = false;
            ResetPosRos = false;
            Move4 = true;
        }
    }
    void MoveRightN90()
    {
        if (GameObject.Find("Niryo_assembly").transform.localRotation == Quaternion.Euler(270, 0, 0) && ResetPos == true)
        {
            ResetPos = true;
            Move1RightN90 = true;
            destinationDelay2 = new Vector3(0, 0, 0);
            destinationBase.eulerAngles = new Vector3(270, 0, 0);
        }
        if (Move1RightN90 == true && GameObject.Find("Niryo_assembly").transform.localRotation == Quaternion.Euler(270, 0, 0))
        {
            destinationElbow.eulerAngles = new Vector3(0f, -7f, 0f);
            destinationArm.eulerAngles = new Vector3(7, 0, 0);
            destinationWrist.eulerAngles = new Vector3(0, 0, 0);
            destinationDelay2 = new Vector3(5, 0, 0);
            Move1RightN90 = false;
            Move2RightN90 = true;
        }
        if (Move2RightN90 == true && Move3RightN90 == false && GameObject.Find("elbow_link").transform.localRotation.eulerAngles == destinationElbow.eulerAngles)
        {
            GameObject.Find("servo_head").transform.localScale = new Vector3(1f, 1f, 2.5f);
            Move2RightN90 = false;
            Move3RightN90 = true;
        }
        if (Move3RightN90 == true && GameObject.Find("servo_head").transform.localScale == new Vector3(1, 1, 2.5f) &&
            GameObject.Find("Niryo_assembly").transform.localRotation == Quaternion.Euler(270, 0, 0))
        {
            Move1RightN90 = false;
            Move2RightN90 = false;
            Move3RightN90 = false;
            destinationShoulderPlus.eulerAngles = new Vector3(0, 90, 0);
            RotateSide(cubeState.right, 90);
            ResetPos = false;
            Move4 = true;
        }
    }
    void MoveRight180()
    {
        if (GameObject.Find("Niryo_assembly").transform.localRotation == Quaternion.Euler(270, 0, 0) && ResetPos == true)
        {
            ResetPos = true;
            Move1Right180 = true;
            destinationDelay2 = new Vector3(0, 0, 0);
            destinationBase.eulerAngles = new Vector3(270, 0, 0);
        }
        if (Move1Right180 == true && GameObject.Find("Niryo_assembly").transform.localRotation == Quaternion.Euler(270, 0, 0))
        {
            destinationElbow.eulerAngles = new Vector3(0f, -7f, 0f);
            destinationArm.eulerAngles = new Vector3(7, 0, 0);
            destinationWrist.eulerAngles = new Vector3(0, 0, 0);
            destinationDelay2 = new Vector3(5, 0, 0);
            Move1Right180 = false;
            Move2Right180 = true;
        }
        if (Move2Right180 == true && Move3Right180 == false && GameObject.Find("elbow_link").transform.localRotation.eulerAngles == destinationElbow.eulerAngles)
        {
            GameObject.Find("servo_head").transform.localScale = new Vector3(1f, 1f, 2.5f);
            Move2Right180 = false;
            Move3Right180 = true;
        }
        if (Move3Right180 == true && GameObject.Find("servo_head").transform.localScale == new Vector3(1, 1, 2.5f) &&
            GameObject.Find("Niryo_assembly").transform.localRotation == Quaternion.Euler(270, 0, 0))
        {
            Move1Right180 = false;
            Move2Right180 = false;
            Move3Right180 = false;
            destinationShoulderPlus.eulerAngles = new Vector3(0, 180, 0);
            RotateSide(cubeState.right, 180);
            ResetPosRos = false;
            ResetPos = false;
            Move4 = true;
        }
    }


    void MoveLeft90()
    {
        if (GameObject.Find("Niryo_assembly").transform.localRotation == Quaternion.Euler(90, 0, 0) && ResetPos == true)
        {
            ResetPos = true;
            Move1Left90 = true;
            destinationDelay2 = new Vector3(0, 0, 0);
            destinationBase.eulerAngles = new Vector3(90, 0, 0);
        }
        if (Move1Left90 == true && GameObject.Find("Niryo_assembly").transform.localRotation == Quaternion.Euler(90, 0, 0))
        {
            destinationElbow.eulerAngles = new Vector3(0f, -7f, 0f);
            destinationArm.eulerAngles = new Vector3(7, 0, 0);
            destinationWrist.eulerAngles = new Vector3(0, 0, 0);
            destinationDelay2 = new Vector3(5, 0, 0);
            Move1Left90 = false;
            Move2Left90 = true;
        }
        if (Move2Left90 == true && Move3Left90 == false && GameObject.Find("elbow_link").transform.localRotation.eulerAngles == destinationElbow.eulerAngles)
        {
            GameObject.Find("servo_head").transform.localScale = new Vector3(1f, 1f, 2.5f);
            Move2Left90 = false;
            Move3Left90 = true;
        }
        if (Move3Left90 == true && GameObject.Find("servo_head").transform.localScale == new Vector3(1, 1, 2.5f) &&
            GameObject.Find("Niryo_assembly").transform.localRotation == Quaternion.Euler(90, 0, 0))
        {
            Move1Left90 = false;
            Move2Left90 = false;
            Move3Left90 = false;
            destinationShoulderPlus.eulerAngles = new Vector3(0, -90, 0);
            RotateSide(cubeState.left, -90);
            ResetPosRos = false;
            ResetPos = false;
            Move4 = true;
        }
    }
    void MoveLeftN90()
    {
        if (GameObject.Find("Niryo_assembly").transform.localRotation == Quaternion.Euler(90, 0, 0) && ResetPos == true)
        {
            ResetPos = true;
            Move1LeftN90 = true;
            destinationDelay2 = new Vector3(0, 0, 0);
            destinationBase.eulerAngles = new Vector3(90, 0, 0);
        }
        if (Move1LeftN90 == true && GameObject.Find("Niryo_assembly").transform.localRotation == Quaternion.Euler(90, 0, 0))
        {
            destinationElbow.eulerAngles = new Vector3(0f, -7f, 0f);
            destinationArm.eulerAngles = new Vector3(7, 0, 0);
            destinationWrist.eulerAngles = new Vector3(0, 0, 0);
            destinationDelay2 = new Vector3(5, 0, 0);
            Move1LeftN90 = false;
            Move2LeftN90 = true;
        }
        if (Move2LeftN90 == true && Move3LeftN90 == false && GameObject.Find("elbow_link").transform.localRotation.eulerAngles == destinationElbow.eulerAngles)
        {
            GameObject.Find("servo_head").transform.localScale = new Vector3(1f, 1f, 2.5f);
            Move2LeftN90 = false;
            Move3LeftN90 = true;
        }
        if (Move3LeftN90 == true && GameObject.Find("servo_head").transform.localScale == new Vector3(1, 1, 2.5f) &&
            GameObject.Find("Niryo_assembly").transform.localRotation == Quaternion.Euler(90, 0, 0))
        {
            Move1LeftN90 = false;
            Move2LeftN90 = false;
            Move3LeftN90 = false;
            destinationShoulderPlus.eulerAngles = new Vector3(0, 90, 0);
            RotateSide(cubeState.left, 90);
            ResetPosRos = false;
            ResetPos = false;
            Move4 = true;
        }
    }
    void MoveLeft180()
    {
        if (GameObject.Find("Niryo_assembly").transform.localRotation == Quaternion.Euler(90, 0, 0) && ResetPos == true)
        {
            ResetPos = true;
            Move1Left180 = true;
            destinationDelay2 = new Vector3(0, 0, 0);
            destinationBase.eulerAngles = new Vector3(90, 0, 0);
        }
        if (Move1Left180 == true && GameObject.Find("Niryo_assembly").transform.localRotation == Quaternion.Euler(90, 0, 0))
        {
            destinationElbow.eulerAngles = new Vector3(0f, -7f, 0f);
            destinationArm.eulerAngles = new Vector3(7, 0, 0);
            destinationWrist.eulerAngles = new Vector3(0, 0, 0);
            destinationDelay2 = new Vector3(5, 0, 0);
            Move1Left180 = false;
            Move2Left180 = true;
        }
        if (Move2Left180 == true && Move3Left180 == false && GameObject.Find("elbow_link").transform.localRotation.eulerAngles == destinationElbow.eulerAngles)
        {
            GameObject.Find("servo_head").transform.localScale = new Vector3(1f, 1f, 2.5f);
            Move2Left180 = false;
            Move3Left180 = true;
        }
        if (Move3Left180 == true && GameObject.Find("servo_head").transform.localScale == new Vector3(1, 1, 2.5f) &&
            GameObject.Find("Niryo_assembly").transform.localRotation == Quaternion.Euler(90, 0, 0))
        {
            Move1Left180 = false;
            Move2Left180 = false;
            Move3Left180 = false;
            destinationShoulderPlus.eulerAngles = new Vector3(0, 180, 0);
            RotateSide(cubeState.left, 180);
            ResetPosRos = false;
            ResetPos = false;
            Move4 = true;
        }
    }

    void MoveFront90()
    {
        if (GameObject.Find("Niryo_assembly").transform.localRotation == Quaternion.Euler(0, 0, 90) && ResetPos == true)
        {
            ResetPos = true;
            Move1Front90 = true;
            destinationDelay2 = new Vector3(0, 0, 0);
            destinationBase.eulerAngles = new Vector3(0, 0, 90);
        }
        if (Move1Front90 == true && GameObject.Find("Niryo_assembly").transform.localRotation == Quaternion.Euler(0, 0, 90))
        {
            destinationElbow.eulerAngles = new Vector3(0f, -7f, 0f);
            destinationArm.eulerAngles = new Vector3(7, 0, 0);
            destinationWrist.eulerAngles = new Vector3(0, 0, 0);
            destinationDelay2 = new Vector3(5, 0, 0);
            Move1Front90 = false;
            Move2Front90 = true;
        }
        if (Move2Front90 == true && Move3Front90 == false && GameObject.Find("elbow_link").transform.localRotation.eulerAngles == destinationElbow.eulerAngles)
        {
            GameObject.Find("servo_head").transform.localScale = new Vector3(1f, 1f, 2.5f);
            Move2Front90 = false;
            Move3Front90 = true;
        }
        if (Move3Front90 == true && GameObject.Find("servo_head").transform.localScale == new Vector3(1, 1, 2.5f) &&
            GameObject.Find("Niryo_assembly").transform.localRotation == Quaternion.Euler(0, 0, 90))
        {
            Move1Front90 = false;
            Move2Front90 = false;
            Move3Front90 = false;
            destinationShoulderPlus.eulerAngles = new Vector3(0, -90, 0);
            RotateSide(cubeState.front, -90);
            ResetPosRos = false;
            ResetPos = false;
            Move4 = true;
        }
    }
    void MoveFrontN90()
    {
        if (GameObject.Find("Niryo_assembly").transform.localRotation == Quaternion.Euler(0, 0, 90) && ResetPos == true)
        {
            ResetPos = true;
            Move1FrontN90 = true;
            destinationDelay2 = new Vector3(0, 0, 0);
            destinationBase.eulerAngles = new Vector3(0, 0, 90);
        }
        if (Move1FrontN90 == true && GameObject.Find("Niryo_assembly").transform.localRotation == Quaternion.Euler(0, 0, 90))
        {
            destinationElbow.eulerAngles = new Vector3(0f, -7f, 0f);
            destinationArm.eulerAngles = new Vector3(7, 0, 0);
            destinationWrist.eulerAngles = new Vector3(0, 0, 0);
            destinationDelay2 = new Vector3(5, 0, 0);
            Move1FrontN90 = false;
            Move2FrontN90 = true;
        }
        if (Move2FrontN90 == true && Move3FrontN90 == false && GameObject.Find("elbow_link").transform.localRotation.eulerAngles == destinationElbow.eulerAngles)
        {
            GameObject.Find("servo_head").transform.localScale = new Vector3(1f, 1f, 2.5f);
            Move2FrontN90 = false;
            Move3FrontN90 = true;
        }
        if (Move3FrontN90 == true && GameObject.Find("servo_head").transform.localScale == new Vector3(1, 1, 2.5f) &&
            GameObject.Find("Niryo_assembly").transform.localRotation == Quaternion.Euler(0, 0, 90))
        {
            Move1FrontN90 = false;
            Move2FrontN90 = false;
            Move3FrontN90 = false;
            destinationShoulderPlus.eulerAngles = new Vector3(0, 90, 0);
            RotateSide(cubeState.front, 90);
            ResetPosRos = false;
            ResetPos = false;
            Move4 = true;
        }
    }
    void MoveFront180()
    {
        if (GameObject.Find("Niryo_assembly").transform.localRotation == Quaternion.Euler(0, 0, 90) && ResetPos == true)
        {
            ResetPos = true;
            Move1Front180 = true;
            destinationDelay2 = new Vector3(0, 0, 0);
            destinationBase.eulerAngles = new Vector3(0, 0, 90);
        }
        if (Move1Front180 == true && GameObject.Find("Niryo_assembly").transform.localRotation == Quaternion.Euler(0, 0, 90))
        {
            destinationElbow.eulerAngles = new Vector3(0f, -7f, 0f);
            destinationArm.eulerAngles = new Vector3(7, 0, 0);
            destinationWrist.eulerAngles = new Vector3(0, 0, 0);
            destinationDelay2 = new Vector3(5, 0, 0);
            Move1Front180 = false;
            Move2Front180 = true;
        }
        if (Move2Front180 == true && Move3Front180 == false && GameObject.Find("elbow_link").transform.localRotation.eulerAngles == destinationElbow.eulerAngles)
        {
            GameObject.Find("servo_head").transform.localScale = new Vector3(1f, 1f, 2.5f);
            Move2Front180 = false;
            Move3Front180 = true;
        }
        if (Move3Front180 == true && GameObject.Find("servo_head").transform.localScale == new Vector3(1, 1, 2.5f) &&
            GameObject.Find("Niryo_assembly").transform.localRotation == Quaternion.Euler(0, 0, 90))
        {
            Move1Front180 = false;
            Move2Front180 = false;
            Move3Front180 = false;
            destinationShoulderPlus.eulerAngles = new Vector3(0, 180, 0);
            RotateSide(cubeState.front, 180);
            ResetPosRos = false;
            ResetPos = false;
            Move4 = true;
        }
    }

    void MoveBack90()
    {
        if (GameObject.Find("Niryo_assembly").transform.localRotation == Quaternion.Euler(0, 0, 270) && ResetPos == true)
        {
            ResetPos = true;
            Move1Back90 = true;
            destinationDelay2 = new Vector3(0, 0, 0);
            destinationBase.eulerAngles = new Vector3(0, 0, 270);
        }
        if (Move1Back90 == true && GameObject.Find("Niryo_assembly").transform.localRotation == Quaternion.Euler(0, 0, 270))
        {
            destinationElbow.eulerAngles = new Vector3(0f, -7f, 0f);
            destinationArm.eulerAngles = new Vector3(7, 0, 0);
            destinationWrist.eulerAngles = new Vector3(0, 0, 0);
            destinationDelay2 = new Vector3(5, 0, 0);
            Move1Back90 = false;
            Move2Back90 = true;
        }
        if (Move2Back90 == true && Move3Back90 == false && GameObject.Find("elbow_link").transform.localRotation.eulerAngles == destinationElbow.eulerAngles)
        {
            GameObject.Find("servo_head").transform.localScale = new Vector3(1f, 1f, 2.5f);
            Move2Back90 = false;
            Move3Back90 = true;
        }
        if (Move3Back90 == true && GameObject.Find("servo_head").transform.localScale == new Vector3(1, 1, 2.5f) &&
            GameObject.Find("Niryo_assembly").transform.localRotation == Quaternion.Euler(0, 0, 270))
        {
            Move1Back90 = false;
            Move2Back90 = false;
            Move3Back90 = false;
            destinationShoulderPlus.eulerAngles = new Vector3(0, -90, 0);
            RotateSide(cubeState.back, -90);
            ResetPosRos = false;
            ResetPos = false;
            Move4 = true;
        }
    }
    void MoveBackN90()
    {
        if (GameObject.Find("Niryo_assembly").transform.localRotation == Quaternion.Euler(0, 0, 270) && ResetPos == true)
        {
            ResetPos = true;
            Move1BackN90 = true;
            destinationDelay2 = new Vector3(0, 0, 0);
            destinationBase.eulerAngles = new Vector3(0, 0, 270);
        }
        if (Move1BackN90 == true && GameObject.Find("Niryo_assembly").transform.localRotation == Quaternion.Euler(0, 0, 270))
        {
            destinationElbow.eulerAngles = new Vector3(0f, -7f, 0f);
            destinationArm.eulerAngles = new Vector3(7, 0, 0);
            destinationWrist.eulerAngles = new Vector3(0, 0, 0);
            destinationDelay2 = new Vector3(5, 0, 0);
            Move1BackN90 = false;
            Move2BackN90 = true;
        }
        if (Move2BackN90 == true && Move3BackN90 == false && GameObject.Find("elbow_link").transform.localRotation.eulerAngles == destinationElbow.eulerAngles)
        {
            GameObject.Find("servo_head").transform.localScale = new Vector3(1f, 1f, 2.5f);
            Move2BackN90 = false;
            Move3BackN90 = true;
        }
        if (Move3BackN90 == true && GameObject.Find("servo_head").transform.localScale == new Vector3(1, 1, 2.5f) &&
            GameObject.Find("Niryo_assembly").transform.localRotation == Quaternion.Euler(0, 0, 270))
        {
            Move1BackN90 = false;
            Move2BackN90 = false;
            Move3BackN90 = false;
            destinationShoulderPlus.eulerAngles = new Vector3(0, 90, 0);
            RotateSide(cubeState.back, 90);
            ResetPosRos = false;
            ResetPos = false;
            Move4 = true;
        }
    }
    void MoveBack180()
    {
        if (GameObject.Find("Niryo_assembly").transform.localRotation == Quaternion.Euler(0, 0, 270) && ResetPos == true)
        {
            ResetPos = true;
            Move1Back180 = true;
            destinationDelay2 = new Vector3(0, 0, 0);
            destinationBase.eulerAngles = new Vector3(0, 0, 270);
        }
        if (Move1Back180 == true && GameObject.Find("Niryo_assembly").transform.localRotation == Quaternion.Euler(0, 0, 270))
        {
            destinationElbow.eulerAngles = new Vector3(0f, -7f, 0f);
            destinationArm.eulerAngles = new Vector3(7, 0, 0);
            destinationWrist.eulerAngles = new Vector3(0, 0, 0);
            destinationDelay2 = new Vector3(5, 0, 0);
            Move1Back180 = false;
            Move2Back180 = true;
        }
        if (Move2Back180 == true && Move3Back180 == false && GameObject.Find("elbow_link").transform.localRotation.eulerAngles == destinationElbow.eulerAngles)
        {
            GameObject.Find("servo_head").transform.localScale = new Vector3(1f, 1f, 2.5f);
            Move2Back180 = false;
            Move3Back180 = true;
        }
        if (Move3Back180 == true && GameObject.Find("servo_head").transform.localScale == new Vector3(1, 1, 2.5f) &&
            GameObject.Find("Niryo_assembly").transform.localRotation == Quaternion.Euler(0, 0, 270))
        {
            Move1Back180 = false;
            Move2Back180 = false;
            Move3Back180 = false;
            destinationShoulderPlus.eulerAngles = new Vector3(0, 180, 0);
            RotateSide(cubeState.back, 180);
            ResetPosRos = false;
            ResetPos = false;
            Move4 = true;
        }
    }

    void Callback_Destination(PositionServiceResponse response)
    {
        awaitingResponseUntilTimestamp = -1;
        destinationElbow.eulerAngles = new Vector3(0f, -7f, 0f);
        destinationArm.eulerAngles = new Vector3(7, 0, 0);
        destinationWrist.eulerAngles = new Vector3(0, 0, 0);
        Move4 = false;
        Move2 = false;
        Move3 = false;
    }

    void Callback_Destination2(PositionService2Response response)
    {
        awaitingResponseUntilTimestamp = -1;
        destinationShoulderPlus.eulerAngles = new Vector3(0f, 90f, 0f);
        //Debug.Log("New Destination: " + destination);
        Move2 = true;
        Move1 = false;
        Move4 = false;
        Move3 = false;
        //print(destination90.eulerAngles);
    }
    void Callback_Destination3(PositionService3Response response)
    {
        awaitingResponseUntilTimestamp = -1;
        destinationShoulderPlus.eulerAngles = new Vector3(0f, 0f, 0f);
        Move1 = false;
        Move2 = false;
        Move4 = false;
        //Debug.Log("New Destination: " + destination);
        //print(speed);
    }

    void Callback_Destination4(PositionService4Response response)
    {
        awaitingResponseUntilTimestamp = -1;
        destinationElbow.eulerAngles = new Vector3(0f, 90f, 0f);
        destinationArm.eulerAngles = new Vector3(-50, 0, 0);
        destinationWrist.eulerAngles = new Vector3(0, 0, 80);

        Move1 = false;
        Move2 = false;
        Move3 = false;
       

    }
    //MoveTopSide
    void Callback_Destination5(PositionService5Response response)
    {
        awaitingResponseUntilTimestamp = -1;
        //Debug.Log("New Destination: " + destination);
        //Move1 = true;
        destinationDelay1 = new Vector3(5, 5, 5);
        destinationBase.eulerAngles = new Vector3(0, 0, 0);
        
    }

    //MoveDownSide
    void Callback_Destination6(PositionService6Response response)
    {
        awaitingResponseUntilTimestamp = -1;
        //Debug.Log("New Destination: " + destination);
        //Move1 = true;
        destinationDelay1 = new Vector3(5, 5, 5);
        destinationBase.eulerAngles = new Vector3(180, 0, 0);
    }

    //MoveRightSide
    void Callback_Destination7(PositionService7Response response)
    {
        awaitingResponseUntilTimestamp = -1;
        //Debug.Log("New Destination: " + destination);
        //Move1 = true;
        destinationDelay1 = new Vector3(5, 5, 5);
        destinationBase.eulerAngles = new Vector3(-90, 0, 0);
    }

    //MoveLeftSide
    void Callback_Destination8(PositionService8Response response)
    {
        awaitingResponseUntilTimestamp = -1;
        //Debug.Log("New Destination: " + destination);
        //Move1 = true;
        destinationDelay1 = new Vector3(5, 5, 5);
        destinationBase.eulerAngles = new Vector3(90, 0, 0);
    }

    //MoveFrontSide
    void Callback_Destination9(PositionService9Response response)
    {
        awaitingResponseUntilTimestamp = -1;
        //Debug.Log("New Destination: " + destination);
        //Move1 = true;
        destinationDelay1 = new Vector3(5, 5, 5);
        destinationBase.eulerAngles = new Vector3(0, 0, 90);
    }

    //MoveBackSide
    void Callback_Destination10(PositionService10Response response)
    {
        awaitingResponseUntilTimestamp = -1;
        //Debug.Log("New Destination: " + destination);
        //Move1 = true;
        destinationDelay1 = new Vector3(5, 5, 5);
        destinationBase.eulerAngles = new Vector3(0, 0, -90);
    }

    //Shuffle
    void Callback_DestinationShuffle(ShuffleResponse response)
    {
        awaitingResponseUntilTimestamp = -1;
        ShuffleActive = true;
        StartCoroutine(LateStart(2f));
    }

    void Callback_DestinationSolve(SolveResponse response)
    {
        awaitingResponseUntilTimestamp = -1;
        solver.Solver();
    }

    //Push
    void SolveChange(RosSolve solveMessage)
    {
        solver.Solver();
    }
    void ShuffleChange(RosShuffle shuffleMessage)
    {
        Shuffle();
    }

    void LeftChange(RosLeft leftMessage)
    {
        destinationDelay1 = new Vector3(5, 5, 5);
        destinationBase.eulerAngles = new Vector3(90, 0, 0);
    }

    void RightChange(RosRight rightMessage)
    {
        destinationDelay1 = new Vector3(5, 5, 5);
        destinationBase.eulerAngles = new Vector3(-90, 0, 0);
    }

    void DownChange(RosDown downMessage)
    {
        destinationDelay1 = new Vector3(5, 5, 5);
        destinationBase.eulerAngles = new Vector3(180, 0, 0);
    }

    void UpChange(RosUp upMessage)
    {
        destinationDelay1 = new Vector3(5, 5, 5);
        destinationBase.eulerAngles = new Vector3(0, 0, 0);
    }

    void BackChange(RosBack backMessage)
    {
        destinationDelay1 = new Vector3(5, 5, 5);
        destinationBase.eulerAngles = new Vector3(0, 0, -90);
    }

    void FrontChange(RosFront frontMessage)
    {
        destinationDelay1 = new Vector3(5, 5, 5);
        destinationBase.eulerAngles = new Vector3(0, 0, 90);
    }
    void ColorChange(RosColor colorMessage)
    {

        GameObject.Find("RingX").GetComponent<Renderer>().material.color = new Color32((byte)colorMessage.r, (byte)colorMessage.g, (byte)colorMessage.b, (byte)colorMessage.a);
        GameObject.Find("RingY").GetComponent<Renderer>().material.color = new Color32((byte)colorMessage.r, (byte)colorMessage.g, (byte)colorMessage.b, (byte)colorMessage.a);
        GameObject.Find("RingZ").GetComponent<Renderer>().material.color = new Color32((byte)colorMessage.r, (byte)colorMessage.g, (byte)colorMessage.b, (byte)colorMessage.a);
    }


    public void Shuffle()
    {
        List<string> moves = new List<string>();
        int shuffleLength = Random.Range(1, 1);
        for (int i = 0; i < shuffleLength; i++)
        {
            int randomMove = Random.Range(0, allMoves.Count);
            moves.Add(allMoves[randomMove]); 
            
        }
        moveList = moves;
       

    }



    public void DoMove(string move)
    {
        float step = speed * Time.deltaTime;
        readCube.ReadState();
        
        //print(move);
        //Upside
        if (move == "U" && ResetPos == true)
        {
            destinationBase.eulerAngles = new Vector3(0, 0, 0);
            Move1Up90 = true;
            MoveUp90();
            ResetPosRos = false;
        }
        if (move == "U'" && ResetPos == true)
        {
            destinationBase.eulerAngles = new Vector3(0, 0, 0);
            Move1UpN90 = true;
            MoveUpN90();
            ResetPosRos = false;
        }
        if (move == "U2" && ResetPos == true)
        {
            destinationBase.eulerAngles = new Vector3(0, 0, 0);
            Move1Up180 = true;
            MoveUp180();
            ResetPosRos = false;
        }
        //Downside
        if (move == "D" && ResetPos == true)
        {
            destinationBase.eulerAngles = new Vector3(180, 0, 0);
            Move1Down90 = true;
            MoveDown90();
            ResetPosRos = false;
        }
        if (move == "D'" && ResetPos == true)
        {
            destinationBase.eulerAngles = new Vector3(180, 0, 0);
            Move1DownN90 = true;
            MoveDownN90();
            ResetPosRos = false;
        }
        if (move == "D2" && ResetPos == true)
        {
            destinationBase.eulerAngles = new Vector3(180, 0, 0);
            Move1Down180 = true;
            MoveDown180();
            ResetPosRos = false;
        }
        //Leftside
        if (move == "L" && ResetPos == true)
        {
            destinationBase.eulerAngles = new Vector3(90, 0, 0);
            Move1Left90 = true;
            MoveLeft90();
            ResetPosRos = false;
        }
        if (move == "L'" && ResetPos == true)
        {
            destinationBase.eulerAngles = new Vector3(90, 0, 0);
            Move1LeftN90 = true;
            MoveLeftN90();
            ResetPosRos = false;
        }
        if (move == "L2" && ResetPos == true)
        {
            destinationBase.eulerAngles = new Vector3(90, 0, 0);
            Move1Left180 = true;
            MoveLeft180();
            ResetPosRos = false;
        }
        //Rightside
        if (move == "R" && ResetPos == true)
        {
            destinationBase.eulerAngles = new Vector3(270, 0, 0);
            Move1Right90 = true;
            MoveRight90();
            ResetPosRos = false;
        }
        if (move == "R'" && ResetPos == true)
        {
            destinationBase.eulerAngles = new Vector3(270, 0, 0);
            Move1RightN90 = true;
            MoveRightN90();
            ResetPosRos = false;
        }
        if (move == "R2" && ResetPos == true)
        {
            destinationBase.eulerAngles = new Vector3(270, 0, 0);
            Move1Right180 = true;
            MoveRight180();
            ResetPosRos = false;
        }

        //Frontside
        if (move == "F" && ResetPos == true)
        {
            destinationBase.eulerAngles = new Vector3(0, 0, 90);
            Move1Front90 = true;
            MoveFront90();
            ResetPosRos = false;
        }
        if (move == "F'" && ResetPos == true)
        {
            destinationBase.eulerAngles = new Vector3(0, 0, 90);
            Move1FrontN90 = true;
            MoveFrontN90();
            ResetPosRos = false;
        }
        if (move == "F2" && ResetPos == true)
        {
            destinationBase.eulerAngles = new Vector3(0, 0, 90);
            Move1Front180 = true;
            MoveFront180();
            ResetPosRos = false;
        }
        //Backside
        if (move == "B" && ResetPos == true)
        {
            destinationBase.eulerAngles = new Vector3(0, 0, 270);
            Move1Back90 = true;
            MoveBack90();
            ResetPosRos = false;
        }
        if (move == "B'" && ResetPos == true)
        {
            destinationBase.eulerAngles = new Vector3(0, 0, 270);
            Move1BackN90 = true;
            MoveBackN90();
            ResetPosRos = false;
        }
        if (move == "B2" && ResetPos == true)
        {
            destinationBase.eulerAngles = new Vector3(0, 0, 270);
            Move1Back180 = true;
            MoveBack180();
            ResetPosRos = false;
        }

        //print(move);
    }
    
     void RotateSide(List < GameObject > side, float angle)
        {
            
            PivotRotation pr = side[4].transform.parent.GetComponent<PivotRotation>();
            CubeState.autoRotating = true;
            pr.StartAutoRotate(side, angle);
            moveList.Remove(moveList[0]);
        

    }

} 