using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera _CVCamera;
    private CinemachineTransposer _CVTrans;
    [SerializeField] private List<Transform> _followPoints;
    private Vector3 _cameraPoint;
    private float _zoomMult;
    private float _cameraXPos;

    private void Awake()
    {
        _CVTrans = _CVCamera.GetCinemachineComponent<CinemachineTransposer>();
        _cameraXPos = _CVTrans.m_FollowOffset.x;
    }

    private void Update()
    {
        if (_followPoints.Count != 0)
        {
            _cameraPoint = Vector3.zero;
            _zoomMult = 0;

            for (int i = 0; i < _followPoints.Count; i++)
            {
                _cameraPoint += _followPoints[i].position;
            }

            _cameraPoint /= _followPoints.Count;

            for (int i = 0; i < _followPoints.Count; i++)
            {
                _zoomMult += Vector3.Distance(_cameraPoint, _followPoints[i].position) / 60;
            }
            Debug.Log(_zoomMult);
            _CVTrans.m_FollowOffset.x = _cameraXPos * (_zoomMult + 1);
            transform.position = _cameraPoint;
        }
    }
}
