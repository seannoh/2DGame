using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Tilemaps;

public class PlayerMovement : MonoBehaviour
{
    private PlayerRenderer playerRenderer;

    private Grid grid;
    private Tilemap tilemap;
    private Tilemap tilemap_collider;
    private Tilemap tilemap_interactables;

    private InteractionPanel interactionPanel;

    private bool canMove = false;

    // Start is called before the first frame update
    void Start()
    {
        playerRenderer = gameObject.GetComponent<PlayerRenderer>();
        grid = GameObject.Find("Grid").GetComponent<Grid>();
        tilemap = GameObject.Find("Tilemap - Base").GetComponent<Tilemap>();
        tilemap_collider = GameObject.Find("Tilemap - Collider").GetComponent<Tilemap>();
        tilemap_interactables = GameObject.Find("Tilemap - Interactables").GetComponent<Tilemap>();
        interactionPanel = InteractionPanel.Instance();
        interactionPanel.interactionEvent.AddListener(OnInteractionEvent);
    }

    // Update is called once per frame
    void Update()
    {
        if(!canMove) {
            return;
        }
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
            GameObject interactableObj;

            bool hasCollider = getNewTile(newTileCell, out interactableObj);

            if (hasCollider == true) {
                Debug.Log(interactableObj);
                OnCollisionCheckInteraction(interactableObj);
                return;
            }

            //  has to be tilemap.GetCellCenterWorld() because the grid is offset from the tilemap
            transform.position = tilemap.GetCellCenterWorld(newTileCell);
            // Debug.Log("Player moved to " + newTileCell + "at " + transform.position);

        }

    }


    private bool getNewTile(Vector3Int newTileCell, out GameObject interactableObj) {
        Debug.Log("newTileCell: " + newTileCell);
        Collider2D collider = Physics2D.OverlapPoint(tilemap_interactables.CellToWorld(newTileCell));
        if(collider != null) {
            Debug.Log("Interactable");
            interactableObj = collider.gameObject;
            return true;
        } 
        else if(tilemap_collider.GetTile(newTileCell + Vector3Int.back) != null){
            Debug.Log("Collider");
            interactableObj = null;
            return true;
        } 
        else {
            interactableObj = null;
            return false;
        }
    }

    private void OnCollisionCheckInteraction(GameObject interactableObj) {

        if (interactableObj == null) {
            Debug.Log("No interaction");
            return;
        }
        Debug.Log(interactableObj.gameObject.name);

        Interactable interactable = interactableObj.GetComponent<Interactable>();
        if (interactable != null) {
            interactable.Interact();
        }
        else {
            Debug.Log("No interaction");
        }
        // TODO: move this to separate module

        // switch (interactableObj.gameObject.name)
        // {
        //     case "tile_044":
        //         message = "It's a nice bush.";
        //         break;
        //     case "tile_046":
        //         message = "It's a pretty flower.";
        //         break;
        //     case "tile_049": case "tile_050": case "tile_051":
        //         message = "It's a log.";
        //         break;
        //     case "tile_053": case "tile_054": case "tile_055": case "tile_056": case "tile_057": case "tile_058": case "tile_059": case "tile_060": case "tile_068":
        //         message = "It's a rock.";
        //         break;
        //     case "boar_SW_idle_2":
        //         message = "It's a friend.";
        //         break;
        //     default:
        //         break;
        // }
        // Debug.Log(message);
        // onInteract.Invoke(message);
    }

    private void OnInteractionEvent(bool interactionEvent) {
        this.canMove = !interactionEvent;
    }
}
