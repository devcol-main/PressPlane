using UnityEngine;


// 2d object scroll with repositioning
public class LoopScrollWithRepos : MonoBehaviour
{
    [Header("Settings")]
    [Tooltip("How fast should the object scroll / Range -0.5~10")]
    [Range((float)0.5, (float)10.0)]
    [SerializeField] private float scrollSpeed;

    //
    [SerializeField] private float startPosition;
    [SerializeField] private float endPosition;

    

}
