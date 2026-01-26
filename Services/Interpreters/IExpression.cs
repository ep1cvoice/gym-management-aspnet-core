using GymApp.Models;

namespace GymApp.Services.Interpreters
{
    public interface IExpression
    {
        bool Interpret(TrainingClass trainingClass);
    }
}
