using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GridCursor : Singleton<GridCursor>
{
    public Vector3 offSet = new Vector3( 0.5f, 0.5f, 0);

    //Tiles where gridcursor can go to
    public Tilemap selectableTileMap;

    public static bool trackCursor = true;
    public static Vector2 mousePositionScreen { get; private set; }
    public static Vector2 mousePositionWorld { get; private set; }
    public static Vector3Int gridPosition { get; private set; }
    public static Vector3 gridPositionWorld { get; private set; }
    public static Vector3 gridPositionOffset { get; private set; }
    
    private MouseInput mouseInput;
    private Camera mainCamera;
   
    private void Awake() {
        mouseInput = new MouseInput();
    }

    private void OnEnable() {
        mouseInput.Enable();
    }

    private void OnDisable() {
        mouseInput.Disable();
    }

    private void Start() {
        mainCamera = Camera.main;
    }

    private void Update() {
        if (!trackCursor) return;

        //Various mouse position coordinates
        mousePositionScreen = mouseInput.Mouse.MousePosition.ReadValue<Vector2>();

        mousePositionWorld = mainCamera.ScreenToWorldPoint(mousePositionScreen);

        gridPosition = selectableTileMap.WorldToCell(mousePositionWorld);
     
        if (selectableTileMap.HasTile(gridPosition)) {
            gridPositionOffset = gridPosition + offSet;
        }
     
        transform.position = gridPositionOffset;
        gridPositionWorld = transform.position;
    }
}
