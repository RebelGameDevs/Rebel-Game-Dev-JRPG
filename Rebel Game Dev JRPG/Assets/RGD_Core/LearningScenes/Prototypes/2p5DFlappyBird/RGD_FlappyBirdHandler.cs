using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RGD_FlappyBirdHandler : MonoBehaviour
{
    [SerializeField] private Rigidbody flappysRB;
    [SerializeField] private float PipeSpeed = 1f;
    [SerializeField] private float range = 0.45f;
    [SerializeField] private float PipeSpawnRate;
    [SerializeField] private Transform PREFAB_Pipe;
    [SerializeField] private Canvas canvas;
    private List<Transform> PipesSpawned = new List<Transform>();
    private Vector3 playButtonStartSize;
    private Coroutine UICo = null;
    private void Update()
    {
        foreach (Transform pipe in PipesSpawned) pipe.position += Vector3.left * PipeSpeed * Time.deltaTime;
    }
    private void Awake()
    {
        playButtonStartSize = canvas.transform.Find("BC/PLAY").localScale;
        flappysRB.constraints = RigidbodyConstraints.FreezePosition;
    }
    private IEnumerator Spawner()
    {
        flappysRB.constraints = RigidbodyConstraints.None;
        flappysRB.constraints = RigidbodyConstraints.FreezePositionX;
        flappysRB.constraints = RigidbodyConstraints.FreezePositionZ;

        while (true)
        {
            SpawnPipe();
            yield return new WaitForSeconds(PipeSpawnRate);
        }
    }
    private void SpawnPipe()
    {
        var item = Instantiate(PREFAB_Pipe, transform.position + new Vector3(0, Random.Range(-range, range), 0), transform.rotation);
        PipesSpawned.Add(item);
        StartCoroutine(DestroyAfterSomeTime(item));
    }
    private IEnumerator DestroyAfterSomeTime(Transform item)
    {
        yield return new WaitForSeconds(10);
        PipesSpawned.Remove(item);
        Destroy(item.gameObject);
    }
    private void DeleteAfterTime(Transform item)
    {

    }
    public void UIAction(string action)
    {
        action = action.ToLower();
        if(UICo is not null) StopCoroutine(UICo);
        switch(action)
        {
            case "hovering":
                UICo = StartCoroutine(Hovered(true));
                break;

            case "stophovering":
                UICo = StartCoroutine(Hovered(false));
                break;

            case "pressed":
                StartCoroutine(LerpOutCanvas());
                StartCoroutine(Hovered(false));
                canvas.transform.Find("BC/PLAY").GetComponent<Image>().raycastTarget = false;
                StartCoroutine(Spawner());
                break;

            default:
                Debug.LogWarning("Unknown UIAction Type");
                break;
        }
    }
    private IEnumerator LerpOutCanvas()
    {
        var obj = canvas.transform.Find("BC");
        Vector3 currentSize = obj.transform.localScale;
        float localTTime = 0;
        while(localTTime < 1)
        {
            localTTime += Time.deltaTime;
            obj.transform.localScale = Vector3.Lerp(currentSize, Vector3.zero, localTTime);
            yield return null;
        }
        obj.transform.localScale = Vector3.zero;
    }
    private IEnumerator Hovered(bool type)
    {
        var button = canvas.transform.Find("BC/PLAY");
        Vector3 currentSize = button.localScale;
        Vector3 targetSize = type ? playButtonStartSize * 1.1f : playButtonStartSize;
        float currentRotationZ = button.localEulerAngles.z;
        float localTTime = 0;
        while(localTTime < 1)
        {
            localTTime += Time.deltaTime / 0.1f;
            button.localScale = Vector3.Lerp(currentSize, targetSize, localTTime);
            button.rotation = Quaternion.Euler(0, 0, Mathf.Lerp(currentRotationZ, type ? 5f : 0, localTTime));
            yield return null;
        }
        button.localScale = targetSize;
        button.rotation = Quaternion.Euler(0, 0, type ? 5 : 0);
    }
        
}
