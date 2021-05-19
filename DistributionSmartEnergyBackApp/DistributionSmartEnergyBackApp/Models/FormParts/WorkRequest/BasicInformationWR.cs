﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DistributionSmartEnergyBackApp.Models.FormParts
{
    public class BasicInformationWR
    {

        public long Id { get; set; }
        public string DocumentId { get; set; }
        public string Status { get; set; }
        public string Type { get; set; }
        public string Street { get; set; }
        public long incidentId { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
        public bool emergency { get; set; }
        public string Company { get; set; }
        public string PhoneNumber { get; set; }
        public string User { get; set; }
        public DateTime DateCreated { get; set; }
        public string Purpose { get; set; }
        public string Details { get; set; }
        public string Notes { get; set; }
    }
}
