using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{

    public GameObject backgroundAsset;

    public float width;
    public int halfNumBackgrounds = 1;

    public Transform player;

    List<GameObject> instantiatedBgs = new List<GameObject>();

    public float zDepth = 10;

    public float targetSpeedFractionX = 0.5f;
    public float targetSpeedFractionY = 0.5f;

    public float yOffset = -10;

	// Use this for initialization
	void Start ()
    {
        Vector3 startPos = player.transform.position - halfNumBackgrounds * width * Vector3.right;
        startPos.z = zDepth;
        startPos.y += yOffset;
        for (int i = 0; i < 2 * halfNumBackgrounds + 1; i++)
        {
            GameObject bgInstance = Instantiate(backgroundAsset, startPos + i * width * Vector3.right, Quaternion.identity);
            bgInstance.transform.parent = transform;
            instantiatedBgs.Add(bgInstance);
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.position += player.GetComponent<Entity>().GetHorizontalSpeed() * targetSpeedFractionX * Vector3.right * Time.deltaTime;
        transform.position += player.GetComponent<Entity>().GetVerticalSpeed() * targetSpeedFractionY * Vector3.up * Time.deltaTime;

        if (player.position.x > instantiatedBgs[halfNumBackgrounds].transform.position.x + width / 2)
        {
            GameObject firstBg = instantiatedBgs[0];
            firstBg.transform.position += (2 * halfNumBackgrounds +1) * width * Vector3.right;
            instantiatedBgs.Remove(firstBg);
            instantiatedBgs.Add(firstBg);
        }
        if (player.position.x < instantiatedBgs[halfNumBackgrounds].transform.position.x - width / 2)
        {
            GameObject lastBg = instantiatedBgs[instantiatedBgs.Count - 1];
            lastBg.transform.position += (2 * halfNumBackgrounds + 1) * width * Vector3.right;
            instantiatedBgs.Remove(lastBg);
            instantiatedBgs.Insert(0, lastBg);
        }
    }
}
