using System;
using System.IO;

namespace ParseLegionCalendar
{
   using IDbMaster;
   using LegionCalObj;
   using MasterToAttachmentFileProcess;
   using NowTime;

   class CParseLegionCalendar
   {
      static void Main()
      {
         string strError = "";
         DateTime dtNow = DateTime.Now;
         string strArgs = string.Format("SPECIAL={0}-{0}", dtNow.ToString("yyyyMMdd"));
         string strFileTitle = string.Format("Legion_Calendar_{0}_{1}.xlsx", dtNow.Year.ToString("D4"), dtNow.Month.ToString("D2"));
         //
         string makeFileName(string s) => Path.Combine(Path.GetTempPath(), strFileTitle);
         IDbMaster getMaster(DateTime dt1, DateTime dt2, string strReportFile) =>
            new CLegionCalObjMaster(strReportFile, dt1, 3);
         //
         CNowTime.TimeSay("Getting the Legion Calendar");
         
         if (!CMasterToAttachmentFileProcess.ProcessReport
            (
               new string[] { strArgs },
               getMaster,
#if !DEBUG
               "AMERICAN_LEGION",
#else 
               "TOM",
#endif
               ("American Legion Periodic Calendar (" + dtNow.ToString("y") + ")"),
               "This is an automated message\r\nAutomated by Tom Hines\r\n\r\nSee: attachment",
               makeFileName,
               ref strError)
            )
         {
            CNowTime.TimeSay("Could not process report: " + strError);
            return;
         }
         
         CNowTime.TimeSay("Finished!");
      }
   }
}
