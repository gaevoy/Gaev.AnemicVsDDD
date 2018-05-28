using System;

namespace Gaev.AnemicVsDDD.Tests.Anemic
{
    public abstract class TestBase
    {
        protected readonly TestConfig Config = new TestConfig();
        protected static readonly Random Random = new Random();
        protected static string RandomString() => Guid.NewGuid().ToString();
    }
}