using GymApp.Models;
using GymApp.Models.Enums;

namespace GymApp.Data
{
    public static class DbSeeder
    {
        public static void Seed(AppDbContext context)
        {
            // ===== TRAINERS =====
            if (!context.Trainers.Any())
            {
                var trainers = new List<Trainer>
                {
                    new Trainer
                    {
                        FullName = "Wiktoria Szurowska",
                        TrainerType = TrainerType.Group
                    },
                    new Trainer
                    {
                        FullName = "Beata Walczak",
                        TrainerType = TrainerType.Group
                    },
                    new Trainer
                    {
                        FullName = "Damian Lebowski",
                        TrainerType = TrainerType.Personal
                    },
                    new Trainer
                    {
                        FullName = "Mateusz Zuch",
                        TrainerType = TrainerType.Personal
                    }
                };

                context.Trainers.AddRange(trainers);
                context.SaveChanges();
            }

            // ===== CLASSES =====
            if (!context.TrainingClasses.Any())
            {
                var groupTrainer = context.Trainers
                    .First(t => t.TrainerType == TrainerType.Group);

                var classes = new List<TrainingClass>
                {
                    new TrainingClass
                    {
                        Name = "Yoga Flow",
                        Duration = TimeSpan.FromMinutes(60),
                        StartTime = DateTime.Today.AddHours(18),
                        MaxSlots = 12,
                        TakenSlots = 0,
                        TrainerId = groupTrainer.Id
                    },
                    new TrainingClass
                    {
                        Name = "Cross Training",
                        Duration = TimeSpan.FromMinutes(45),
                        StartTime = DateTime.Today.AddDays(1).AddHours(19),
                        MaxSlots = 10,
                        TakenSlots = 0,
                        TrainerId = groupTrainer.Id
                    }
                };

                context.TrainingClasses.AddRange(classes);
                context.SaveChanges();
            }
        }
    }
}
