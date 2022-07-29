using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawObject : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] GameObject[] circle;
    [SerializeField] float timeDelay;
    [SerializeField] float timeDelayMin;
    [SerializeField] float timeDelayMax;

    [SerializeField] Transform minPos;
    [SerializeField] Transform maxPos;

    private int times;
    private float time;

    void Start()
    {
        
    }
    [SerializeField] bool isX = false;
    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;

        if(time > timeDelay)
        {
            timeDelay = Random.Range(timeDelayMin, timeDelayMax);
            time = 0;
            Spaw();

            times++;
            if (isX)
            {
                if (times > 5)
                {
                    timeDelayMin -= 0.5f;
                    timeDelayMax -= 0.5f;
                    if (timeDelayMin < 1.5f) timeDelayMin = 1.5f;
                    if (timeDelayMax < 3f) timeDelayMax = 3f;
                    times = 0;
                }
            }
        }
    }

    public void Spaw()
    {
        GameObject obj = Instantiate(circle[Random.Range(0, circle.Length)], Vector2.zero, Quaternion.identity);
        obj.GetComponent<CircleController>().RandomColor();
        obj.transform.SetParent(transform);
        //obj.transform.localPosition = Vector2.zero;
        obj.transform.localScale = Vector3.one;

        float x = Random.Range(minPos.position.x, maxPos.position.x);
        float y = Random.Range(minPos.position.y, maxPos.position.y);
        obj.transform.position = new Vector2(x, y);

        
    }
}
