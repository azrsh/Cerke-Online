using System;
using System.Text;

namespace Azarashi.Utilities.Text
{
    public class RandomStringGenerator
    {
        readonly string usableCharacters;

        public RandomStringGenerator(string usableCharacters = "0123456789abcdefghijklmnopqrstuvwxyz_-")
        {
            this.usableCharacters = usableCharacters;
        }

        public string Generate(int length)
        {
            StringBuilder stringBuilder = new StringBuilder(length);
            Random random = new Random();

            for (int i = 0; i < length; i++)
            {
                int characterIndex = random.Next(usableCharacters.Length);
                char character = usableCharacters[characterIndex];
                stringBuilder.Append(character);
            }

            return stringBuilder.ToString();
        }
    }
}