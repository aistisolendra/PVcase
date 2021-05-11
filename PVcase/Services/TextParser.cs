using System.Text.RegularExpressions;

namespace PVcase.Services
{
    public class TextParser
    {
        private readonly Regex _onlyInt = new Regex("[^0-9.]+");
        public bool IsTextInt(string text)
        {
            return _onlyInt.IsMatch(text);
        }

        public bool IsInAngleLimit(string text)
        {
            return int.TryParse(text, out int value) && value >= 0 && value <= 60;
        }
    }
}
