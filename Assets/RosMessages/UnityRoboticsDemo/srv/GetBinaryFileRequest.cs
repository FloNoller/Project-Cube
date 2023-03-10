//Do not edit! This file was generated by Unity-ROS MessageGeneration.
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Unity.Robotics.ROSTCPConnector.MessageGeneration;

namespace RosMessageTypes.UnityRoboticsDemo
{
    [Serializable]
    public class GetBinaryFileRequest : Message
    {
        public const string k_RosMessageName = "unity_robotics_demo_msgs/GetBinaryFile";
        public override string RosMessageName => k_RosMessageName;

        public string name;

        public GetBinaryFileRequest()
        {
            this.name = "";
        }

        public GetBinaryFileRequest(string name)
        {
            this.name = name;
        }

        public static GetBinaryFileRequest Deserialize(MessageDeserializer deserializer) => new GetBinaryFileRequest(deserializer);

        private GetBinaryFileRequest(MessageDeserializer deserializer)
        {
            deserializer.Read(out this.name);
        }

        public override void SerializeTo(MessageSerializer serializer)
        {
            serializer.Write(this.name);
        }

        public override string ToString()
        {
            return "GetBinaryFileRequest: " +
            "\nname: " + name.ToString();
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
