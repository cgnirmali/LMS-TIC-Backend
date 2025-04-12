﻿using LMS.DB.Entities;
using System.ComponentModel.DataAnnotations;

namespace LMS.DTOs.RequestModel
{
    public class StaffRequest
    {

      
        public string Name { get; set; }
        [Required]
        public string NIC { get; set; }

        public string UserEmail { get; set; }
        public string UTEmail { get; set; }

        public string UTPassword { get; set; }

        [Required]
        public string Address { get; set; }
        public string PhoneNumber { get; set; }

  
      
    }
}
