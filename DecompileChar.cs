using System;
using System.Linq;
using System.Xml.Linq;
using System.Collections.Generic;

namespace Script_Editor_Reverse
{
    public class DecompileChar
    {
        public static string DecompileMSG(int location, string address, string msgLine, byte[] file, List<int> list)
        {
            int t = 0;
            string m;
            string endChar = "Ω";

            msgLine += "\n" + "#msg " + address + "\n";

            int locationChar = location;

            locationChar = CheckOffset.Listing(locationChar, address, list);

            do
            {
                m = Convert.ToString(string.Format("{0:x2}", file[locationChar + t]));

                var mojixml = XElement.Load(@"moji.xml");

                switch (m)
                {
                    case "ab":
                        m = m.Replace("ab", "！");
                        msgLine += m;
                        t++;
                        break;

                    case "ac":
                        m = m.Replace("ac", "？");
                        msgLine += m;
                        t++;
                        break;

                    case "fa":
                        m = m.Replace("fa", "￥m");
                        msgLine += m + "\n";
                        t++;
                        break;

                    case "fb":
                        m = m.Replace("fb", "￥p");
                        msgLine += m + "\n";
                        t++;
                        break;

                    case "fd":
                        t++;
                        m += " " + Convert.ToString(string.Format("{0:x2}", file[locationChar + t]));

                        var fdxx = (
                            from p in mojixml.Elements("node")
                            where p.Element("ID").Value == m
                            select p
                            ).FirstOrDefault();

                        if (fdxx != null)
                        {
                            msgLine += fdxx.Element("moji").Value;
                        }
                        else
                        {
                            msgLine += m + " ";
                        }
                        t++;
                        break;

                    case "fe":
                        m = m.Replace("fe", "￥n");
                        msgLine += m + "\n";
                        t++;
                        break;

                    case "ff":
                        m = m.Replace("ff", "Ω");
                        msgLine += m + "\n";
                        t++;
                        break;

                    default:
                        m = Convert.ToString(string.Format("{0:x2}", file[locationChar + t]));

                        var moji = (
                            from p in mojixml.Elements("node")
                            where p.Element("ID").Value == m
                            select p
                            ).FirstOrDefault();

                        if (moji != null)
                        {
                            msgLine += moji.Element("moji").Value;
                        }
                        else
                        {
                            msgLine += m + " ";
                        }
                        t++;
                        break;
                }
            }
            while (m != endChar);

            return msgLine;
        }
    }
}
