namespace Problem04.BalancedParentheses
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class BalancedParenthesesSolve : ISolvable
    {
        public bool AreBalanced(string parentheses)
        {

            while (parentheses.Length > 0)
            {
                char currentBracket = parentheses[0];
                char oppositeBracket = currentBracket switch
                {
                    '{' => '}',
                    '(' => ')',
                    '[' => ']',
                    _=> default
                };

                if (oppositeBracket == default)
                {
                    return false;
                }

                int indexOfCurrentBracket = parentheses.IndexOf(currentBracket);
                int indexOfoppositeBracket = parentheses.IndexOf(oppositeBracket);

                if (indexOfoppositeBracket == -1)
                {
                    return false;
                }

                parentheses = parentheses.Remove(indexOfoppositeBracket, 1);
                parentheses = parentheses.Remove(indexOfCurrentBracket, 1);
            }

            return true;
        }
    }
}
