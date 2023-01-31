using UnityEngine;

public class FoodPreparation : MonoBehaviour
{
    [SerializeField] BoxCollider2D gridArea;

    // Start is called before the first frame update
    void Start()
    {
        RePositionOfFood();
    }
    
    void RePositionOfFood()
    {
        Bounds bounds = gridArea.bounds;

        float xPosition = Random.Range(bounds.min.x, bounds.max.x);
        float yPosition = Random.Range(bounds.min.y, bounds.max.y);

        transform.position = new Vector3(xPosition, yPosition, 0f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            RePositionOfFood();
    }
}
