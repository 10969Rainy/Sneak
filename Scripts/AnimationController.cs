using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour {
    
    private int state = 0;
    private Vector3 pos;

    void Update()
    {
        if (state == 1)
        {
            transform.position = pos;
        }
        else if (state == 2)
        {
            transform.localPosition = Vector3.Slerp(transform.localPosition, new Vector3(0.0f, 0.0f, 0.0f), 0.15f);
        }
    }

    public void BeBraced()
    {
        state = 0;
        transform.parent.GetComponent<Rigidbody>().useGravity = false;
        transform.position = Vector3.Slerp(transform.position, new Vector3(transform.position.x, transform.position.y + 0.2f, transform.position.z), 1.0f);
    }

    public void BeNotBraced()
    {
        transform.parent.GetComponent<Rigidbody>().useGravity = true;
        transform.position = new Vector3(transform.position.x, transform.position.y - 0.2f, transform.position.z);
    }

    public void ClimbUp()
    {
        state = 1;
        pos = transform.position;
        transform.parent.transform.position = new Vector3(transform.parent.position.x + (transform.forward.x * 0.675f), transform.parent.position.y + 2.5f, transform.parent.position.z + (transform.forward.z * 0.675f));
    }

    public void ClimbOver()
    {
        state = 2;
        transform.parent.GetComponent<Rigidbody>().useGravity = true;
    }
}
