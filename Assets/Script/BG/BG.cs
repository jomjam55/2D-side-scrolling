using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BG : MonoBehaviour
{
    private GameObject cam;

    [SerializeField] private float paraEffect;

    private float xPosition;
    private float length;

    // Start is called before the first frame update
    void Start()
    {

        cam = GameObject.Find("Main Camera");

        length = GetComponent<SpriteRenderer>().bounds.size.x;
        xPosition = transform.position.x;


    }

    // Update is called once per frame
    void Update()
    {
        float distanceMove = cam.transform.position.x * (1 - paraEffect);
        float distanceToMove = cam.transform.position.x *  paraEffect;

        transform.position = new Vector3(xPosition + distanceToMove, transform.position.y);


        if ( distanceMove > xPosition + length)
        {
            xPosition = xPosition + length;
        }else if ( distanceMove < xPosition - length)
        {
            xPosition = xPosition - length;
        }

    }
}
