//Do not edit! This file was generated by Unity-ROS MessageGeneration.
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Unity.Robotics.ROSTCPConnector.MessageGeneration;

namespace RosMessageTypes.UnityRoboticsDemo
{
    [Serializable]
    public class PositionService4Response : Message
    {
        public const string k_RosMessageName = "unity_robotics_demo_msgs/PositionService4";
        public override string RosMessageName => k_RosMessageName;

        public PosRotMsg output;

        public PositionService4Response()
        {
            this.output = new PosRotMsg();
        }

        public PositionService4Response(PosRotMsg output)
        {
            this.output = output;
        }

        public static PositionService4Response Deserialize(MessageDeserializer deserializer) => new PositionService4Response(deserializer);

        private PositionService4Response(MessageDeserializer deserializer)
        {
            this.output = PosRotMsg.Deserialize(deserializer);
        }

        public override void SerializeTo(MessageSerializer serializer)
        {
            serializer.Write(this.output);
        }

        public override string ToString()
        {
            return "PositionService4Response: " +
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
