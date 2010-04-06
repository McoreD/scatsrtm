using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;

namespace SMExporter
{

    public class SMHeaderLine
    {
        /// <summary>
        /// 24-hour format e.g. 16:00
        /// </summary>
        public string Time { get; private set; }
        /// <summary>
        /// Sunday, Monday, ..., Saturday
        /// </summary>
        public string DayRead { get; private set; }
        /// <summary>
        /// 06-September-2006
        /// </summary>
        public string DateRead { get; private set; }

        // Wednesday 06-September-2006 16:00 SS  55   PL 1.3  PV   1.4 CL   60 +0 RL  60' SA 57  DS 107
        // Sunday 28-March-2010 17:29 SS  16F   PL 1.1  PVs1.3 CT   50 +0 RL 50  SA 0   DS 0

        public SMHeaderLine(string line)
        {

        }
    }

    public class SMParser
    {
        private string mFilePath = string.Empty;
        private string mSMtitle = string.Empty;
        private int MaxValue { get; set; }
        private int CurrValue { get; set; }

        public SMParser(string fp)
        {
            mFilePath = fp;
        }

        public List<Reading> AnalyzeFile()
        {

            List<Reading> myListReadings = new List<Reading>();
            StreamReader sr = new StreamReader(mFilePath);
            string currLine = sr.ReadLine();
            mSMtitle = currLine;

            // int countReadings = 0;
            string prevLine = "";
            Reading currRead = new Reading();
            int readID = 0;

            using (StreamReader tempSr = new StreamReader(mFilePath))
            {
                Adapter.MaxValue = Regex.Split(tempSr.ReadToEnd(), Environment.NewLine).Length;
                Adapter.CurrentValue = 0;
            }

            while (!sr.EndOfStream)
            {
                currLine = sr.ReadLine();

                Adapter.CurrentValue += 1;
                CurrValue += 1;

                if (currLine != null && currLine.Length > 0)
                {
                    currRead = new Reading();

                    // this sub ensures that headings are read only if there are 
                    // active intersection data

                    if (currLine.Substring(0, 4) == " Int")
                    {
                        // break curr line 
                        string fixedLine = prevLine;
                        fixedLine = fixedLine.Replace(" + ", " ");
                        fixedLine = fixedLine.Replace(" - ", " ");
                        fixedLine = fixedLine.Replace("'", " ");
                        fixedLine = fixedLine.Replace("''", " ");
                        fixedLine = fixedLine.Replace("  ", " ").Replace("  ", " ");
                        fixedLine = fixedLine.Replace("PV", "PV "); // sometimes u get values like PVs1.3
                        fixedLine = RemoveDoubleSpaces(fixedLine);

                        string[] arrLine = Regex.Split(fixedLine, " ");
                        currRead.DayRead = arrLine[0].ToString();
                        currRead.DateRead = arrLine[1].ToString();
                        currRead.Time = arrLine[2].ToString();

                        currRead.PlansActive = arrLine[6];
                        currRead.PlansVoted = arrLine[8];

                        string cl = arrLine[10].Trim();
                        int clN = 0;
                        int clA = 0;

                        string[] arr = cl.Split('+'); // Regex.Split(cl, "+");
                        int.TryParse(Regex.Split(arr[0], "-")[0], out clN);
                        clA = clN;

                        if (arr.Length > 1)
                        {
                            int temp = 0;
                            int.TryParse(arr[1], out temp);
                            clA = clN + temp;
                        }
                        else
                        {
                            arr = Regex.Split(cl, "-");
                            if (arr.Length > 1)
                            {
                                int temp = 0;
                                int.TryParse(arr[1], out temp);
                                clA = clN - temp;
                            }
                            else
                            {
                                int offset = 0;
                                if (Program.IsNumeric(arrLine[11].Trim()))
                                {
                                    int.TryParse(arrLine[11], out offset);
                                    clA = clN + offset;
                                }
                            }
                        }

                        currRead.CycleLengthNominal = clN;
                        currRead.CycleLengthActual = clA;

                        // this has to be done because when 70+18 for example, happens
                        // we lose one split string

                        if (Program.IsNumeric(arrLine[13]))
                        {
                            int temp = 0;
                            int.TryParse(arrLine[13], out temp);
                            currRead.CycleLengthRequired = temp;
                        }
                        else
                        {
                            int temp = 0;
                            int.TryParse(arrLine[12], out temp);
                            currRead.CycleLengthRequired = temp;
                        }


                        if (Program.IsNumeric(arrLine[15]))
                        {
                            int temp = 0;
                            int.TryParse(arrLine[15], out temp);
                            currRead.CycleLengthSA = temp;
                        }
                        else
                        {
                            int temp = 0;
                            int.TryParse(arrLine[14], out temp);
                            currRead.CycleLengthSA = temp;
                        }

                        //Console.WriteLine (cl.ToString & " is " & clN.ToString & " and " & cla.ToString)

                        myListReadings.Add(currRead);

                        readID += 1;
                    }
                    else if (currLine.Substring(0, 2) == "  ")
                    {

                        // count number of lanes
                        currLine = currLine.Trim();
                        currLine = currLine.Replace("'", "");
                        currLine = currLine.Replace(">", "!");
                        currLine = currLine.Replace("^", "");
                        currLine = currLine.Replace("  ", " ");
                        currLine = currLine.Replace("  ", " ");
                        currLine = currLine.Replace("  ", " ");

                        string[] fixValues = Regex.Split(currLine, "!");
                        Approach approach = new Approach();

                        try
                        {
                            int temp = 0;
                            int.TryParse(Regex.Split(fixValues[0], " ")[0], out temp);
                            approach.Intersection = temp;
                            int temp1, temp2 = 0;
                            int.TryParse(Regex.Split(fixValues[0], " ")[1], out temp1);
                            int.TryParse(Regex.Split(fixValues[0], " ")[2], out temp2);
                            approach.SAorLK = Regex.Split(fixValues[0], " ")[1] + Regex.Split(fixValues[0], " ")[2];
                            approach.PhaseId = Regex.Split(fixValues[0], " ")[3];
                            int temp4 = 0;
                            int.TryParse(Regex.Split(fixValues[0], " ")[4], out temp4);
                            approach.PhaseTime = temp4;
                            if (Program.IsNumeric(fixValues[fixValues.Length - 1]))
                            {
                                int ads = 0;
                                int.TryParse(fixValues[fixValues.Length - 1].Trim(), out ads);
                                approach.ADS = ads;
                            }
                            else
                            {
                                approach.ADS = 0;

                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(CurrValue + " " + currLine);
                            throw ex;
                        }


                        int numLanes = fixValues.Length - 2;

                        for (int i = 1; i <= numLanes; i++)
                        {

                            if (approach.PhaseTime > 0)
                            {

                                Lane lane = new Lane();

                                lane.LaneId = i;

                                int ds = 0;
                                int.TryParse(Regex.Split(fixValues[i].Trim(), " ")[0], out ds);
                                lane.DS = ds;

                                if (fixValues[i].Trim().Length > 2)
                                {
                                    int vo = 0;
                                    int.TryParse(Regex.Split(fixValues[i].Trim(), " ")[1], out vo);
                                    lane.VO = vo;
                                }
                                else
                                {
                                    lane.VO = 0;
                                }

                                if (fixValues[i].Trim().Length > 3)
                                {
                                    int vk = 0;
                                    int.TryParse(Regex.Split(fixValues[i].Trim(), " ")[2], out vk);
                                    lane.VK = vk;
                                }
                                else
                                {
                                    lane.VK = 0;
                                }

                                if (approach.PhaseTime > 0 && lane.DS > 0)
                                {
                                    lane.MaxFlow = (3600 * lane.VK) / (approach.PhaseTime * (lane.DS / 100.0));
                                }

                                if (myListReadings[readID - 1].CycleLengthActual > 0)
                                {
                                    lane.Capacity = lane.MaxFlow * approach.PhaseTime / myListReadings[readID - 1].CycleLengthActual;
                                }

                                if (lane.DS > 0 || lane.VO > 0 || lane.VK > 0 || approach.PhaseTime > 0)
                                {
                                    approach.Lanes.Add(lane);
                                }

                            }
                        }


                        myListReadings[readID - 1].Approaches.Add(approach);
                    }
                    else if (fIsPhaseDataLine(currLine))
                    {

                        string[] phases = Regex.Split(Regex.Replace(currLine, "  ", " "), " ");
                        if (phases.Length > 0)
                        {
                            myListReadings[readID - 1].PhaseA = phases[0].Replace("A=", "").Trim();
                        }
                        if (phases.Length > 1)
                        {
                            myListReadings[readID - 1].PhaseB = phases[1].Replace("B=", "").Trim();
                        }
                        if (phases.Length > 2)
                        {
                            myListReadings[readID - 1].PhaseC = phases[2].Replace("C=", "").Trim();
                        }
                        if (phases.Length > 3)
                        {
                            myListReadings[readID - 1].PhaseD = phases[3].Replace("D=", "").Trim();
                        }
                        if (phases.Length > 4)
                        {
                            myListReadings[readID - 1].PhaseE = phases[4].Replace("E=", "").Trim();
                        }
                        if (phases.Length > 5)
                        {
                            myListReadings[readID - 1].PhaseF = phases[5].Replace("F=", "").Trim();
                        }
                        if (phases.Length > 6)
                        {
                            myListReadings[readID - 1].PhaseG = phases[6].Replace("G=", "").Trim();
                        }
                    }


                }
                // currLine IsNot Nothing


                prevLine = currLine;
            }

            sr.Close();

            return myListReadings;
        }

        private static string RemoveDoubleSpaces(string line)
        {
            while (line.IndexOf("  ") != -1)
            {
                line = line.Replace("  ", " ");
            }
            return line;
        }

        private bool fIsPhaseDataLine(string line)
        {
            return line.Substring(0, 2) == "A=" || line.Substring(0, 2) == "B=" || line.Substring(0, 2) == "C=" || line.Substring(0, 2) == "D=" || line.Substring(0, 2) == "E=";
        }
    }
}
