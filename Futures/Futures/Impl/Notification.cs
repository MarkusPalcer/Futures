using System;
using System.Collections.Generic;

namespace Futures
{
    public struct Notification<T>
    {
        public enum NotificationKind
        {
            OnDone,
            OnError
        }

        public NotificationKind Kind { get; private set; }

        public T Value { get; private set; }

        public Exception Error { get; private set; }

        public static Notification<T> OnDone(T value)
        {
            return new Notification<T>
            {
                Kind = NotificationKind.OnDone,
                Value = value
            };
        }

        public static Notification<T> OnError(Exception ex)
        {
            return new Notification<T>
            {
                Kind = NotificationKind.OnError,
                Error = ex
            };
        }

        public bool Equals(Notification<T> other)
        {
            return Kind == other.Kind && EqualityComparer<T>.Default.Equals(Value, other.Value) && Equals(Error, other.Error);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is Notification<T> && Equals((Notification<T>) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (int) Kind;
                hashCode = (hashCode*397) ^ EqualityComparer<T>.Default.GetHashCode(Value);
                hashCode = (hashCode*397) ^ (Error != null ? Error.GetHashCode() : 0);
                return hashCode;
            }
        }
    }
}