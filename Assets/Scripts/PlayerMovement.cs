using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Tilemaps;

public class PlayerMovement : MonoBehaviour
{
    [Serializable]
    public class StringEvent : UnityEvent<string> { }
    public StringEvent onInteract;

    private PlayerRenderer playerRenderer;

    private Grid grid;
    private Tilemap tilemap;
    private Tilemap tilemap_collider;
    private Tilemap tilemap_interactables;

    // Start is called before the first frame update
    void Start()
    {
        playerRenderer = gameObject.GetComponent<PlayerRenderer>();
        grid = GameObject.Find("Grid").GetComponent<Grid>();
        tilemap = GameObject.Find("Tilemap - Base").GetComponent<Tilemap>();
        tilemap_collider = GameObject.Find("Tilemap - Collider").GetComponent<Tilemap>();
        tilemap_interactables = GameObject.Find("Tilemap - Interactables").GetComponent<Tilemap>();
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2Int inputVector = new Vector2Int(0,0);
        if(Input.GetKeyDown(KeyCode.W))
        {
            inputVector.y = 1;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            inputVector.y = -1;
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            inputVector.x = -1;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            inputVector.x = 1;
        }

        if(inputVector != Vector2Int.zero) {
            // get coords of current cell player is in
            Vector3Int playerCell = grid.WorldToCell(transform.position);
            // get coords of new cell player will be in based on input vector direction
            Vector3Int newTileCell = playerCell + new Vector3Int(inputVector.x, inputVector.y, 0);
            
            // set player sprite direction
            playerRenderer.setDirection(inputVector);
            
            // check if player can move to new cell
            TileBase newTile;

            bool hasCollider = getNewTile(newTileCell, out newTile);

            if (hasCollider == true) {
                Debug.Log(newTile);
                OnCollisionCheckInteraction(newTile as Tile);
                return;
            }

            //  has to be tilemap.GetCellCenterWorld() because the grid is offset from the tilemap
            transform.position = tilemap.GetCellCenterWorld(newTileCell);
            // Debug.Log("Player moved to " + newTileCell + "at " + transform.position);

        }

    }


    private bool getNewTile(Vector3Int newTileCell, out TileBase newTile) {
        Debug.Log("newTileCell: " + newTileCell);
        if(tilemap_interactables.GetTile<Tile>(newTileCell) != null) {
            Debug.Log("Interactable");
            newTile = tilemap_interactables.GetTile<Tile>(newTileCell);
            return true;
        } else if(tilemap_collider.GetTile(newTileCell + Vector3Int.back) != null){
            Debug.Log("Collider");
            newTile = tilemap_collider.GetTile(newTileCell + Vector3Int.back);
            return true;
        } else {
            newTile = null;
            return false;
        }
    }

    private void OnCollisionCheckInteraction(Tile newTile) {

        if (newTile == null) {
            Debug.Log("No interaction");
            return;
        }
        string message = "";
        Debug.Log(newTile.sprite.name);
        // TODO: move this to separate module
        switch (newTile.sprite.name)
        {
            case "tile_044":
                message = "It's a nice bush.";
                break;
            case "tile_046":
                message = "It's a pretty flower.";
                break;
            case "tile_049": case "tile_050": case "tile_051":
                message = "It's a log.";
                break;
            case "tile_053": case "tile_054": case "tile_055": case "tile_056": case "tile_057": case "tile_058": case "tile_059": case "tile_060": case "tile_068":
                message = "It's a rock.";
                break;
            case "boar_SW_idle_2":
                message = "It's a friend.";
                break;
            default:
                break;
        }
        Debug.Log(message);
        onInteract.Invoke(message);
    }
}
