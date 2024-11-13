using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SegmentManager : MonoBehaviour
{
    [SerializeField] private GameObject[] segments;
    [Range(1, 5)] public int segmentsInLevel;
    [SerializeField] private int distanceBetweenSegments;

    private void Awake()
    {
        CreateSegments();
    }

    private void CreateSegments()
    {
        for (int i = 0; i < segmentsInLevel; i++)
        {
            Vector3 spawnLocation =
            new Vector3(transform.position.x, transform.position.y -
            (distanceBetweenSegments * (segmentsInLevel - 1) / 2) + (distanceBetweenSegments * i), transform.position.z);

            Instantiate(segments[i], spawnLocation, Quaternion.identity);
        }
    }
}
