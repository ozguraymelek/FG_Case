using System;
using Cinemachine;
using GenericScriptableArchitecture;
using nyy.FG_Case.FSMBuilder;
using nyy.FG_Case.ReferenceValue;
using Sirenix.OdinInspector;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace nyy.FG_Case.PlayerSc
{
    public class Player : MonoBehaviour
    {
        #region PROPERTIES

        [BoxGroup("Finite State Machine Properties")][DisableIn(PrefabKind.All)]
        [SerializeField] private PlayerBaseState initState;
        
        [BoxGroup("Finite State Machine Properties")]
        [InfoBox("Will set via script")][DisableIn(PrefabKind.All)]
        public PlayerBaseState CurrentState;
        
        [TabGroup("A","Interact")]  public Rigidbody rb;
        [TabGroup("A","Art")] public Animator animator;
        
        [TabGroup("B","Settings")] public Vector3Ref MovementVector;
        [TabGroup("B","Settings")] public float walkSpeed;
        [TabGroup("B","Settings")] public float runSpeed;
        [TabGroup("B","Settings")] public float rotationSpeed;
        
        [TabGroup("A","Events")]
        [InfoBox("Joystick takes the active input value the moment it is triggered.")]
        public ScriptableEvent OnJoystickTriggered;

        #endregion

        #region EVENT FUNCTIONS

        private void Awake()
        {
            CurrentState = initState;
        }

        private void Start()
        {
            this.ObserveEveryValueChanged(player => player.CurrentState).Subscribe(unit =>
            {
                CurrentState.Onset(this);
            });

            this.FixedUpdateAsObservable().Subscribe(unit =>
            {
                OnJoystickTriggered.Invoke();
                CurrentState.Updating(this);
            });
        }

        #endregion

        #region IMPLEMENTED FUNCTIONS

        #endregion

        #region PUBLIC FUNCTIONS

        #endregion

        #region PRIVATE FUNCTIONS
    
        #endregion
    }
}