using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatScript : MonoBehaviour
{
    private Vector3 startPos;
    private float repeatWidth;
    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;

        //Gets the mid portion from the background
        repeatWidth = GetComponent<BoxCollider>().size.x / 2;
    }

    // Update is called once per frame
    void Update()
    {
        //If the x pos of background is less than the starting pos of background and the repeat width
        if(transform.position.x < startPos.x - repeatWidth)
        {
            transform.position = startPos;
        }
        
    }
}
