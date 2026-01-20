using GymApp.Models.Enums;

namespace GymApp.Models.Factories
{
    public class PassFactory : IPassFactory
    {
        public UserPass Create(PassType passType, string userId)
        {
            var now = DateTime.UtcNow;

            var pass = passType switch
            {
                PassType.Student => CreateStudentPass(userId, now),
                PassType.Basic   => CreateBasicPass(userId, now),
                PassType.Pro     => CreateProPass(userId, now),
                PassType.Athlete => CreateAthletePass(userId, now),

                _ => throw new ArgumentException("Nieznany typ karnetu")
            };

            return pass;
        }

        private static UserPass CreateStudentPass(string userId, DateTime now)
        {
            return new UserPass
            {
                PassType = PassType.Student,
                Name = "Student Package",
                Price = 45m,
                StartDate = now,
                EndDate = now.AddDays(30),
                UserId = userId
            };
        }

        private static UserPass CreateBasicPass(string userId, DateTime now)
        {
            return new UserPass
            {
                PassType = PassType.Basic,
                Name = "Basic Package",
                Price = 65m,
                StartDate = now,
                EndDate = now.AddDays(30),
                UserId = userId
            };
        }

        private static UserPass CreateProPass(string userId, DateTime now)
        {
            return new UserPass
            {
                PassType = PassType.Pro,
                Name = "Pro Package",
                Price = 89m,
                StartDate = now,
                EndDate = now.AddDays(30),
                UserId = userId
            };
        }

        private static UserPass CreateAthletePass(string userId, DateTime now)
        {
            return new UserPass
            {
                PassType = PassType.Athlete,
                Name = "Athlete Package",
                Price = 119m,
                StartDate = now,
                EndDate = now.AddDays(30),
                UserId = userId
            };
        }
    }
}
