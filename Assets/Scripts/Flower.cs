using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flower : MonoBehaviour {
    public GameObject pollenPrefab;
    public int totalPollen = 10;

    private BoxCollider2D flowerCollider;
    private int pollenPerGrab = 2;  // always 2?
    private List<GameObject> pollens;

    public int GimmePollen()
    {
        if (totalPollen > 0)  // Give pollen if there's any to give
        {
            totalPollen -= pollenPerGrab;
            GameObject deadPollen = pollens[0];
            pollens.RemoveAt(0);
            Destroy(deadPollen);
            return pollenPerGrab;
        } else
        {
            return 0;
        }
    }

    // Use this for initialization
    void Start () {
        flowerCollider = GetComponent<BoxCollider2D>();
        pollens = new List<GameObject>();

        int numberPollens = totalPollen / pollenPerGrab;
        float radius = 0.25f;
        Vector3 center = transform.TransformPoint(flowerCollider.offset);
  
        for (int i = 0; i < numberPollens; i++)
        {
            Vector3 pollenPosition = RandomCirclePoint(center, radius);
            pollens.Add(Instantiate(pollenPrefab, pollenPosition, Quaternion.identity, transform));
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private Vector3 RandomCirclePoint(Vector3 center, float radius)
    {
        float ang = Random.value * 360;
        Vector3 pos;
        pos.x = center.x + radius * Mathf.Sin(ang * Mathf.Deg2Rad);
        pos.y = center.y + radius * Mathf.Cos(ang * Mathf.Deg2Rad);
        pos.z = center.z;
        return pos;
    }
}
