using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeTrap : Trap
{
    private GameObject axe;
    private Joint _joint;

    private void Awake()
    {
        axe = transform.GetChild(0).gameObject;
    }

    public override void ActivateTrap()
    {
        axe.GetComponent<Axe>().ActivateAxe();
        _joint = axe.AddComponent<HingeJoint>();
        _joint.autoConfigureConnectedAnchor = false;
        _joint.connectedAnchor = axe.transform.position;
        _joint.axis = new Vector3(0, 0, 1);
    }

    private void FixedUpdate()
    {
        if ((_joint != null) && axe.transform.rotation.eulerAngles.z >= 180)
        {
            Destroy(_joint);
        }
    }
}
