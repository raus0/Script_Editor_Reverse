using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace Script_Editor_Reverse
{
    public class DecompileChar
    {
        public static List<string> DecompileMSG(string selectedROMPath, int location)
        {
            //外部プロセスで開いているファイルを読み取る
            using (FileStream fs = new FileStream(selectedROMPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                List<string> Result = new List<string>();

                byte[] file = new BinaryReader(fs).ReadBytes((int)fs.Length);
                string m;
                string endChar = "Ω";

                Result.Add("#msg 0x" + Convert.ToString(string.Format("{0:X6}", location)));

                string resultbuffer = "";

                int t = 0;

                var mojixml = XElement.Load(@"moji.xml");

                do
                {
                    m = Convert.ToString(string.Format("{0:x2}", file[location + t]));

                    switch (m)
                    {
                        case "00":
                            m = m.Replace("00", " ");
                            resultbuffer += m;
                            t++;
                            break;

                        case "fa":
                            m = m.Replace("fa", "￥m");
                            Result.Add(resultbuffer + m);
                            resultbuffer = "";
                            t++;
                            break;

                        case "fb":
                            m = m.Replace("fb", "￥p");
                            Result.Add(resultbuffer + m);
                            resultbuffer = "";
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
                                resultbuffer += fdxx.Element("moji").Value;
                            }
                            else
                            {
                                resultbuffer += m + " ";
                            }
                            t++;
                            break;

                        case "fe":
                            m = m.Replace("fe", "￥n");
                            Result.Add(resultbuffer + m);
                            resultbuffer = "";
                            t++;
                            break;

                        case "ff":
                            m = m.Replace("ff", "Ω");
                            Result.Add(resultbuffer + m);
                            resultbuffer = "";
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
                                resultbuffer += moji.Element("moji").Value;
                            }
                            else
                            {
                                resultbuffer += m + " ";
                            }
                            t++;
                            break;
                    }
                }
                while (m != endChar);

                Result.Add("");

                return Result;
            }
        }
    }
}
