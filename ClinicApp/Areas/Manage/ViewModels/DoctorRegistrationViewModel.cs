﻿using ClinicApp.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ClinicApp.Areas.Manage.ViewModels
{
    public class DoctorRegistrationViewModel
    {
        public IEnumerable<Speciality>? Specialities { get; set; }

        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string ConfirmPassword { get; set; } = string.Empty;
        public byte SpecialityId { get; set; }
    }
}