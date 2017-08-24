using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {

    // movement speed of AI
    public float speed;
    //roation speed of AI
    public float rotationSpeed;
    //distance between AI needed before they can flock
    public float distanceBetween;

    //average movement direction of the flock
    Vector3 flockDirection;
    //average postion of flock
    Vector3 flockPosition;

    //bool used for turning
    public bool isTurning = false;

	// Use this for initialization
	void Start ()
    {
        //randoming set speed variable between 1 and 4
        //used so that each AI has a different speed
        speed = Random.Range(1.0f, 4.0f);
	}
	
	// Update is called once per frame
	void Update ()
    {
        //checking to see if AI has hit a boundary
        if (Vector3.Distance(transform.position, Vector3.zero) >= FlockingManager.worldBoundaries)
        {
            //set isTurning to true
            isTurning = true;
        }
        else
        {
            //else isTurning is set to false
            isTurning = false;
        }

        //checking if is turning true
        if (isTurning == true)
        {
            //change the direction the Ai is moving towards
            Vector3 direction = Vector3.zero - transform.position;
            //change AI rotation to the new movement direction
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), rotationSpeed * Time.deltaTime);

            //randomly set new new speed between 1 and 4
            speed = Random.Range(1.0f, 4.0f);
        }

        //else isTurning is not true
        else
        {
            //checking if random interger between 0 and 8 is lower than 1
            if (Random.Range(0, 8) < 1)
            {
                //called this function
                Flocking();
            }
        }
        //Move Ai withoiut uising rigidbody
        transform.Translate(0, 0, speed * Time.deltaTime);
    }

    //Function controls AI flocking logic
    void Flocking()
    {
        //create and array of game objects
        GameObject[] objects;
        //set the game objects to the same ones that where spawned
        objects = FlockingManager.prefabsToSpawn;

        //set the center point of the flock to 0
        Vector3 flockCentre = Vector3.zero;
        //set the other objects postion to 0
        Vector3 otherObject = Vector3.zero;
        //set flock target to target
        Vector3 target = FlockingManager.target;

        //speed of AI flock
        float flockSpeed = 0.01f;
        //distance variable
        float distance;

        //number of game object in the flock
        int flockCount = 0;

        //checking how many game objects are in the array
        foreach (GameObject gObject in objects)
        {
            //checking if the game object added is not itself
            if (gObject != this.gameObject)
            {
                //distance variable is set to the distance between AI
                distance = Vector3.Distance(gObject.transform.position, this.transform.position);

                //checking if distance is less than the distanceBetween
                //used to check if AI is about to join the flock
                if(distance <= distanceBetween)
                {
                    //increase the centre of the flock by the new AI added
                    flockCentre += gObject.transform.position;
                    //incease the amount of game objects in the flock
                    flockCount++;

                    //checking if distance is below 1
                    if (distance < 1.0f)
                    {
                        //move away from the other game object
                        //used so that game objects dont get too close together
                        otherObject = otherObject + (this.transform.position - gObject.transform.position);
                    }

                    //can create more than one flock of game objects
                    Movement anotherFlock = gObject.GetComponent<Movement>();
                    //change the average speed of the flock
                    flockSpeed = flockSpeed + anotherFlock.speed;
                }
            }
        }

        //checking if flock size is greater than zero
        if (flockCount > 0)
        {
            //calculating the centre of the flock
            flockCentre = flockCentre / flockCount + (target - this.transform.position);
            //calculating AI spedd depending on the flock speed
            speed = flockSpeed / flockCount;

            //calculating flock direction
            Vector3 direction = (flockCentre + otherObject) - transform.position;

            //checking if diection is equal to 0
            if (direction != Vector3.zero)
            {
                //change rotation towards moving direction
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), rotationSpeed * Time.deltaTime);
            }
        }
    }
}
