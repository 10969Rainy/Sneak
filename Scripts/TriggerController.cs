using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerController : MonoBehaviour {
    
    public enum TrigggerType{infrared, trigger }

    public TrigggerType type = TrigggerType.trigger;

    private GameObject playerHandle;
    private static Vector3 recordPos;

	void Awake () {

        playerHandle = GameObject.Find("PlayerHandle").gameObject;
	}
	
	void Update () {
        
    }

    void OnTriggerEnter(Collider coll)
    {
        switch (type)
        {
            case TrigggerType.infrared:
                //playerHandle.transform.position = recordPos;
                //playerHandle.GetComponent<Rigidbody>().position = recordPos;
                break;
            case TrigggerType.trigger:
                recordPos = transform.position - transform.up * 0.5f;
                break;
            default:
                break;
        }
    }
}
