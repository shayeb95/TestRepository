using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatForm : MonoBehaviour
{
    //Dimensions
    public int x;
    public int y;


    //Tile sprite
    public GameObject platformTile;

    //Nine sliced tiles
    public List<GameObject> top = new List<GameObject>();
    public List<GameObject> middle = new List<GameObject>();
    public List<GameObject> bottom = new List<GameObject>();

    //Dimensions of Tile Sprite
    public float width;
    public float height;

    public List<GameObject> instantiatedTiles = new List<GameObject>();



    // Use this for initialization
    void Start()
    {
        
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    public void GeneratePlatform()
    {
        for (int i = 0; i < x; i++)

        {
            for (int j = 0; j < y; j++)

            {
                float xPos = width / 2 + i * width;
                float yPos = -height / 2 - j * height;
                Vector3 position = new Vector3(xPos, yPos, 0);
                position += this.transform.position;

                GameObject tileToSpawn;
                List<GameObject> layerTiles;

                if (j == 0)
                {
                    layerTiles = top;

                }
                else if (j == y - 1)
                {
                    layerTiles = bottom;

                }
                else
                {
                    layerTiles = middle;

                }

                if (i == 0)
                {
                    tileToSpawn = layerTiles[0];
                    
                }
                else if (i == x - 1)
                {
                    tileToSpawn = layerTiles[2];
                    
                }
                else
                {
                    tileToSpawn = layerTiles[1];
                    
                }
                

                GameObject instantiatedTile = Instantiate(tileToSpawn, position, Quaternion.identity);
                instantiatedTile.transform.parent = this.transform;
                //instantiatedTiles.Add(instantiatedTile);

            }
        }
        CreateCollider();
    }

    public void DestroyExistingTiles()
    {
        for (int i = 0; i < instantiatedTiles.Count; i++)
        {
            DestroyImmediate(instantiatedTiles[i]);
        }
        instantiatedTiles = new List<GameObject>();
    }

    public void CreateCollider()
    {
        if (!GetComponent<BoxCollider2D>())
        {
            gameObject.AddComponent<BoxCollider2D>();
        }

        BoxCollider2D myCollider = GetComponent<BoxCollider2D>();
        myCollider.size = new Vector2(x * width, y * height);
        myCollider.offset = new Vector2(x * width / 2, -y * height / 2);

    }
}
