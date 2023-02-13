using UnityEngine;
using Unity.Robotics.ROSTCPConnector;
using RosColor = RosMessageTypes.UnityRoboticsDemo.UnityColorMsg;

public class RosSubscriberExample : MonoBehaviour
{
    public GameObject cube;
    
    

    void Start()
    {
        
        ROSConnection.GetOrCreateInstance().Subscribe<RosColor>("color2", ColorChange2);
    }

    void ColorChange2(RosColor color2Message)
    {
        //GameObject.Find("servo_head").transform.localScale = new Vector3(1, 1, 2.5f);
        //GameObject.Find("servo_head2").transform.localScale = new Vector3(1, 1, 2.5f);
        cube.GetComponent<Renderer>().material.color = new Color32((byte)color2Message.r, (byte)color2Message.g, (byte)color2Message.b, (byte)color2Message.a);
    }
}