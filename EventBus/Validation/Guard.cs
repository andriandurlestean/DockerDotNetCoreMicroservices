using System;

namespace EventBus.Validation
{
    public static class Guard
    {
        /// <summary>
        ///     Checks if an argument is null. If it is, throws an <see cref="ArgumentNullException" /> with the specified
        ///     <paramref name="argName" />
        /// </summary>
        /// <typeparam name="T">type of the argument, must be a reference type</typeparam>
        /// <param name="arg">argument to check</param>
        /// <param name="argName">name of argument</param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void ArgNotNull<T>(T arg, string argName) where T : class
        {
            if (ReferenceEquals(arg, null)) throw new ArgumentNullException(argName);
        }

        /// <summary>
        ///     Checks if an argument is empty or default arg. If it is, throws an <see cref="ArgumentNullException" /> with the
        ///     specified
        ///     <paramref name="argName" />
        /// </summary>
        /// <typeparam name="T">type of the argument, must be a reference type</typeparam>
        /// <param name="arg">argument to check</param>
        /// <param name="argName">name of argument</param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void ArgNotEmpty<T>(T arg, string argName)
        {
            if (arg.Equals(default(T))) throw new ArgumentNullException(argName);
        }

        /// <summary>
        ///     Checks if an argument is null or empty. If it is, throws an <see cref="ArgumentNullException" /> with the specified
        ///     <paramref name="argName" />
        /// </summary>
        /// <param name="arg">argument to check</param>
        /// <param name="argName">name of argument</param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void ArgNotNullOrEmpty(string arg, string argName)
        {
            if (string.IsNullOrEmpty(arg)) throw new ArgumentNullException(argName);
        }

        /// <summary>
        ///     Checks if an argument is greater than zero. If it is, throws an <see cref="ArgumentNullException" /> with the
        ///     specified
        ///     <paramref name="argName" />
        /// </summary>
        /// <param name="arg">argument to check</param>
        /// <param name="argName">name of argument</param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void ArgGreaterThanZero(int arg, string argName)
        {
            if (arg <= 0) throw new ArgumentOutOfRangeException(argName);
        }
    }
}