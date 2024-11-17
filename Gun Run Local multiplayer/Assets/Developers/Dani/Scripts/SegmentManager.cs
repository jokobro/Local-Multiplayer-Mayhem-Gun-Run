using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SegmentManager : MonoBehaviour
{
    [SerializeField] private GameObject[] segments;
    [Range(1, 5)] public int segmentsInLevel;
    [SerializeField] private int distanceBetweenSegments;
    [Tooltip("Flip every second segment horizontally")]
    [SerializeField] private bool flip;

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
            (distanceBetweenSegments * (float)(segmentsInLevel - 1) / 2) + (distanceBetweenSegments * i), transform.position.z);

            GameObject segment = Instantiate(segments[i], spawnLocation, Quaternion.identity);
            if (flip && i % 2 == 1)
            {
                segment.transform.forward = -transform.forward;
            }
        }
    }
}
