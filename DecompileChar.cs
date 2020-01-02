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
                string n;
                string o;
                string p;
                string q;

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

                        case "f7":
                            t++;
                            m += " " + Convert.ToString(string.Format("{0:x2}", file[location + t]));

                            var f7xx = (
                                from e in mojixml.Elements("node")
                                where e.Element("ID").Value == m
                                select e
                                ).FirstOrDefault();

                            if (f7xx != null)
                            {
                                resultbuffer += f7xx.Element("moji").Value;
                            }
                            else
                            {
                                resultbuffer += m + " ";
                            }
                            t++;
                            break;

                        case "f8":
                            t++;
                            m += " " + Convert.ToString(string.Format("{0:x2}", file[location + t]));

                            var f8xx = (
                                from e in mojixml.Elements("node")
                                where e.Element("ID").Value == m
                                select e
                                ).FirstOrDefault();

                            if (f8xx != null)
                            {
                                resultbuffer += f8xx.Element("moji").Value;
                            }
                            else
                            {
                                resultbuffer += m + " ";
                            }
                            t++;
                            break;

                        case "f9":
                            t++;
                            m += " " + Convert.ToString(string.Format("{0:x2}", file[location + t]));

                            var f9xx = (
                                from e in mojixml.Elements("node")
                                where e.Element("ID").Value == m
                                select e
                                ).FirstOrDefault();

                            if (f9xx != null)
                            {
                                resultbuffer += f9xx.Element("moji").Value;
                            }
                            else
                            {
                                resultbuffer += m + " ";
                            }
                            t++;
                            break;

                        case "fa":

                            var fa = (
                                from d in mojixml.Elements("node")
                                where d.Element("ID").Value == m
                                select d
                                ).FirstOrDefault();

                            if (fa != null)
                            {
                                m = fa.Element("moji").Value;
                            }
                            else
                            {
                                m = m + " ";
                            }


                            Result.Add(resultbuffer + m);
                            resultbuffer = "";

                            t++;
                            break;

                        case "fb":

                            var fb = (
                                from d in mojixml.Elements("node")
                                where d.Element("ID").Value == m
                                select d
                                ).FirstOrDefault();

                            if (fb != null)
                            {
                                m = fb.Element("moji").Value;
                            }
                            else
                            {
                                m = m + " ";
                            }

                            Result.Add(resultbuffer + m);
                            resultbuffer = "";

                            t++;
                            break;

                        case "fc":
                            t++;
                            n = Convert.ToString(string.Format("{0:x2}", file[location + t]));
                            m += " " + n;

                            if (n == "07" || n == "09" || n == "0a" || n == "0f" || n == "15" || n == "16" || n == "17" || n == "18")
                            {
                                var fcA = (
                                    from a in mojixml.Elements("node")
                                    where a.Element("ID").Value == m
                                    select a
                                    ).FirstOrDefault();

                                if (fcA != null)
                                {
                                    m = fcA.Element("moji").Value;
                                }
                                else
                                {
                                    m = m + " ";
                                }
                                t++;
                                break;
                            }

                            t++;
                            o = Convert.ToString(string.Format("{0:x2}", file[location + t]));
                            m += " " + o;

                            if (n != "04" && n != "0b" && n != "10")
                            {
                                var fcAB = (
                                    from b in mojixml.Elements("node")
                                    where b.Element("ID").Value == m
                                    select b
                                    ).FirstOrDefault();

                                if (fcAB != null)
                                {
                                    resultbuffer += fcAB.Element("moji").Value;
                                }
                                else
                                {
                                    resultbuffer += m + " ";
                                }
                                t++;
                                break;
                            }

                            t++;
                            p = Convert.ToString(string.Format("{0:x2}", file[location + t]));
                            m += " " + p;

                            if (n == "0b" || n == "10")
                            {
                                var fcABC = (
                                    from c in mojixml.Elements("node")
                                    where c.Element("ID").Value == m
                                    select c
                                    ).FirstOrDefault();

                                if (fcABC != null)
                                {
                                    resultbuffer += fcABC.Element("moji").Value;
                                }
                                else
                                {
                                    resultbuffer += m + " ";
                                }
                                t++;
                                break;
                            }

                            t++;
                            q = Convert.ToString(string.Format("{0:x2}", file[location + t]));
                            m += " " + q;

                            if (n == "04")
                            {
                                var fcABCD = (
                                    from d in mojixml.Elements("node")
                                    where d.Element("ID").Value == m
                                    select d
                                    ).FirstOrDefault();

                                if (fcABCD != null)
                                {
                                    resultbuffer += fcABCD.Element("moji").Value;
                                }
                                else
                                {
                                    resultbuffer += m + " ";
                                }
                                t++;
                                break;
                            }

                            t++;
                            break;

                        case "fd":
                            t++;
                            m += " " + Convert.ToString(string.Format("{0:x2}", file[location + t]));

                            var fdxx = (
                                from e in mojixml.Elements("node")
                                where e.Element("ID").Value == m
                                select e
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

                            var fe = (
                                from d in mojixml.Elements("node")
                                where d.Element("ID").Value == m
                                select d
                                ).FirstOrDefault();

                            if (fe != null)
                            {
                                m = fe.Element("moji").Value;
                            }
                            else
                            {
                                m = m + " ";
                            }

                            Result.Add(resultbuffer + m);
                            resultbuffer = "";

                            t++;
                            break;

                        case "ff":

                            var ff = (
                                from d in mojixml.Elements("node")
                                where d.Element("ID").Value == m
                                select d
                                ).FirstOrDefault();

                            if (ff != null)
                            {
                                m = ff.Element("moji").Value;
                            }
                            else
                            {
                                m = m + " ";
                            }

                            Result.Add(resultbuffer + m);
                            resultbuffer = "";

                            m = "ff";

                            t++;
                            break;

                        default:

                            var moji = (
                                from d in mojixml.Elements("node")
                                where d.Element("ID").Value == m
                                select d
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
                while (m != "ff");

                Result.Add("");

                return Result;
            }
        }

        public static List<string> DecompileBinary(string selectedROMPath, int location)
        {
            using (FileStream fs = new FileStream(selectedROMPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                List<string> Result = new List<string>();

                byte[] file = new BinaryReader(fs).ReadBytes((int)fs.Length);
                string m;
                string n;
                string o;
                string p;
                string q;

                Result.Add("#msg 0x" + Convert.ToString(string.Format("{0:X6}", location)));

                string resultbuffer = "";

                int t = 0;

                do
                {
                    m = Convert.ToString(string.Format("{0:x2}", file[location + t]));

                    switch (m)
                    {
                        case "00":
                            m = m.Replace("00", "00 ");
                            resultbuffer += m;
                            t++;
                            break;

                        case "fa":
                            m = m.Replace("fa", "fa ");
                            Result.Add(resultbuffer + m);
                            resultbuffer = "";
                            t++;
                            break;

                        case "fb":
                            m = m.Replace("fb", "fb ");
                            Result.Add(resultbuffer + m);
                            resultbuffer = "";
                            t++;
                            break;

                        case "fc":
                            t++;
                            n = Convert.ToString(string.Format("{0:x2}", file[location + t]));
                            m += " " + n;

                            if (n == "07" || n == "09" || n == "0a" || n == "0f" || n == "15" || n == "16" || n == "17" || n == "18")
                            {
                                resultbuffer += m + " ";
                                t++;
                                break;
                            }

                            t++;
                            o = Convert.ToString(string.Format("{0:x2}", file[location + t]));
                            m += " " + o;

                            if (n != "04" && n != "0b" && n != "10")
                            {
                                resultbuffer += m + " ";
                                t++;
                                break;
                            }

                            t++;
                            p = Convert.ToString(string.Format("{0:x2}", file[location + t]));
                            m += " " + p;

                            if (n == "0b" || n == "10")
                            {
                                resultbuffer += m + " ";
                                t++;
                                break;
                            }

                            t++;
                            q = Convert.ToString(string.Format("{0:x2}", file[location + t]));
                            m += " " + q;

                            if (n == "04")
                            {
                                resultbuffer += m + " ";
                                t++;
                                break;
                            }

                            t++;
                            break;

                        case "fd":
                            t++;
                            m += " " + Convert.ToString(string.Format("{0:x2}", file[location + t]));
                            resultbuffer += m + " ";
                            t++;
                            break;

                        case "fe":
                            m = m.Replace("fe", "fe ");
                            Result.Add(resultbuffer + m);
                            resultbuffer = "";
                            t++;
                            break;

                        case "ff":
                            Result.Add(resultbuffer + m);
                            resultbuffer = "";
                            t++;
                            break;

                        default:
                            m = Convert.ToString(string.Format("{0:x2}", file[location + t]));
                            resultbuffer += m + " ";
                            t++;
                            break;
                    }
                }
                while (m != "ff");

                Result.Add("");

                return Result;
            }
        }

        public static List<string> DecompileRaw(string selectedROMPath, int location)
        {
            using (FileStream fs = new FileStream(selectedROMPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                List<string> Result = new List<string>();

                byte[] file = new BinaryReader(fs).ReadBytes((int)fs.Length);
                string m;
                string n;
                string o;
                string p;
                string q;

                Result.Add("$" + Convert.ToString(string.Format("{0:X6}", location)) + ":");

                string resultbuffer = "";

                int t = 0;

                resultbuffer += ".byte ";

                do
                {
                    m = Convert.ToString(string.Format("{0:x2}", file[location + t]));

                    switch (m)
                    {
                        case "00":
                            m = m.Replace("00", "0x00,");
                            resultbuffer += m;

                            t++;
                            break;

                        case "fa":
                            m = m.Replace("fa", "0xfa");
                            Result.Add(resultbuffer + m);
                            resultbuffer = ".byte ";

                            t++;
                            break;

                        case "fb":
                            m = m.Replace("fb", "0xfb");
                            Result.Add(resultbuffer + m);
                            resultbuffer = ".byte ";

                            t++;
                            break;

                        case "fc":
                            resultbuffer += m.Replace("fc", "0xfc,");

                            t++;
                            n = Convert.ToString(string.Format("{0:x2}", file[location + t]));
                            resultbuffer += "0x" + n + ",";

                            if (n == "07" || n == "09" || n == "0a" || n == "0f" || n == "15" || n == "16" || n == "17" || n == "18")
                            {
                                t++;
                                break;
                            }

                            t++;
                            o = Convert.ToString(string.Format("{0:x2}", file[location + t]));
                            resultbuffer += "0x" + o + ",";

                            if (n != "04" && n != "0b" && n != "10")
                            {
                                t++;
                                break;
                            }

                            t++;
                            p = Convert.ToString(string.Format("{0:x2}", file[location + t]));
                            resultbuffer += "0x" + p + ",";

                            if (n == "0b" || n == "10")
                            {
                                t++;
                                break;
                            }

                            t++;
                            q = Convert.ToString(string.Format("{0:x2}", file[location + t]));
                            resultbuffer += "0x" + q + ",";

                            if (n == "04")
                            {
                                t++;
                                break;
                            }

                            t++;
                            break;

                        case "fd":
                            resultbuffer += m.Replace("fd", "0xfd,");

                            t++;
                            n = Convert.ToString(string.Format("{0:x2}", file[location + t]));
                            resultbuffer += "0x" + n + ",";

                            t++;
                            break;

                        case "fe":
                            m = m.Replace("fe", "0xfe");
                            Result.Add(resultbuffer + m);
                            resultbuffer = ".byte ";

                            t++;
                            break;

                        case "ff":
                            m = m.Replace("ff", "0xff");
                            Result.Add(resultbuffer + m);
                            resultbuffer = "";

                            t++;
                            break;

                        default:
                            m = Convert.ToString(string.Format("{0:x2}", file[location + t]));
                            resultbuffer += "0x" + m + ",";

                            t++;
                            break;
                    }
                }
                while (m != "0xff");

                Result.Add("");

                return Result;
            }
        }
    }
}
