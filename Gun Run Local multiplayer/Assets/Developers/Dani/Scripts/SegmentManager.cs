using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SegmentManager : MonoBehaviour
{
    [SerializeField] private GameObject[] segments;
    [Tooltip("What segments are still available to be used")] [SerializeField] private List<int> availableSegments;
    [Range(1, 5)] public int segmentsInLevel;
    [SerializeField] private int distanceBetweenSegments; 
    [Tooltip("Flip every second segment horizontally")] [SerializeField] private bool flip;

    private void Awake()
    {
        new List<int>(availableSegments);
        for (int i = 0; i < segments.Length; i++) // fills the availableSegments list with all possible segments
        {
            availableSegments.Add(i);
        }

        CreateSegments();
    }

    private void CreateSegments()
    {

        for (int i = 0; i < segmentsInLevel; i++)
        {
            int chosenSegment = UnityEngine.Random.Range(0, availableSegments.Count); // gets one of the available segment indexes


            Vector3 spawnLocation =
            new Vector3(transform.position.x, transform.position.y -
            (distanceBetweenSegments * (float)(segmentsInLevel - 1) / 2) + (distanceBetweenSegments * i), transform.position.z); 
            // calculates the vertical instantiate position by essentially splitting the segments across the y axis

            GameObject segment = Instantiate(segments[availableSegments[chosenSegment]], spawnLocation, Quaternion.identity);
            availableSegments.RemoveAt(chosenSegment); // removes the segment that you just instantiated. If you removed index 2, index 3 will take it's place
            if (flip && i % 2 == 1) // flips every second segment horizontally
            {
                segment.transform.forward = -transform.forward; // face the segment towards the opposite direction than it'd initially face
            }
        }
    }
}
