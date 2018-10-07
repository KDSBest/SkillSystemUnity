using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityScript.Parser;

namespace Assets.SkillTreeSystem.Language
{
    public static class SkillConditionTokenizer
    {
        public static List<SkillConditionToken> Tokenize(string unprocessedText)
        {
            var result = new List<SkillConditionToken>();
            var textToken = new SkillConditionToken(SkillConditionTokenType.TextOrNumber);
            var text = unprocessedText.Replace("\t", string.Empty).Replace(" ", string.Empty).Replace("\r", string.Empty).Replace("\n", string.Empty);
            for (int i = 0; i < text.Length; i++)
            {
                switch(text[i])
                {
                    case '&':
                        PopulizeTextToken(textToken, result);
                        textToken = new SkillConditionToken(SkillConditionTokenType.TextOrNumber);

                        result.Add(new SkillConditionToken(SkillConditionTokenType.And, "&"));
                        break;
                    case '|':
                        PopulizeTextToken(textToken, result);
                        textToken = new SkillConditionToken(SkillConditionTokenType.TextOrNumber);

                        result.Add(new SkillConditionToken(SkillConditionTokenType.Or, "|"));
                        break;
                    case '[':
                        PopulizeTextToken(textToken, result);
                        textToken = new SkillConditionToken(SkillConditionTokenType.TextOrNumber);

                        result.Add(new SkillConditionToken(SkillConditionTokenType.SquareBracketOpen, "["));
                        break;
                    case ']':
                        PopulizeTextToken(textToken, result);
                        textToken = new SkillConditionToken(SkillConditionTokenType.TextOrNumber);

                        result.Add(new SkillConditionToken(SkillConditionTokenType.SquareBracketClose, "["));
                        break;
                    case '(':
                        PopulizeTextToken(textToken, result);
                        textToken = new SkillConditionToken(SkillConditionTokenType.TextOrNumber);

                        result.Add(new SkillConditionToken(SkillConditionTokenType.RoundBracketOpen, "("));
                        break;
                    case ')':
                        PopulizeTextToken(textToken, result);
                        textToken = new SkillConditionToken(SkillConditionTokenType.TextOrNumber);

                        result.Add(new SkillConditionToken(SkillConditionTokenType.RoundBracketClose, ")"));
                        break;
                    case ',':
                        PopulizeTextToken(textToken, result);
                        textToken = new SkillConditionToken(SkillConditionTokenType.TextOrNumber);

                        result.Add(new SkillConditionToken(SkillConditionTokenType.Comma, ","));
                        break;
                    case ':':
                        PopulizeTextToken(textToken, result);
                        textToken = new SkillConditionToken(SkillConditionTokenType.TextOrNumber);

                        result.Add(new SkillConditionToken(SkillConditionTokenType.DoubleDot, ":"));
                        break;
                    default:
                        textToken.Value += text[i];
                        break;
                }
            }

            PopulizeTextToken(textToken, result);
            return result;
        }

        private static void PopulizeTextToken(SkillConditionToken textToken, List<SkillConditionToken> result)
        {
            if (!string.IsNullOrEmpty(textToken.Value))
            {
                result.Add(textToken);
            }
        }
    }
}
