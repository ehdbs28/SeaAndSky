using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileManager : MonoBehaviour
{
    [SerializeField] private List<Tilemap> _StageTileMaps;

    public void InitTileSetting(){
        _StageTileMaps.Clear();
        foreach(Tilemap tilemap in FindObjectsOfType<Tilemap>()){
            _StageTileMaps.Add(tilemap);
        }
    }

    public bool ThereAreNoTiles(Vector3 pos){
        if(_StageTileMaps.Count == 0) return false;

        foreach(Tilemap tilemap in _StageTileMaps){
            Vector3Int tilePos = tilemap.WorldToCell(pos);
            if(tilemap.GetTile(tilePos)){
                return false;
            }
        }

        return true;
    }
}
