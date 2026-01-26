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

            var parts = query.Split(
                "AND",
                StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

            IExpression? expression = null;

            foreach (var part in parts)
            {
                var tokens = part.Split(
                    ':',
                    2,
                    StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

                if (tokens.Length != 2)
                    continue;

                IExpression? current = tokens[0].ToLower() switch
                {
                    "trainer" => new TrainerExpression(tokens[1]),
                    "name"    => new NameExpression(tokens[1]),
                    _         => null
                };

                if (current == null)
                    continue;

                expression = expression == null
                    ? current
                    : new AndExpression(expression, current);
            }

            if (expression == null)
                return classes;

            return classes
                .Where(c => expression.Interpret(c))
                .ToList();
        }
    }
}
