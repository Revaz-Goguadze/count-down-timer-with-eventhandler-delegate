using System;
using CustomTimer.EventArgsClasses;

namespace CustomTimer
{
    /// <summary>
    /// A custom class for simulating a countdown clock, which implements the ability to send a messages and additional
    /// information about the Started, Tick and Stopped events to any types that are subscribing the specified events.
    /// - When creating a CustomTimer object, it must be assigned:
    ///     - name (not null or empty string, otherwise ArgumentException will be thrown);
    ///     - the number of ticks (the number must be greater than 0 otherwise an exception will throw an ArgumentException).
    /// - After the timer has been created, it should fire the Started event, the event should contain information about
    /// the name of the timer and the number of ticks to start.
    /// - After starting the timer, it fires Tick events, which contain information about the name of the timer and
    /// the number of ticks left for triggering, there should be delays between Tick events, delays are modeled by Thread.Sleep.
    /// - After all Tick events are triggered, the timer should start the Stopped event, the event should contain information about
    /// the name of the timer.
    /// </summary>
    public class Timer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Timer"/> class.
        /// </summary>
        /// <param name="timerName">Name.</param>
        /// <param name="ticks">Ticks.</param>
        public event EventHandler<StartedEventArgs>? Started;

        public event EventHandler<TickEventArgs>? Tick;

        public event EventHandler<StoppedEventArgs>? Stopped;

#pragma warning disable SA1201
        private int ticks;
#pragma warning restore SA1201
#pragma warning disable SA1214
        private readonly string timerName;
#pragma warning restore SA1214

        public Timer(string timerName, int ticks)
        {
            if (string.IsNullOrEmpty(timerName))
            {
                throw new ArgumentException("Timer name cannot be null or empty.", nameof(timerName));
            }

            if (ticks <= 0)
            {
                throw new ArgumentException("Number of ticks must be greater than 0.", nameof(ticks));
            }

            this.timerName = timerName;
            this.ticks = ticks;
        }

        public void Run()
        {
            this.OnStarted();

            while (this.ticks > 0)
            {
                Thread.Sleep(1000); // Simulate a tick delay of 1 second
                this.OnTick();
                this.ticks--;
            }

            this.OnStopped();
        }

        protected virtual void OnStarted()
        {
            this.Started?.Invoke(this, new StartedEventArgs(this.timerName, this.ticks));
        }

        protected virtual void OnTick()
        {
            this.Tick?.Invoke(this, new TickEventArgs(this.timerName, this.ticks));
        }

        protected virtual void OnStopped()
        {
            this.Stopped?.Invoke(this, new StoppedEventArgs(this.timerName));
        }
    }
}
