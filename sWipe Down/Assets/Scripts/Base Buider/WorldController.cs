using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class WorldController : MonoBehaviour
{
    [SerializeField] Sprite emptysprite;
    public World World { get; protected set; }
    public static WorldController Instance { get; protected set; }

    // Start is called before the first frame update
    void Start()
    {
        World = new World();
        this.MakeTiles();
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void MakeTiles()
    {
        for (int x = 0; x < World.getWidth(); x++)
        {
            for (int z = 0; z < World.getHeight(); z++)
            {
                Tile currenttile = World.GetTileAt(x, z);

                GameObject tilen = new GameObject();
                tilen.name = "Tile_" + x + " " + z;
                tilen.transform.position = new Vector3(currenttile.GetX(), 0, currenttile.GetZ());
                tilen.transform.Rotate(90, 0, 0);

                SpriteRenderer tile_spriterenderer = tilen.AddComponent<SpriteRenderer>();

                if (currenttile.gettiletype() == Tile.TileType.Empty)
                {
                    tile_spriterenderer.sprite = emptysprite;
                }

            }
        }
    }
    void OnTileTyoeChange(Tile td, GameObject tile)
    {
        if(td.gettiletype() == Tile.TileType.Empty)
        {
            tile.GetComponent<SpriteRenderer>().sprite = emptysprite; 
        }

        else if (td.gettiletype() == Tile.TileType.Empty)
        {
            tile.GetComponent<SpriteRenderer>().sprite = null;
        }
    }
}
