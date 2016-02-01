using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Core.Common.Core;
using Core.Contracts;

namespace Business.Entities
{
    [DataContract]
    public class Developer : EntityBase, IIdentifiableEntity
    {
        [DataMember]
        public int DeveloperId { get; set; }
        [DataMember]
        public string Email { get; set; }
        [DataMember]
        public string Link { get; set; }
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public int YearsOfExprience { get; set; }
        [DataMember]
        public decimal MonthlySalary { get; set; }
        [DataMember]
        public bool CurrentlyHired { get; set; }

        public int EntityId
        {
            get { return DeveloperId; }
            set { DeveloperId = value; }
        }
    }
}
