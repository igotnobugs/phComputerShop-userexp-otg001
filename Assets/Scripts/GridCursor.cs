using UnityEngine;
using UnityEngine.Tilemaps;

/* The GridCursor
 * Sets various values that might be improtant
 */

public class GridCursor : Singleton<GridCursor>
{
    public Vector3 offSetPosition = new Vector3(0.5f, 0.5f, 0);
    public static Vector3 offSet = new Vector3( 0.5f, 0.5f, 0);

    //Tiles where gridcursor can go to
    public static Tilemap selectableTileMap;

    //public static bool trackCursor = true;
    [SerializeField] public static Vector2 MousePositionScreen { get; private set; }
    [SerializeField] public static Vector2 MousePositionWorld { get; private set; }
    [SerializeField] public static Vector3Int GridPosition { get; private set; }
    [SerializeField] public static Vector3 GridPositionWorld { get; private set; }
    [SerializeField] public static Vector3 GridPositionOffset { get; private set; }
    [SerializeField] public static Vector3 GridPositionActual { get; private set; }

    private MouseInput mouseInput;
    private Camera mainCamera;
    private SpriteRenderer gridRenderer;
   
    private void Awake() {
        mouseInput = new MouseInput();

        if (selectableTileMap == null) {
            GameObject go = GameObject.FindGameObjectWithTag("SelectableTile");
            selectableTileMap = go.GetComponent<Tilemap>();
        }

        gridRenderer = GetComponent<SpriteRenderer>();
        mainCamera = Camera.main;
        offSet = offSetPosition;
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
        //Various mouse position coordinates
        MousePositionScreen = mouseInput.Mouse.MousePosition.ReadValue<Vector2>();

        MousePositionWorld = mainCamera.ScreenToWorldPoint(MousePositionScreen);

        GridPosition = selectableTileMap.WorldToCell(MousePositionWorld);
        
        if (selectableTileMap.HasTile(GridPosition)) {
            GridPositionOffset = GridPosition + offSet;
            GridPositionActual = GridPositionOffset;
            Color newColor = gridRenderer.color;
            newColor.a = 0.75f;
            gridRenderer.color = newColor;
        } else {
            Color newColor = gridRenderer.color;
            newColor.a = 0;
            gridRenderer.color = newColor;
        }

        transform.position = GridPositionOffset;
        GridPositionWorld = transform.position;
    }

    public static Vector3 WorldToGrid(Vector3 worldPosition, bool applyOffset = true) {
        Vector3 position = selectableTileMap.WorldToCell(worldPosition);

        if (applyOffset) position += offSet;

        return position;
    }
}
