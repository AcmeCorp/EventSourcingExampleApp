namespace AcmeCorp.EventSourcingExampleApp
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using AcmeCorp.EventSourcing;
    using AcmeCorp.EventSourcing.Logging;
    using AcmeCorp.EventSourcing.Providers.InMemory;

    public class InMemoryEventStoreWithDispatcherProvider : InMemoryEventStoreProvider
    {
        private readonly IBus bus;

        private readonly IApplicationLogger applicationLogger;

        public InMemoryEventStoreWithDispatcherProvider(IBus bus, IApplicationLogger applicationLogger, ILogger logger)
            : base(logger)
        {
            this.bus = bus;
            this.applicationLogger = applicationLogger;
        }

        /// <remarks>
        /// This would be a hack. There is no guaranteed dispatch. If there was an error
        /// during the publish action there is a risk the message is written to the Event
        /// Store but not published. For the purposes of the proof of concept it does not
        /// matter - the Event Store is in memory so there are zero guarantees anyway and
        /// because everything runs in memory there is a very low risk of an error during
        /// the publish action.
        /// </remarks>
        protected override async Task<int> CommitEventsAsync(string eventStreamId, int expectedStreamRevision, IEnumerable<EventStoreMessage> eventStoreMessages)
        {
            IEnumerable<EventStoreMessage> messages = eventStoreMessages as EventStoreMessage[] ?? eventStoreMessages.ToArray();
            int count = await base.CommitEventsAsync(eventStreamId, expectedStreamRevision, messages).ConfigureAwait(false);
            foreach (EventStoreMessage eventStoreMessage in messages)
            {
                this.applicationLogger.PublishMessage(eventStoreMessage.Body);
                await Task.Run(() => this.bus.Publish(eventStoreMessage.Body)).ConfigureAwait(false);
                Thread.Sleep(500);
            }

            return count;
        }
    }
}
