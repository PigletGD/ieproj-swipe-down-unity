using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using UnityEngine;

public class Tile {
    public enum TileType { Occupied, Empty };
    TileType Type = TileType.Empty;
    Action<Tile> cbTileTypeChanged;
    InstalledObjects installedObjects;
    public bool selected = false;
    World world;
    int x;
    int z;

    public Tile(World world, int x, int z) {
        this.world = world;
        this.x = x;
        this.z = z;
    }

    public TileType gettiletype()
    {
        return this.Type;
    }

    public void setTileType(TileType type)
    {
        this.Type = type;
        if(cbTileTypeChanged != null)
            cbTileTypeChanged(this);
    }

    public float GetX()
    {
        return this.x;
    }
    public float GetZ()
    {
        return this.z;
    }

    public void RegisterTileTypeChangedCallback(Action<Tile> callback)
    {
        cbTileTypeChanged += callback;
    }

    public void UnRegisterTileTypeChangedCallback(Action<Tile> callback)
    {
        cbTileTypeChanged -= callback;
    }
}

