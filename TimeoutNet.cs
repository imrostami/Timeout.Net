using System.Collections.Concurrent;

namespace Timeout.Net
{
    public class TimeoutContext<TKey, TValue>
    {
        private readonly ConcurrentDictionary<TKey, Tuple<TValue, Task>> _dictionary = new ConcurrentDictionary<TKey, Tuple<TValue, Task>>();
        public delegate void KeyChange(TKey key);

        public event KeyChange OnScheduledItemExpired;
        public event KeyChange OnItemScheduled;

        public TimeoutNet()
        {
            this.OnScheduledItemExpired += TimeOutStatics.DefaultScheduledExpired;
        }

        public void SetTimeout(TKey key, TValue value, TimeSpan timeSpan)
        {
            Task timeoutTask = Task.Delay(timeSpan).ContinueWith(t =>
            {
                lock (_dictionary)
                {
                    if (_dictionary.TryRemove(key, out _))
                    {
                        if (OnScheduledItemExpired != null)
                            OnScheduledItemExpired(key);
                    }
                }
            });

            _dictionary.AddOrUpdate(key, Tuple.Create(value, timeoutTask), (k, v) => Tuple.Create(value, timeoutTask));

            if (OnItemScheduled != null)
                OnItemScheduled(key);
        }

        public bool GetValue(TKey key, out TValue value)
        {
            if (_dictionary.TryGetValue(key, out var tuple))
            {
                value = tuple.Item1;
                return true;
            }

            value = default;
            return false;
        }
    }

    public static class TimeOutStatics
    {
        // By default, this method is assigned to delegate "OnScheduledItemExpired" when the class is created
        public static void DefaultScheduledExpired(Action key)
        {
            key();
        }
    }
}
