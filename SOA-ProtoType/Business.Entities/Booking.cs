using System;
using System.Runtime.Serialization;
using Core.Common.Core;
using Core.Contracts;

namespace Business.Entities
{
    [DataContract]
    public class Booking: EntityBase, IIdentifiableEntity, IAccountOwnedEntity
    {
        [DataMember]
        public int BookedId { get; set; }
        [DataMember]
        public int AccountId { get; set; }
        [DataMember]
        public int DeveloperId { get; set; }
        [DataMember]
        public DateTime StartDate { get; set; }
        [DataMember]
        public DateTime EndDate { get; set; }


        public int EntityId
        {
            get { return BookedId; }
            set { BookedId = value; }
        }

        int IAccountOwnedEntity.OwnerAccountId => AccountId;
    }
}