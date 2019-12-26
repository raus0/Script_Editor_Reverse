using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace Script_Editor_Reverse
{
    public class DecompileChar
    {
        public static string DecompileMSG(string selectedROMPath, int location)
        {
            //外部プロセスで開いているファイルを読み取る
            using (FileStream fs = new FileStream(selectedROMPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                byte[] file = new BinaryReader(fs).ReadBytes((int)fs.Length);
                string m;
                string endChar = "Ω";

                string toReturn = "#msg 0x" + Convert.ToString(string.Format("{0:X6}", location)) + "\n";

                int t = 0;

                do
                {
                    m = Convert.ToString(string.Format("{0:x2}", file[location + t]));

                    var mojixml = XElement.Load(@"moji.xml");

                    switch (m)
                    {
                        case "ab":
                            m = m.Replace("ab", "！");
                            toReturn += m;
                            t++;
                            break;

                        case "ac":
                            m = m.Replace("ac", "？");
                            toReturn += m;
                            t++;
                            break;

                        case "fa":
                            m = m.Replace("fa", "￥m");
                            toReturn += m + "\n";
                            t++;
                            break;

                        case "fb":
                            m = m.Replace("fb", "￥p");
                            toReturn += m + "\n";
                            t++;
                            break;

                        case "fd":
                            t++;
                            m += " " + Convert.ToString(string.Format("{0:x2}", file[location + t]));

                            var fdxx = (
                                from p in mojixml.Elements("node")
                                where p.Element("ID").Value == m
                                select p
                                ).FirstOrDefault();

                            if (fdxx != null)
                            {
                                toReturn += fdxx.Element("moji").Value;
                            }
                            else
                            {
                                toReturn += m + " ";
                            }
                            t++;
                            break;

                        case "fe":
                            m = m.Replace("fe", "￥n");
                            toReturn += m + "\n";
                            t++;
                            break;

                        case "ff":
                            m = m.Replace("ff", "Ω");
                            toReturn += m + "\n";
                            t++;
                            break;

                        default:
                            m = Convert.ToString(string.Format("{0:x2}", file[location + t]));

                            var moji = (
                                from p in mojixml.Elements("node")
                                where p.Element("ID").Value == m
                                select p
                                ).FirstOrDefault();

                            if (moji != null)
                            {
                                toReturn += moji.Element("moji").Value;
                            }
                            else
                            {
                                toReturn += m + " ";
                            }
                            t++;
                            break;
                    }
                }
                while (m != endChar);

                return toReturn;
            }
        }
    }
}
