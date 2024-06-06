using System.Collections.Generic;

namespace Sandbox.CustomLoop
{
    public class ProcessingUpdate
    {
        static List<ITick> ticks = new();
        static List<IFixedTick> fixedticks = new();
        static List<ILateTick> lateticks = new();

        static int tickCount, lateTickCount, fixedTickCount;

        public static void Subscribe<T>(T val)
        {
            if (val is ITick t && !ticks.Contains(t))
            { 
                ticks.Add(t);
                tickCount = ticks.Count;
            }
            if (val is IFixedTick f && !fixedticks.Contains(f))
            {
                fixedticks.Add(f);
                fixedTickCount = fixedticks.Count;
            }
            if (val is ILateTick l && !lateticks.Contains(l))
            {
                lateticks.Add(l);
                lateTickCount = lateticks.Count;
            }
        }

        public static void Unsubscribe<T>(T val)
        {
            if (val is ITick t && ticks.Contains(t))
            {
                ticks.Remove(t);
                tickCount = ticks.Count;
            }
            if (val is IFixedTick f && fixedticks.Contains(f))
            {
                fixedticks.Remove(f);
                fixedTickCount = fixedticks.Count;
            }
            if (val is ILateTick l && lateticks.Contains(l))
            {
                lateticks.Remove(l);
                lateTickCount = lateticks.Count;
            }
        }


        internal static void Tick()
        {
            for (int i = 0; i < tickCount; i++) ticks[i].Tick();
        }

        internal static void FixedTick()
        {
            for (int i = 0; i < fixedTickCount; i++) fixedticks[i].FixedTick();
        }

        internal static void LateTick()
        {
            for (int i = 0; i < lateTickCount; i++) lateticks[i].LateTick();
        }

        internal static void Clear()
        {
            ticks.Clear();
            fixedticks.Clear();
            lateticks.Clear();
            tickCount = 0;
            lateTickCount = 0;
            fixedTickCount = 0;
        }
    }


    public interface ITick
    {
        public void Tick();
    }

    public interface IFixedTick
    {
        public void FixedTick();
    }

    public interface ILateTick
    {
        public void LateTick();
    }
}