// Copyright 2019-2021 Robotec.ai.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using UnityEngine;

namespace ROS2
{

/// <summary>
/// An example class provided for testing of basic ROS2 communication
/// </summary>
public class ROS2TalkerExampleElbow : MonoBehaviour
{
    // Start is called before the first frame update
    private ROS2UnityComponentElbow ros2Unity;
    private ROS2Node ros2Node;
    private IPublisher<geometry_msgs.msg.Point> coordsElbow_pub;
    private int i;

    void Start()
    {
        ros2Unity = GetComponent<ROS2UnityComponentElbow>();
    }

    void Update()
    {
        if (ros2Unity.Ok())
        {
            if (ros2Node == null)
            {

                ros2Node = ros2Unity.CreateNode("ElbowNode");
                coordsElbow_pub = ros2Node.CreatePublisher<geometry_msgs.msg.Point>("coordsElbow");
            }

            i++;
            geometry_msgs.msg.Point msg = new geometry_msgs.msg.Point();
            msg.X = transform.position.x;
            msg.Y = transform.position.y;
            msg.Z = transform.position.z;
            coordsElbow_pub.Publish(msg);
        }


    }
}

}  // namespace ROS2
