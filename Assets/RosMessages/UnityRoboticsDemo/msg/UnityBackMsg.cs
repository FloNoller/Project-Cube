//Do not edit! This file was generated by Unity-ROS MessageGeneration.
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Unity.Robotics.ROSTCPConnector.MessageGeneration;

namespace RosMessageTypes.UnityRoboticsDemo
{
    [Serializable]
    public class UnityBackMsg : Message
    {
        public const string k_RosMessageName = "unity_robotics_demo_msgs/UnityBack";
        public override string RosMessageName => k_RosMessageName;

        public int r;
        public int g;
        public int b;
        public int a;

        public UnityBackMsg()
        {
            this.r = 0;
            this.g = 0;
            this.b = 0;
            this.a = 0;
        }

        public UnityBackMsg(int r, int g, int b, int a)
        {
            this.r = r;
            this.g = g;
            this.b = b;
            this.a = a;
        }

        public static UnityBackMsg Deserialize(MessageDeserializer deserializer) => new UnityBackMsg(deserializer);

        private UnityBackMsg(MessageDeserializer deserializer)
        {
            deserializer.Read(out this.r);
            deserializer.Read(out this.g);
            deserializer.Read(out this.b);
            deserializer.Read(out this.a);
        }

        public override void SerializeTo(MessageSerializer serializer)
        {
            serializer.Write(this.r);
            serializer.Write(this.g);
            serializer.Write(this.b);
            serializer.Write(this.a);
        }

        public override string ToString()
        {
            return "UnityBackMsg: " +
            "\nr: " + r.ToString() +
            "\ng: " + g.ToString() +
            "\nb: " + b.ToString() +
            "\na: " + a.ToString();
        }

#if UNITY_EDITOR
        [UnityEditor.InitializeOnLoadMethod]
#else
        [UnityEngine.RuntimeInitializeOnLoadMethod]
#endif
        public static void Register()
        {
            MessageRegistry.Register(k_RosMessageName, Deserialize);
        }
    }
}
