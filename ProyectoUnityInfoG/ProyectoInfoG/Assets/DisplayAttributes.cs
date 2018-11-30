using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayAttributes : MonoBehaviour
{

    //Distance
    public GameObject rayStartPos;
    public LayerMask groundLayer;
    private Vector3 groundPos;
    private float distanceFromGround;
    public Text distanceText;

    //velocity
    private float velocity;
    private Vector3 finalPos;
    private Vector3 startPos;
    private float finalTime;
    private float startTime;
    private float time;
    public Text velocityText;
    

    //Acceleration
    private float acceleration;
    private float startAccel;
    private float finalAccel;
    private float finalATime;
    private float startATime;
    public Text accelerationText;

    // Use this for initialization
    void Start()
    {
        InvokeRepeating("GetVelocityValues", 0.01f, 0.2f);//sirve para llamar la funcion dentro de 0.01 segundos y despues se ejecuta cada 0.1 segundos
        InvokeRepeating("GetAcceleretionValues", 0.01f, 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        GetDistance();
        GetVelocity();
        GetAcceleration();
    }
    public void GetGroundPos()
    {
        Ray ray = new Ray(rayStartPos.transform.position, new Vector3(0, -1f, 0));
        RaycastHit hitinfo;
        if (Physics.Raycast(ray, out hitinfo, groundLayer))
        {
            groundPos = hitinfo.point;
        }
    }
    public void GetDistance()
    {
        GetGroundPos();
        distanceFromGround = Vector3.Distance(rayStartPos.transform.position, groundPos);
        Debug.Log(distanceFromGround);
        RoundDistance();
        ShowDistance();
    }
    public void RoundDistance()
    {
        distanceFromGround = distanceFromGround * 10;
        distanceFromGround = Mathf.Round(distanceFromGround);
        distanceFromGround = distanceFromGround / 10;
    }
    public void ShowDistance()
    {
        if (distanceFromGround >= 1)
            distanceText.text = "Distancia del suelo: " + distanceFromGround + "m";
        else
            distanceText.text = "Distancia del suelo: 0m";
    }

    public void GetVelocityValues()
    {        
        finalPos = startPos;
        startPos = rayStartPos.transform.position;
        finalTime = startTime;
        startTime = time;
    }
    public void GetVelocity()
    {
        time += Time.deltaTime;
        velocity = (Vector3.Distance(finalPos,startPos)) / (startTime - finalTime);
        RoundVelocity();
        ShowVelocity();
    }
    public void RoundVelocity()
    {
        velocity = velocity * 10;
        velocity = Mathf.Round(velocity);
        velocity = velocity / 10;
        velocity = velocity * 3.6f;
    }
    public void ShowVelocity()
    {
        velocityText.text = gameObject.GetComponent<Rigidbody>().velocity.y + "km/hr";//Pasar a positivo
    }

    public void GetAcceleretionValues()
    {
        finalAccel = startAccel;
        startAccel = velocity;
        finalATime = startATime;
        startATime = time;
    }
    public void GetAcceleration()
    {
        acceleration = (finalAccel - startAccel) / (finalATime- startATime);
        RoundAcceleration();
        ShowAcceleration();
    }
    public void RoundAcceleration()
    {
        acceleration = acceleration * 10;
        acceleration = Mathf.Round(acceleration);
        acceleration = acceleration / 10;
        acceleration = acceleration * 3.6f;
    }
    public void ShowAcceleration()
    {
        accelerationText.text = "Aceleracion: " + acceleration + "M/s";
    }
}

