using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace Script_Editor_Reverse
{
    public class DecompileMovement
    {
        public static string DecompileCommand(string selectedROMPath, int location, string romCode)
        {
            //外部プロセスで開いているファイルを読み取る
            using (FileStream fs = new FileStream(selectedROMPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                byte[] file = new BinaryReader(fs).ReadBytes((int)fs.Length);
                string m;
                string endMovement = "end";

                string toReturn = "#movement 0x" + Convert.ToString(string.Format("{0:X6}", location)) + "\n";

                int i = 0;

                do
                {
                    m = Convert.ToString(string.Format("{0:X2}", file[location + i]));

                    var movementxml = XElement.Load(@"movement.xml");

                    switch (m)
                    {
                        case "FE":
                            m = m.Replace("FE", "end");
                            toReturn += m + "\n";
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
                                if(romCode == "BPRJ" || romCode == "BPRE" || romCode == "BPGJ" || romCode == "BPGE")
                                {
                                    toReturn += movement.Element("FRLG").Value + "\n";
                                }
                                else if(romCode == "BPEJ" || romCode == "BPEE")
                                {
                                    toReturn += movement.Element("EM").Value + "\n";
                                }
                                else
                                {
                                    toReturn += m + " \n";
                                }
                            }
                            else
                            {
                                toReturn += m + " \n";
                            }
                            i++;
                            break;
                    }
                }
                while (m != endMovement);

                return toReturn;
            }
        }
    }
}
