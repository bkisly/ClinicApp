using ClinicApp.Data;
using ClinicApp.Models;
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
            mock.Setup(repository => repository.AddSpeciality(It.IsAny<Speciality>()))
                .Callback((Speciality s) => specialities.Add(s));

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

        public static Mock<IUserManagerProvider> GetUserManagerProviderMock<TUser>(IList<TUser> users)
            where TUser : IdentityUser
        {
            var mock = new Mock<IUserManagerProvider>();
            mock.Setup(p => p.Provide(It.IsAny<TUser>())).Returns(GetUserManagerMock(users).Object);
            return mock;
        }
    }
}
