using System;
using System.IO;
using MessageBox = System.Windows.MessageBox;
using System.Collections.Generic;

namespace Script_Editor_Reverse
{
    public class CheckOffset
    {
        public static int Listing(int location, string DecompileOffset, List<int> list)
        {
            //入力したオフセットをチェック
            bool success = int.TryParse(DecompileOffset, out location);
            if (!success)
            {
                success = int.TryParse(ToDecimal(DecompileOffset), out location);
                if (!success)
                {
                    MessageBox.Show("入力したオフセットが無効です。");
                }
            }
            try
            {
                list.Add(location);

                //return location; ここに記述しても値を返さない　tryを削ることも不可
            }
            catch (IOException)
            {
                MessageBox.Show("ROMを読み込めません。");
            }
            catch (Exception exc)
            {
                MessageBox.Show("不明な例外がありました。\n" + exc);
            }

            return location;
        }

        private static string ToDecimal(string input)
        {
            if (input.ToLower().StartsWith("0x"))
            {
                return Convert.ToUInt32(input.Substring(2), 16).ToString();
            }
            else
            {
                return Convert.ToUInt32(input, 16).ToString();
            }
        }
    }
}
