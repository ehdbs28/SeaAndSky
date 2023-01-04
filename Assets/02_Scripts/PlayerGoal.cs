using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class PlayerGoal : MonoBehaviour
{
    public static bool isLoad = false;
    private Transform player;

    private void Awake() {
        player = GameObject.Find("Player").transform;
    }

    private void Start() {
        isLoad = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player") && !isLoad)
        {
            isLoad = true;
            collision.collider.GetComponent<PlayerAudio>()?.PlayClearPortalSound();
            StartCoroutine(PlayerGoalIN());
        }
    }

    IEnumerator PlayerGoalIN()
    {
        GameObject goalParticle = GameObject.Instantiate(Resources.Load<GameObject>("PortalEffect"));
        goalParticle.transform.position = player.position;

        int curStage = ++DataManager.Instance.User.stage;

        if(DataManager.Instance.User.maxStage < curStage){
            DataManager.Instance.User.maxStage = curStage;
            DataManager.Instance.User.maxStage = Mathf.Clamp(DataManager.Instance.User.maxStage, 1, DataManager.Instance.User.stageLimit);
        }

        yield return new WaitForSecondsRealtime(3.5f);

        isLoad = false;
        SceneChangeManager.Instance.LoadScene((curStage >= DataManager.Instance.User.stageLimit + 1) ? "MTitle" : "Main");
    }
}
