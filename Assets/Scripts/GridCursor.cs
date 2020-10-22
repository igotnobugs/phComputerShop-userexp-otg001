using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GridCursor : Singleton<GridCursor>
{
    public Vector3 _offSet = new Vector3( 0.5f, 0.5f, 0);

    //Tiles where gridcursor can go to
    public Tilemap _SelectableTileMap;
    
    public static Vector2 _mousePositionScreen;
    public static Vector2 _mousePositionWorld;
    public static Vector3Int _gridPosition;
    public static Vector3 _gridPositionOffset = new Vector3();
    
    private MouseInput _mouseInput;
    private Camera _mainCamera;
   
    private void Awake() {
        _mouseInput = new MouseInput();
    }

    private void OnEnable() {
        _mouseInput.Enable();
    }

    private void OnDisable() {
        _mouseInput.Disable();
    }

    private void Start() {
        _mainCamera = Camera.main;
    }


    private void Update() {

        //Various mouse position coordinates

        _mousePositionScreen = _mouseInput.Mouse.MousePosition.ReadValue<Vector2>();

        _mousePositionWorld = _mainCamera.ScreenToWorldPoint(_mousePositionScreen);

        _gridPosition = _SelectableTileMap.WorldToCell(_mousePositionWorld);

        if (_SelectableTileMap.HasTile(_gridPosition)) {
            _gridPositionOffset = _gridPosition + _offSet;
        }

        transform.position = _gridPositionOffset;
    }
}
