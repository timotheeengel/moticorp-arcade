using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour {

    [SerializeField] string displayName = "testname";
    [SerializeField] int points;
    [SerializeField] int id;
    [SerializeField] Texture icon;

    public void Flight(Vector3 target, float speed, float hangtime)
    {
        StartCoroutine(Trajectory(target, speed, hangtime));
    }

    IEnumerator Trajectory(Vector3 target, float speed, float hangtime)
    {
        GetComponent<Rigidbody>().useGravity = false;
        Vector3 movement = transform.position - target;
        Vector3 upwardsMovement = new Vector3(0,3,0);//TODO magic number
        while (true)
        {
            movement = target - transform.position;
            if (movement.magnitude < 0.1)
            {
                transform.Translate(FireTarget.instance.GetOffset(), Space.World);
                break;
            }
            transform.Translate((movement.normalized * speed + upwardsMovement) * Time.deltaTime, Space.World);
            
            yield return null;
        }
        yield return new WaitForSeconds(hangtime);

        
        GetComponent<Rigidbody>().useGravity = true;
    }

    public int GetPointValue()
    {
        return points;
    }

    public string GetName()
    {
        return displayName;
    }

    public int GetID()
    {
        return id;
    }

    public void SetID(int newID)
    {
        id = newID;
    }

    public Texture GetIcon()
    {
        return icon;
    }
}
