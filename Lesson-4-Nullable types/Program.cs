namespace NullableTypes
{
    class Program
    {
        static void Main(string[] args)
        {
            List<SurveryQuestion> questions = new List<SurveryQuestion>();

            questions.Add(new SurveryQuestion(1, "What is your name"));

            var question2 = new SurveryQuestion(2, "how old are you?");
            question2.QuestionHint = "age";
            questions.Add(question2);

            foreach (var question in questions)
            {
                var hint = question.QuestionHint == null ? string.Empty : $"(hint: {question.QuestionHint})";

                Console.WriteLine($"{question.QuestionText} {hint}");
            }
        }

    }

    public class SurveryQuestion(int number, string text)
    {
        public string QuestionText { get; } = text;
        public int QuestionNum { get; } = number;
        public string? QuestionHint { get; set; }
    }

}