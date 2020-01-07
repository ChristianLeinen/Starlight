#define TRACE_DISPATCH
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Starlight.Core.Messaging
{
    public static class MessageQueue
    {
        private struct DelayedMessage
        {
            public object Sender;
            public EventArgs EventArgs;
        };

        private static readonly Queue<DelayedMessage> delayedMessages = new Queue<DelayedMessage>();

        public delegate void MessageEventHandler(object sender, EventArgs e);

        public static event MessageEventHandler MessageReceived;

        public static void Dispatch(object sender, EventArgs e)
        {
#if TRACE_DISPATCH
            // HACK: filter messages that spam the output
            //if (message.Action != MessageAction.MousePosition && message.Action != MessageAction.MouseX && message.Action != MessageAction.MouseY)
            {
                Debug.WriteLine($"MessageQueue: Dispatching message from {sender}: '{e}'");
            }
#endif
            MessageQueue.MessageReceived?.Invoke(sender, e);
        }

        public static void Post(object sender, EventArgs e)
        {
            MessageQueue.delayedMessages.Enqueue(new DelayedMessage { Sender = sender, EventArgs = e });
        }

        public static void ProcessMessageQueue()
        {
            while (MessageQueue.delayedMessages.Any())
            {
                var msg = MessageQueue.delayedMessages.Dequeue();
                MessageQueue.Dispatch(msg.Sender, msg.EventArgs);
            }
        }
    }
}
