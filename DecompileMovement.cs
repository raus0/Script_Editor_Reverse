using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace Script_Editor_Reverse
{
    public class DecompileMovement
    {
        public static List<string> DecompileCommand(string selectedROMPath, int location, string romCode)
        {
            //外部プロセスで開いているファイルを読み取る
            using (FileStream fs = new FileStream(selectedROMPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                List<string> Result = new List<string>();

                byte[] file = new BinaryReader(fs).ReadBytes((int)fs.Length);
                string m;
                string endMovement = "end";

                Result.Add("#movement 0x" + Convert.ToString(string.Format("{0:X6}", location)));
                string resultbuffer = "";

                int i = 0;

                var movementxml = XElement.Load(@"movement.xml");

                do
                {
                    m = Convert.ToString(string.Format("{0:X2}", file[location + i]));

                    switch (m)
                    {
                        case "FE":
                            m = m.Replace("FE", "end");

                            Result.Add(resultbuffer + m);
                            i++;
                            break;

                        default:
                            m = Convert.ToString(string.Format("{0:X2}", file[location + i]));

                            var movement = (
                                from p in movementxml.Elements("node")
                                where p.Element("ID").Value == m
                                select p
                                ).FirstOrDefault();

                            if (movement != null)
                            {
                                if (romCode == "BPRJ" || romCode == "BPRE" || romCode == "BPGJ" || romCode == "BPGE")
                                {
                                    Result.Add(resultbuffer + movement.Element("FRLG").Value);
                                }
                                else if (romCode == "BPEJ" || romCode == "BPEE")
                                {
                                    Result.Add(resultbuffer + movement.Element("EM").Value);
                                }
                                else
                                {
                                    Result.Add(resultbuffer + m);
                                }
                            }
                            else
                            {
                                Result.Add(resultbuffer + m);
                            }
                            i++;
                            break;
                    }
                }
                while (m != endMovement);

                Result.Add("");

                return Result;
            }
        }
    }
}
