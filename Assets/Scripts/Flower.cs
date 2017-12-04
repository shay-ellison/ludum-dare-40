using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flower : MonoBehaviour {
    public GameObject pollenPrefab;
    public int totalPollen = 10;

    private BoxCollider2D flowerCollider;
    private int pollenPerGrab = 1;
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
        float radius = flowerCollider.size.x / 3f;
        Vector3 center = transform.TransformPoint(flowerCollider.offset);
  
        for (int i = 0; i < numberPollens; i++)
        {
            Vector3 pollenPosition = IndexCirclePoint(center, radius, i);
            pollens.Add(Instantiate(pollenPrefab, pollenPosition, Quaternion.identity, transform));
        }
    }

    private Vector3 IndexCirclePoint(Vector3 center, float radius, int index)
    {
        int numberPollens = totalPollen / pollenPerGrab;
        float angleRadians = Mathf.Deg2Rad * (360f / numberPollens) * index;
        Vector3 pos = new Vector3(
            center.x + radius * Mathf.Cos(angleRadians),
            center.y + radius * Mathf.Sin(angleRadians),
            center.z);
        return pos;
    }
}
