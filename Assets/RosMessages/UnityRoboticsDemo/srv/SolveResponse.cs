//Do not edit! This file was generated by Unity-ROS MessageGeneration.
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Unity.Robotics.ROSTCPConnector.MessageGeneration;

namespace RosMessageTypes.UnityRoboticsDemo
{
    [Serializable]
    public class SolveResponse : Message
    {
        public const string k_RosMessageName = "unity_robotics_demo_msgs/Solve";
        public override string RosMessageName => k_RosMessageName;

        public PosRotMsg output;

        public SolveResponse()
        {
            this.output = new PosRotMsg();
        }

        public SolveResponse(PosRotMsg output)
        {
            this.output = output;
        }

        public static SolveResponse Deserialize(MessageDeserializer deserializer) => new SolveResponse(deserializer);

        private SolveResponse(MessageDeserializer deserializer)
        {
            this.output = PosRotMsg.Deserialize(deserializer);
        }

        public override void SerializeTo(MessageSerializer serializer)
        {
            serializer.Write(this.output);
        }

        public override string ToString()
        {
            return "SolveResponse: " +
            "\noutput: " + output.ToString();
        }

#if UNITY_EDITOR
        [UnityEditor.InitializeOnLoadMethod]
#else
        [UnityEngine.RuntimeInitializeOnLoadMethod]
#endif
        public static void Register()
        {
            MessageRegistry.Register(k_RosMessageName, Deserialize, MessageSubtopic.Response);
        }
    }
}