using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Sandbox.Helper
{
    public static class CoroutineExtensions
    {
        static MonoBehaviour monoBehaviour;

        public static Coroutine WaitTill(this MonoBehaviour behaviour, Action action, float waitTime)
        {
            monoBehaviour = behaviour;
            return behaviour.StartCoroutine(I_WaitTill(action, CoroutineHelper.GetWaitForSeconds(waitTime)));
        }
        public static Coroutine WaitTill(this MonoBehaviour behaviour, Action action, YieldInstruction instruction)
        {
            monoBehaviour = behaviour;
            return behaviour.StartCoroutine(I_WaitTill(action, instruction));
        }
        static IEnumerator I_WaitTill(Action action, YieldInstruction yield)
        {
            yield return yield;
            action();
        }


        public static Coroutine ExtendBy(this Coroutine coroutine, Action action, float waitTime) => monoBehaviour.StartCoroutine(I_ExtendBy(coroutine, action, CoroutineHelper.GetWaitForSeconds(waitTime)));
        public static Coroutine ExtendBy(this Coroutine coroutine, Action action, YieldInstruction instruction) => monoBehaviour.StartCoroutine(I_ExtendBy(coroutine, action, instruction));
        static IEnumerator I_ExtendBy(this Coroutine coroutine, Action action, YieldInstruction instruction)
        {
            yield return coroutine;
            yield return instruction;
            action?.Invoke();
        }
    }

    public class CoroutineHelper
    {
        static readonly Dictionary<float, WaitForSeconds> WaitForSeconds = new Dictionary<float, WaitForSeconds>();

        public static WaitForSeconds GetWaitForSeconds(float seconds)
        {
            if (WaitForSeconds.TryGetValue(seconds, out var forSeconds)) return forSeconds;

            var waitForSeconds = new WaitForSeconds(seconds);
            WaitForSeconds.Add(seconds, waitForSeconds);
            return waitForSeconds;
        }
    }
}