using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpBall : MonoBehaviour
{
    public Vector3 Acceleration;
    public float ExistTime;
    private BallForce ballForce;
    private float GravityValue = 1f;//重力
    private float GravityMargin = 1.35f;//质量

    private Transform BallP;
    // Start is called before the first frame update
    void Start()
    {
        BallP = GameObject.Find("BallP").transform;
        ballForce = BallP.transform.GetChild(0).GetComponent<BallForce>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 force = ballForce.GetForce();
        float mass = ballForce.GetMass();
        Acceleration = force / mass;
        this.ExistTime += Time.deltaTime*0.1f;
        float x = Acceleration.x * ExistTime;
        float y = -1 / 2f * GravityValue / mass * Mathf.Pow(ExistTime, 2) * GravityMargin + Acceleration.y * ExistTime;

        float StartPostionX;
        float StartPositionY;
        StartPostionX = ballForce.GetPosition().x;
        StartPositionY = ballForce.GetPosition().y;
        this.transform.position = new Vector3(x + StartPostionX, y + StartPositionY, this.transform.position.z);

        if (Input.GetMouseButtonUp(0))
        {
            Destroy(gameObject);
        }

    }
}

