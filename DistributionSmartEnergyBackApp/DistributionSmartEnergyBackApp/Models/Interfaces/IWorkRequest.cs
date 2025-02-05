﻿using DistributionSmartEnergyBackApp.Models.FormParts;
using DistributionSmartEnergyBackApp.Models.FormParts.WorkRequest;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DistributionSmartEnergyBackApp.Models.Interfaces
{
    public interface IWorkRequest
    {
        Task<long> AddWorkRequest(WorkRequestViewModel wrapper);
        Task<IEnumerable<BasicInformationWR>> GetMyBasicInfo(string username);

        Task<IEnumerable<BasicInformationWR>> GetAllBasicInfo();
        Task<BasicInformationWR> GetBasicInfo(string id);
        Task<IEnumerable<HistoryOfStateChanges>> GetHistory(string id);

        Task UpdateBasicInfo(BasicInformationWR basicInfo);
        Task DeleteBasicInfo(string id);
        Task UpdateHistory(HistoryOfStateChanges[] changes);

        Task Save();
    }
}