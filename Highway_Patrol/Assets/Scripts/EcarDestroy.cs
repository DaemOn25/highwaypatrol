using UnityEngine;

public class EcarDestroy : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "EnemyCar")
        {
            Destroy(col.gameObject);
        }
    }
}
