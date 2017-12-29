using System;
using System.Runtime.Serialization;
namespace NSI.DC.PricingPackageRepository
{
    [DataContract]
    public class PricingPackageDto
    {
        [DataMember]
        public int PricingPackageId { get; set; }
        [DataMember]
        public long PricingPackageName { get; set; }
        [DataMember]
        public bool? IsActive { get; set; }
        [DataMember]
        public decimal Price { get; set; }
        [DataMember]
        public bool? IsDeleted { get; set; }
        [DataMember]
        public DateTime DateCreated { get; set; }
        [DataMember]
        public DateTime? DateModified { get; set; }

    }
}
