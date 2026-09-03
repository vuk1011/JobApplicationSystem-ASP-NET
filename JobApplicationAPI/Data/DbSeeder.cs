using Domain.Entities;
using Domain.Repositories;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;

namespace JobApplicationAPI.Data
{
    public static class DbSeeder
    {
        private const string CompanyName = "Robotika";

        private const string EmployeeEmail = "marina@gmail.com";
        private const string EmployeePassword = "Sifra123!";

        private const string CandidateEmail = "vuk@gmail.com";
        private const string CandidatePassword = "Sifra123!";

        public static async Task SeedAsync(IServiceProvider services)
        {
            var userManager = services.GetRequiredService<UserManager<AppUser>>();
            var uow = services.GetRequiredService<IUnitOfWork>();

            var company = uow.Companies.GetAll().FirstOrDefault(c => c.Name == CompanyName);
            if (company is null)
            {
                company = new Company
                {
                    Name = CompanyName,
                    About = "Proizvodnja i programiranje robota.",
                    Address = "Ustanička 3",
                };
                uow.Companies.Add(company);
                await uow.SaveChangesAsync();
            }

            if (await userManager.FindByEmailAsync(EmployeeEmail) is null)
            {
                var employeeUser = new AppUser
                {
                    UserName = EmployeeEmail,
                    Email = EmployeeEmail,
                    PhoneNumber = "38163000111",
                    UserType = UserType.Employee,
                };

                var result = await userManager.CreateAsync(employeeUser, EmployeePassword);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(employeeUser, "Employee");

                    uow.Employees.Add(new Employee
                    {
                        AppUserId = employeeUser.Id,
                        FirstName = "Marina",
                        LastName = "Matić",
                        Sex = Sex.Female,
                        Address = "Sarajevska 1",
                        NationalId = "0101988700001",
                        DateBorn = new DateOnly(1990, 1, 1),
                        DateHired = new DateOnly(2020, 1, 1),
                        CompanyId = company.Id,
                    });
                    await uow.SaveChangesAsync();
                }
            }

            if (await userManager.FindByEmailAsync(CandidateEmail) is null)
            {
                var candidateUser = new AppUser
                {
                    UserName = CandidateEmail,
                    Email = CandidateEmail,
                    PhoneNumber = "38163000222",
                    UserType = UserType.Candidate,
                };

                var result = await userManager.CreateAsync(candidateUser, CandidatePassword);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(candidateUser, "Candidate");

                    uow.Candidates.Add(new Candidate
                    {
                        AppUserId = candidateUser.Id,
                        FirstName = "Vuk",
                        LastName = "Perović",
                        Sex = Sex.Male,
                        Address = "Bulevar umetnosti 10",
                    });
                    await uow.SaveChangesAsync();
                }
            }
        }
    }
}
