using GymApp.Models;

namespace GymApp.Services.Interpreters
{
    public class AndExpression : IExpression
    {
        private readonly IExpression _left;
        private readonly IExpression _right;

        public AndExpression(IExpression left, IExpression right)
        {
            _left = left;
            _right = right;
        }

        public bool Interpret(TrainingClass trainingClass)
        {
            return _left.Interpret(trainingClass)
                && _right.Interpret(trainingClass);
        }
    }
}
