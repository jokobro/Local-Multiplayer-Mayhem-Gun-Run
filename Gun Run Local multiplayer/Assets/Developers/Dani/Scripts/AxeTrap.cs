using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeTrap : Trap
{
    private GameObject axe;
    private Joint joint;

    private void Awake()
    {
        axe = transform.GetChild(0).gameObject;
        ActivateTrap();
    }

    public override void ActivateTrap()
    {
        activated = true;
        axe.GetComponent<Axe>().LoseHit();
        joint = axe.AddComponent<HingeJoint>();
        joint.autoConfigureConnectedAnchor = false;
        joint.connectedAnchor = axe.transform.position;
        joint.axis = new Vector3(0, 0, 1);
    }

    private void FixedUpdate()
    {
        if (axe.transform.rotation.eulerAngles.z >= 180)
        {
            Debug.Log("Hellooo");
            Destroy(joint);
        }
    }
}
