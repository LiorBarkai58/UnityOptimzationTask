using UnityEngine;

public class ArrowObject : MonoBehaviour
{
    public float speed;
    public float damage;


    // Update is called once per frame
    void Update()
    {
        transform.Translate(transform.forward * speed * Time.deltaTime);
    }
}
