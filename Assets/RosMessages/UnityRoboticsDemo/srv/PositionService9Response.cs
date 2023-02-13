//Do not edit! This file was generated by Unity-ROS MessageGeneration.
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Unity.Robotics.ROSTCPConnector.MessageGeneration;

namespace RosMessageTypes.UnityRoboticsDemo
{
    [Serializable]
    public class PositionService9Response : Message
    {
        public const string k_RosMessageName = "unity_robotics_demo_msgs/PositionService9";
        public override string RosMessageName => k_RosMessageName;

        public PosRotMsg output;

        public PositionService9Response()
        {
            this.output = new PosRotMsg();
        }

        public PositionService9Response(PosRotMsg output)
        {
            this.output = output;
        }

        public static PositionService9Response Deserialize(MessageDeserializer deserializer) => new PositionService9Response(deserializer);

        private PositionService9Response(MessageDeserializer deserializer)
        {
            this.output = PosRotMsg.Deserialize(deserializer);
        }

        public override void SerializeTo(MessageSerializer serializer)
        {
            serializer.Write(this.output);
        }

        public override string ToString()
        {
            return "PositionService9Response: " +
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
