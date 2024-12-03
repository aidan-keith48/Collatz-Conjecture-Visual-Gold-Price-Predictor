using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collatz_Conjecture_WFA
{
    public class ConjectureLogic
    {
        private readonly List<long> _collatzSequence = new List<long>();

        public List<long> dataListPublic => this._collatzSequence;

        public ConjectureLogic()
        {
            _collatzSequence = new List<long>();
        }

        //METHOD RETURN THE COLLATZ SEQUENCE
        public List<long> CollatzSequence(long number)
        {
            _collatzSequence.Clear();
            _collatzSequence.Add(number);

            while (number != 1)
            {
                if (number % 2 == 0)
                {
                    number = number / 2;
                }
                else
                {
                    number = 3 * number + 1;
                }
                _collatzSequence.Add(number);
            }
            return _collatzSequence;
        }

        public string DisplayList()
        {
            StringBuilder results = new StringBuilder();
            foreach (long number in _collatzSequence)
            {
                results.Append(number);
                results.Append(" ");
            }
            return results.ToString();
        }
    }
}
