using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    Vector2 startPosition;
    Vector2 endPosition;
    Vector2 direction;
    float distance;
    Vector2 force;
    public float pushForce;
    bool isDraging = false;
    Rigidbody2D rb;



    public GameObject point;
    public GameObject pointsParents;
    Transform[] points;
    public int numberOfPoints;
    public float spaceBetweenPoint;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        PrepareDots();
        pointsParents.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isDraging = true;
            OnDrugStarted();
        }
        if (Input.GetMouseButtonUp(0))
        {
            OnDrugEnd();
            isDraging = false;
        }
        if (isDraging)
        {
            onDruging();
        }
        rb.angularVelocity = 0f;
        rb.freezeRotation = true;
    }
    void OnDrugStarted()
    {
        startPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
    void OnDrugEnd()
    {
        rb.AddForce(force, ForceMode2D.Impulse);
        pointsParents.SetActive(false);
        

    }
    void onDruging()
    {
        pointsParents.SetActive(true);
        endPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        distance = Vector2.Distance(startPosition, endPosition);
        direction = (startPosition - endPosition).normalized;
        force = distance * direction * pushForce;

        Debug.DrawLine(startPosition, endPosition);

        for (int i = 0; i < numberOfPoints; i++)
        {
            //transform.position.x +=direction*
            points[i].transform.position = pointPosition(i * spaceBetweenPoint);
        }

    }

    void PrepareDots()
    {
        points = new Transform[numberOfPoints];
        for (int i = 0; i < numberOfPoints; i++)
        {
            points[i] = Instantiate(point, null).transform;
            points[i].parent = pointsParents.transform;
        }

    }
    Vector2 pointPosition(float t)
    {
        Vector2 pos = (Vector2)transform.position + (force * t) + 0.5f * Physics2D.gravity * t * t;
        return pos;
    }
    
    
}




