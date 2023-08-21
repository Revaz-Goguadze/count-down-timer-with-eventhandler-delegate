using System;
using CustomTimer.EventArgsClasses;
using CustomTimer.Interfaces;

namespace CustomTimer.Implementation
{
    /// <inheritdoc/>
    public class CountDownNotifier : ICountDownNotifier
    {
        /// <inheritdoc cref="CustomTimer.Interfaces.ICountDownNotifier" />
        private EventHandler<StartedEventArgs>? startHandler;
        private EventHandler<StoppedEventArgs>? stopHandler;
        private EventHandler<TickEventArgs>? tickHandler;

        public CountDownNotifier(Timer timer)
        {
            if (timer == null)
            {
                throw new ArgumentNullException(nameof(timer));
            }

            this.Init(null, null, null); // Initialize the event handlers

            timer.Started += this.OnTimerStarted;
            timer.Tick += this.OnTimerTick;
            timer.Stopped += this.OnTimerStopped;
        }

        private void OnTimerStarted(object? sender, StartedEventArgs e)
        {
            this.startHandler?.Invoke(sender, e);
        }

        private void OnTimerTick(object? sender, TickEventArgs e)
        {
            this.tickHandler?.Invoke(sender, e);
        }

        private void OnTimerStopped(object? sender, StoppedEventArgs e)
        {
            this.stopHandler?.Invoke(sender, e);
        }

#pragma warning disable SA1202
        public void Init(EventHandler<StartedEventArgs>? startHandler, EventHandler<StoppedEventArgs>? stopHandler,
#pragma warning restore SA1202
#pragma warning disable SA1117
            EventHandler<TickEventArgs>? tickHandler)
#pragma warning restore SA1117
        {
            this.startHandler = startHandler;
            this.stopHandler = stopHandler;
            this.tickHandler = tickHandler;
        }

        public void Run()
        {
            if (this.startHandler == null || this.stopHandler == null || this.tickHandler == null)
            {
                throw new InvalidOperationException("Event handlers must be initialized.");
            }

            // Create a Timer instance
            Timer timer = new Timer("MyTimer", 5);

            // Subscribe to Timer events
            timer.Started += this.startHandler;
            timer.Tick += this.tickHandler;
            timer.Stopped += this.stopHandler;

            // Run the Timer
            timer.Run();
        }
    }
}
