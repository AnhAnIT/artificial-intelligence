using UnityEngine;

public class Frog
{
    public GameObject gameObject;
    public Vector3 position;
    public float fitness;

    public Frog(GameObject gameObject, Vector3 position, float fitness)
    {
        this.gameObject = gameObject;
        this.position = position;
        this.fitness = fitness;
    }

    public void UpdateGameObjectPosition()
    {
        gameObject.transform.position = position;
    }
}
