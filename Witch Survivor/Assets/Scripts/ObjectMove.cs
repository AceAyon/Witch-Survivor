using UnityEngine;

public class ObjectMove : MonoBehaviour
{
    Vector3 startPos;
    public Vector3 endPos;
    public float speed;
    float timeElapsed = 0.0f;
    bool willMove = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position == startPos || willMove)
        {
            willMove = true;
            timeElapsed += Time.deltaTime;
            transform.position = Vector3.Lerp(startPos, endPos, timeElapsed * speed);
        }
        if (transform.position == endPos)
        {
            willMove = false;
            timeElapsed = 0.0f;
            (endPos, startPos) = (startPos, endPos);
        }
    }
}
