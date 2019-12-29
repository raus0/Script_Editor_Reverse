using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using System.Collections.Generic;

namespace Script_Editor_Reverse
{
    public class DecompileBIN
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

                    BC = c;

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
                            toReturn += BC + " \n";
                            i++;
                            break;

                        case ".void":
                            toReturn += BC + " \n";
                            i++;
                            break;

                        case ".byte":
                            cmdLine = BC + " ";

                            i++;
                            offset = Convert.ToString(string.Format("{0:X2} ", file[location + i]));
                            offset.ToUpper();
                            cmdLine += offset;

                            toReturn += cmdLine + "\n";
                            i++;
                            break;

                        case ".bytex2":
                            cmdLine = BC + " ";

                            for (int n = 0; n < 2; n++)
                            {
                                i++;
                                offset = Convert.ToString(string.Format("{0:X2} ", file[location + i]));
                                offset.ToUpper();
                                cmdLine += offset;
                            }

                            toReturn += cmdLine + "\n";
                            i++;
                            break;

                        case ".bytex3":
                            cmdLine = BC + " ";

                            for (int n = 0; n < 3; n++)
                            {
                                i++;
                                offset = Convert.ToString(string.Format("{0:X2} ", file[location + i]));
                                offset.ToUpper();
                                cmdLine += offset;
                            }

                            toReturn += cmdLine + "\n";
                            i++;
                            break;

                        case ".bytex4":
                            cmdLine = BC + " ";

                            for (int n = 0; n < 4; n++)
                            {
                                i++;
                                offset = Convert.ToString(string.Format("{0:X2} ", file[location + i]));
                                offset.ToUpper();
                                cmdLine += offset;
                            }

                            toReturn += cmdLine + "\n";
                            i++;
                            break;

                        case ".bytex5":
                            cmdLine = BC + " ";

                            for (int n = 0; n < 5; n++)
                            {
                                i++;
                                offset = Convert.ToString(string.Format("{0:X2} ", file[location + i]));
                                offset.ToUpper();
                                cmdLine += offset;
                            }

                            toReturn += cmdLine + "\n";
                            i++;
                            break;

                        case ".hword":
                            cmdLine = BC + " ";

                            for (int h = 0; h < 2; h++)
                            {
                                i++;
                                offset = Convert.ToString(string.Format("{0:X2} ", file[location + i]));
                                offset.ToUpper();
                                cmdLine += offset;
                            }

                            toReturn += cmdLine + "\n";
                            i++;
                            break;

                        case ".hwordx2":
                            cmdLine = BC + " ";

                            for (int n = 0; n < 2; n++)
                            {
                                for (int h = 0; h < 2; h++)
                                {
                                    i++;
                                    offset = Convert.ToString(string.Format("{0:X2} ", file[location + i]));
                                    offset.ToUpper();
                                    cmdLine += offset;
                                }
                            }

                            toReturn += cmdLine + "\n";
                            i++;
                            break;

                        case ".hwordx3":
                            cmdLine = BC + " ";

                            for (int n = 0; n < 3; n++)
                            {
                                for (int h = 0; h < 2; h++)
                                {
                                    i++;
                                    offset = Convert.ToString(string.Format("{0:X2} ", file[location + i]));
                                    offset.ToUpper();
                                    cmdLine += offset;
                                }
                            }

                            toReturn += cmdLine + "\n";
                            i++;
                            break;

                        case ".hwordx4":
                            cmdLine = BC + " ";

                            for (int n = 0; n < 4; n++)
                            {
                                for (int h = 0; h < 2; h++)
                                {
                                    i++;
                                    offset = Convert.ToString(string.Format("{0:X2} ", file[location + i]));
                                    offset.ToUpper();
                                    cmdLine += offset;
                                }
                            }

                            toReturn += cmdLine + "\n";
                            i++;
                            break;

                        case ".word.call":
                            cmdLine = BC + " ";

                            j = i;

                            for (int w = 0; w < 4; w++)
                            {
                                i++;
                                offset = Convert.ToString(string.Format("{0:X2} ", file[location + i]));
                                offset.ToUpper();
                                cmdLine += offset;
                            }

                            j++;
                            k = j;
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
                                j++;
                            }
                            j--;
                            offset.ToUpper();

                            address += offset;
                            for (int z = 0; z < 5; z++)
                            {
                                address = address.Replace("0x0", "0x");
                            }

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

                            j = i;

                            for (int w = 0; w < 4; w++)
                            {
                                i++;
                                offset = Convert.ToString(string.Format("{0:X2} ", file[location + i]));
                                offset.ToUpper();
                                cmdLine += offset;
                            }

                            j++;
                            k = j;
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
                                j++;
                            }
                            j--;
                            offset.ToUpper();

                            address += offset;
                            for (int z = 0; z < 5; z++)
                            {
                                address = address.Replace("0x0", "0x");
                            }

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

                            for (int w = 0; w < 4; w++)
                            {
                                i++;
                                offset = Convert.ToString(string.Format("{0:X2} ", file[location + i]));
                                offset.ToUpper();
                                cmdLine += offset;
                            }

                            toReturn += cmdLine + "\n";
                            i++;
                            break;

                        case ".wordx2":
                            cmdLine = BC + " ";

                            for (int n = 0; n < 2; n++)
                            {
                                for (int w = 0; w < 4; w++)
                                {
                                    i++;
                                    offset = Convert.ToString(string.Format("{0:X2} ", file[location + i]));
                                    offset.ToUpper();
                                    cmdLine += offset;
                                }
                            }

                            toReturn += cmdLine + "\n";
                            i++;
                            break;

                        case ".byte.word.branch":
                            cmdLine = BC + " ";

                            i++;
                            offset = Convert.ToString(string.Format("{0:X2} ", file[location + i]));
                            offset.ToUpper();
                            cmdLine += offset;

                            j = i;

                            for (int w = 0; w < 4; w++)
                            {
                                i++;
                                offset = Convert.ToString(string.Format("{0:X2} ", file[location + i]));
                                offset.ToUpper();
                                cmdLine += offset;
                            }

                            j++;
                            k = j;
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
                                j++;
                            }
                            j--;
                            offset.ToUpper();

                            address += offset;
                            for (int z = 0; z < 5; z++)
                            {
                                address = address.Replace("0x0", "0x");
                            }

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
                            offset = Convert.ToString(string.Format("{0:X2} ", file[location + i]));
                            offset.ToUpper();
                            cmdLine += offset;

                            for (int w = 0; w < 4; w++)
                            {
                                i++;
                                offset = Convert.ToString(string.Format("{0:X2} ", file[location + i]));
                                offset.ToUpper();
                                cmdLine += offset;
                            }

                            toReturn += cmdLine + "\n";
                            i++;
                            break;

                        case ".word.byte":
                            cmdLine = BC + " ";

                            for (int w = 0; w < 4; w++)
                            {
                                i++;
                                offset = Convert.ToString(string.Format("{0:X2} ", file[location + i]));
                                offset.ToUpper();
                                cmdLine += offset;
                            }

                            i++;
                            offset = Convert.ToString(string.Format("{0:X2} ", file[location + i]));
                            offset.ToUpper();
                            cmdLine += offset;

                            toReturn += cmdLine + "\n";
                            i++;
                            break;

                        case ".hword.byte":
                            cmdLine = BC + " ";

                            for (int h = 0; h < 2; h++)
                            {
                                i++;
                                offset = Convert.ToString(string.Format("{0:X2} ", file[location + i]));
                                offset.ToUpper();
                                cmdLine += offset;
                            }

                            i++;
                            offset = Convert.ToString(string.Format("{0:X2} ", file[location + i]));
                            offset.ToUpper();
                            cmdLine += offset;

                            toReturn += cmdLine + "\n";
                            i++;
                            break;

                        case ".byte.hword":
                            cmdLine = BC + " ";

                            i++;
                            offset = Convert.ToString(string.Format("{0:X2} ", file[location + i]));
                            offset.ToUpper();
                            cmdLine += offset;

                            for (int h = 0; h < 2; h++)
                            {
                                i++;
                                offset = Convert.ToString(string.Format("{0:X2} ", file[location + i]));
                                offset.ToUpper();
                                cmdLine += offset;
                            }

                            toReturn += cmdLine + "\n";
                            i++;
                            break;

                        case ".hword.word":
                            cmdLine = BC + " ";

                            for (int h = 0; h < 2; h++)
                            {
                                i++;
                                offset = Convert.ToString(string.Format("{0:X2} ", file[location + i]));
                                offset.ToUpper();
                                cmdLine += offset;
                            }

                            for (int w = 0; w < 4; w++)
                            {
                                i++;
                                offset = Convert.ToString(string.Format("{0:X2} ", file[location + i]));
                                offset.ToUpper();
                                cmdLine += offset;
                            }

                            toReturn += cmdLine + "\n";
                            i++;
                            break;

                        case ".hword.bytex2":
                            cmdLine = BC + " ";

                            for (int h = 0; h < 2; h++)
                            {
                                i++;
                                offset = Convert.ToString(string.Format("{0:X2} ", file[location + i]));
                                offset.ToUpper();
                                cmdLine += offset;
                            }

                            for (int n = 0; n < 2; n++)
                            {
                                i++;
                                offset = Convert.ToString(string.Format("{0:X2} ", file[location + i]));
                                offset.ToUpper();
                                cmdLine += offset;
                            }

                            toReturn += cmdLine + "\n";
                            i++;
                            break;

                        case ".hword.bytex3":
                            cmdLine = BC + " ";

                            for (int h = 0; h < 2; h++)
                            {
                                i++;
                                offset = Convert.ToString(string.Format("{0:X2} ", file[location + i]));
                                offset.ToUpper();
                                cmdLine += offset;
                            }

                            for (int n = 0; n < 3; n++)
                            {
                                i++;
                                offset = Convert.ToString(string.Format("{0:X2} ", file[location + i]));
                                offset.ToUpper();
                                cmdLine += offset;
                            }

                            toReturn += cmdLine + "\n";
                            i++;
                            break;

                        case ".bytex2.hword":
                            cmdLine = BC + " ";

                            for (int n = 0; n < 2; n++)
                            {
                                i++;
                                offset = Convert.ToString(string.Format("{0:X2} ", file[location + i]));
                                offset.ToUpper();
                                cmdLine += offset;
                            }

                            for (int h = 0; h < 4; h++)
                            {
                                i++;
                                offset = Convert.ToString(string.Format("{0:X2} ", file[location + i]));
                                offset.ToUpper();
                                cmdLine += offset;
                            }

                            toReturn += cmdLine + "\n";
                            i++;
                            break;

                        case ".bytex3.hwordx2":
                            cmdLine = BC + " ";

                            for (int n = 0; n < 3; n++)
                            {
                                i++;
                                offset = Convert.ToString(string.Format("{0:X2} ", file[location + i]));
                                offset.ToUpper();
                                cmdLine += offset;
                            }

                            for (int n = 0; n < 2; n++)
                            {
                                for (int h = 0; h < 2; h++)
                                {
                                    i++;
                                    offset = Convert.ToString(string.Format("{0:X2} ", file[location + i]));
                                    offset.ToUpper();
                                    cmdLine += offset;
                                }
                            }

                            toReturn += cmdLine + "\n";
                            i++;
                            break;

                        case ".hword.word.bytex2":
                            cmdLine = BC + " ";

                            for (int h = 0; h < 2; h++)
                            {
                                i++;
                                offset = Convert.ToString(string.Format("{0:X2} ", file[location + i]));
                                offset.ToUpper();
                                cmdLine += offset;
                            }

                            for (int w = 0; w < 4; w++)
                            {
                                i++;
                                offset = Convert.ToString(string.Format("{0:X2} ", file[location + i]));
                                offset.ToUpper();
                                cmdLine += offset;
                            }

                            for (int n = 0; n < 2; n++)
                            {
                                i++;
                                offset = Convert.ToString(string.Format("{0:X2} ", file[location + i]));
                                offset.ToUpper();
                                cmdLine += offset;
                            }

                            toReturn += cmdLine + "\n";
                            i++;
                            break;

                        case ".bytex2.hwordx2.bytex2":
                            cmdLine = BC + " ";

                            for (int n = 0; n < 2; n++)
                            {
                                i++;
                                offset = Convert.ToString(string.Format("{0:X2} ", file[location + i]));
                                offset.ToUpper();
                                cmdLine += offset;
                            }

                            for (int n = 0; n < 2; n++)
                            {
                                for (int h = 0; h < 2; h++)
                                {
                                    i++;
                                    offset = Convert.ToString(string.Format("{0:X2} ", file[location + i]));
                                    offset.ToUpper();
                                    cmdLine += offset;
                                }
                            }

                            for (int n = 0; n < 2; n++)
                            {
                                i++;
                                offset = Convert.ToString(string.Format("{0:X2} ", file[location + i]));
                                offset.ToUpper();
                                cmdLine += offset;
                            }

                            toReturn += cmdLine + "\n";
                            i++;
                            break;

                        case ".hword.byte.hword":
                            cmdLine = BC + " ";

                            for (int h = 0; h < 2; h++)
                            {
                                i++;
                                offset = Convert.ToString(string.Format("{0:X2} ", file[location + i]));
                                offset.ToUpper();
                                cmdLine += offset;
                            }

                            i++;
                            offset = Convert.ToString(string.Format("{0:X2} ", file[location + i]));
                            offset.ToUpper();
                            cmdLine += offset;

                            for (int h = 0; h < 2; h++)
                            {
                                i++;
                                offset = Convert.ToString(string.Format("{0:X2} ", file[location + i]));
                                offset.ToUpper();
                                cmdLine += offset;
                            }

                            toReturn += cmdLine + "\n";
                            i++;
                            break;

                        case ".hword.byte.hword.wordx2.byte":
                            cmdLine = BC + " ";

                            for (int h = 0; h < 2; h++)
                            {
                                i++;
                                offset = Convert.ToString(string.Format("{0:X2} ", file[location + i]));
                                offset.ToUpper();
                                cmdLine += offset;
                            }

                            i++;
                            offset = Convert.ToString(string.Format("{0:X2} ", file[location + i]));
                            offset.ToUpper();
                            cmdLine += offset;

                            for (int h = 0; h < 2; h++)
                            {
                                i++;
                                offset = Convert.ToString(string.Format("{0:X2} ", file[location + i]));
                                offset.ToUpper();
                                cmdLine += offset;
                            }

                            for (int n = 0; n < 2; n++)
                            {
                                for (int w = 0; w < 4; w++)
                                {
                                    i++;
                                    offset = Convert.ToString(string.Format("{0:X2} ", file[location + i]));
                                    offset.ToUpper();
                                    cmdLine += offset;
                                }
                            }

                            i++;
                            offset = Convert.ToString(string.Format("{0:X2} ", file[location + i]));
                            offset.ToUpper();
                            cmdLine += offset;

                            toReturn += cmdLine + "\n";
                            i++;
                            break;

                        case ".byte.hwordx2.word(.wordx3)":
                            cmdLine = BC + " ";

                            i++;
                            c = Convert.ToString(string.Format("{0:X2}", file[location + i]));
                            c.ToUpper();
                            cmdLine += c + " ";

                            switch (c)
                            {
                                case "01":
                                    arg = 3;
                                    break;

                                case "02":
                                    arg = 3;
                                    break;

                                case "03":
                                    arg = 1;
                                    break;

                                case "04":
                                    arg = 3;
                                    break;

                                case "06":
                                    arg = 4;
                                    break;

                                case "07":
                                    arg = 3;
                                    break;

                                case "08":
                                    arg = 4;
                                    break;

                                default:
                                    arg = 2;
                                    break;
                            }

                            for (int n = 0; n < 2; n++)
                            {
                                for (int h = 0; h < 2; h++)
                                {
                                    i++;
                                    offset = Convert.ToString(string.Format("{0:X2} ", file[location + i]));
                                    offset.ToUpper();
                                    cmdLine += offset;
                                }
                            }

                            j = i;

                            for (int n = 0; n < arg; n++)
                            {
                                for (int w = 0; w < 4; w++)
                                {
                                    i++;
                                    offset = Convert.ToString(string.Format("{0:X2} ", file[location + i]));
                                    offset.ToUpper();
                                    cmdLine += offset;
                                }
                            }

                            for (int n = 0; n < arg; n++)
                            {
                                j++;
                                k = j;
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
                                    j++;
                                }
                                j--;
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
                                }
                                if (n == arg - 1)
                                {

                                    if (c == "01" || c == "02" || c == "06" || c == "08")
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
                            }

                            toReturn += cmdLine + "\n";
                            i++;
                            break;

                        case ".word.msg":
                            cmdLine = BC + " ";

                            k = i;

                            for (int w = 0; w < 4; w++)
                            {
                                i++;
                                offset = Convert.ToString(string.Format("{0:X2} ", file[location + i]));
                                offset.ToUpper();
                                cmdLine += offset;
                            }

                            k++;
                            j = k;
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
                                k++;
                            }
                            k--;
                            offset.ToUpper();

                            address += offset;
                            for (int z = 0; z < 5; z++)
                            {
                                address = address.Replace("0x0", "0x");
                            }

                            locationChar = CheckOffset.Listing(location, address);

                            msg.Add(locationChar);

                            toReturn += cmdLine + "\n";
                            i++;
                            break;

                        case ".byte.word.msg":
                            cmdLine = BC + " ";

                            i++;
                            offset = Convert.ToString(string.Format("{0:X2} ", file[location + i]));
                            offset.ToUpper();
                            cmdLine += offset;

                            k = i;

                            for (int w = 0; w < 4; w++)
                            {
                                i++;
                                offset = Convert.ToString(string.Format("{0:X2} ", file[location + i]));
                                offset.ToUpper();
                                cmdLine += offset;
                            }

                            k++;
                            j = k;
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
                                k++;
                            }
                            k--;
                            offset.ToUpper();

                            address += offset;
                            for (int z = 0; z < 5; z++)
                            {
                                address = address.Replace("0x0", "0x");
                            }

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
