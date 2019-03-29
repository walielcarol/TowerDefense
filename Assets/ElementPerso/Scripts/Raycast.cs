using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raycast : MonoBehaviour
{
    Camera cam;

    public Collider previousHit;
    public Collider currentHit;

    public delegate void ClickHandler(Raycast raycast);

    public ClickHandler onClick;



    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            RayCastOnClick();
        }


    }

    void RayCastOnClick()
    {
        int layerMask = 1 << 16;
        RaycastHit hit;
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
        {

                previousHit = currentHit;
                currentHit = hit.collider;
            if(onClick != null)
            {
                onClick.Invoke(this);
            }

        }
    }
}
