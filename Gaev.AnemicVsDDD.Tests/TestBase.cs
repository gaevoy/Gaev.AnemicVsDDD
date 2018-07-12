using System;

namespace Gaev.AnemicVsDDD.Tests
{
    public abstract class TestBase
    {
        protected readonly TestConfig Config = new TestConfig();
        protected static readonly Random Random = new Random();
        protected static string RandomString() => Guid.NewGuid().ToString();
        protected static int RandomInt() => Random.Next();
    }
}