using GymApp.Models;

namespace GymApp.Services.Interpreters
{
    public class NameExpression : IExpression
    {
        private readonly string _name;

        public NameExpression(string name)
        {
            _name = name.ToLower();
        }

        public bool Interpret(TrainingClass trainingClass)
        {
            return trainingClass.Name
                .ToLower()
                .Contains(_name);
        }
    }
}
