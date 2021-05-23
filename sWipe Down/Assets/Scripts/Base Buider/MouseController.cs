using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseController : MonoBehaviour
{
    [SerializeField] GameObject cursor;
    [SerializeField] Sprite tilesprite;
    [SerializeField] Sprite selectedsprite;
    Vector3 startDragPosition;
    private void Update()
    {
        Vector3 currframepositon =(Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,Input.mousePosition.y, Camera.main.nearClipPlane)));
        currframepositon.y = 1;
        Tile tileunder = GetTileAtWorldCoor(currframepositon);
        if (tileunder != null){

            cursor.SetActive(true);
            Vector3 currposition = new Vector3(tileunder.GetX(), 0, tileunder.GetZ());
            cursor.transform.position = currposition;
        }
        else
        {
            cursor.SetActive(false);
        }
        if (Input.GetMouseButtonDown(0))
        {
            startDragPosition = currframepositon;
        }
        if (Input.GetMouseButtonUp(0))
        {
            int start_x = Mathf.FloorToInt(startDragPosition.x);
            int end_x = Mathf.FloorToInt(currframepositon.x);
            if (end_x < start_x)
            {
                int tmp = end_x;
                end_x = start_x;
                start_x = tmp;
            }
            int start_y = Mathf.FloorToInt(startDragPosition.y);
            int end_y = Mathf.FloorToInt(currframepositon.y);
            if (end_y < start_y)
            {
                int tmp = end_y;
                end_y = start_y;
                start_y = tmp;
            }
            for (int x = start_x; x <= end_x ; x++){
            }
            
            //if (tileunder != null)
            //{
                /*GameObject target = GameObject.Find("Tile_" + tileunder.GetX() + " " + tileunder.GetZ());
                if (tileunder.selected == false)
                {
                    tileunder.selected = true;
                    target.GetComponent<SpriteRenderer>().sprite = selectedsprite;
                }


                else if (tileunder.selected == true)
                {
                    tileunder.selected = false;
                    target.GetComponent<SpriteRenderer>().sprite = tilesprite;
                }*/
           // }
        }
    }

    Tile GetTileAtWorldCoor(Vector3 coor)
    {
        int x = Mathf.FloorToInt(coor.x);
        int z = Mathf.FloorToInt(coor.z);

        return WorldController.Instance.World.GetTileAt(x, z);
    }
}

