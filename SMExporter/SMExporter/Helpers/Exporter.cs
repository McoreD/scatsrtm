using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Data.OleDb;
using System.IO;

namespace SMExporter
{
    public class Exporter
    {

        private List<Reading> mListReadings = new List<Reading>();
        private int mMaxValue = 0;
        private int mValue = 0;

        public Exporter(List<Reading> myList)
        {
            mListReadings = myList;
            mMaxValue = mListReadings.Count;
        }

        public void sExportToDatabase(string mdbPath)
        {

            //Dim mdbPath As String = Application.StartupPath & "\stratmon.mdb"

            string connString = "Provider=Microsoft.ACE.OLEDB.12.0;" + "Data Source=" + mdbPath;

            System.Data.OleDb.OleDbConnection conn = new System.Data.OleDb.OleDbConnection(connString);
            string qry = "SELECT * FROM tblSM";

            System.Data.OleDb.OleDbDataAdapter da = new System.Data.OleDb.OleDbDataAdapter(qry, conn);
            OleDbCommandBuilder cb = new OleDbCommandBuilder(da);

            DataSet ds = new DataSet();
            da.Fill(ds, "tblSM");
            DataTable dt = ds.Tables["tblSM"];

            mValue = 0;
            Adapter.CurrentValue = 0;
            Adapter.MaxValue = mListReadings.Count;

            foreach (Reading read in mListReadings)
            {

                foreach (Approach approach in read.Approaches)
                {

                    foreach (Lane lane in approach.Lanes)
                    {

                        DataRow r = dt.NewRow();

                        r["colDate"] = read.DateRead;
                        r["colDay"] = read.DayRead;
                        r["colTime"] = read.Time;
                        r["colPL"] = read.PlansActive;
                        r["colPV"] = read.PlansVoted;
                        r["colCLn"] = read.CycleLengthNominal;
                        r["colCLa"] = read.CycleLengthActual;
                        r["colCLr"] = read.CycleLengthRequired;
                        r["colCLsa"] = read.CycleLengthSA;

                        r["colPhaseA"] = read.PhaseA;
                        r["colPhaseB"] = read.PhaseB;
                        r["colPhaseC"] = read.PhaseC;
                        r["colPhaseD"] = read.PhaseD;
                        r["colPhaseE"] = read.PhaseE;
                        r["colPhaseF"] = read.PhaseF;
                        r["colPhaseG"] = read.PhaseG;

                        r["colIntID"] = approach.Intersection;
                        r["colSAorLK"] = approach.SAorLK;
                        r["colPhaseID"] = approach.PhaseId;
                        r["colPT"] = approach.PhaseTime;
                        r["colADS"] = approach.ADS;

                        r["colLaneID"] = lane.LaneId;
                        r["colDS"] = lane.DS;
                        r["colVO"] = lane.VO;
                        r["colVK"] = lane.VK;

                        r["colVehplh"] = approach.GetVehPerLanePerHourPT(lane);
                        r["colCongRatio"] = approach.GetCongestionIndex(read, lane);


                        dt.Rows.Add(r);

                    }
                }

                Adapter.CurrentValue += 1;

                mValue += 1;
            }

            da.Update(ds, "tblSM");
            Adapter.CurrentValue += 1;

            conn.Close();
        }


        public void sExportToText(string outputPath)
        {

            bool fileExisted = File.Exists(outputPath);

            StreamWriter sw = new StreamWriter(outputPath, true);

            if (!fileExisted)
            {

                sw.Write("Time");
                sw.Write(";");
                sw.Write("Intersection");
                sw.Write(";");
                sw.Write("PhaseNumber");
                sw.Write(";");
                sw.Write("Lane ID");
                sw.Write(";");
                sw.Write("DS");
                sw.Write(";");
                sw.Write("Green Time");
                sw.Write(";");
                sw.Write("CycleLength");
                sw.Write(";");
                sw.Write("VK");
                sw.Write(";");
                sw.Write("VO");
                sw.Write(";");
                sw.Write("Veh/lane/hour");

                sw.WriteLine(";");
            }

            foreach (Reading read in mListReadings)
            {

                foreach (Approach approach in read.Approaches)
                {

                    foreach (Lane lane in approach.Lanes)
                    {

                        sw.Write(read.Time);
                        sw.Write(";");
                        sw.Write(approach.Intersection);
                        sw.Write(";");
                        sw.Write("phase ");
                        sw.Write(approach.PhaseId);
                        sw.Write(";");
                        sw.Write("lane ");
                        sw.Write(lane.LaneId);
                        sw.Write(";");
                        sw.Write(lane.DS);
                        sw.Write(";");
                        sw.Write(approach.PhaseTime);
                        sw.Write(";");
                        sw.Write(read.CycleLengthNominal);
                        sw.Write(";");
                        sw.Write(lane.VK);
                        sw.Write(";");
                        sw.Write(lane.VO);
                        sw.Write(";");
                        sw.Write(approach.GetVehPerLanePerHourPT(lane));

                        sw.WriteLine(";");

                    }
                }
            }

            //System.Diagnostics.Process.Start(outputPath)

            sw.Close();
        }

        public int MaxValue
        {
            get { return mMaxValue; }
            set { mMaxValue = value; }
        }


        public int CurrentValue
        {
            get { return mValue; }
            set { mValue = value; }
        }


    }

}
