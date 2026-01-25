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
                        TrainerType = TrainerType.Personal,
                        ImageUrl = "https://images.unsplash.com/photo-1534368270820-9de3d8053204?q=80&w=1170&auto=format&fit=crop"
                    },
                    new Trainer
                    {
                        FullName = "Mateusz Zuch",
                        TrainerType = TrainerType.Personal,
                        ImageUrl = "https://images.unsplash.com/photo-1579758629938-03607ccdbaba"
                    }
                };

                context.Trainers.AddRange(trainers);
                context.SaveChanges();
            }

            // ===== CLASSES =====
            // ===== CLASSES =====
            if (!context.TrainingClasses.Any())
            {
                var groupTrainers = context.Trainers
                    .Where(t => t.TrainerType == TrainerType.Group)
                    .ToList();

                var wiktoria = groupTrainers.First(t => t.FullName.Contains("Wiktoria"));
                var beata = groupTrainers.First(t => t.FullName.Contains("Beata"));

                var classes = new List<TrainingClass>();

                // ===== WIKTORIA – YOGA =====
                var yogaDates = new[]
                {
                    new DateTime(2026, 2, 3, 18, 0, 0),
                    new DateTime(2026, 2, 5, 9, 0, 0),
                    new DateTime(2026, 2, 7, 10, 0, 0),
                    new DateTime(2026, 2, 10, 18, 0, 0),
                    new DateTime(2026, 2, 12, 9, 0, 0),
                    new DateTime(2026, 2, 14, 10, 0, 0),
                    new DateTime(2026, 2, 17, 18, 0, 0),
                    new DateTime(2026, 2, 19, 9, 0, 0),
                    new DateTime(2026, 2, 21, 10, 0, 0),
                    new DateTime(2026, 2, 24, 18, 0, 0)
                };

                foreach (var date in yogaDates)
                {
                    classes.Add(new TrainingClass
                    {
                        Name = "Yoga Flow",
                        Duration = TimeSpan.FromMinutes(60),
                        StartTime = date,
                        MaxSlots = 12,
                        TakenSlots = 0,
                        TrainerId = wiktoria.Id
                    });
                }

                // ===== BEATA – CROSSFIT =====
                var crossfitDates = new[]
                {
                    new DateTime(2026, 2, 4, 19, 0, 0),
                    new DateTime(2026, 2, 6, 17, 0, 0),
                    new DateTime(2026, 2, 8, 11, 0, 0),
                    new DateTime(2026, 2, 11, 19, 0, 0),
                    new DateTime(2026, 2, 13, 17, 0, 0),
                    new DateTime(2026, 2, 15, 11, 0, 0),
                    new DateTime(2026, 2, 18, 19, 0, 0),
                    new DateTime(2026, 2, 20, 17, 0, 0),
                    new DateTime(2026, 2, 22, 11, 0, 0),
                    new DateTime(2026, 2, 26, 19, 0, 0)
                };

                foreach (var date in crossfitDates)
                {
                    classes.Add(new TrainingClass
                    {
                        Name = "Cross Training",
                        Duration = TimeSpan.FromMinutes(45),
                        StartTime = date,
                        MaxSlots = 12,
                        TakenSlots = 0,
                        TrainerId = beata.Id
                    });
                }

                context.TrainingClasses.AddRange(classes);
                context.SaveChanges();
            }

        }
    }
}
