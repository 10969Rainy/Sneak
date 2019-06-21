using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnGroundSensor : MonoBehaviour {

    public CapsuleCollider capCol;
    public float offset = 0.1f;

    private Vector3 point1;
    private Vector3 point2;
    private float radius;

    void Awake () {

        radius = capCol.radius - 0.05f;
	}
	
	void FixedUpdate () {

        point1 = transform.position + transform.up * (radius - offset);
        point2 = transform.position + transform.up * (capCol.height - offset) - transform.up * radius;

        Collider[] outputCols = Physics.OverlapCapsule(point1, point2, radius,LayerMask.GetMask("Ground"));
        if (outputCols.Length != 0)
        {
            SendMessageUpwards("IsGround");
        }
        else
        {
            SendMessageUpwards("IsNotGround");
        }

        Vector3 origin = transform.position;
        Vector3 lowPoint = origin + transform.up * 2.4f;
        Vector3 wallPoint = origin + transform.up * 0.0f;
        Vector3 highPoint = origin + transform.up * 2.6f;
        Vector3 dir = transform.forward;
        RaycastHit hit;
        if (Physics.Raycast(lowPoint, dir, out hit, 0.75f) && !Physics.Raycast(highPoint, dir, out hit, 1))
        {
            if (Physics.Raycast(wallPoint, dir, out hit, 1))
            {
                SendMessageUpwards("IsBraced", CalculateAngle(transform.parent.eulerAngles.y));
            }
        }

        Vector3 dir1 = transform.parent.GetChild(0).forward;
        if (!Physics.Raycast(wallPoint, dir1, out hit, 1))
        {
            SendMessageUpwards("IsNotBraced");
        }
    }

    public Vector3 WallPoint(Vector3 p)
    {
        Vector3 point = p - Vector3.right * 0.3f;
        return point;
    }

    public float CalculateAngle(float a)
    {
        float angle = 0.0f;
        if (a >= 350 || a <= 10)
        {
            angle = 0.0f;
        }
        else if (a >= 80 && a <= 100)
        {
            angle = 90.0f;
        }
        else if (a >= 170 && a <= 190)
        {
            angle = 180.0f;
        }
        else if (a >= 260 && a <= 280)
        {
            angle = 270.0f;
        }
        return angle;
    }
}
