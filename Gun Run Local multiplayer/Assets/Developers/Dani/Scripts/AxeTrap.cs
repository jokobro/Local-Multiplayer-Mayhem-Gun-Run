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
    }

    public override void ActivateTrap()
    {
        axe.GetComponent<Axe>().ActivateAxe();
        joint = axe.AddComponent<HingeJoint>();
        joint.autoConfigureConnectedAnchor = false;
        joint.connectedAnchor = axe.transform.position;
        joint.axis = new Vector3(0, 0, 1);
    }

    private void FixedUpdate()
    {
        if ((joint != null) && axe.transform.rotation.eulerAngles.z >= 180)
        {
            Destroy(joint);
        }
    }
}
