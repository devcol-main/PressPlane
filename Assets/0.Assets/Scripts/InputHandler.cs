using UnityEngine;
using UnityEngine.InputSystem;

//[RequireComponent(typeof(CharacterController))]
public class InputHandler : MonoBehaviour
{
    [Header("Input Action Asset")]
    //private CharacterController characterController;
    [SerializeField] private InputActionAsset InputAction;

    [Header("Action Map Name References")] 
    [SerializeField] [ReadOnly] private string actionMapName = "Player";

    [Header("Action Name References")]
    [SerializeField] [ReadOnly] private string Fly = "Fly";

    //private InputAction flyAction;
    public InputAction flyAction;

    private void Awake()
    {
        flyAction = InputAction.FindAction(Fly);
    }

    void OnEnable()
    {
        InputAction.FindActionMap(actionMapName).Enable();
    }
    void OnDisable()
    {
        InputAction.FindActionMap(actionMapName).Disable();
    }

}
