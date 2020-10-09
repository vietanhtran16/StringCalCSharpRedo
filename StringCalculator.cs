using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace c_sharp_kata
{
    public class StringCalculator
    {
        private string[] defaultDelimiters = new String[] { ",", "\n" };
        private Regex multiCharCustomDelimiterRegex = new Regex(@"\[(.*?)\]");
        public int Add(string numbers)
        {
            if (numbers.Length == 0)
            {
                return 0;
            }

            var parsedNumbers = IsCustomDelimiter(numbers) ? GetNumbersWithCustomDelimiter(numbers) : GetNumbersWithDefaultDelimiter(numbers);
            var validNumbers = Validate(parsedNumbers);
            return validNumbers.Sum();
        }

        private bool IsCustomDelimiter(string numbers)
        {
            return numbers.StartsWith("//");
        }

        private int[] GetNumbersWithCustomDelimiter(string numbers)
        {
            var delimiterAndNumbers = numbers.Split("\n");
            var customDelimiterDefinition = delimiterAndNumbers[0].Replace("//", string.Empty);
            var customDelimiter = GetCustomDelimiters(customDelimiterDefinition);
            var extractedNumbers = delimiterAndNumbers[1];
            return ParseNumbers(customDelimiter, extractedNumbers);
        }

        private string[] GetCustomDelimiters(string customDelimiterDefinition)
        {
            var isMultiCharCustomDelimiter = customDelimiterDefinition.StartsWith("[");
            if (isMultiCharCustomDelimiter)
            {
                var matches = multiCharCustomDelimiterRegex.Matches(customDelimiterDefinition);
                return matches.Select(match => match.Groups[1].Value).ToArray();
            }
            else
            {
                return new string[] { customDelimiterDefinition };
            }
        }

        private int[] GetNumbersWithDefaultDelimiter(string numbers)
        {
            return ParseNumbers(defaultDelimiters, numbers);
        }

        private int[] ParseNumbers(string[] customDelimiter, string extractedNumbers)
        {
            return extractedNumbers.Split(customDelimiter, StringSplitOptions.None).Select(number => int.Parse(number)).ToArray();
        }

        private int[] Validate(int[] numbers)
        {
            var negativeNumbers = numbers.Where(number => number < 0);
            if (negativeNumbers.Count() > 0)
            {
                throw new Exception($"negatives not allowed: {string.Join(", ", negativeNumbers)}");
            }
            else
            {
                return numbers.Where(number => number < 1001).ToArray();
            }
        }
    }
}