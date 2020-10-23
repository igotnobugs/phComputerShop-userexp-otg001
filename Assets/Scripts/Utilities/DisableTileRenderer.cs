using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DisableTileRenderer : MonoBehaviour 
{
    private TilemapRenderer _tilemapRenderer;

    private void Start() 
	{
        _tilemapRenderer = GetComponent<TilemapRenderer>();
        _tilemapRenderer.enabled = false;
    }
}
