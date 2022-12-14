using UnityEngine;
using UnityEngine.Events;

public class Brick : MonoBehaviour
{
    public UnityEvent<int> onDestroyed;
    
    public int PointValue;
    private Renderer brickMat;

    void Start()
    {
        brickMat = GetComponent<Renderer>();
        var mat = brickMat.material;
        
        switch (PointValue)
        {
            case 1 :
                mat.SetColor("Green", Color.green);
                break;
            case 2:
                mat.SetColor("Yellow", Color.yellow);
                break;
            case 5:
                mat.SetColor("Blue", Color.blue);
                break;
            default:
                mat.SetColor("Red", Color.red);
                break;
        }
        
    }

    private void OnCollisionEnter(Collision other)
    {
        onDestroyed.Invoke(PointValue);
        
        //slight delay to be sure the ball have time to bounce
        Destroy(gameObject, 0.2f);
    }
}
