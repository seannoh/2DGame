using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Tilemaps;

public class Player_Movement : MonoBehaviour
{
    [Serializable]
    public class StringEvent : UnityEvent<string> { }
    public StringEvent onInteract;

    public Sprite sprite_nw;
    public Sprite sprite_ne;
    public Sprite sprite_sw;
    public Sprite sprite_se;

    private Tilemap tilemap;
    private Tilemap tilemap_collider;

    // Start is called before the first frame update
    void Start()
    {
        tilemap = GameObject.Find("Tilemap").GetComponent<Tilemap>();
        tilemap_collider = GameObject.Find("Tilemap (Collider)").GetComponent<Tilemap>();
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2Int gridMoveDirection = new Vector2Int();
        Sprite newSpriteDirection = null;
        if(Input.GetKeyDown(KeyCode.W))
        {
            gridMoveDirection.y = 1;
            newSpriteDirection = sprite_nw;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            gridMoveDirection.y = -1;
            newSpriteDirection = sprite_se;
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            gridMoveDirection.x = -1;
            newSpriteDirection = sprite_sw;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            gridMoveDirection.x = 1;
            newSpriteDirection = sprite_ne;
        }

        if(gridMoveDirection != Vector2Int.zero) {
            Vector3Int playerCell = tilemap.WorldToCell(transform.position);
            Vector3Int newTileCell = playerCell + new Vector3Int(gridMoveDirection.x, gridMoveDirection.y, 0);
            Tile newTileDirection = Tile.CreateInstance<Tile>();
            newTileDirection.sprite = newSpriteDirection; 

            if (checkCollider(newTileCell)) {
                tilemap.SetTile(playerCell, null);
                tilemap.SetTile(playerCell, newTileDirection);
                OnCollisionCheckInteraction(newTileCell);
                return;
            }

            tilemap.SetTile(playerCell, null);
            tilemap.SetTile(newTileCell, newTileDirection);
            transform.position = tilemap.CellToWorld(newTileCell);
            gameObject.GetComponent<SpriteRenderer>().sprite = null;
        }

    }

    private bool checkCollider(Vector3Int newTileCell) {
        Tile tileCollider = tilemap_collider.GetTile<Tile>(newTileCell);
        Tile tileCollider2 = tilemap_collider.GetTile<Tile>(newTileCell + new Vector3Int(0, 0, -1));
        return tileCollider != null || tileCollider2 != null;
    }

    private void OnCollisionCheckInteraction(Vector3Int newTileCell) {
        Tile tileInteraction = tilemap_collider.GetTile<Tile>(newTileCell);
        if (tileInteraction == null) {
            return;
        }
        String message = "";
        Debug.Log(tileInteraction.sprite.name);
        switch (tileInteraction.sprite.name)
        {
            case "tile_044":
                Debug.Log("It's a nice bush.");
                message = "It's a nice bush.";
                break;
            case "tile_046":
                Debug.Log("It's a pretty flower.");
                message = "It's a pretty flower.";
                break;
            case "tile_049": case "tile_050": case "tile_051":
                Debug.Log("It's a log.");
                message = "It's a log.";
                break;
            case "tile_068":
                Debug.Log("It's a rock.");
                message = "It's a rock.";
                break;
            default:
                break;
        }
        onInteract.Invoke(message);
    }
}
