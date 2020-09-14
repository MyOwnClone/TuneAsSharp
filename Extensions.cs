using System;

namespace TweakAsSharp
{
    public static class Extensions {
        public static void Times(this int n, Action action)
        {
            if (action == null) return;
            
            for (var i = 0; i < n; ++i)
            {
                action();
            }
        }
        
        public static void Times(this int n, Action<int> action)
        {
            if (action == null) return;
            
            for (var i = 0; i < n; ++i)
            {
                action(i);
            }
        }

    }
}