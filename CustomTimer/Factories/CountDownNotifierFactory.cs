using System;
using CustomTimer.Implementation;
using CustomTimer.Interfaces;

namespace CustomTimer.Factories
{
    /// <summary>
    /// Implements the factory method pattern
    /// <see>
    ///     <cref>https://en.wikipedia.org/wiki/Factory_method_pattern</cref>
    /// </see>
    /// for creating an object of the class that implements the <see cref="ICountDownNotifier"/> interface.
    /// </summary>
    public class CountDownNotifierFactory
    {
        /// <summary>
        /// Create an object of the class that implements the <see cref="ICountDownNotifier"/> interface.
        /// </summary>
        /// <param name="timer">A reference to a class CustomTimer.</param>
        /// <returns>A reference to an object of the class that implements the <see cref="ICountDownNotifier"/> interface.</returns>
        /// <exception cref="ArgumentNullException">When timer is null.</exception>
#pragma warning disable CA1822
        public ICountDownNotifier CreateNotifierForTimer(Timer? timer)
#pragma warning restore CA1822
        {
            if (timer == null)
            {
                throw new ArgumentNullException(nameof(timer));
            }

            return new CountDownNotifier(timer);
        }
    }
}
