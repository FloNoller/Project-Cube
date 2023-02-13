using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotRotation : MonoBehaviour
{
    public Quaternion originU1;
    public Quaternion originU2;
    public Quaternion originU3;
    public Quaternion originU4;
    public Quaternion originU5;
    public Quaternion originU6;
    public Quaternion originU7;
    public Quaternion originU8;
    public Quaternion originU9;
    public Quaternion originU10;
    public Quaternion originU11;

    public Quaternion originD1;
    public Quaternion originD2;
    public Quaternion originD3;
    public Quaternion originD4;
    public Quaternion originD5;
    public Quaternion originD6;
    public Quaternion originD7;
    public Quaternion originD8;
    public Quaternion originD9;
    public Quaternion originD10;

    // Start is called before the first frame update
    void Start()
    {
        originU1.eulerAngles = new Vector3(0f, 0f, 0f);
        originU2.eulerAngles = new Vector3(0f, 90f, 0f);
        originU3.eulerAngles = new Vector3(0f, 180f, 0f);
        originU4.eulerAngles = new Vector3(0f, 270f, 0f);
        originU5.eulerAngles = new Vector3(180f, 0f, 180f);
        originU11.eulerAngles = new Vector3(180, 180, 180);
        originU6.eulerAngles = new Vector3(-180f, 0f, -180f);
        originU7.eulerAngles = new Vector3(0f, 0f, 0f);
        originU8.eulerAngles = new Vector3(0f, -90f, 0f);
        originU9.eulerAngles = new Vector3(0f, -180f, 0f);
        originU10.eulerAngles = new Vector3(0f, -270f, 0f);

        originD1.eulerAngles = new Vector3(180f, 0f, 0f);
        originD2.eulerAngles = new Vector3(180f, 90f, 0f);
        originD3.eulerAngles = new Vector3(180f, 180f, 0f);
        originD4.eulerAngles = new Vector3(180f, 270f, 0f);
        originD5.eulerAngles = new Vector3(180f, 0f, 180f);
        originD6.eulerAngles = new Vector3(-180f, 0f, -180f);
        originD7.eulerAngles = new Vector3(180f, 0f, 0f);
        originD8.eulerAngles = new Vector3(180f, -90f, 0f);
        originD9.eulerAngles = new Vector3(180f, -180f, 0f);
        originD10.eulerAngles = new Vector3(180f, -270f, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        //if (GameObject.Find("Niryo_assembly").transform.localRotation == originU1)
        //{
        //    GameObject.Find("servo_head").transform.localScale = new Vector3(1, 1, 2.5f);
        //}
        //if (GameObject.Find("Niryo_assembly").transform.localRotation == originU11)
        //{
        //    GameObject.Find("servo_head").transform.localScale = new Vector3(1, 1, 2.5f);
        //}
        //if (GameObject.Find("Niryo_assembly").transform.localRotation != originU1)
        //{
        //    GameObject.Find("servo_head").transform.localScale = new Vector3(1, 1, 3f);
        //}
        //if (GameObject.Find("Niryo_assembly").transform.localRotation != originU11)
        //{
        //    GameObject.Find("servo_head").transform.localScale = new Vector3(1, 1, 3f);
        //}
    }
}
