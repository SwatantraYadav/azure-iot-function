using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Data
{

    public class LogicAppMessage
    {
        public string ContentData { get; set; }
        public string ContentType { get; set; }
        public string ContentTransferEncoding { get; set; }
        public Properties Properties { get; set; }
        public string MessageId { get; set; }
        public object To { get; set; }
        public object ReplyTo { get; set; }
        public object ReplyToSessionId { get; set; }
        public object Label { get; set; }
        public DateTime ScheduledEnqueueTimeUtc { get; set; }
        public object SessionId { get; set; }
        public object CorrelationId { get; set; }
        public int SequenceNumber { get; set; }
        public string LockToken { get; set; }
        public string TimeToLive { get; set; }
    }

    public class Properties
    {
        public string level { get; set; }
        public string iothubconnectiondeviceid { get; set; }
        public string iothubconnectionauthmethod { get; set; }
        public string iothubconnectionauthgenerationid { get; set; }
        public string DeliveryCount { get; set; }
        public string EnqueuedSequenceNumber { get; set; }
        public DateTime EnqueuedTimeUtc { get; set; }
        public DateTime ExpiresAtUtc { get; set; }
        public DateTime LockedUntilUtc { get; set; }
        public string LockToken { get; set; }
        public string MessageId { get; set; }
        public DateTime ScheduledEnqueueTimeUtc { get; set; }
        public string SequenceNumber { get; set; }
        public string Size { get; set; }
        public string State { get; set; }
        public string TimeToLive { get; set; }
    }

}
