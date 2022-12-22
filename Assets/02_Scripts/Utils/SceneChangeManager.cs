using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangeManager : MonoBehaviour
{
    [SerializeField] private List<Animator> _transitions = new List<Animator>();

    private WaitForSecondsRealtime _sceneLoadWaitingTime = new WaitForSecondsRealtime(1f);

    private void Awake() {
        foreach(GameObject obj in GameObject.FindGameObjectsWithTag("Transitions")){
            _transitions.Add(obj.GetComponent<Animator>());
        }

        DontDestroyOnLoad(gameObject);
    }

    public void LoadScene(string sceneValue){
        foreach(Animator anim in _transitions){
            anim.Play("TransitionUp");
        };
        StartCoroutine(SceneLoadCoroutine(sceneValue));
    }

    private IEnumerator SceneLoadCoroutine(string sceneValue){
        yield return _sceneLoadWaitingTime;

        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneValue);

        while(!operation.isDone){
            yield return null;
        }

        yield return _sceneLoadWaitingTime;

        _transitions.Clear();
        foreach(GameObject obj in GameObject.FindGameObjectsWithTag("Transitions")){
            _transitions.Add(obj.GetComponent<Animator>());
        }

        foreach(Animator anim in _transitions){
            anim.Play("TransitionDown");
        }
        GameManager.Instance.TileManager.InitTileSetting();
    }
}
