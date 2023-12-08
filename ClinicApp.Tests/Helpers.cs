using ClinicApp.Data;
using ClinicApp.Infrastructure;
using ClinicApp.Models;
using ClinicApp.Models.Users;
using ClinicApp.Repositories;
using ClinicApp.Services.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace ClinicApp.Tests
{
    internal static class Helpers
    {
        public static ApplicationDbContext CreateInMemoryContext(string dbName)
        {
            var contextOptions = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(dbName)
                .EnableDetailedErrors().Options;
            return new ApplicationDbContext(contextOptions);
        }

        public static Mock<ISpecialityRepository> GetSpecialityRepositoryMock(IList<Speciality> specialities)
        {
            var mock = new Mock<ISpecialityRepository>();

            mock.SetupGet(repository => repository.Specialities).Returns(specialities.AsQueryable());
            mock.Setup(repository => repository.GetById(It.IsAny<byte>()))
                .Returns((byte id) => specialities.Single(s => s.Id == id));
            mock.Setup(repository => repository.AddSpeciality(It.IsAny<Speciality>()))
                .Callback((Speciality s) => specialities.Add(s));

            return mock;
        }

        public static Mock<IScheduleEntryRepository> GetScheduleEntryRepositoryMock(IList<ScheduleEntry> entries)
        {
            var mock = new Mock<IScheduleEntryRepository>();

            mock.SetupGet(repository => repository.ScheduleEntries).Returns(entries.AsQueryable());
            mock.Setup(repository => repository.AddAsync(It.IsAny<ScheduleEntry>()))
                .Callback((ScheduleEntry s) => entries.Add(s));

            return mock;
        }

        public static Mock<UserManager<TUser>> GetUserManagerMock<TUser>(IList<TUser> users) where TUser : IdentityUser
        {
            var store = new Mock<IUserStore<TUser>>();
            var mgr = new Mock<UserManager<TUser>>(store.Object, null!, null!, null!, null!, null!, null!, null!, null!);
            mgr.Object.UserValidators.Add(new UserValidator<TUser>());
            mgr.Object.PasswordValidators.Add(new PasswordValidator<TUser>());

            mgr.Setup(x => x.DeleteAsync(It.IsAny<TUser>())).ReturnsAsync(IdentityResult.Success);
            mgr.Setup(x => x.CreateAsync(It.IsAny<TUser>(), It.IsAny<string>())).ReturnsAsync(IdentityResult.Success).Callback<TUser, string>((x, y) => users.Add(x));

            mgr.Setup(x => x.UpdateAsync(It.IsAny<TUser>())).ReturnsAsync(IdentityResult.Success);
            mgr.Setup(x => x.FindByNameAsync(It.IsAny<string>()))
                .ReturnsAsync(new Func<string, TUser?>(userName => users.SingleOrDefault(u => u.UserName == userName)));

            return mgr;
        }

        public static Mock<UserManager<IdentityUser>> GetDefaultUserManagerMock(IList<IdentityUser> users)
        {
            var store = new Mock<IUserStore<IdentityUser>>();
            var mgr = new Mock<UserManager<IdentityUser>>(store.Object, null!, null!, null!, null!, null!, null!, null!, null!);
            mgr.Object.UserValidators.Add(new UserValidator<IdentityUser>());
            mgr.Object.PasswordValidators.Add(new PasswordValidator<IdentityUser>());

            mgr.Setup(x => x.DeleteAsync(It.IsAny<IdentityUser>())).ReturnsAsync(IdentityResult.Success);
            mgr.Setup(x => x.CreateAsync(It.IsAny<IdentityUser>(), It.IsAny<string>())).ReturnsAsync(IdentityResult.Success).Callback<IdentityUser, string>((x, y) => users.Add(x));

            mgr.Setup(x => x.UpdateAsync(It.IsAny<IdentityUser>())).ReturnsAsync(IdentityResult.Success);
            mgr.Setup(x => x.FindByNameAsync(It.IsAny<string>()))
                .ReturnsAsync(new Func<string, IdentityUser?>(userName => users.SingleOrDefault(u => u.UserName == userName)));

            return mgr;
        }

        public static Mock<IUserDependenciesProvider> GetUserManagerProviderMock<TUser>(IList<TUser> users, IList<IdentityUser>? defaultUsers = null)
            where TUser : IdentityUser, new()
        {
            var mock = new Mock<IUserDependenciesProvider>();

            mock.SetupGet(p => p.DefaultManager).Returns(GetDefaultUserManagerMock(defaultUsers ?? new List<IdentityUser>()).Object);
            mock.Setup(p => p.ProvideManager<TUser>()).Returns(GetUserManagerMock(users).Object);
            mock.Setup(p => p.ProvideRoleName<Manager>()).Returns(Constants.Roles.ManagerRoleName);
            mock.Setup(p => p.ProvideRoleName<Patient>()).Returns(Constants.Roles.PatientRoleName);
            mock.Setup(p => p.ProvideRoleName<Doctor>()).Returns(Constants.Roles.DoctorRoleName);
            return mock;
        }
    }
}
