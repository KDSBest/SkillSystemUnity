using System.Collections.Generic;
using System.Linq;
using SkillTreeSystem.Conditions;

namespace Assets.SkillTreeSystem.Language
{

    public static class SkillConditionParser
    {
        public static ISkillConditionCheck Parse(string text)
        {
            var tokens = SkillConditionTokenizer.Tokenize(text);

            if (tokens.Count(x => x.Type == SkillConditionTokenType.RoundBracketOpen) ==
                tokens.Count(x => x.Type == SkillConditionTokenType.RoundBracketClose))
            {
                UnityEngine.Debug.LogError("There are not as many ( as there are )");
            }

            if (tokens.Count(x => x.Type == SkillConditionTokenType.SquareBracketOpen) ==
                tokens.Count(x => x.Type == SkillConditionTokenType.SquareBracketClose))
            {
                UnityEngine.Debug.LogError("There are not as many [ as there are ]");
            }

            int i = 0;
            return ParseTillEndOrComma(tokens, ref i);
        }

        private static ISkillConditionCheck ParseTillEndOrComma(List<SkillConditionToken> tokens, ref int i)
        {
            ISkillConditionCheck currentConditionCheck = null;
            for (; i < tokens.Count && tokens[i].Type != SkillConditionTokenType.Comma && tokens[i].Type != SkillConditionTokenType.RoundBracketClose; i++)
            {
                currentConditionCheck = GetCondition(currentConditionCheck, tokens, ref i);
            }

            return currentConditionCheck;
        }

        private static ISkillConditionCheck GetCondition(ISkillConditionCheck leftCondition, List<SkillConditionToken> tokens, ref int i)
        {
            if (tokens[i].Type == SkillConditionTokenType.TextOrNumber && tokens[i].Value == "Skill")
            {
                return ParseSkill(tokens, ref i);
            }
            else if (tokens[i].Type == SkillConditionTokenType.TextOrNumber && tokens[i].Value == "Level")
            {
                return ParseLevel(tokens, ref i);
            }
            else if (tokens[i].Type == SkillConditionTokenType.And)
            {
                i++;
                return new AndConditionCheck(leftCondition, GetCondition(null, tokens, ref i));
            }
            else if (tokens[i].Type == SkillConditionTokenType.Or)
            {
                i++;
                return new OrConditionCheck(leftCondition, GetCondition(null, tokens, ref i));
            }
            else if (tokens[i].Type == SkillConditionTokenType.TextOrNumber && tokens[i].Value == "SkillForLevel")
            {
                return ParseSkillForLevel(tokens, ref i);
            }

            UnityEngine.Debug.Log("Expected expression.");
            return null;
        }

        private static ISkillConditionCheck ParseSkillForLevel(List<SkillConditionToken> tokens, ref int i)
        {
            i++;
            if (!ExpectToken(SkillConditionTokenType.SquareBracketOpen, tokens[i]))
            {
                return null;
            }

            i++;
            int? x = ReadNumber(tokens[i]);
            if (x == null)
            {
                return null;
            }

            i++;
            if (!ExpectToken(SkillConditionTokenType.SquareBracketClose, tokens[i]))
            {
                return null;
            }

            i++;
            if (!ExpectToken(SkillConditionTokenType.RoundBracketOpen, tokens[i]))
            {
                return null;
            }

            var currentSkillCondition = new SkillCheckForEveryLevel(x.Value, new Dictionary<int, ISkillConditionCheck>());

            while (tokens[i].Type != SkillConditionTokenType.RoundBracketClose)
            {
                i++;
                int? y = ReadNumber(tokens[i]);
                if (y == null)
                {
                    return null;
                }

                i++;
                if (!ExpectToken(SkillConditionTokenType.DoubleDot, tokens[i]))
                {
                    return null;
                }

                i++;
                currentSkillCondition.ConditionChecks.Add(y.Value, ParseTillEndOrComma(tokens, ref i));
            }

            if (!ExpectToken(SkillConditionTokenType.RoundBracketClose, tokens[i]))
            {
                return null;
            }

            return currentSkillCondition;
        }

        private static ISkillConditionCheck ParseLevel(List<SkillConditionToken> tokens, ref int i)
        {
            i++;
            if (!ExpectToken(SkillConditionTokenType.RoundBracketOpen, tokens[i]))
            {
                return null;
            }

            i++;
            int? x = ReadNumber(tokens[i]);
            if (x == null)
            {
                return null;
            }

            i++;
            if (!ExpectToken(SkillConditionTokenType.RoundBracketClose, tokens[i]))
            {
                return null;
            }

            return new PlayerLevelConditionCheck(x.Value);
        }

        private static ISkillConditionCheck ParseSkill(List<SkillConditionToken> tokens, ref int i)
        {
            i++;
            if (!ExpectToken(SkillConditionTokenType.SquareBracketOpen, tokens[i]))
            {
                return null;
            }

            i++;
            int? x = ReadNumber(tokens[i]);
            if (x == null)
            {
                return null;
            }

            i++;
            if (!ExpectToken(SkillConditionTokenType.SquareBracketClose, tokens[i]))
            {
                return null;
            }

            i++;
            if (!ExpectToken(SkillConditionTokenType.RoundBracketOpen, tokens[i]))
            {
                return null;
            }

            i++;
            int? y = ReadNumber(tokens[i]);
            if (y == null)
            {
                return null;
            }

            i++;
            if (!ExpectToken(SkillConditionTokenType.RoundBracketClose, tokens[i]))
            {
                return null;
            }

            return new SkillConditionCheck(x.Value, y.Value);
        }

        private static int? ReadNumber(SkillConditionToken token)
        {
            if (!ExpectToken(SkillConditionTokenType.TextOrNumber, token))
            {
                return null;
            }

            int result = -1;
            if (int.TryParse(token.Value, out result))
            {
                return result;
            }

            UnityEngine.Debug.LogError("Expected Token: Number");
            return null;
        }

        private static bool ExpectToken(SkillConditionTokenType type, SkillConditionToken token)
        {
            if (token.Type != type)
            {
                UnityEngine.Debug.LogError("Expected Token: " + type);
                return false;
            }

            return true;
        }
    }
}