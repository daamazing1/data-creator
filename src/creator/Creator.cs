using System.Text.RegularExpressions;

namespace creator
{
    public static class Creator
    {
        //ASCII codes for letters
        private const int PATTERNLOWERLETTER = 108;
        private const int PATTERNUPPERLETTER = 76;
        private const int PATTERNNUMBER = 110;
        private const int PATTERNSPECIAL = 115;
        private const int PATTERNANY = 42;
        private const int PATTERNESCAPE = 92;
        private const int PATTERNLENGTHSTART = 123;
        private const int PATTERNLENGTHRANGE = 45;
        private const int PATTERNLENGTHEND = 125;
        private const byte NORMALSTATE = 0;
        private const byte ESCAPESTATE = 1;
        private const byte LENGTHSTATE = 2;


        /// <summary>
        /// Validate input pattern string to allowed pattern.
        /// Possible characters are:
        /// L - uppercase character
        /// l - lowercase character
        /// n - number
        /// s - special
        /// * - any character
        /// \ - escape character
        /// {min-max} length of previous characater
        /// 
        /// example: "llLLnnl\T", "l{1-15}", "n{3}\-n{2}\-n{4}"
        /// </summary>
        /// <param name="pattern"></param>
        public static bool ValidatePattern(string pattern)
        {
            var state = NORMALSTATE;
            foreach (var c in pattern)
            {
                if (state == NORMALSTATE)
                {
                    // In a NORMALSTATE the current character is check for validity.
                    if (IsLowerLetterPattern(c) |
                        IsUpperLetterPattern(c) |
                        IsAnyPattern(c) |
                        IsSpecialPattern(c) |
                        IsNumberPattern(c)) continue;

                    // Character starts a change in pattern matching.
                    if (IsEscapePattern(c))
                    {
                        state = ESCAPESTATE;
                        continue;
                    }
                }

                if(state == ESCAPESTATE)
                {
                    // any character read after the escape character is valid.  Consume the character
                    // and return to a NORMAL STATE
                    state = NORMALSTATE;
                    continue;
                }
                return false;
            }
            return state == NORMALSTATE;
        }

        /// <summary>
        /// test if character is a lowercase letter
        /// </summary>
        /// <param name="c">character to match against</param>
        /// <returns>true, if character is a lowercase letter</returns> 
        private static bool IsLowerLetter(char c)
        {
            return (c >= 97 && c <= 122);
        }

        /// <summary>
        /// test if character is uppercase letter
        /// </summary>
        /// <param name="c">character to match against</param>
        /// <returns>true, if character is a uppercase letter</returns>
        private static bool IsUpperLetter(char c)
        {
            return (c >= 65 && c <= 90);
        }

        /// <summary>
        /// Test if the pattern character is "l"
        /// </summary>
        /// <param name="c">Character to match</param>
        /// <returns>true, if character is "l"</returns>
        private static bool IsLowerLetterPattern(char c)
        {
            return c == PATTERNLOWERLETTER;
        }

        /// <summary>
        /// Test pattern character is "L"
        /// </summary>
        /// <param name="c">Character to match</param>
        /// <returns>true, if character is "L"</returns>
        private static bool IsUpperLetterPattern(char c)
        {
            return c == PATTERNUPPERLETTER;
        }

        /// <summary>
        /// Test pattern for "n"
        /// </summary>
        /// <param name="c">Character to match</param>
        /// <returns>true, if character is "n"</returns>
        private static bool IsNumberPattern(char c)
        {
            return c == PATTERNNUMBER;
        }

        /// <summary>
        /// Test pattern for "s"
        /// </summary>
        /// <param name="c">Character to match</param>
        /// <returns>true, if character is "s"</returns>
        private static bool IsSpecialPattern(char c)
        {
            return c == PATTERNSPECIAL;
        }

        /// <summary>
        /// Test pattern for "*"
        /// </summary>
        /// <param name="c">Character to match</param>
        /// <returns>true, if character is "*"</returns>
        private static bool IsAnyPattern(char c)
        {
            return c == PATTERNANY;
        }

        /// <summary>
        /// Test pattern for "\"
        /// </summary>
        /// <param name="c">Character to match</param>
        /// <returns>true, if character is "\"</returns>
        private static bool IsEscapePattern(char c)
        {
            return c == PATTERNESCAPE;
        }
    }
}
