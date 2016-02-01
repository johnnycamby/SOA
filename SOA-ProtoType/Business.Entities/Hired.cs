using System;
using System.Runtime.Serialization;
using Core.Common.Core;
using Core.Contracts;

namespace Business.Entities
{
    [DataContract]
    public class Hired : EntityBase, IIdentifiableEntity, IAccountOwnedEntity
    {
        [DataMember]
        public int HiredId { get; set; }
        [DataMember]
        public int AccountId { get; set; }
        [DataMember]
        public int DeveloperId { get; set; }
        [DataMember]
        public DateTime StartDate { get; set; }
        [DataMember]
        public DateTime? EndDate { get; set; }
        [DataMember]
        public DateTime DateDue { get; set; }

        public int EntityId
        {
            get { return HiredId; }
            set { HiredId = value; }
        }

        // Tell to which Hiring person/company the AccountId belongs to.
        int IAccountOwnedEntity.OwnerAccountId => AccountId;
    }
}