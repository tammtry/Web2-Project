﻿using DistributionSmartEnergyBackApp.Models;
using DistributionSmartEnergyBackApp.Models.EntityModels;
using DistributionSmartEnergyBackApp.Models.Interfaces;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DistributionSmartEnergyBackApp.Services
{
    public class DeviceService : IDevice
    {
        private readonly AuthenticationContext _context;

        public DeviceService(AuthenticationContext context)
        {
            _context = context;
        }
        public async Task AddDevice(DeviceModel device)
        {
            DeviceModel novi = new DeviceModel();
            novi.Type = device.Type;
            novi.Address = device.Address;        
            novi.Longitude = device.Longitude;
            novi.Latitude = device.Latitude;
            

            var prethodni = await _context.Devices.OrderBy(x=> x.Id).Where(d => d.Type == device.Type).LastOrDefaultAsync();
            var prethodniId = await _context.Devices.OrderBy(x => x.Id).Where(d => d.Id == device.Id).LastOrDefaultAsync();

            if (prethodni == null)
            {
                novi.Name = device.Type.ToString().Substring(0,3).ToUpper() + "0";
               
            }
            else
            {
                int broj = Int32.Parse(prethodni.Name.Substring(3)) +1;
                string name = device.Type.ToString().Substring(0, 3).ToUpper();
                novi.Name = name + broj;
                
            }

            
            _context.Devices.Add(novi);
           

            await _context.SaveChangesAsync(true);
        }

        public async Task DeleteDevice(long id)
        {
            var device = await _context.Devices.FindAsync(id);

            _context.Devices.Remove(device);

            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<long>> GetConnectedDevices(string documentId)
        {
            var deviceList = await _context.Devices.Where(x => x.documentId == documentId).ToListAsync();

            if (deviceList != null)
            {
                return deviceList.Select(x => x.Id).ToList();
            }

            return null;
        }

        public async Task<DeviceModel> GetDevice(long id)
        {
            return await _context.Devices.FindAsync(id);
        }

        public async Task<IEnumerable<DeviceModel>> GetDevices()
        {
            return await _context.Devices.ToListAsync();
        }
        public async Task<IEnumerable<DeviceModel>> GetDevicesAtLocation(string address)
        {
            return await _context.Devices.Where(d=> d.Address == address).ToListAsync();
        }

        public async Task<IEnumerable<DeviceModel>> SearchDevices(string address, string type)
        {
            var devices = await _context.Devices.ToListAsync();
            if (String.Equals(type, "Any") || type == null)
            {

            }
            else
            {
                devices = devices.Where(d => d.Type == type).ToList();
            }

            if (!String.IsNullOrEmpty(address))
            {
                devices = devices.Where(d => d.Address.Contains(address)).ToList();
            }

            return devices;
        }
    }
}
