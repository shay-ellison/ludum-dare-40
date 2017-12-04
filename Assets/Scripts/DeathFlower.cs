using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathFlower : MonoBehaviour {
    public GameObject deathPollenPrefab;
    public int totalDeathPollen = 10;

    private BoxCollider2D deathFlowerCollider;
    private int pollenPerGrab = 1;
    private List<GameObject> deathPollens;

    public int GimmeDeathPollen()
    {
        if (totalDeathPollen > 0)  // Give pollen if there's any to give
        {
            totalDeathPollen -= pollenPerGrab;
            GameObject deadPollen = deathPollens[0];
            deathPollens.RemoveAt(0);
            Destroy(deadPollen);
            return pollenPerGrab;
        }
        else
        {
            return 0;
        }
    }

    // Use this for initialization
    void Start () {
        deathFlowerCollider = GetComponent<BoxCollider2D>();
        deathPollens = new List<GameObject>();

        int numberPollens = totalDeathPollen / pollenPerGrab;
        float radius = deathFlowerCollider.size.x / 3f;
        Vector3 center = transform.TransformPoint(deathFlowerCollider.offset);

        for (int i = 0; i < numberPollens; i++)
        {
            Vector3 pollenPosition = IndexCirclePoint(center, radius, i);
            deathPollens.Add(Instantiate(deathPollenPrefab, pollenPosition, Quaternion.identity, transform));
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private Vector3 IndexCirclePoint(Vector3 center, float radius, int index)
    {
        int numberPollens = totalDeathPollen / pollenPerGrab;
        float angleRadians = Mathf.Deg2Rad * (360f / numberPollens) * index;
        Vector3 pos = new Vector3(
            center.x + radius * Mathf.Cos(angleRadians),
            center.y + radius * Mathf.Sin(angleRadians),
            center.z);
        return pos;
    }
}
