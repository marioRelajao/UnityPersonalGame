using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{
    [SerializeField] string lvlToLoad;
    [SerializeField] Animator transition;
    [SerializeField] Transform spawnPoint;

    // Start is called before the first frame update
    private void Start()
    {
        PutPlayerInPoint();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            StartCoroutine(LoadLevel(lvlToLoad));
        }
    }
    IEnumerator LoadLevel(string levelName)
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(levelName);

    }

    public void PutPlayerInPoint()
    {
        PlayerController.instance.transform.position = spawnPoint.position;
    }
}
