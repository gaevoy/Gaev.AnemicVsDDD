using System;

namespace Gaev.AnemicVsDDD
{
    public interface ISystem
    {
        Guid NewGuid();
        DateTimeOffset GetUtcNow();
    }

    public class SystemEnvironment : ISystem
    {
        public Guid NewGuid() => Guid.NewGuid();
        public DateTimeOffset GetUtcNow() => DateTimeOffset.UtcNow;
    }
}