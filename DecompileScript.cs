using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using System.Collections.Generic;

namespace Script_Editor_Reverse
{
    public class DecompileScript
    {
        public static string DecompileCommand(string selectedROMPath, int location, List<int> list, List<int> msg)
        {
            //外部プロセスで開いているファイルを読み取る
            using (FileStream fs = new FileStream(selectedROMPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                byte[] file = new BinaryReader(fs).ReadBytes((int)fs.Length);
                string c;
                string endCmd = ".endm";
                string subLine = "";
                string cmdLine = "";
                string address = "";
                string offset = "";
                string BC = "";
                string CommandType = "";

                string toReturn = "#org 0x" + Convert.ToString(string.Format("{0:X6}", location)) + "\n";

                int i = 0;
                int j = 0;
                int k = 0;
                int arg = 0;

                int sublocation;
                int locationChar;

                do
                {
                    c = Convert.ToString(string.Format("{0:X2}", file[location + i]));
                    c.ToUpper();

                    var commandxml = XElement.Load(@"command.xml");

                    var Name = (
                        from p in commandxml.Elements("node")
                        where p.Element("ID").Value == c
                        select p
                        ).FirstOrDefault();

                    if (Name != null)
                    {
                        BC = Name.Element("Name").Value;
                    }
                    else
                    {
                        BC = c;
                    }

                    var Type = (
                        from p in commandxml.Elements("node")
                        where p.Element("ID").Value == c
                        select p
                        ).FirstOrDefault();

                    if (Type != null)
                    {
                        CommandType = Type.Element("Type").Value;
                    }
                    else
                    {
                        return null;
                    }

                    //構文全パターン 要精度確認
                    switch (CommandType)
                    {
                        case ".endm":
                            toReturn += BC + "\n";
                            i++;
                            break;

                        case ".void":
                            toReturn += BC + "\n";
                            i++;
                            break;

                        case ".byte":
                            cmdLine = BC + " ";

                            i++;
                            c = Convert.ToString(string.Format("0x{0:X1}", file[location + i]));
                            c.ToUpper();
                            cmdLine += c;

                            toReturn += cmdLine + "\n";
                            i++;
                            break;

                        case ".bytex2":
                            cmdLine = BC + " ";

                            for (int n = 0; n < 2; n++)
                            {
                                i++;
                                c = Convert.ToString(string.Format("0x{0:X1}", file[location + i]));
                                c.ToUpper();
                                cmdLine += c;

                                if (n < 1)
                                {
                                    cmdLine += " ";
                                }
                            }

                            toReturn += cmdLine + "\n";
                            i++;
                            break;

                        case ".bytex3":
                            cmdLine = BC + " ";

                            for (int n = 0; n < 3; n++)
                            {
                                i++;
                                c = Convert.ToString(string.Format("0x{0:X1}", file[location + i]));
                                c.ToUpper();
                                cmdLine += c;

                                if (n < 2)
                                {
                                    cmdLine += " ";
                                }
                            }

                            toReturn += cmdLine + "\n";
                            i++;
                            break;

                        case ".bytex4":
                            cmdLine = BC + " ";

                            for (int n = 0; n < 4; n++)
                            {
                                i++;
                                c = Convert.ToString(string.Format("0x{0:X1}", file[location + i]));
                                c.ToUpper();
                                cmdLine += c;

                                if (n < 3)
                                {
                                    cmdLine += " ";
                                }
                            }

                            toReturn += cmdLine + "\n";
                            i++;
                            break;

                        case ".bytex5":
                            cmdLine = BC + " ";

                            for (int n = 0; n < 5; n++)
                            {
                                i++;
                                c = Convert.ToString(string.Format("0x{0:X1}", file[location + i]));
                                c.ToUpper();
                                cmdLine += c;

                                if (n < 4)
                                {
                                    cmdLine += " ";
                                }
                            }

                            toReturn += cmdLine + "\n";
                            i++;
                            break;

                        case ".hword":
                            cmdLine = BC + " ";

                            i++;
                            k = i;
                            address = "0x";
                            offset = "";

                            for (int w = 1; w >= 0; w--)
                            {
                                offset += Convert.ToString(string.Format("{0:X2}", file[location + k + w]));
                                if (w == 1)
                                {
                                    offset = offset.Replace("00", "");
                                }
                                i++;
                            }
                            i--;
                            offset.ToUpper();

                            address += offset;
                            address = address.Replace("0x0", "0x");

                            cmdLine += address;

                            toReturn += cmdLine + "\n";
                            i++;
                            break;

                        case ".hwordx2":
                            cmdLine = BC + " ";

                            for (int n = 0; n < 2; n++)
                            {
                                i++;
                                k = i;
                                address = "0x";
                                offset = "";

                                for (int w = 1; w >= 0; w--)
                                {
                                    offset += Convert.ToString(string.Format("{0:X2}", file[location + k + w]));
                                    if (w == 1)
                                    {
                                        offset = offset.Replace("00", "");
                                    }
                                    i++;
                                }
                                i--;
                                offset.ToUpper();

                                address += offset;
                                address = address.Replace("0x0", "0x");

                                if (n < 1)
                                {
                                    address += " ";
                                }

                                cmdLine += address;
                            }

                            toReturn += cmdLine + "\n";
                            i++;
                            break;

                        case ".hwordx3":
                            cmdLine = BC + " ";

                            for (int n = 0; n < 3; n++)
                            {
                                i++;
                                k = i;
                                address = "0x";
                                offset = "";

                                for (int w = 1; w >= 0; w--)
                                {
                                    offset += Convert.ToString(string.Format("{0:X2}", file[location + k + w]));
                                    if (w == 1)
                                    {
                                        offset = offset.Replace("00", "");
                                    }
                                    i++;
                                }
                                i--;
                                offset.ToUpper();

                                address += offset;
                                address = address.Replace("0x0", "0x");

                                if (n < 2)
                                {
                                    address += " ";
                                }

                                cmdLine += address;
                            }

                            toReturn += cmdLine + "\n";
                            i++;
                            break;

                        case ".hwordx4":
                            cmdLine = BC + " ";

                            for (int n = 0; n < 4; n++)
                            {
                                i++;
                                k = i;
                                address = "0x";
                                offset = "";

                                for (int w = 1; w >= 0; w--)
                                {
                                    offset += Convert.ToString(string.Format("{0:X2}", file[location + k + w]));
                                    if (w == 1)
                                    {
                                        offset = offset.Replace("00", "");
                                    }
                                    i++;
                                }
                                i--;
                                offset.ToUpper();

                                address += offset;
                                address = address.Replace("0x0", "0x");

                                if (n < 3)
                                {
                                    address += " ";
                                }

                                cmdLine += address;
                            }

                            toReturn += cmdLine + "\n";
                            i++;
                            break;

                        case ".word.call":
                            cmdLine = BC + " ";

                            i++;
                            k = i;
                            address = "0x";
                            offset = "";

                            for (int w = 3; w >= 0; w--)
                            {
                                offset += Convert.ToString(string.Format("{0:X2}", file[location + k + w]));
                                if (w == 3)
                                {
                                    offset = offset.Replace("00", "");
                                    offset = offset.Replace("08", "");
                                }
                                i++;
                            }
                            i--;
                            offset.ToUpper();

                            address += offset;
                            for (int z = 0; z < 5; z++)
                            {
                                address = address.Replace("0x0", "0x");
                            }

                            cmdLine += address;

                            sublocation = CheckOffset.Listing(location, address);

                            if (list.Contains(sublocation))
                            {
                                //リストに要素が含まれている場合は逆コンパイルを実行しない
                            }
                            else
                            {
                                list.Add(sublocation);
                                subLine += "\n" + DecompileCommand(selectedROMPath, sublocation, list, msg);
                            }

                            toReturn += cmdLine + "\n";
                            i++;
                            break;

                        case ".word.branch":
                            cmdLine = BC + " ";

                            i++;
                            k = i;
                            address = "0x";
                            offset = "";

                            for (int w = 3; w >= 0; w--)
                            {
                                offset += Convert.ToString(string.Format("{0:X2}", file[location + k + w]));
                                if (w == 3)
                                {
                                    offset = offset.Replace("00", "");
                                    offset = offset.Replace("08", "");
                                }
                                i++;
                            }
                            i--;
                            offset.ToUpper();

                            address += offset;
                            for (int z = 0; z < 5; z++)
                            {
                                address = address.Replace("0x0", "0x");
                            }

                            cmdLine += address;

                            sublocation = CheckOffset.Listing(location, address);

                            if (list.Contains(sublocation))
                            {
                                //リストに要素が含まれている場合は逆コンパイルを実行しない
                            }
                            else
                            {
                                list.Add(sublocation);
                                subLine += "\n" + DecompileCommand(selectedROMPath, sublocation, list, msg);
                            }

                            CommandType = endCmd;

                            toReturn += cmdLine + "\n";
                            i++;
                            break;

                        case ".word":
                            cmdLine = BC + " ";

                            i++;
                            k = i;
                            address = "0x";
                            offset = "";

                            for (int w = 3; w >= 0; w--)
                            {
                                offset += Convert.ToString(string.Format("{0:X2}", file[location + k + w]));
                                if (w == 3)
                                {
                                    offset = offset.Replace("00", "");
                                    offset = offset.Replace("08", "");
                                }
                                i++;
                            }
                            i--;
                            offset.ToUpper();

                            address += offset;
                            for (int z = 0; z < 5; z++)
                            {
                                address = address.Replace("0x0", "0x");
                            }

                            cmdLine += address;

                            toReturn += cmdLine + "\n";
                            i++;
                            break;

                        case ".wordx2":
                            cmdLine = BC + " ";

                            for (int n = 0; n < 2; n++)
                            {
                                i++;
                                k = i;
                                address = "0x";
                                offset = "";

                                for (int w = 3; w >= 0; w--)
                                {
                                    offset += Convert.ToString(string.Format("{0:X2}", file[location + k + w]));
                                    if (w == 3)
                                    {
                                        offset = offset.Replace("00", "");
                                        offset = offset.Replace("08", "");
                                    }
                                    i++;
                                }
                                i--;
                                offset.ToUpper();

                                address += offset;
                                for (int z = 0; z < 5; z++)
                                {
                                    address = address.Replace("0x0", "0x");
                                }

                                if (n < 1)
                                {
                                    address += " ";
                                }

                                cmdLine += address;
                            }

                            toReturn += cmdLine + "\n";
                            i++;
                            break;

                        case ".byte.word.branch":
                            cmdLine = BC + " ";

                            i++;
                            c = Convert.ToString(string.Format("0x{0:X1}", file[location + i]));
                            c.ToUpper();
                            cmdLine += c;

                            i++;
                            k = i;
                            address = "0x";
                            offset = "";

                            for (int w = 3; w >= 0; w--)
                            {
                                offset += Convert.ToString(string.Format("{0:X2}", file[location + k + w]));
                                if (w == 3)
                                {
                                    offset = offset.Replace("00", "");
                                    offset = offset.Replace("08", "");
                                }
                                i++;
                            }
                            i--;
                            offset.ToUpper();

                            address += offset;
                            for (int z = 0; z < 5; z++)
                            {
                                address = address.Replace("0x0", "0x");
                            }

                            cmdLine += " " + address;

                            sublocation = CheckOffset.Listing(location, address);

                            if (list.Contains(sublocation))
                            {
                                //リストに要素が含まれている場合は逆コンパイルを実行しない
                            }
                            else
                            {
                                list.Add(sublocation);
                                subLine += "\n" + DecompileCommand(selectedROMPath, sublocation, list, msg);
                            }

                            toReturn += cmdLine + "\n";
                            i++;
                            break;

                        case ".byte.word":
                            cmdLine = BC + " ";

                            i++;
                            c = Convert.ToString(string.Format("0x{0:X1}", file[location + i]));
                            c.ToUpper();
                            cmdLine += c;

                            i++;
                            k = i;
                            address = "0x";
                            offset = "";

                            for (int w = 3; w >= 0; w--)
                            {
                                offset += Convert.ToString(string.Format("{0:X2}", file[location + k + w]));
                                if (w == 3)
                                {
                                    offset = offset.Replace("00", "");
                                    offset = offset.Replace("08", "");
                                }
                                i++;
                            }
                            i--;
                            offset.ToUpper();

                            address += offset;
                            for (int z = 0; z < 5; z++)
                            {
                                address = address.Replace("0x0", "0x");
                            }

                            cmdLine += " " + address;

                            toReturn += cmdLine + "\n";
                            i++;
                            break;

                        case ".word.byte":
                            cmdLine = BC + " ";

                            i++;
                            k = i;
                            address = "0x";
                            offset = "";

                            for (int w = 3; w >= 0; w--)
                            {
                                offset += Convert.ToString(string.Format("{0:X2}", file[location + k + w]));
                                if (w == 3)
                                {
                                    offset = offset.Replace("00", "");
                                    offset = offset.Replace("08", "");
                                }
                                i++;
                            }
                            i--;
                            offset.ToUpper();

                            address += offset;
                            for (int z = 0; z < 5; z++)
                            {
                                address = address.Replace("0x0", "0x");
                            }

                            cmdLine += address + " ";

                            i++;
                            c = Convert.ToString(string.Format("0x{0:X1}", file[location + i]));
                            c.ToUpper();
                            cmdLine += c;

                            toReturn += cmdLine + "\n";
                            i++;
                            break;

                        case ".hword.byte":
                            cmdLine = BC + " ";

                            i++;
                            k = i;
                            address = "0x";
                            offset = "";

                            for (int w = 1; w >= 0; w--)
                            {
                                offset += Convert.ToString(string.Format("{0:X2}", file[location + k + w]));
                                if (w == 1)
                                {
                                    offset = offset.Replace("00", "");
                                }
                                i++;
                            }
                            i--;
                            offset.ToUpper();

                            address += offset;
                            address = address.Replace("0x0", "0x");

                            cmdLine += address + " ";

                            i++;
                            c = Convert.ToString(string.Format("0x{0:X1}", file[location + i]));
                            c.ToUpper();
                            cmdLine += c;

                            toReturn += cmdLine + "\n";
                            i++;
                            break;

                        case ".byte.hword":
                            cmdLine = BC + " ";

                            i++;
                            c = Convert.ToString(string.Format("0x{0:X1}", file[location + i]));
                            c.ToUpper();
                            cmdLine += c + " ";

                            i++;
                            k = i;
                            address = "0x";
                            offset = "";

                            for (int w = 1; w >= 0; w--)
                            {
                                offset += Convert.ToString(string.Format("{0:X2}", file[location + k + w]));
                                if (w == 1)
                                {
                                    offset = offset.Replace("00", "");
                                }
                                i++;
                            }
                            i--;
                            offset.ToUpper();

                            address += offset;
                            address = address.Replace("0x0", "0x");

                            cmdLine += address;

                            toReturn += cmdLine + "\n";
                            i++;
                            break;

                        case ".hword.word":
                            cmdLine = BC + " ";

                            i++;
                            k = i;
                            address = "0x";
                            offset = "";

                            for (int w = 1; w >= 0; w--)
                            {
                                offset += Convert.ToString(string.Format("{0:X2}", file[location + k + w]));
                                if (w == 1)
                                {
                                    offset = offset.Replace("00", "");
                                }
                                i++;
                            }
                            i--;
                            offset.ToUpper();

                            address += offset;
                            address = address.Replace("0x0", "0x");

                            cmdLine += address + " ";

                            i++;
                            k = i;
                            address = "0x";
                            offset = "";

                            for (int w = 3; w >= 0; w--)
                            {
                                offset += Convert.ToString(string.Format("{0:X2}", file[location + k + w]));
                                if (w == 3)
                                {
                                    offset = offset.Replace("00", "");
                                    offset = offset.Replace("08", "");
                                }
                                i++;
                            }
                            i--;
                            offset.ToUpper();

                            address += offset;
                            for (int z = 0; z < 5; z++)
                            {
                                address = address.Replace("0x0", "0x");
                            }

                            cmdLine += address;

                            toReturn += cmdLine + "\n";
                            i++;
                            break;

                        case ".hword.bytex2":
                            cmdLine = BC + " ";

                            i++;
                            k = i;
                            address = "0x";
                            offset = "";

                            for (int w = 1; w >= 0; w--)
                            {
                                offset += Convert.ToString(string.Format("{0:X2}", file[location + k + w]));
                                if (w == 1)
                                {
                                    offset = offset.Replace("00", "");
                                }
                                i++;
                            }
                            i--;
                            offset.ToUpper();

                            address += offset;
                            address = address.Replace("0x0", "0x");

                            cmdLine += address + " ";

                            for (int n = 0; n < 2; n++)
                            {
                                i++;
                                c = Convert.ToString(string.Format("0x{0:X1}", file[location + i]));
                                c.ToUpper();
                                cmdLine += c;

                                if (n < 1)
                                {
                                    cmdLine += " ";
                                }
                            }

                            toReturn += cmdLine + "\n";
                            i++;
                            break;

                        case ".hword.bytex3":
                            cmdLine = BC + " ";

                            i++;
                            k = i;
                            address = "0x";
                            offset = "";

                            for (int w = 1; w >= 0; w--)
                            {
                                offset += Convert.ToString(string.Format("{0:X2}", file[location + k + w]));
                                if (w == 1)
                                {
                                    offset = offset.Replace("00", "");
                                }
                                i++;
                            }
                            i--;
                            offset.ToUpper();

                            address += offset;
                            address = address.Replace("0x0", "0x");

                            cmdLine += address + " ";

                            for (int n = 0; n < 3; n++)
                            {
                                i++;
                                c = Convert.ToString(string.Format("0x{0:X1}", file[location + i]));
                                c.ToUpper();
                                cmdLine += c;

                                if (n < 2)
                                {
                                    cmdLine += " ";
                                }
                            }

                            toReturn += cmdLine + "\n";
                            i++;
                            break;

                        case ".bytex2.hword":
                            cmdLine = BC + " ";

                            for (int n = 0; n < 2; n++)
                            {
                                i++;
                                c = Convert.ToString(string.Format("0x{0:X1}", file[location + i]));
                                c.ToUpper();
                                cmdLine += c + " ";
                            }

                            i++;
                            k = i;
                            address = "0x";
                            offset = "";

                            for (int w = 1; w >= 0; w--)
                            {
                                offset += Convert.ToString(string.Format("{0:X2}", file[location + k + w]));
                                if (w == 1)
                                {
                                    offset = offset.Replace("00", "");
                                }
                                i++;
                            }
                            i--;
                            offset.ToUpper();

                            address += offset;
                            address = address.Replace("0x0", "0x");

                            cmdLine += address;

                            toReturn += cmdLine + "\n";
                            i++;
                            break;

                        case ".bytex3.hwordx2":
                            cmdLine = BC + " ";

                            for (int n = 0; n < 3; n++)
                            {
                                i++;
                                c = Convert.ToString(string.Format("0x{0:X1}", file[location + i]));
                                c.ToUpper();
                                cmdLine += c + " ";
                            }

                            for (int n = 0; n < 2; n++)
                            {
                                i++;
                                k = i;
                                address = "0x";
                                offset = "";

                                for (int w = 1; w >= 0; w--)
                                {
                                    offset += Convert.ToString(string.Format("{0:X2}", file[location + k + w]));
                                    if (w == 1)
                                    {
                                        offset = offset.Replace("00", "");
                                    }
                                    i++;
                                }
                                i--;
                                offset.ToUpper();

                                address += offset;
                                address = address.Replace("0x0", "0x");

                                if (n < 1)
                                {
                                    address += " ";
                                }

                                cmdLine += address;
                            }

                            toReturn += cmdLine + "\n";
                            i++;
                            break;

                        case ".hword.word.bytex2":
                            cmdLine = BC + " ";

                            i++;
                            k = i;
                            address = "0x";
                            offset = "";

                            for (int w = 1; w >= 0; w--)
                            {
                                offset += Convert.ToString(string.Format("{0:X2}", file[location + k + w]));
                                if (w == 1)
                                {
                                    offset = offset.Replace("00", "");
                                }
                                i++;
                            }
                            i--;
                            offset.ToUpper();

                            address += offset;
                            address = address.Replace("0x0", "0x");

                            cmdLine += address + " ";

                            i++;
                            k = i;
                            address = "0x";
                            offset = "";

                            for (int w = 3; w >= 0; w--)
                            {
                                offset += Convert.ToString(string.Format("{0:X2}", file[location + k + w]));
                                if (w == 3)
                                {
                                    offset = offset.Replace("00", "");
                                    offset = offset.Replace("08", "");
                                }
                                i++;
                            }
                            i--;
                            offset.ToUpper();

                            address += offset;
                            for (int z = 0; z < 5; z++)
                            {
                                address = address.Replace("0x0", "0x");
                            }

                            cmdLine += address + " ";

                            for (int n = 0; n < 2; n++)
                            {
                                i++;
                                c = Convert.ToString(string.Format("0x{0:X1}", file[location + i]));
                                c.ToUpper();
                                cmdLine += c;

                                if (n < 1)
                                {
                                    cmdLine += " ";
                                }
                            }

                            toReturn += cmdLine + "\n";
                            i++;
                            break;

                        case ".bytex2.hwordx2.bytex2":
                            cmdLine = BC + " ";

                            for (int n = 0; n < 2; n++)
                            {
                                i++;
                                c = Convert.ToString(string.Format("0x{0:X1}", file[location + i]));
                                c.ToUpper();
                                cmdLine += c + " ";
                            }

                            for (int n = 0; n < 2; n++)
                            {
                                i++;
                                k = i;
                                address = "0x";
                                offset = "";

                                for (int w = 1; w >= 0; w--)
                                {
                                    offset += Convert.ToString(string.Format("{0:X2}", file[location + k + w]));
                                    if (w == 1)
                                    {
                                        offset = offset.Replace("00", "");
                                    }
                                    i++;
                                }
                                i--;
                                offset.ToUpper();

                                address += offset;
                                address = address.Replace("0x0", "0x");

                                address += " ";

                                cmdLine += address;
                            }

                            for (int n = 0; n < 2; n++)
                            {
                                i++;
                                c = Convert.ToString(string.Format("0x{0:X1}", file[location + i]));
                                c.ToUpper();
                                cmdLine += c;

                                if (n < 1)
                                {
                                    cmdLine += " ";
                                }
                            }

                            toReturn += cmdLine + "\n";
                            i++;
                            break;

                        case ".hword.byte.hword":
                            cmdLine = BC + " ";

                            i++;
                            k = i;
                            address = "0x";
                            offset = "";

                            for (int w = 1; w >= 0; w--)
                            {
                                offset += Convert.ToString(string.Format("{0:X2}", file[location + k + w]));
                                if (w == 1)
                                {
                                    offset = offset.Replace("00", "");
                                }
                                i++;
                            }
                            i--;
                            offset.ToUpper();

                            address += offset;
                            address = address.Replace("0x0", "0x");

                            cmdLine += address + " ";

                            i++;
                            c = Convert.ToString(string.Format("0x{0:X1}", file[location + i]));
                            c.ToUpper();
                            cmdLine += c + " ";

                            i++;
                            k = i;
                            address = "0x";
                            offset = "";

                            for (int w = 1; w >= 0; w--)
                            {
                                offset += Convert.ToString(string.Format("{0:X2}", file[location + k + w]));
                                if (w == 1)
                                {
                                    offset = offset.Replace("00", "");
                                }
                                i++;
                            }
                            i--;
                            offset.ToUpper();

                            address += offset;
                            address = address.Replace("0x0", "0x");

                            cmdLine += address;

                            toReturn += cmdLine + "\n";
                            i++;
                            break;

                        case ".hword.byte.hword.wordx2.byte":
                            cmdLine = BC + " ";

                            i++;
                            k = i;
                            address = "0x";
                            offset = "";

                            for (int w = 1; w >= 0; w--)
                            {
                                offset += Convert.ToString(string.Format("{0:X2}", file[location + k + w]));
                                if (w == 1)
                                {
                                    offset = offset.Replace("00", "");
                                }
                                i++;
                            }
                            i--;
                            offset.ToUpper();

                            address += offset;
                            address = address.Replace("0x0", "0x");

                            cmdLine += address + " ";

                            i++;
                            c = Convert.ToString(string.Format("0x{0:X1}", file[location + i]));
                            c.ToUpper();
                            cmdLine += c + " ";

                            i++;
                            k = i;
                            address = "0x";
                            offset = "";

                            for (int w = 1; w >= 0; w--)
                            {
                                offset += Convert.ToString(string.Format("{0:X2}", file[location + k + w]));
                                if (w == 1)
                                {
                                    offset = offset.Replace("00", "");
                                }
                                i++;
                            }
                            i--;
                            offset.ToUpper();

                            address += offset;
                            address = address.Replace("0x0", "0x");

                            cmdLine += address + " ";

                            for (int n = 0; n < 2; n++)
                            {
                                i++;
                                k = i;
                                address = "0x";
                                offset = "";

                                for (int w = 3; w >= 0; w--)
                                {
                                    offset += Convert.ToString(string.Format("{0:X2}", file[location + k + w]));
                                    if (w == 3)
                                    {
                                        offset = offset.Replace("00", "");
                                        offset = offset.Replace("08", "");
                                    }
                                    i++;
                                }
                                i--;
                                offset.ToUpper();

                                address += offset;
                                for (int z = 0; z < 5; z++)
                                {
                                    address = address.Replace("0x0", "0x");
                                }

                                address += " ";

                                cmdLine += address;
                            }

                            i++;
                            c = Convert.ToString(string.Format("0x{0:X1}", file[location + i]));
                            c.ToUpper();
                            cmdLine += c;

                            toReturn += cmdLine + "\n";
                            i++;
                            break;

                        case ".byte.hwordx2.word(.wordx3)":
                            cmdLine = BC + " ";

                            i++;
                            c = Convert.ToString(string.Format("0x{0:X1}", file[location + i]));
                            c.ToUpper();
                            cmdLine += c + " ";

                            switch (c)
                            {
                                case "0x1":
                                    arg = 3;
                                    break;

                                case "0x2":
                                    arg = 3;
                                    break;

                                case "0x3":
                                    arg = 1;
                                    break;

                                case "0x4":
                                    arg = 3;
                                    break;

                                case "0x6":
                                    arg = 4;
                                    break;

                                case "0x7":
                                    arg = 3;
                                    break;

                                case "0x8":
                                    arg = 4;
                                    break;

                                default:
                                    arg = 2;
                                    break;
                            }

                            for (int n = 0; n < 2; n++)
                            {
                                i++;
                                k = i;
                                address = "0x";
                                offset = "";

                                for (int w = 1; w >= 0; w--)
                                {
                                    offset += Convert.ToString(string.Format("{0:X2}", file[location + k + w]));
                                    if (w == 1)
                                    {
                                        offset = offset.Replace("00", "");
                                    }
                                    i++;
                                }
                                i--;
                                offset.ToUpper();

                                address += offset;
                                address = address.Replace("0x0", "0x");

                                address += " ";

                                cmdLine += address;
                            }

                            for (int n = 0; n < arg; n++)
                            {
                                i++;
                                k = i;
                                address = "0x";
                                offset = "";

                                for (int w = 3; w >= 0; w--)
                                {
                                    offset += Convert.ToString(string.Format("{0:X2}", file[location + k + w]));
                                    if (w == 3)
                                    {
                                        offset = offset.Replace("00", "");
                                        offset = offset.Replace("08", "");
                                    }
                                    i++;
                                }
                                i--;
                                offset.ToUpper();

                                address += offset;
                                for (int z = 0; z < 5; z++)
                                {
                                    address = address.Replace("0x0", "0x");
                                }

                                if (n < arg - 1)
                                {
                                    locationChar = CheckOffset.Listing(location, address);

                                    msg.Add(locationChar);

                                    address += " ";
                                }
                                if (n == arg - 1)
                                {

                                    if (c == "0x1" || c == "0x2" || c == "0x6" || c == "0x8")
                                    {
                                        sublocation = CheckOffset.Listing(location, address);

                                        if (list.Contains(sublocation))
                                        {
                                            //リストに要素が含まれている場合は逆コンパイルを実行しない
                                        }
                                        else
                                        {
                                            list.Add(sublocation);
                                            subLine += "\n" + DecompileCommand(selectedROMPath, sublocation, list, msg);
                                        }
                                    }
                                    else
                                    {
                                        locationChar = CheckOffset.Listing(location, address);

                                        msg.Add(locationChar);
                                    }
                                }

                                cmdLine += address;
                            }

                            toReturn += cmdLine + "\n";
                            i++;
                            break;

                        case ".word.msg":
                            cmdLine = BC + " ";

                            i++;
                            j = i;
                            address = "0x";
                            offset = "";

                            for (int w = 3; w >= 0; w--)
                            {
                                offset += Convert.ToString(string.Format("{0:X2}", file[location + j + w]));
                                if (w == 3)
                                {
                                    offset = offset.Replace("00", "");
                                    offset = offset.Replace("08", "");
                                }
                                i++;
                            }
                            i--;
                            offset.ToUpper();

                            address += offset;
                            for (int z = 0; z < 5; z++)
                            {
                                address = address.Replace("0x0", "0x");
                            }

                            cmdLine += address;

                            locationChar = CheckOffset.Listing(location, address);

                            msg.Add(locationChar);

                            toReturn += cmdLine + "\n";
                            i++;
                            break;

                        case ".byte.word.msg":
                            cmdLine = BC + " ";

                            //loadpointer 第一引数[byte] メモリバンク
                            i++;
                            c = Convert.ToString(string.Format("0x{0:X1}", file[location + i]));
                            c.ToUpper();
                            cmdLine += c + " ";

                            /*コマンドの統一は必要だと思うが、ある程度柔軟性も持たせるべき
                            例えば0Fがloadpointerのみだと名称と実際に使われている機能との乖離が大きく直感的にわかりにくい
                            loadpointerとmsgboxのように仕様的なコマンドと汎用的なコマンドの両方を用意するべき*/

                            //出力
                            switch (c)
                            {
                                case "0x0":
                                    cmdLine = cmdLine.Replace("loadpointer 0x0", "msgbox");　//loadpointerの第一引数が0x0であればmsgboxに変換
                                    i++;
                                    break;
                                default:
                                    i++;
                                    break;
                            }

                            //loadpointer 第二引数[word] ポインタ
                            j = i;
                            address = "0x";
                            offset = "";

                            for (int w = 3; w >= 0; w--)
                            {
                                offset += Convert.ToString(string.Format("{0:X2}", file[location + j + w]));
                                if (w == 3)
                                {
                                    offset = offset.Replace("00", "");
                                    offset = offset.Replace("08", "");
                                }
                                i++;
                            }
                            i--;
                            offset.ToUpper();

                            //直観的に分かりやすくするため0x8******を0x******と省略

                            address += offset;
                            for (int z = 0; z < 5; z++)
                            {
                                address = address.Replace("0x0", "0x");
                            }

                            cmdLine += address;

                            locationChar = CheckOffset.Listing(location, address);

                            msg.Add(locationChar);

                            toReturn += cmdLine + "\n";
                            i++;
                            break;

                        default:
                            toReturn += BC + "\n";
                            i++;
                            break;
                    }
                }
                while (CommandType != endCmd);

                toReturn += subLine;

                return toReturn;
            }
        }
    }
}
