using System;

namespace ApiPerformanceComparison.Shared;

public class AtomicCounter
    {
        private int _value;

        public AtomicCounter(int initialValue = 0)
        {
            _value = initialValue;
        }

        public int GetNext()
        {
            return Interlocked.Increment(ref _value);
        }

        public int Current => _value;
    }
