using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lowerit : MonoBehaviour {

    [ContextMenu("lowerit")]
	void lower()
    {
        foreach(Transform t in transform.GetComponentsInChildren<Transform>())
        {
            t.gameObject.name = t.gameObject.name.ToLower();
            Debug.Log("lower " + t.gameObject.name);
        }
    }
}
