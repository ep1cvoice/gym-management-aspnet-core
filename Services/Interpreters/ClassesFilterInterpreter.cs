using GymApp.Models;

namespace GymApp.Services.Interpreters
{
    public class ClassesFilterInterpreter
    {
        public List<TrainingClass> Apply(
            List<TrainingClass> classes,
            string? query)
        {
            if (string.IsNullOrWhiteSpace(query))
                return classes;

            var keywords = query
                .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                .Select(k => k.Trim())
                .ToList();

            IExpression? expression = null;

            foreach (var keyword in keywords)
            {
                var current = new KeywordExpression(keyword);

                expression = expression == null
                    ? current
                    : new AndExpression(expression, current);
            }

            return classes
                .Where(c => expression!.Interpret(c))
                .ToList();
        }
    }
}
