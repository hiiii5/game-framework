using System.Collections;
using Game.Core;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests.Runtime
{
    public class GameFrameworkTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [TearDown]
        public void Teardown()
        {
        }

        [UnityTest]
        public IEnumerator Controller_GetControllerID_TestResult()
        {
            // Arrange
            // Create a new game object
            var gameObject = new GameObject();
            IController controller = gameObject.AddComponent<Controller>();
            
            // Act
            yield return null;
            
            // Test
            Assert.IsNotNull(controller);
            Assert.IsNotEmpty(controller.GetControllerID());
        }
        
        [UnityTest]
        public IEnumerator Controller_GetState_TestResult()
        {
            // Arrange
            // Create a new game object
            var gameObject = new GameObject();
            IController controller = gameObject.AddComponent<Controller>();
            
            // Act
            yield return null;

            IGameState state = controller.GetState();
            
            // Test
            Assert.IsNotNull(controller);
            Assert.IsNotNull(state);
            Assert.IsInstanceOf(typeof(GameState), state);
        }
        
        [UnityTest]
        public IEnumerator NetworkState_GetNetworkManager_TestResult()
        {
            // Arrange
            // Create a new game object
            var gameObject = new GameObject();
            IController controller = gameObject.AddComponent<NetworkController>();
            
            // Act
            yield return null;

            IGameState state = controller.GetState();
            
            // Test
            Assert.IsNotNull(controller);
            Assert.IsNotNull(state);
            Assert.IsInstanceOf(typeof(NetworkState), state);
            Assert.IsNotNull(((NetworkState) state).GetNetworkManager());
        }
        
        [UnityTest]
        public IEnumerator NetworkController_OnStartClient_TestResult()
        {
            // Arrange
            // Create a new game object
            var gameObject = new GameObject();
            IController controller = gameObject.AddComponent<NetworkController>();
            
            // Act
            yield return null;

            IGameState state = controller.GetState();
            
            // Test
            Assert.IsInstanceOf(typeof(NetworkController), controller);
            Assert.IsInstanceOf(typeof(NetworkState), state);
            Assert.IsNotNull(gameObject.GetComponent<NetworkController>());
        }
    }
}
