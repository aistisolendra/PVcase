using System.Text.RegularExpressions;

namespace PVcase.Services
{
    public class TextParser
    {
        private readonly Regex _onlyInt = new Regex("[^0-9.-]+");
        public bool IsTextInt(string text)
        {
            return _onlyInt.IsMatch(text);
        }
    }
}
