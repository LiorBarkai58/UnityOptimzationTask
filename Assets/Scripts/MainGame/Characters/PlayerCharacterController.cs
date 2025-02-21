using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerCharacterController : MonoBehaviour
{
    public event UnityAction<int> onTakeDamageEventAction;
    [SerializeField] private UnityEvent<int> onTakeDamageEvent;

    [Header("Navigation")] 
    [SerializeField] private NavMeshAgent navMeshAgent;

    [SerializeField] private Transform[] pathWaypoints;
    
    [SerializeField] private Animator animator;

    [SerializeField] private Camera mainCamera;

    private static readonly string speedParameter = "Speed";
    private static readonly int SpeedHash = Animator.StringToHash(speedParameter);

    private const int AnimatorSecondLayer = 1;

    private const int MudNavigationLayer = 3;

    private const int ResistenceToLayerValue = 1;

    public int Hp
    {
        get => hp;
        set => hp = value;
    }

    public int CurrentWaypointIndex
    {
        get => currentWaypointIndex;
        set => currentWaypointIndex = value;
    }

    private bool isMoving = true;
    private int currentWaypointIndex = 0;

    private bool hasBloodyBoots = true;


    private int hp;
    private int startingHp;

    public void ToggleMoving(bool shouldMove)
    {
        isMoving = shouldMove;
        if (navMeshAgent) navMeshAgent.enabled = shouldMove;
    }

    public void SetDestination(Transform targetTransformWaypoint)
    {
        if (navMeshAgent)
            navMeshAgent.SetDestination(targetTransformWaypoint.position);
    }

    public void SetDestination(int waypointIndex)
    {
        SetDestination(pathWaypoints[waypointIndex]);
    }

    public void TakeDamage(int damageAmount)
    {
        hp -= damageAmount;
        float hpPercentLeft = (float) hp / startingHp;
        animator.SetLayerWeight(AnimatorSecondLayer, (1 - hpPercentLeft));
        onTakeDamageEvent.Invoke(hp);
        onTakeDamageEventAction?.Invoke(hp);
    }

    private void Start()
    {
        hp = 100;
        startingHp = hp;
        SetMudAreaCost();
        ToggleMoving(true);
        SetDestination(pathWaypoints[0]);
    }

    private void SetMudAreaCost()
    {
        if (hasBloodyBoots)
        {
            navMeshAgent.SetAreaCost(MudNavigationLayer, ResistenceToLayerValue);
        }
    }

    [ContextMenu("Take Damage Test")]
    private void TakeDamageTesting()
    {
        TakeDamage(10);
    }


    private void Update()
    {
        if (isMoving && !navMeshAgent.isStopped && navMeshAgent.remainingDistance <= 0.1f)
        {
            currentWaypointIndex++;
            if (currentWaypointIndex >= pathWaypoints.Length)
                currentWaypointIndex = 0;
            SetDestination(pathWaypoints[currentWaypointIndex]);
        }

        if (animator)
            animator.SetFloat(SpeedHash, navMeshAgent.velocity.magnitude);
        
        if (mainCamera != null)
        {
            // Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            // if (Physics.Raycast(ray, out RaycastHit hit, 100f))
            // {
            //     //We want to know what the mouse is hovering now
            //     Debug.Log($"Hit: {hit.collider.name}");
            // }
        }

    }
    
    
}