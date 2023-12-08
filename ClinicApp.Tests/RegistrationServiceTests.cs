using ClinicApp.Infrastructure;
using ClinicApp.Models;
using ClinicApp.Models.Users;
using ClinicApp.Services.User;
using Microsoft.AspNetCore.Identity;
using Moq;

namespace ClinicApp.Tests
{
    public class RegistrationServiceTests
    {
        [Fact]
        public async Task CanRegisterDoctors()
        {
            // Arrange

            var doctors = new List<Doctor>();

            var specialities = new List<Speciality>(DataFactory.GetSpecialities());
            var providerMock = Helpers.GetUserManagerProviderMock(doctors);
            var repositoryMock = Helpers.GetSpecialityRepositoryMock(specialities);
            var service = new RegistrationService(providerMock.Object);

            var doctor1 = new Doctor { UserName = "doctor1", Speciality = repositoryMock.Object.GetById(1) };
            var doctor2 = new Doctor { UserName = "doctor2", Speciality = repositoryMock.Object.GetById(2) };

            // Act

            var result1 = await service.RegisterAsync(doctor1, "P@ssw0rd");
            var result2 = await service.RegisterAsync(doctor2, "p@Ssw0rd");

            // Assert

            Assert.Equal(2, doctors.Count);

            Assert.True(result1.Succeeded);
            Assert.Equal("doctor1", doctors[0].UserName);
            Assert.Equivalent(specialities[0], doctors[0].Speciality);
            providerMock.Verify(p => p.ProvideManager<Doctor>().CreateAsync(doctors[0], "P@ssw0rd"), Times.Once);
            providerMock.Verify(p => p.ProvideManager<Doctor>().AddToRoleAsync(doctors[0], Constants.Roles.DoctorRoleName), Times.Once);

            Assert.True(result2.Succeeded);
            Assert.Equal("doctor2", doctors[1].UserName);
            Assert.Equivalent(specialities[1], doctors[1].Speciality);
            providerMock.Verify(p => p.ProvideManager<Doctor>().CreateAsync(doctors[1], "p@Ssw0rd"), Times.Once);
            providerMock.Verify(p => p.ProvideManager<Doctor>().AddToRoleAsync(doctors[1], Constants.Roles.DoctorRoleName), Times.Once);
        }

        [Fact]
        public async Task CanRegisterPatients()
        {
            // Arrange

            var patients = new List<Patient>();
            var providerMock = Helpers.GetUserManagerProviderMock(patients);
            var service = new RegistrationService(providerMock.Object);

            var patient1 = new Patient { UserName = "patient1" };
            var patient2 = new Patient { UserName = "patient2" };

            // Act

            var result1 = await service.RegisterAsync(patient1, "P@ssw0rd");
            var result2 = await service.RegisterAsync(patient2, "p@Ssw0rd");

            // Assert

            Assert.Equal(2, patients.Count);

            Assert.True(result1.Succeeded);
            Assert.Equal("patient1", patients[0].UserName);
            providerMock.Verify(p => p.ProvideManager<Patient>().CreateAsync(patients[0], "P@ssw0rd"), Times.Once);
            providerMock.Verify(p => p.ProvideManager<Patient>().AddToRoleAsync(patients[0], Constants.Roles.PatientRoleName), Times.Once);

            Assert.True(result2.Succeeded);
            Assert.Equal("patient2", patients[1].UserName);
            providerMock.Verify(p => p.ProvideManager<Patient>().CreateAsync(patients[1], "p@Ssw0rd"), Times.Once);
            providerMock.Verify(p => p.ProvideManager<Patient>().AddToRoleAsync(patients[1], Constants.Roles.PatientRoleName), Times.Once);
        }

        [Fact]
        public async Task CanRegisterManagers()
        {
            // Arrange

            var managers = new List<Manager>();
            var providerMock = Helpers.GetUserManagerProviderMock(managers);
            var service = new RegistrationService(providerMock.Object);

            var manager1 = new Manager { UserName = "manager1" };
            var manager2 = new Manager { UserName = "manager2" };

            // Act

            var result1 = await service.RegisterAsync(manager1, "P@ssw0rd");
            var result2 = await service.RegisterAsync(manager2, "p@Ssw0rd");

            // Assert

            Assert.Equal(2, managers.Count);

            Assert.True(result1.Succeeded);
            Assert.Equal("manager1", managers[0].UserName);
            providerMock.Verify(p => p.ProvideManager<Manager>().CreateAsync(managers[0], "P@ssw0rd"), Times.Once);
            providerMock.Verify(p => p.ProvideManager<Manager>().AddToRoleAsync(managers[0], Constants.Roles.ManagerRoleName), Times.Once);

            Assert.True(result2.Succeeded);
            Assert.Equal("manager2", managers[1].UserName);
            providerMock.Verify(p => p.ProvideManager<Manager>().CreateAsync(managers[1], "p@Ssw0rd"), Times.Once);
            providerMock.Verify(p => p.ProvideManager<Manager>().AddToRoleAsync(managers[1], Constants.Roles.ManagerRoleName), Times.Once);
        }

        [Fact]
        public async Task CannotRegisterExistingDoctors()
        {
            // Arrange

            var doctors = new List<IdentityUser> { new() { UserName = "doctor1" } };
            var specialities = new List<Speciality>(DataFactory.GetSpecialities());
            var repositoryMock = Helpers.GetSpecialityRepositoryMock(specialities);
            var providerMock = Helpers.GetUserManagerProviderMock(new List<Doctor>(), doctors);
            var service = new RegistrationService(providerMock.Object);

            var doctor = new Doctor { UserName = "doctor1", Speciality = repositoryMock.Object.GetById(1) };

            // Act

            var result = await service.RegisterAsync(doctor, "P@ssw0rd");

            // Assert

            Assert.Single(doctors);
            Assert.False(result.Succeeded);
            Assert.Single(result.Errors);
            providerMock.Verify(p => p.ProvideManager<Doctor>().CreateAsync(It.IsAny<Doctor>(), It.IsAny<string>()), Times.Never);
        }

        [Fact]
        public async Task CannotRegisterExistingPatients()
        {
            // Arrange

            var patients = new List<IdentityUser> { new() { UserName = "patient1" } };
            var providerMock = Helpers.GetUserManagerProviderMock(new List<Patient>(), patients);
            var service = new RegistrationService(providerMock.Object);

            var patient = new Patient { UserName = "patient1" };

            // Act

            var result = await service.RegisterAsync(patient, "P@ssw0rd");

            // Assert

            Assert.Single(patients);
            Assert.False(result.Succeeded);
            Assert.Single(result.Errors);
            providerMock.Verify(p => p.ProvideManager<Patient>().CreateAsync(It.IsAny<Patient>(), It.IsAny<string>()), Times.Never);
        }

        [Fact]
        public async Task CannotRegisterExistingManagers()
        {
            // Arrange

            var managers = new List<IdentityUser> { new() { UserName = "manager1" } };
            var providerMock = Helpers.GetUserManagerProviderMock(new List<Manager>(), managers);
            var service = new RegistrationService(providerMock.Object);

            var manager = new Manager { UserName = "manager1" };

            // Act

            var result = await service.RegisterAsync(manager, "P@ssw0rd");

            // Assert

            Assert.Single(managers);
            Assert.False(result.Succeeded);
            Assert.Single(result.Errors);
            providerMock.Verify(p => p.ProvideManager<Manager>().CreateAsync(It.IsAny<Manager>(), It.IsAny<string>()), Times.Never);
        }
    }
}
