using System;
using System.Runtime.Serialization;
using Core.Common.ServiceModel;

namespace Client.Contracts.DataContracts
{
    [DataContract]
    public class ClientHiringData : DataContractBase
    {
        [DataMember]
        public int HiredId { get; set; }
        [DataMember]
        public string ClientName { get; set; }
        [DataMember]
        public string Developer { get; set; }
        [DataMember]
        public DateTime HiredDate { get; set; }
        [DataMember]
        public DateTime ExpectedReturn { get; set; }
    }
}