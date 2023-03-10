//Do not edit! This file was generated by Unity-ROS MessageGeneration.
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Unity.Robotics.ROSTCPConnector.MessageGeneration;

namespace RosMessageTypes.UnityRoboticsDemo
{
    [Serializable]
    public class PositionService5Request : Message
    {
        public const string k_RosMessageName = "unity_robotics_demo_msgs/PositionService5";
        public override string RosMessageName => k_RosMessageName;

        public PosRotMsg input;

        public PositionService5Request()
        {
            this.input = new PosRotMsg();
        }

        public PositionService5Request(PosRotMsg input)
        {
            this.input = input;
        }

        public static PositionService5Request Deserialize(MessageDeserializer deserializer) => new PositionService5Request(deserializer);

        private PositionService5Request(MessageDeserializer deserializer)
        {
            this.input = PosRotMsg.Deserialize(deserializer);
        }

        public override void SerializeTo(MessageSerializer serializer)
        {
            serializer.Write(this.input);
        }

        public override string ToString()
        {
            return "PositionService5Request: " +
            "\ninput: " + input.ToString();
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
