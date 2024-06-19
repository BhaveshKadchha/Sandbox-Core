#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.LowLevel;
using UnityEngine.PlayerLoop;

namespace Sandbox.CustomLoop
{
    internal static class ProcessingUpdateBootstrapper
    {
        static PlayerLoopSystem processingSystem;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterAssembliesLoaded)]
        internal static void Initialize()
        {
            PlayerLoopSystem currentPlayerLoop = PlayerLoop.GetCurrentPlayerLoop();
            InsertProcessingUpdate<Update>(ref currentPlayerLoop, 0, ProcessingUpdate.Tick);
            InsertProcessingUpdate<FixedUpdate>(ref currentPlayerLoop, 0, ProcessingUpdate.FixedTick);
            InsertProcessingUpdate<PostLateUpdate>(ref currentPlayerLoop, 0, ProcessingUpdate.LateTick);
            
            PlayerLoop.SetPlayerLoop(currentPlayerLoop);
            PlayerLoopUtils.PrintPlayerLoop(currentPlayerLoop);

#if UNITY_EDITOR
            EditorApplication.playModeStateChanged -= OnPlayModeState;
            EditorApplication.playModeStateChanged += OnPlayModeState;
#endif

            static void OnPlayModeState(PlayModeStateChange state)
            {
                if (state == PlayModeStateChange.ExitingPlayMode)
                {
                    PlayerLoopSystem current = PlayerLoop.GetCurrentPlayerLoop();
                    RemoveProcessingUpdate<Update>(ref current);
                    RemoveProcessingUpdate<FixedUpdate>(ref current);
                    RemoveProcessingUpdate<PostLateUpdate>(ref current);
                    PlayerLoop.SetPlayerLoop(current);

                    ProcessingUpdate.Clear();
                }
            }
        }

        static void RemoveProcessingUpdate<T>(ref PlayerLoopSystem loop)
        {
            PlayerLoopUtils.RemoveSystem<T>(ref loop, in processingSystem);
        }

        static bool InsertProcessingUpdate<T>(ref PlayerLoopSystem loop, int index)
        {
            processingSystem = new PlayerLoopSystem()
            {
                type = typeof(ProcessingUpdate),
                updateDelegate = ProcessingUpdate.Tick,
                subSystemList = null
            };
            return PlayerLoopUtils.InsertSystem<T>(ref loop, in processingSystem, index);
        }
        
        static void InsertProcessingUpdate<T>(ref PlayerLoopSystem loop, int index, PlayerLoopSystem.UpdateFunction function)
        {
            processingSystem = new PlayerLoopSystem()
            {
                type = typeof(ProcessingUpdate),
                updateDelegate = function,
                subSystemList = null
            };

            if (!PlayerLoopUtils.InsertSystem<T>(ref loop, in processingSystem, index))
                Debug.LogWarning($"ProcessingUpdate not Initialize, unable to register into {typeof(T).Name} loop.");
        }
    }
}