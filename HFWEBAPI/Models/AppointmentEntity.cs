using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HFWEBAPI.Models
{
    public class AppointmentEntity
    {
        public string id { get; set; }
        public string date { get; set; }
        public string from { get; set; }
        public string to { get; set; }
        public string userId { get; set; }
        public string username { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public bool isActive { get; set; }
        public bool isAvailable { get; set; }
        public string createdBy { get; set; }
        public string createdDate { get; set; }
        public string modifiedBy { get; set; }
        public string modifiedDate { get; set; }
    }
    public class AppointmentQuery
    {
        public string id { get; set; }
        public string date { get; set; }
        public string from { get; set; }
        public string to { get; set; }
        public string userId { get; set; }
        public string username { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public bool isActive { get; set; }
        public bool isAvailable { get; set; }
        public string createdBy { get; set; }
        public string createdDate { get; set; }
        public string modifiedBy { get; set; }
        public string modifiedDate { get; set; }
        public string queryParameter { get; set; }
    }
}
