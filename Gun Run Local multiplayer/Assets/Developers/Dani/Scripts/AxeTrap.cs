using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeTrap : Trap
{
    private GameObject _axe;
    private Joint _joint;

    private void Awake()
    {
        _axe = transform.GetChild(0).gameObject;
    }

    public override void ActivateTrap()
    {
        base.ActivateTrap();
        _axe.GetComponent<Collider>().enabled = true;
        _axe.GetComponent<CapsuleCollider>().enabled = true;
        _axe.GetComponent<Axe>().ActivateAxe();
        _joint = _axe.AddComponent<HingeJoint>();
        _joint.autoConfigureConnectedAnchor = false;
        _joint.connectedAnchor = _axe.transform.position;
        _joint.axis = new Vector3(0, 0, 1);
    }

    private void FixedUpdate()
    {
        if ((_joint != null) && _axe.transform.rotation.eulerAngles.z >= 180)
        {
            Destroy(_joint);
        }
    }
}
