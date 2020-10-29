using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GridCursor : Singleton<GridCursor>
{
    public Vector3 offSet = new Vector3( 0.5f, 0.5f, 0);

    //Tiles where gridcursor can go to
    public static Tilemap selectableTileMap;

    public static bool trackCursor = true;
    public static Vector2 MousePositionScreen { get; private set; }
    public static Vector2 MousePositionWorld { get; private set; }
    public static Vector3Int GridPosition { get; private set; }
    public static Vector3 GridPositionWorld { get; private set; }
    public static Vector3 GridPositionOffset { get; private set; }
    
    private MouseInput mouseInput;
    private Camera mainCamera;
   
    private void Awake() {
        mouseInput = new MouseInput();
        selectableTileMap = GameObject.FindGameObjectWithTag("SelectableTile").GetComponent<Tilemap>();
        mainCamera = Camera.main;
    }

    private void OnEnable() {
        mouseInput.Enable();
    }

    private void OnDisable() {
        mouseInput.Disable();
    }

    private void Update() {
        TrackCursor();
    }

    private void TrackCursor() {
        if (!trackCursor) return;

        //Various mouse position coordinates
        MousePositionScreen = mouseInput.Mouse.MousePosition.ReadValue<Vector2>();

        MousePositionWorld = mainCamera.ScreenToWorldPoint(MousePositionScreen);

        GridPosition = selectableTileMap.WorldToCell(MousePositionWorld);

        if (selectableTileMap.HasTile(GridPosition)) {
            GridPositionOffset = GridPosition + offSet;
        }

        transform.position = GridPositionOffset;
        GridPositionWorld = transform.position;
    }

    public static Vector3Int WorldToGrid(Vector3 worldPosition) {
        return selectableTileMap.WorldToCell(worldPosition);
    }
}
