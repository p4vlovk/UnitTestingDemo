namespace GameEngine
{
    using System;

    public class EnemyCreationException : Exception
    {
        public EnemyCreationException()
        {
        }

        public EnemyCreationException(string message)
            : base(message)
        {
        }

        public EnemyCreationException(string message, Exception inner)
            : base(message, inner)
        {
        }

        public EnemyCreationException(string message, string requestedEnemyName)
            : base(message)
        {
            this.RequestedEnemyName = requestedEnemyName;
        }

        public string RequestedEnemyName { get; }
    }
}
