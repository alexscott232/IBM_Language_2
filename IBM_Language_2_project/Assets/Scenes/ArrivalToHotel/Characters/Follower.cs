using UnityEngine;
using PathCreation;

public class Follower : MonoBehaviour
{

    public PathCreator PathCreator;
    public float speed = 5;
    float distanceTravelled;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        distanceTravelled += speed * Time.deltaTime;
        transform.position = PathCreator.path.GetPointAtDistance(distanceTravelled);
       
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, PathCreator.path.GetRotationAtDistance(distanceTravelled).eulerAngles.y, transform.rotation.eulerAngles.z);

    }
}
