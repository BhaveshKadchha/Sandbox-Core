using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.LowLevel;

namespace Sandbox.CustomLoop
{
    public static class PlayerLoopUtils
    {
        public static void RemoveSystem<T>(ref PlayerLoopSystem loop, in PlayerLoopSystem systemToRemove)
        {
            if (loop.subSystemList == null) return;

            var playerLoopSystemList = new List<PlayerLoopSystem>(loop.subSystemList);
            for(int i = 0; i < playerLoopSystemList.Count; ++i) 
            {
                if (playerLoopSystemList[i].type == systemToRemove.type &&
                    playerLoopSystemList[i].updateDelegate == systemToRemove.updateDelegate)
                {
                    playerLoopSystemList.RemoveAt(i);
                    loop.subSystemList = playerLoopSystemList.ToArray();
                }
            }

            HandleSubSystemLoopForRemoval<T>(ref loop, systemToRemove);
        }

        static void HandleSubSystemLoopForRemoval<T>(ref PlayerLoopSystem loop, PlayerLoopSystem systemToRemove)
        {
            if(loop.subSystemList == null) return;

            for (int i = 0; i < loop.subSystemList.Length; ++i)
                RemoveSystem<T>(ref loop.subSystemList[i], systemToRemove);
        }

        public static bool InsertSystem<T>(ref PlayerLoopSystem loop, in PlayerLoopSystem systemToInsert, int index)
        {
            if (loop.type != typeof(T)) return HandleSubSystem<T>(ref loop, in systemToInsert, index);

            var playerLoopSystem = new List<PlayerLoopSystem>();
            if (loop.subSystemList != null) playerLoopSystem.AddRange(loop.subSystemList);
            playerLoopSystem.Insert(index, systemToInsert);
            loop.subSystemList = playerLoopSystem.ToArray();
            return true;
        }

        static bool HandleSubSystem<T>(ref PlayerLoopSystem loop, in PlayerLoopSystem systemToInsert, int index)
        {
            if (loop.subSystemList == null) return false;

            for (int i = 0; i < loop.subSystemList.Length; ++i)
            {
                if (InsertSystem<T>(ref loop.subSystemList[i], in systemToInsert, index))
                    return true;
            }

            return false;
        }


        public static void PrintPlayerLoop(PlayerLoopSystem loop)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("Unity Player Loop");

            foreach (PlayerLoopSystem subSystem in loop.subSystemList)
                PrintSubsystem(subSystem, sb, 0);

            Debug.Log(sb.ToString());
        }

        static void PrintSubsystem(PlayerLoopSystem system, StringBuilder sb, int level)
        {
            sb.Append(' ', level * 2).AppendLine(system.type.ToString());
            if (system.subSystemList == null || system.subSystemList.Length == 0) return;

            foreach (PlayerLoopSystem subSystem in system.subSystemList)
                PrintSubsystem(subSystem, sb, level + 1);
        }
    }
}