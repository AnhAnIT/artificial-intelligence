using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFLAController : MonoBehaviour
{
    [SerializeField]
    [Range(0f, 1f)]
    private float timeJump = 0.1f; 
    public GameObject frogPrefab; 
    public int numberOfFrogs = 20; 
    public int memeplexCount = 4; 
    public int iterations = 50; 
    public float searchSpaceSize = 10;
    private List<Frog> frogs = new List<Frog>();

    private void Start()
    {
        InitializeFrogs();
        StartCoroutine(Optimize());
    }

    private void InitializeFrogs()
    {
    
        for (int i = 0; i < numberOfFrogs; i++)
        {
            Vector3 position = new Vector3(
                Random.Range(-searchSpaceSize, searchSpaceSize),
                Random.Range(-searchSpaceSize, searchSpaceSize),
                0);
            GameObject frogObject = Instantiate(frogPrefab, position, Quaternion.identity);
            Frog frog = new Frog(frogObject, position, EvaluateFitness(position));
            frogs.Add(frog);

            UpdateFrogColor(frog);
        }
    }

    private float EvaluateFitness(Vector3 position)
    {

        return -Mathf.Pow(position.x, 2) - Mathf.Pow(position.y, 2); 
    }

    private IEnumerator Optimize()
    {
        for (int iter = 0; iter < iterations; iter++)
        {
         
            frogs.Sort((a, b) => b.fitness.CompareTo(a.fitness));

       
            List<List<Frog>> memeplexes = DivideIntoMemeplexes();

           
            foreach (var memeplex in memeplexes)
            {
                OptimizeMemeplex(memeplex);
            }

            
            ShuffleFrogs();

            yield return new WaitForSeconds(timeJump); 
        }
    }

    private List<List<Frog>> DivideIntoMemeplexes()
    {
        List<List<Frog>> memeplexes = new List<List<Frog>>();
        for (int i = 0; i < memeplexCount; i++)
            memeplexes.Add(new List<Frog>());

        for (int i = 0; i < frogs.Count; i++)
            memeplexes[i % memeplexCount].Add(frogs[i]);

        return memeplexes;
    }

    private void OptimizeMemeplex(List<Frog> memeplex)
    {
        Frog bestFrog = memeplex[0];
        Frog worstFrog = memeplex[memeplex.Count - 1];

        Vector3 newPosition = worstFrog.position + Random.Range(0.5f, 1.5f) * (bestFrog.position - worstFrog.position);

        newPosition.x = Mathf.Clamp(newPosition.x, -searchSpaceSize, searchSpaceSize);
        newPosition.y = Mathf.Clamp(newPosition.y, -searchSpaceSize, searchSpaceSize);

        float newFitness = EvaluateFitness(newPosition);

        if (newFitness > worstFrog.fitness)
        {
            worstFrog.position = newPosition;
            worstFrog.fitness = newFitness;
            worstFrog.UpdateGameObjectPosition();

            
            UpdateFrogColor(worstFrog);
        }
    }

    private void ShuffleFrogs()
    {
        // Trộn lẫn lại các ếch
        for (int i = 0; i < frogs.Count; i++)
        {
            int randomIndex = Random.Range(0, frogs.Count);
            var temp = frogs[i];
            frogs[i] = frogs[randomIndex];
            frogs[randomIndex] = temp;
        }
    }

    private void UpdateFrogColor(Frog frog)
    {
        // Thay đổi màu sắc của con ếch dựa trên fitness
        float maxFitness = Mathf.Abs(EvaluateFitness(Vector3.zero));
        Color frogColor = Color.Lerp(Color.green, Color.red, frog.fitness / maxFitness);
        frog.gameObject.GetComponent<Renderer>().material.color = frogColor;
    }
}

