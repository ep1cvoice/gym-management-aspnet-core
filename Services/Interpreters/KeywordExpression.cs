using GymApp.Models;

namespace GymApp.Services.Interpreters
{
    public class KeywordExpression : IExpression
    {
        private readonly string _keyword;

        public KeywordExpression(string keyword)
        {
            _keyword = keyword.ToLower();
        }

        public bool Interpret(TrainingClass trainingClass)
        {
            return trainingClass.Name.ToLower().Contains(_keyword)
                || trainingClass.Trainer.FullName.ToLower().Contains(_keyword);
        }
    }
}
