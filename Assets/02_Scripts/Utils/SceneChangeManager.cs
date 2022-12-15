using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangeManager : MonoBehaviour
{
    [SerializeField] private List<Animator> _transitions = new List<Animator>();

    private WaitForSeconds _sceneLoadWaitingTime = new WaitForSeconds(0.7f);

    private void Awake() {
        foreach(GameObject obj in GameObject.FindGameObjectsWithTag("Transitions")){
            _transitions.Add(obj.GetComponent<Animator>());
        }
    }

    public void LoadScene(string sceneValue){
        foreach(Animator anim in _transitions){
            anim.Play("TransitionUp");
        }

        StartCoroutine(SceneLoadCoroutine(sceneValue));
    }

    private IEnumerator SceneLoadCoroutine(string sceneValue){
        yield return _sceneLoadWaitingTime;

        AsyncOperation operation = SceneManager.LoadSceneAsync(SceneManager.GetSceneByName(sceneValue).buildIndex);

        while(operation.isDone is false){
            yield return null;
        }

        yield return _sceneLoadWaitingTime;

        foreach(Animator anim in _transitions){
            anim.Play("TransitionDown");
        }
    }
}
