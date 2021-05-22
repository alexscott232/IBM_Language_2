using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRaycasting : MonoBehaviour
{
    public float distanceToSee;
    RaycastHit what;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(this.transform.position, this.transform.forward * distanceToSee, Color.magenta);

        if(Physics.Raycast(this.transform.position, this.transform.forward, out what, distanceToSee))
        {
          Debug.Log("I touched " + what.collider.gameObject.name);
          if((what.collider.gameObject.name != "FirstPerson-AIO") && (what.collider.gameObject.name != "Terrain"))
          {
              //Destroy (what.collider.gameObject);
          }

        }
    }
}
