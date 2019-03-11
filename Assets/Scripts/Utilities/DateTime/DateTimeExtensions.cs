namespace Azarashi.Utilities.DateTime
{
    public static class DateTimeExtensions
    {
        /// <summary>
        /// DateTimeの拡張メソッド. 日時を数字のみの文字列に変換する. 各項目の数値が指定桁数以下の場合、自動的に0が挿入される.
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static string ToStringOfNumberOnly(this System.DateTime dateTime)
        {
            System.DateTime now = System.DateTime.Now;
            string year = AlignDigits(now.Year.ToString(), 4);
            string month = AlignDigits(now.Month.ToString(), 2);
            string day = AlignDigits(now.Day.ToString(), 2);
            string hour = AlignDigits(now.Hour.ToString(), 2);
            string minute = AlignDigits(now.Minute.ToString(), 2);
            string second = AlignDigits(now.Second.ToString(), 2);
            string millisecond = AlignDigits(now.Millisecond.ToString(), 3);

            string result = year + month + day + hour + minute + second + millisecond;
            return result;
        }

        /// <summary>
        /// 文字列の桁数が一定以下なら前の部分に0を挿入する.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="numberOfDigits"></param>
        /// <returns></returns>
        static string AlignDigits(string value, int numberOfDigits)
        {
            int shortage = numberOfDigits - value.Length;
            string zeroString = string.Empty;
            for (int i = 0; i < shortage; i++)
                zeroString += "0";
            value = zeroString + value;
            return value;
        }
    }
}