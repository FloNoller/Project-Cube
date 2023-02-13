//Do not edit! This file was generated by Unity-ROS MessageGeneration.
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Unity.Robotics.ROSTCPConnector.MessageGeneration;

namespace RosMessageTypes.UnityRoboticsDemo
{
    [Serializable]
    public class PositionService10Response : Message
    {
        public const string k_RosMessageName = "unity_robotics_demo_msgs/PositionService10";
        public override string RosMessageName => k_RosMessageName;

        public PosRotMsg output;

        public PositionService10Response()
        {
            this.output = new PosRotMsg();
        }

        public PositionService10Response(PosRotMsg output)
        {
            this.output = output;
        }

        public static PositionService10Response Deserialize(MessageDeserializer deserializer) => new PositionService10Response(deserializer);

        private PositionService10Response(MessageDeserializer deserializer)
        {
            this.output = PosRotMsg.Deserialize(deserializer);
        }

        public override void SerializeTo(MessageSerializer serializer)
        {
            serializer.Write(this.output);
        }

        public override string ToString()
        {
            return "PositionService10Response: " +
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