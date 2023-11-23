using ClinicApp.Infrastructure;
using ClinicApp.Models;
using ClinicApp.Models.Users;
using ClinicApp.Services.User;
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
            var specialities = new List<Speciality>(DataInitializer.GetSpecialities());
            var providerMock = Helpers.GetUserManagerProviderMock(doctors);
            var repositoryMock = Helpers.GetSpecialityRepositoryMock(specialities);
            var service = new RegistrationService(providerMock.Object, repositoryMock.Object);

            // Act

            var result1 = await service.RegisterDoctor("doctor1", "P@ssw0rd", 1);
            var restul2 = await service.RegisterDoctor("doctor2", "p@Ssw0rd", 2);

            // Assert

            Assert.Equal(2, doctors.Count);

            var doctor1 = doctors[0];
            var doctor2 = doctors[1];

            Assert.Equal(RegistrationResult.Succeeded, result1);
            Assert.Equal("doctor1", doctor1.UserName);
            Assert.Equivalent(specialities[0], doctor1.Speciality);
            providerMock.Verify(p => p.Provide(doctor1).CreateAsync(doctor1, "P@ssw0rd"), Times.Once);
            providerMock.Verify(p => p.Provide(doctor1).AddToRoleAsync(doctor1, Constants.Roles.DoctorRoleName), Times.Once);

            Assert.Equal(RegistrationResult.Succeeded, result1);
            Assert.Equal("doctor2", doctor2.UserName);
            Assert.Equivalent(specialities[1], doctor2.Speciality);
            providerMock.Verify(p => p.Provide(doctor2).CreateAsync(doctor2, "p@Ssw0rd"), Times.Once);
            providerMock.Verify(p => p.Provide(doctor2).AddToRoleAsync(doctor2, Constants.Roles.DoctorRoleName), Times.Once);
        }

        [Fact]
        public async Task CanRegisterPatients()
        {
            // Arrange

            var patients = new List<Patient>();
            var providerMock = Helpers.GetUserManagerProviderMock(patients);
            var service = new RegistrationService(providerMock.Object, null!);

            // Act

            var result1 = await service.RegisterPatient("patient1", "P@ssw0rd");
            var result2 = await service.RegisterPatient("patient2", "p@Ssw0rd");

            // Assert

            Assert.Equal(2, patients.Count);

            var patient1 = patients[0];
            var patient2 = patients[1];

            Assert.Equal(RegistrationResult.Succeeded, result1);
            Assert.Equal("patient1", patient1.UserName);
            providerMock.Verify(p => p.Provide(patient1).CreateAsync(patient1, "P@ssw0rd"), Times.Once);
            providerMock.Verify(p => p.Provide(patient1).AddToRoleAsync(patient1, Constants.Roles.PatientRoleName), Times.Once);

            Assert.Equal(RegistrationResult.Succeeded, result2);
            Assert.Equal("patient2", patient2.UserName);
            providerMock.Verify(p => p.Provide(patient2).CreateAsync(patient2, "p@Ssw0rd"), Times.Once);
            providerMock.Verify(p => p.Provide(patient2).AddToRoleAsync(patient2, Constants.Roles.PatientRoleName), Times.Once);
        }

        [Fact]
        public async Task CanRegisterManagers()
        {
            // Arrange

            var managers = new List<Manager>();
            var providerMock = Helpers.GetUserManagerProviderMock(managers);
            var service = new RegistrationService(providerMock.Object, null!);

            // Act

            var result1 = await service.RegisterManager("manager1", "P@ssw0rd");
            var result2 = await service.RegisterManager("manager2", "p@Ssw0rd");

            // Assert

            Assert.Equal(2, managers.Count);

            var manager1 = managers[0];
            var manager2 = managers[1];

            Assert.Equal(RegistrationResult.Succeeded, result1);
            Assert.Equal("manager1", manager1.UserName);
            providerMock.Verify(p => p.Provide(manager1).CreateAsync(manager1, "P@ssw0rd"), Times.Once);
            providerMock.Verify(p => p.Provide(manager1).AddToRoleAsync(manager1, Constants.Roles.ManagerRoleName), Times.Once);

            Assert.Equal(RegistrationResult.Succeeded, result2);
            Assert.Equal("manager2", manager2.UserName);
            providerMock.Verify(p => p.Provide(manager2).CreateAsync(manager2, "p@Ssw0rd"), Times.Once);
            providerMock.Verify(p => p.Provide(manager2).AddToRoleAsync(manager2, Constants.Roles.ManagerRoleName), Times.Once);
        }

        [Fact]
        public async Task CannotRegisterExistingDoctors()
        {
            // Arrange

            var doctors = new List<Doctor> { new() { UserName = "doctor1" } };
            var specialities = new List<Speciality>(DataInitializer.GetSpecialities());
            var repositoryMock = Helpers.GetSpecialityRepositoryMock(specialities);
            var providerMock = Helpers.GetUserManagerProviderMock(doctors);
            var service = new RegistrationService(providerMock.Object, repositoryMock.Object);

            // Act

            var result = await service.RegisterDoctor("doctor1", "P@ssw0rd", 1);

            // Assert

            Assert.Single(doctors);
            Assert.Equal(RegistrationResult.UserExists, result);
            providerMock.Verify(p => p.Provide(It.IsAny<Doctor>()).CreateAsync(It.IsAny<Doctor>(), It.IsAny<string>()), Times.Never);
        }

        [Fact]
        public async Task CannotRegisterExistingPatients()
        {
            // Arrange

            var patients = new List<Patient> {  new() { UserName = "patient1" } };
            var providerMock = Helpers.GetUserManagerProviderMock(patients);
            var service = new RegistrationService(providerMock.Object, null!);

            // Act

            var result = await service.RegisterPatient("patient1", "P@ssw0rd");

            // Assert

            Assert.Single(patients);
            Assert.Equal(RegistrationResult.UserExists, result);
            providerMock.Verify(p => p.Provide(It.IsAny<Patient>()).CreateAsync(It.IsAny<Patient>(), It.IsAny<string>()), Times.Never);
        }

        [Fact]
        public async Task CannotRegisterExistingManagers()
        {
            // Arrange

            var managers = new List<Manager> { new() { UserName = "manager1" } };
            var providerMock = Helpers.GetUserManagerProviderMock(managers);
            var service = new RegistrationService(providerMock.Object, null!);

            // Act

            var result = await service.RegisterManager("manager1", "P@ssw0rd");

            // Assert

            Assert.Single(managers);
            Assert.Equal(RegistrationResult.UserExists, result);
            providerMock.Verify(p => p.Provide(It.IsAny<Manager>()).CreateAsync(It.IsAny<Manager>(), It.IsAny<string>()), Times.Never);
        }
    }
}
