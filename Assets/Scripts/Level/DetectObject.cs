using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectObject : MonoBehaviour
{
    public GameObject detectedObj;
    public float detectDistance;
    public GameObject triggerObject;

    private void Update()
    {
        if (Vector2.Distance(detectedObj.transform.position, transform.position) < detectDistance)
        {
            if (triggerObject.GetComponent<DoorMove>())
            {
                triggerObject.GetComponent<DoorMove>().open = true;
            }

        }
        else
        {
            if (triggerObject.GetComponent<DoorMove>())
            {
                triggerObject.GetComponent<DoorMove>().open = false;
            }
        }
    }
}
