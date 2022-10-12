using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerGoal : MonoBehaviour
{
    public static bool isLoad;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player") && !isLoad)
        {
            isLoad = true;
            StartCoroutine(PlayerGoalIN());
        }
    }

    IEnumerator PlayerGoalIN()
    {
        GameObject goalParticle = GameObject.Instantiate(Resources.Load<GameObject>("PortalEffect"));
        goalParticle.transform.position = transform.position;

        int curStage = ++DataManager.Instance.User.stage;

        if (DataManager.Instance.User.maxStage < curStage)
            DataManager.Instance.User.maxStage = curStage;

        yield return new WaitForSecondsRealtime(4f);

        SceneManager.LoadScene("Main");
    }
}
