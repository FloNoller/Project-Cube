using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random=UnityEngine.Random;
using DG.Tweening;

public class Automate : MonoBehaviour
{
    public static List<string> moveList = new List<string>() { };
    private readonly List<string> allMoves = new List<string>()
    {
        "U","D"
        ,"L","R","F","B"
        //,"U'","D'","L'","R'","F'","B'",
        //"U2","D2","L2","R2","F2","B2"
    };
    private GameObject Greifer;
    private CubeState cubeState;
    private ReadCube readCube;
    float time;
    float timeDelay;
    
    public float speed = 0.1f;
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
        originF = new Vector3(4.8f, 0f, 0f);
        originU = new Vector3(-4.8f, 0f, 0f);
        Pivot = new Vector3(0f, 0f, 0f);
        cubeState = FindObjectOfType<CubeState>();
        readCube = FindObjectOfType<ReadCube>();
        //GameObject.Find("Catcher").transform.position = new Vector3(2, 3, 4);
        
        time = 0f;
        timeDelay = 1f;
        StartCoroutine(LateStart(1f));
       

    }
    IEnumerator LateStart(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        Shuffle();
    }

    // Update is called once per frame
    void Update()
    {

        time = time + 1f * Time.deltaTime;
        //only call the list when finished rotating
        if (moveList.Count > 0 && !CubeState.autoRotating && CubeState.started && time >= timeDelay)//&& GreiferMoving=false;
        {
            time = 0f;

            DoMove(moveList[0]);
            moveList.Remove(moveList[0]);

        }
        GameObject.Find("Catcher").transform.LookAt(Pivot);


    }
    public void Shuffle()
    {
        List<string> moves = new List<string>();
        int shuffleLength = Random.Range(10, 15);
        for (int i = 0; i < shuffleLength; i++)
        {
            int randomMove = Random.Range(0, allMoves.Count);
            moves.Add(allMoves[randomMove]); 
            
        }
        moveList = moves;
       

    }

   
    //void CheckPos()
    //{
    //    if (GameObject.Find("Catcher").transform.position == originU || )
    //    {
            
    //        
    //    }
    //}
    
    
    public void DoMove(string move)
    {
        readCube.ReadState();
        
        print(move);
        //Upside
        if (move == "U")
        {
            
            //transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

            //GameObject.Find("Catcher").transform.position = Vector3.Lerp(transform.position, targetDown.position, speed * Time.deltaTime);

            //if (GameObject.Find("CatcherMain").transform.position != targetUp.position)
            //{
            //    GameObject.Find("CatcherMain").transform.position = Vector3.MoveTowards
            //    (GameObject.Find("CatcherMain").transform.position, 
            //        targetUp.position, speed );
            //}
            //else
            //{
            //}

            //GameObject.Find("Catcher").transform.position = Vector3.MoveTowards(GameObject.Find("Catcher").transform.position,
            //originU, speed * Time.deltaTime);
            //GameObject.Find("Catcher").transform.localEulerAngles = new Vector3(0f, 0f, 180f);
            GameObject.Find("Catcher").transform.position = new Vector3(0f, 4.8f, 0f * Time.deltaTime);
            
            RotateSide(cubeState.up, -90);
        }


        if (move == "U'")
        {
            //transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

            //GameObject.Find("Catcher").transform.position = Vector3.MoveTowards(transform.position, targetDown.position, speed * Time.deltaTime);

            //RotateSide(cubeState.up, 90 * timeDelay);
            //if (GameObject.Find("CatcherMain").transform.position != targetUp.position)
            //{
            //    GameObject.Find("CatcherMain").transform.position = Vector3.MoveTowards(GameObject.Find("Catcher").transform.position, 
            //        targetUp.position, speed );
            //}
            //else
            //{
            //    //als Child vom Cube machen
            //    //rotieren
            //}
            //GameObject.Find("Catcher").transform.position = Vector3.MoveTowards(GameObject.Find("Catcher").transform.position,
                //originU, speed * Time.deltaTime);
            GameObject.Find("Catcher").transform.position = new Vector3(0f, 4.8f, 0f * Time.deltaTime);
            //GameObject.Find("Catcher").transform.localEulerAngles = new Vector3(0f, 0f, 180f);
            RotateSide(cubeState.up, 90);

        }


        if (move == "U2")
        {
            //transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
            //transform.RotateAround(center.position, Vector3.up, 90 * Time.deltaTime);
            //GameObject.Find("Catcher").transform.position = Vector3.MoveTowards(transform.position,
            //          targetDown.transform.position, speed * Time.deltaTime);
            //RotateSide(cubeState.up, -180 * timeDelay);
            //if (GameObject.Find("CatcherMain").transform.position != targetUp.position)
            //{
            //    GameObject.Find("CatcherMain").transform.position = Vector3.MoveTowards(GameObject.Find("Catcher").transform.position, 
            //        targetUp.position, speed );
            //}
            //else
            //{
            //}
            //GameObject.Find("Catcher").transform.localEulerAngles = new Vector3(0f, 0f, 180f);
            GameObject.Find("Catcher").transform.position = new Vector3(0f, 4.8f, 0f * Time.deltaTime);
            RotateSide(cubeState.up, -180);
        }


        //Downside
        if (move == "D")
        {
            //GameObject.Find("Catcher").transform.position = Vector3.MoveTowards(GameObject.Find("Catcher").transform.position,
            //    originD, speed * Time.deltaTime);
            GameObject.Find("Catcher").transform.position = new Vector3(0f, -4.8f, 0f);
            //GameObject.Find("Catcher").transform.localEulerAngles = new Vector3(0f, 0f, 0f);
            RotateSide(cubeState.down, -90);
        }
        if (move == "D'")
        {
            GameObject.Find("Catcher").transform.position = new Vector3(0f, -4.8f, 0f);
            //GameObject.Find("Catcher").transform.localEulerAngles = new Vector3(0f, 0f, 0f);
            RotateSide(cubeState.down, 90);
        }
        if (move == "D2")
        {
            GameObject.Find("Catcher").transform.position = new Vector3(0f, -4.8f, 0f);
            //GameObject.Find("Catcher").transform.localEulerAngles = new Vector3(0f, 0f, 0f);
            RotateSide(cubeState.down, -180);
        }

        //Leftside
        if (move == "L")
        {
            GameObject.Find("Catcher").transform.position = new Vector3(0f, 0f, 4.8f);
            //GameObject.Find("Catcher").transform.localEulerAngles = new Vector3(0f, -270f, -90f);
            RotateSide(cubeState.left, -90);
        }
        if (move == "L'")
        {
            GameObject.Find("Catcher").transform.position = new Vector3(0f, 0f, 4.8f);
            //GameObject.Find("Catcher").transform.localEulerAngles = new Vector3(0f, -270f, -90f);
            RotateSide(cubeState.left, 90);
        }
        if (move == "L2")
        {
            GameObject.Find("Catcher").transform.position = new Vector3(0f, 0f, 4.8f);
            //GameObject.Find("Catcher").transform.localEulerAngles = new Vector3(0f, -270f, -90f);
            RotateSide(cubeState.left, -180);
        }

        //Rightside
        if (move == "R")
        {
            GameObject.Find("Catcher").transform.position = new Vector3(0f, 0f, -4.8f);
            //GameObject.Find("Catcher").transform.localEulerAngles = new Vector3(0f, 90f, 90f);
            RotateSide(cubeState.right, -90);
        }
        if (move == "R'")
        {
            GameObject.Find("Catcher").transform.position = new Vector3(0f, 0f, -4.8f);
            //GameObject.Find("Catcher").transform.localEulerAngles = new Vector3(0f, 90f, 90f);
            RotateSide(cubeState.right, 90);
        }
        if (move == "R2")
        {
            GameObject.Find("Catcher").transform.position = new Vector3(0f, 0f, -4.8f);
            //GameObject.Find("Catcher").transform.localEulerAngles = new Vector3(0f, 90f, 90f);
            RotateSide(cubeState.right, -180);
        }

        //Frontside
        if (move == "F")
        {
            GameObject.Find("Catcher").transform.position = new Vector3(-4.8f, 0f, 0f);
            //GameObject.Find("Catcher").transform.localEulerAngles = new Vector3(90f, 0f, 270f);
            RotateSide(cubeState.front, -90);
        }
        if (move == "F'")
        {
            GameObject.Find("Catcher").transform.position = new Vector3(-4.8f, 0f, 0f);
            //GameObject.Find("Catcher").transform.localEulerAngles = new Vector3(90f, 0f, 270f);
            RotateSide(cubeState.front, 90);
        }
        if (move == "F2")
        {
            GameObject.Find("Catcher").transform.position = new Vector3(-4.8f, 0f, 0f);
            //GameObject.Find("Catcher").transform.localEulerAngles = new Vector3(90f, 0f, 270f);
            RotateSide(cubeState.front, -180);
        }

        //Backside
        if (move == "B")
        {
            GameObject.Find("Catcher").transform.position = new Vector3(4.8f, 0f, 0f);
            //GameObject.Find("Catcher").transform.localEulerAngles = new Vector3(90f, 0f, 90f);
            RotateSide(cubeState.back, -90);
        }
        if (move == "B'")
        {
            GameObject.Find("Catcher").transform.position = new Vector3(4.8f, 0f, 0f);
            //GameObject.Find("Catcher").transform.localEulerAngles = new Vector3(90f, 0f, 90f);
            RotateSide(cubeState.back, 90);
        }
        if (move == "B2")
        {
            GameObject.Find("Catcher").transform.position = new Vector3(4.8f, 0f, 0f);
            //GameObject.Find("Catcher").transform.localEulerAngles = new Vector3(90f, 0f, 90f);
            RotateSide(cubeState.back, -180);
        }
    }
    
     void RotateSide(List < GameObject > side, float angle)
        {
            
            PivotRotation pr = side[4].transform.parent.GetComponent<PivotRotation>();
           
            pr.StartAutoRotate(side, angle);

        }
    
} 