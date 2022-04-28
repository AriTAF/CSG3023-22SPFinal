/**** 
 * Created by: Bob Baloney
 * Date Created: April 20, 2022
 * 
 * Last Edited by: Krieger 
 * Last Edited: April 28, 2022
 * 
 * Description: Paddle controler on Horizontal Axis
****/

/*** Using Namespaces ***/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    public float speed = 10f; //speed of paddle


    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Horizontal") != 0f)
        {
            //if moving on the horizontal axis, move the paddle according to its speed over the change in time
            float xPos = Input.GetAxis("Horizontal");

            xPos *= speed;
            xPos *= Time.deltaTime;

            Vector3 pos = new Vector3(xPos, 0f, 0f);

            transform.position += pos;
        }
    }//end Update()
}
