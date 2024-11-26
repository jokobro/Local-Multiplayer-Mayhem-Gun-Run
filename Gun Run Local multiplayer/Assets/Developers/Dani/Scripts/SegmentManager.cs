using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SegmentManager : MonoBehaviour
{
    [SerializeField] private GameObject[] _segments;
    [Tooltip("What segments are still available to be used")][SerializeField] private List<int> _availableSegments;
    [Range(1, 5)][SerializeField] private int _segmentsInLevel = 3;
    [SerializeField] private int _distanceBetweenSegments = 5;
    [Tooltip("Flip every second segment horizontally")][SerializeField] private bool flip = true;

    private void Awake()
    {
        new List<int>(_availableSegments);
        for (int i = 0; i < _segments.Length; i++) // fills the availableSegments list with all possible segments
        {
            _availableSegments.Add(i);
        }

        CreateSegments();
    }

    private void CreateSegments()
    {

        for (int i = 0; i < _segmentsInLevel; i++)
        {
            int chosenSegment = UnityEngine.Random.Range(0, _availableSegments.Count); // gets one of the available segment indexes


            Vector3 spawnLocation =
            new Vector3(transform.position.x, transform.position.y -
            (_distanceBetweenSegments * (float)(_segmentsInLevel - 1) / 2) + (_distanceBetweenSegments * i), transform.position.z);
            // calculates the vertical instantiate position by essentially splitting the segments across the y axis

            GameObject segment = Instantiate(_segments[_availableSegments[chosenSegment]], spawnLocation, Quaternion.identity);
            _availableSegments.RemoveAt(chosenSegment); // removes the segment that you just instantiated. If you removed index 2, index 3 will take it's place
            if (flip && i % 2 == 1) // flips every second segment horizontally
            {
                segment.transform.GetChild(0).gameObject.transform.right = -transform.right; // face the segment towards the opposite direction than it'd initially face
            }
        }
    }
}
