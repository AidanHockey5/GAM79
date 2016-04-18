using UnityEngine;
using UnityEngine.Networking;
using System;
using System.Collections;

//[RequireComponent(typeof(CharacterController))]
//[RequireComponent(typeof(CameraController))]
//[RequireComponent(typeof(AnimatorCharacterController))]
public class PlayerObject : NetworkBehaviour, IEventBroadcaster, IEventListener
{
    // Summary: PlayerObject exists on each player object (each client is guaranteed one object with isLocalPlayer = true)
    // PlayerObject acts as the "antenna" of the player object by designating a known entry point for each player object -
    // The purpose of this method is to avoid GameObject.SendMessage() - which is unpredictable and only allows one parameter,
    //      and instead use GameEventArgs.eventArgs which is a params obj array

    #region Public Fields

    [Header("Sync Variables")]
    [SyncVar(hook = "OnChangeHealth")]
    public int currentHealth = 0;
    [SyncVar]
    public bool isAlive = false;

    [Header("Settings (Initialization Only)")]
    public InputSettings inputSettings;
    public PlayerSettings playerSettings;
    public CameraSettings cameraSettings;
	public MovementSettings movementSettings;
    public AnimatorSettings animatorSettings;
    public WeaponSettings[] equippableWeapons;

    #endregion

    #region Private Members

	private Transform cameraTarget = null;
    private WeaponSettings currentWeapon = null;
    private Vector3 tarDir = Vector3.zero;
    private Vector3 velocity = Vector3.zero;
    private CharacterController charController = null;
    private CameraController camController = null;
    private AnimatorCharacterController animCharController = null;

    #endregion

    #region MonoBehaviours
    void Start()
    {
        if (isLocalPlayer)
        {
            charController = GetComponent<CharacterController>();
            camController = GetComponent<CameraController>();
            animCharController = GetComponent<AnimatorCharacterController>();
            FindCamera();
            isAlive = true;
            currentHealth = playerSettings.maxHealth;
            Subscribe();
        }
    }
    
    void OnDestroy()
    {
        if (isLocalPlayer)
        {
            EventHandlerMainDelegate = null;
            UnSubscribe();
        }
    }
    #endregion

    #region Net Code

    void OnChangeHealth(int health)
    {
        // Update UI
    }

    [ClientRpc]
    void RpcTookDamage(int amount)
    {
		if(isLocalPlayer)
		{
	        Debug.Log("TOOKDAMAGE " + netId);
	        currentHealth -= amount;

	        if (currentHealth <= 0)
	        {
				OnDeath();
	        }
		}
    }

	public void OnDeath()
	{
		print("Something should have died");

		GameManager.Instance.OnPlayerDeath();

		if (GameManager.Instance.CanSpawn (1))
		{
			this.transform.position = SpawnPointManager.Instance.SpawnPointLocation ();
			GameManager.Instance.OnCharacterSpawn (1);
		}
		else
		{
			Debug.LogError ("You ran out of tickets");
		}
	}

	public void TakeDamage(GameEvent attacker, int amount)
	{
        if (!isServer)
            return;

        if (attacker == GameEvent.HIT_FROM_HUMAN)
        {
            RpcTookDamage(amount);
        }
	}
    
	void FindCamera()
	{
        if (camController != null)
        {
            Transform cameraTarget = transform.Find("CameraTarget");

            if (cameraTarget != null)
            {
                camController.SetCameraTarget(cameraTarget.transform, cameraSettings);
            }
            else
            {
                Debug.LogError("CameraTarget not found on player prefab.");
            }
        }
        else
        {
            Debug.LogError("No CameraController found.");
        }
    }
    #endregion

    #region IEventBroadcaster
    public event EventHandler<GameEventArgs> EventHandlerMainDelegate;

    public void RegisterHandler(EventHandler<GameEventArgs> EventHandlerDelegate)
    {
        EventHandlerMainDelegate += EventHandlerDelegate;
    }
    public void UnRegisterHandler(EventHandler<GameEventArgs> EventHandlerDelegate)
    {
        EventHandlerMainDelegate -= EventHandlerDelegate;
    }

    public void BroadcastEvent(GameEvent eventType, params object[] args)
    {
        GameEventArgs gameEventArgs = new GameEventArgs();
        gameEventArgs.eventType = eventType;
        gameEventArgs.eventArgs = args;

        if (EventHandlerMainDelegate != null)
        {
            EventHandlerMainDelegate(this, gameEventArgs);
        }
    }
    #endregion

    #region IEventListener
    public void Subscribe()
    {
        if (isLocalPlayer)
        {
            InputManager.Instance.RegisterHandler(ReceiveBroadcast);
        }
    }

    public void UnSubscribe()
    {
        if (isLocalPlayer)
        {
            InputManager.Instance.UnRegisterHandler(ReceiveBroadcast);
        }
    }

    public void ReceiveBroadcast(object sender, GameEventArgs gameEventArgs)
    {
        BroadcastEvent(gameEventArgs.eventType, gameEventArgs.eventArgs);
    }
    #endregion
}
