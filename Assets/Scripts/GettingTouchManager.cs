using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GettingTouchManager : MonoBehaviour
{
    public static GettingTouchManager Instance;
    [SerializeField] GameObject particles;
    float instantiateZ = 0;
    float celSize = 10;
    [SerializeField] float startScale = 0.1f;
    [SerializeField] float endScale = 0.5f;

    [SerializeField] LayerMask touchableLayerOnlySoldier;
    [SerializeField] LayerMask touchableLayerOnlyGround;
    // Start is called before the first frame update
    public GameObject objectToDrag;
    Vector3 originalPosOrDraggingObject;
    RaycastHit hit;
    Ray ray;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        void Update()
        {
            if (Input.touchCount > 0)
            {
                ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
                if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)                // This is actions when finger/cursor hit screen
                {
                    if (Physics.Raycast(ray.origin, ray.direction, out hit, Mathf.Infinity, touchableLayerOnlySoldier)) // if it hit to a machine object
                    {
                        objectToDrag = hit.collider.gameObject;
                        
                    }
                }

                else if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved && objectToDrag != null)
                {

                    // This is actions when finger/cursor pressed on screen
                    if (Physics.Raycast(ray.origin, ray.direction, out hit, Mathf.Infinity, touchableLayerOnlyGround))
                    {
                        objectToDrag.transform.position = Vector3.Lerp(objectToDrag.transform.position,
                            new Vector3(hit.point.x, objectToDrag.transform.position.y, hit.point.z), 15f * Time.deltaTime);
                    }
                }

                else if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
                {
                    // This is actions when finger/cursor get out from screen
                    if (objectToDrag != null)
                    {
                       /* //if it is in a mergeable position
                        if ()
                        {

                        }
                        else
                        {

                        }
                       */
                    }
                }
            }
        }

    }


}
