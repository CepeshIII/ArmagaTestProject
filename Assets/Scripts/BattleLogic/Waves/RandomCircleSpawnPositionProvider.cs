using UnityEngine;



public class RandomCircleSpawnPositionProvider
    : MonoBehaviour, ISpawnPositionProvider
{
    [SerializeField] private float maxRadius = 2f;


    public float MaxRadius => maxRadius;


    public Vector3 GetNextPosition()
    {
        Vector2 randomPoint = Random.insideUnitCircle * maxRadius;

        return new Vector3(
            transform.position.x + randomPoint.x,
            transform.position.y + randomPoint.y,
            transform.position.z
        );
    }


    public void SetMaxRadius(float maxRadius)
    {
        this.maxRadius = maxRadius;
    }
}



