using Unity.VisualScripting;
using UnityEngine;

public class EnvironmentManager : MonoBehaviour
{
    public static EnvironmentManager Instance { get; private set; }

    [SerializeField] private GameObject environmentParent;

    [SerializeField] private float initialScrollSpeed = 3f;

    [Range(0f, 8f)]
    [SerializeField] private float globalScrollSpeed;    


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); 
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); 
        }
    }

    private void Start()
    {
        globalScrollSpeed = initialScrollSpeed;

        SetScrollSpeed(initialScrollSpeed);
    }


    public void SetScrollSpeed(float speed)
    {               

        LoopScroll[] loopScrolls = environmentParent.GetComponentsInChildren<LoopScroll>();        
        if (null != loopScrolls)
        {
            Debug.Log("SetScrollSpeed act ");
            foreach (LoopScroll loop in loopScrolls)
            {
                loop.ScrollSpeed = speed;
            }
        }
        
    }
    
    #if UNITY_EDITOR
        // For testing in editor
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Debug.Log("UpArrow Pressed, increase scroll speed");
                
                globalScrollSpeed += 1f;
                SetScrollSpeed(globalScrollSpeed);
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            Debug.Log("DownArrow Pressed, decrease scroll speed");
                globalScrollSpeed -= 1f;
                if (globalScrollSpeed < 0f) globalScrollSpeed = 0f;
                SetScrollSpeed(globalScrollSpeed);
            }
        }
    #endif  
}
