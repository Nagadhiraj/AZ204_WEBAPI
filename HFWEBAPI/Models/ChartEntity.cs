using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HFWEBAPI.Models
{
    public class ChartEntity
    {
        public string id { get; set; }
        public string name { get; set; }
        public string remarks { get; set; }
        public string mailsubject { get; set; }
        public string mailcontent { get; set; }
        public List<AttachmentEntity> chartAttachments { get; set; }
        public string userId { get; set; }
        public string username { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public bool isActive { get; set; }
        public string createdBy { get; set; }
        public string createdDate { get; set; }
        public string modifiedBy { get; set; }
        public string modifiedDate { get; set; }
    }
    public class ChartQuery
    {
        public string id { get; set; }
        public string name { get; set; }
        public string remarks { get; set; }
        public string mailsubject { get; set; }
        public string mailcontent { get; set; }
        public List<AttachmentEntity> chartAttachments { get; set; }
        public string userId { get; set; }
        public string username { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public bool isActive { get; set; }
        public string createdBy { get; set; }
        public string createdDate { get; set; }
        public string modifiedBy { get; set; }
        public string modifiedDate { get; set; }
        public string queryParameter { get; set; }
    }

    public class AttachmentEntity
    {
        public string Base64String { get; set; }
        public string ContainerName { get; set; }
        public string FileSize { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public string ContentType { get; set; }
        public string uId { get; set; }
    }
}
