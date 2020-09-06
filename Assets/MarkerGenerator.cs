using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.LSL4Unity.Scripts;

public class MarkerGenerator : MonoBehaviour
{

    public float secondsBetweenMarkers = 1f;
    private LSLMarkerStream marker;
    private int markerIndex;

    // Start is called before the first frame update
    void Start()
    {
      marker = FindObjectOfType<LSLMarkerStream>();
      markerIndex = 0;
      InvokeRepeating("sendMarker", 0f, secondsBetweenMarkers);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void sendMarker(){
      markerIndex += 1;
      marker.Write("Marker " + markerIndex);
      Debug.Log("Marker " + markerIndex + " successful");
    }
}
