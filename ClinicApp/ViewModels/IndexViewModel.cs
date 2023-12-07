﻿using ClinicApp.Models.Users;

namespace ClinicApp.ViewModels
{
    public class IndexViewModel
    {
        public string? RoleName { get; set; }
        public IEnumerable<Doctor> Doctors { get; set; } = null!;
        public bool IsActivated { get; set; }
    }
}
