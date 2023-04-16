using FishNet;
using FishNet.Object;
using FishNet.Object.Prediction;
using FishNet.Transporting;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.Core
{
    public class NetworkPlayer : NetworkController, IPlayer {
        // An event that is called when the player's look value changes
        public delegate void OnLookChanged(Vector2 look);
        public event OnLookChanged OnLookChangedEvent;
        
        // An event that is called when the player's move value changes
        public delegate void OnMoveChanged(Vector2 move);
        public event OnMoveChanged OnMoveChangedEvent;
        
        // An event that is called when the player's move function occurs
        public delegate void OnMove(Vector3 move);
        public event OnMove OnMoveEvent;
        
        // Input action map
        [SerializeField]
        private InputActionAsset inputActionMap;
        public InputActionAsset InputActionMap => inputActionMap;

        public IReconciliationStrategy ReconciliationStrategy { get; private set; }
        
        private Vector2 _move;
        private Vector2 _look;

        private void Awake() {
            /* Prediction is tick based so you must
             * send datas during ticks. You can use whichever
             * tick best fits your need, such as PreTick, Tick, or PostTick.
             * In most cases you will send/move using Tick. For rigidbodies
             * you will send using PostTick. I subscribe to ticks using
             * the InstanceFinder class, which finds the first NetworkManager
             * loaded. If you are using several NetworkManagers you would want
             * to subscrube in OnStartServer/Client using base.TimeManager. */
            InstanceFinder.TimeManager.OnTick += TimeManager_OnTick;

            ReconciliationStrategy ??= new DefaultReconciliationStrategy();
            BindInput();
        }
        
         private void TimeManager_OnTick()
        {
            if (base.IsOwner)
            {
                /* Call reconcile using default, and false for
                 * asServer. This will reset the client to the latest
                 * values from server and replay cached inputs. */
                ReconciliationStrategy.Perform(default, false, transform);
                /* CheckInput builds MoveData from user input. When there
                 * is no input CheckInput returns default. You can handle this
                 * however you like but Move should be called when default if
                 * there is no input which needs to be sent to the server. */
                CheckInput(out MoveData md);
                /* Move using the input, and false for asServer.
                 * Inputs are automatically sent with redundancy. How many past
                 * inputs will be configurable at a later time.
                 * When a default value is used the most recent past inputs
                 * are sent a predetermined amount of times. It's important you
                 * call Move whether your data is default or not. FishNet will
                 * automatically determine how to send the data, and run the logic. */
                Move(md, false);
            }
            if (base.IsServer)
            {
                /* Move using default data with true for asServer.
                 * The server will use stored data from the client automatically.
                 * You may also run any sanity checks on the input as demonstrated
                 * in the method. */
                Move(default, true);
                /* After the server has processed input you will want to send
                 * the result back to clients. You are welcome to skip
                 * a few sends if you like, eg only send every few ticks.
                 * Generate data required on how the client will reset and send it by calling your Reconcile
                 * method with the data, again using true for asServer. Like the
                 * Replicate method (Move) this will send with redundancy a certain
                 * amount of times. If there is no input to process from the client this
                 * will not continue to send data. */
                var rd = new ReconcileData(transform.position, transform.rotation);
                ReconciliationStrategy.Perform(rd, true, transform);
                //Reconciliation(rd, true, transform);
            }
        }
         
         // /// <summary>
         // /// A Reconcile attribute indicates the client will reconcile
         // /// using the data and logic within the method. When asServer
         // /// is true the data is sent to the client with redundancy,
         // /// and the server will not run the logic.
         // /// When asServer is false the client will reset using the logic
         // /// you supply then replay their inputs.
         // /// </summary>
         // [Reconcile]
         // private void Reconciliation(ReconcileData rd, bool asServer, Transform transform, Channel channel = Channel.Unreliable)
         // {
         //     transform.position = rd.Position;
         //     transform.rotation = rd.Rotation;
         // }
         
         /// <summary>
        /// A simple method to get input. This doesn't have any relation to the prediction.
        /// </summary>
         public void CheckInput(out MoveData md)
        {
            md = default;

            // return if _move is zero.
            if (_move == Vector2.zero || _look == Vector2.zero)
                return;

            //Make movedata with input.
            md = new MoveData(_move.x, _move.y);
        }

         // Create a virtual function to bind to the player's input action map.
         public virtual void BindInput()
        {
            if(!IsOwner)
                return;
            
            // Get the player's input action map
            var actionAsset = InputActionMap;
            
            // Get the player's input action map's "Move" action
            var moveAction = actionAsset.FindAction("Move");
            
            // Check that the move action exists
            if (moveAction == null)
            {
                Debug.LogError($"{GetControllerID()} | PlayerController::OnPlayerInput - Move action not found");
                return;
            }
            
            // Bind the player's input action map's "Move" action to the Move function
            moveAction.performed += MoveInput;
            
            // Get the player's input action map's "Look" action
            var lookAction = actionAsset.FindAction("Look");
            
            // Check that the look action exists
            if (lookAction == null)
            {
                Debug.LogError($"{GetControllerID()} | PlayerController::OnPlayerInput - Look action not found");
                return;
            }
            
            // Bind the player's input action map's "Look" action to the Look function
            lookAction.performed += LookInput;
        }

        [ServerRpc]
        private void ServerLook(InputAction.CallbackContext obj)
        {
            LookInput(obj);
        }

        public void LookInput(InputAction.CallbackContext obj)
        {
            // Get the player's input action map's "Look" action's value
            var lookValue = obj.ReadValue<Vector2>();
            
            // Store the look to a class variable
            _look = lookValue;
            
            // Call the OnLookChanged event
            OnLookChangedEvent?.Invoke(lookValue);
        }

        [ServerRpc]
        private void ServerMove(InputAction.CallbackContext obj)
        {
            MoveInput(obj);
        }

        public void MoveInput(InputAction.CallbackContext obj)
        {
            // Get the player's input action map's "Move" action's value
            var moveValue = obj.ReadValue<Vector2>();
            
            // Store the move to a class variable
            _move = moveValue;
            
            // Call the OnMoveChanged event
            OnMoveChangedEvent?.Invoke(moveValue);
        }
        
        /// <summary>
        /// Replicate attribute indicates the data is being sent from the client to the server.
        /// When Replicate is present data is automatically sent with redundancy.
        /// The replay parameter becomes true automatically when client inputs are
        /// being replayed after a reconcile. This is useful for a variety of things,
        /// such as if you only want to show effects the first time input is run you will
        /// do so when replaying is false.
        /// </summary>
        [Replicate]
        private void Move(MoveData md, bool asServer, Channel channel = Channel.Unreliable, bool replaying = false)
        {
            /* You can check if being run as server to
             * add security checks such as normalizing
             * the inputs. */
            if (asServer)
            {
                //Sanity check!
            }
            /* You may also use replaying to know
             * if a client is replaying inputs rather
             * than running them for the first time. This can
             * be useful because you may only want to run
             * VFX during the first input and not during
             * replayed inputs. */
            if (!replaying)
            {
                //VFX!
            }

            var move = new Vector3(md.Horizontal, 0f, md.Vertical);
            move *= (float)base.TimeManager.TickDelta;
            OnMoveEvent?.Invoke(move);
        }
    }
}