using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;


public class World{

    Tile[,] tiles;
    int width;
    int height;


    public World(int width = 10, int height = 10){
        this.width = width;
        this.height = height;

        tiles = new Tile[height, width];
        for(int  x = 0;  x < width; x++){
            for(int z = 0; z < height; z++){
                tiles[x, z] = new Tile(this, x, z);
                tiles[x, z].setTileType(Tile.TileType.Empty);
            }
        }
        Debug.Log("World created with " + (width * height) + "tiles.");
    }

    public Tile GetTileAt(int x, int z){
        if(x>width || x<0 || z >height || z < 0)
        {
            return null;
        }
        return tiles[x, z];
    }
    public int getHeight()
    {
        return height;
    }
    public int getWidth()
    {
        return width;
    }

}
