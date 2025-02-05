﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DistributionSmartEnergyUserMicroservice.Models
{
    public class ApplicationUser : IdentityUser
    {
        public enum UserTypeEnumeration { Administrator, Dispatcher, DataAnalyst, TeamMember, Consumer }
        public enum RegistrationState { Pending, Approved, Denied };
        private string name;
        private string lastname;
        private DateTime birthday;
        private string address;
        private UserTypeEnumeration userType;
        private string filePicture;
        private string teamid;
        private RegistrationState state;
        private UserTypeEnumeration roleRequest;

        [Column]
        public string Name {

            get { return name; }
            set { name = value; }
        }

        [Column]
        public string Lastname {
            get { return lastname; }
            set { lastname = value; }
        }

        [Column]
        public DateTime Birthday {

            get { return birthday; }
            set { birthday = value; }
        }

        [Column]
        public string Address {

            get { return address; }
            set { address = value; }
        }

        [Column]
        public UserTypeEnumeration UserType {

            get { return userType; }
            set { userType = value; }
        }

        [Column]
        public string FilePicture {

            get { return filePicture; }
            set { filePicture = value; }
        }

        [Column]
        public string TeamId {

            get { return teamid; }
            set { teamid = value; }
        }

        [Column]
        public RegistrationState RegState {

            get { return state; }
            set { state = value; }
        }

        [Column]
        public string Teams {

            get { return teamid; }
            set { teamid = value; }
        }

        [Column]
        public UserTypeEnumeration RoleRequest
        {

            get { return roleRequest; }
            set { roleRequest = value; }
        }   
    }
}
