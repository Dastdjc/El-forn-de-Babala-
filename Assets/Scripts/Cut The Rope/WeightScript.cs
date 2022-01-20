using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeightScript : MonoBehaviour
{
    public float distanceFromChainEnd = 0.5f;
    public void ConnectRopeEnd(Rigidbody2D endRB) 
    {
        HingeJoint2D joint = gameObject.AddComponent<HingeJoint2D>();
        joint.autoConfigureConnectedAnchor = false;
        joint.connectedBody = endRB;
        joint.anchor = Vector2.zero;
        joint.connectedAnchor = new Vector2(0, -distanceFromChainEnd);
    }
}
