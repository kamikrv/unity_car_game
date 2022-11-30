using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class MoveObject : MonoBehaviour
{
    [SerializeField] Transform[] Positions;
    [SerializeField] Transform Car;
    [SerializeField] float objectSpeed;
    bool isCarClose = false;
    [SerializeField] BoxCollider[] box;
    [SerializeField] UIController uIController;
    int CountOfFalling = 0;
    bool startMove = true;
    bool calledFall = false;

    int nextPosIndex;
    Animator MyAnimator;
    Transform nextPos;
    // Start is called before the first frame update
    void Start()
    {
        nextPos = Positions[0];
        MyAnimator = transform.GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!isCarClose && startMove)
        {
            MoveGameObject();
        }
         
        else if(isCarClose && startMove)
         {
            RunAway();
        }
        CheckFalling();
        DistanceToCar();
    }
    void MoveGameObject()
    {

        if (Vector3.Distance(transform.position,  nextPos.position)< 0.1f)
        {
            nextPosIndex++;

            if (nextPosIndex >= Positions.Length)
            {
                nextPosIndex = 0;
            }
            nextPos = Positions[nextPosIndex];
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, nextPos.position, objectSpeed * Time.deltaTime);
            // Determine which direction to rotate towards
            Vector3 targetDirection = nextPos.position - transform.position;

            // The step size is equal to speed times frame time.
            float singleStep = 1 * Time.deltaTime;

            // Rotate the forward vector towards the target direction by one step
            Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, singleStep, 0.0f);

            // Draw a ray pointing at our target in
            Debug.DrawRay(transform.position, newDirection, Color.red);

            // Calculate a rotation a step closer to the target and applies rotation to this object
            transform.rotation = Quaternion.LookRotation(newDirection);
        }
    }

    void HitCount()
    {  
        CountOfFalling++;
        Debug.Log(CountOfFalling);
    }

    void CheckFalling()
    {
        if((transform.rotation.x > 20  || transform.rotation.x < -20)
            || (transform.rotation.y > 20 || transform.rotation.y < -20)
            || (transform.rotation.z > 20 || transform.rotation.z < -20))
        {
            var rotationVector = transform.rotation.eulerAngles;
            rotationVector= Vector3.zero;
            transform.rotation = Quaternion.Euler(rotationVector);
        }
    }

   void FallingDown()
    {
        //Debug.Log("dist=" + Vector3.Distance(transform.position, Car.position));
        //if (Vector3.Distance(transform.position, Car.position) < 3 && calledFall ==false)
        //{
            //calledFall = true;
            //HitCount();
            //MyAnimator.SetTrigger("FallDown");
            //isCarClose = true;
            //box[0].enabled = false;
            //box[1].enabled = false;
            //transform.GetComponent<Rigidbody>().isKinematic = true;
            //uIController.ChangeStar(CountOfFalling);
       // }
         
    }
    void ResetCharacter()
    {
        calledFall = false;
        box[0].enabled = true;
        box[1].enabled = true;
        transform.GetComponent<Rigidbody>().isKinematic = false;
        startMove = true;
    }

   void DistanceToCar()
    {
       if (Vector3.Distance(transform.position, Car.position) < 6)
        {
            MyAnimator.SetBool("Run", true);
            isCarClose = true;
        }
        else if (Vector3.Distance(transform.position, Car.position) > 10)
        {
         MyAnimator.SetBool("Run", false);
            isCarClose = false;
        }
    }

    void RunAway()
    {
        Vector3 Direction = (transform.position - Car.position).normalized;
        transform.position = Vector3.MoveTowards(transform.position, Direction, objectSpeed * Time.deltaTime * 4);

        float singleStep = 1 * Time.deltaTime;

        // Rotate the forward vector towards the target direction by one step
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, Direction, singleStep, 0.0f);

        // Calculate a rotation a step closer to the target and applies rotation to this object
        transform.rotation = Quaternion.LookRotation(Direction);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.name == "Car" && calledFall == false) {
            calledFall = true;
            HitCount();
            MyAnimator.SetTrigger("FallDown");
            isCarClose = true;
            startMove = false;
            box[0].enabled = false;
            box[1].enabled = false;
            transform.GetComponent<Rigidbody>().isKinematic = true;
            uIController.ChangeStar(CountOfFalling);
        }
    }
}
