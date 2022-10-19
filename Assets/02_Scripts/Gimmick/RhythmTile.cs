using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class RhythmTile : MonoBehaviour
{
    [SerializeField] private Transform[] _tiles;
    [SerializeField] private float _tileChangeTime = 1f;

    private void Start() {
        _tiles = GetComponentsInChildren<Transform>();

        StartCoroutine(RhythmTileCoroutine());
    }

    IEnumerator RhythmTileCoroutine(){
        int count = 0;

        while(true){
            for(int i = 1; i < _tiles.Length; i++){
                if(i % 2 == ((count % 2 == 0) ? 0 : 1)){
                    _tiles[i].gameObject.SetActive(false);
                }
                else{
                    _tiles[i].gameObject.SetActive(true);
                }
            }

            yield return new WaitForSeconds(_tileChangeTime);
            count++;    
        }
    }
}
