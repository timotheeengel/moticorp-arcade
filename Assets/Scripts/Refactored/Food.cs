using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
        GameObject uiIcon = new GameObject();
        uiIcon.AddComponent<RawImage>().GetComponent<RawImage>().texture = icon;
        uiIcon.transform.parent = GameObject.Find("UI").transform;
        uiIcon.transform.position = new Vector3(Camera.main.WorldToScreenPoint(transform.position).x,1040);
        StartCoroutine(IconBob(uiIcon.transform, hangtime));

        yield return new WaitForSeconds(hangtime);
        GetComponent<Rigidbody>().useGravity = true;
    }

    IEnumerator IconBob(Transform target, float hangtime)
    {
        float time = 0;
        Vector3 basePos = target.position;
        while (time < (hangtime + 0.5f))
        {
            time += Time.deltaTime;
            target.position = new Vector3(basePos.x, basePos.y + Mathf.Sin(time*10) * 10, 0);
            yield return null;
        }
        Destroy(target.gameObject);
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
