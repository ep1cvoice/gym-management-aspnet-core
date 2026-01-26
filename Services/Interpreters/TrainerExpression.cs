using GymApp.Models;

namespace GymApp.Services.Interpreters
{
    public class TrainerExpression : IExpression
    {
        private readonly string _trainerName;

        public TrainerExpression(string trainerName)
        {
            _trainerName = trainerName.ToLower();
        }

        public bool Interpret(TrainingClass trainingClass)
        {
            return trainingClass.Trainer.FullName
                .ToLower()
                .Contains(_trainerName);
        }
    }
}
