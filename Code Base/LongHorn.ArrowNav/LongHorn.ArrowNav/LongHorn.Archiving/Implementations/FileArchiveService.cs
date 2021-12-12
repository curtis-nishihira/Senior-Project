using LongHorn.Archiving;
using LongHorn.ArrowNav.DAL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Text;

namespace LongHorn.Archiving
{
    public class FileArchiveService : IArchiveService
    {
        Stopwatch _stopWatch = new Stopwatch();
        private double _timeElapsed;


        public string Archive() // could change later to Predicate<T>
        {
            LoggingRepository archiveRepository = new LoggingRepository();
            var listOfLogs = archiveRepository.ReadAllBasedOnRangeOf(DateTime.UtcNow.Date.AddDays(-30), DateTime.UtcNow.Date);
            Offload(listOfLogs);
            //checklist()
            // note: Change to just date no time
            var repsonse = archiveRepository.DeleteBasedOnRangeOf(DateTime.UtcNow.Date.AddDays(-30), DateTime.UtcNow.Date);
            return repsonse;
        }

        public void Offload(List<string> result)
        {
            string fileName = @"C:\Users\curti\OneDrive\Desktop\log -"
                 + DateTime.UtcNow.ToString("dd-MM-yyyy")
                + ".txt";
            using (FileStream fileStream = File.Create(fileName))
            {
                for (int counter = 0; counter < result.Count; counter++)
                {
                    Byte[] fileContents = new UTF8Encoding(true).GetBytes(result[counter] + "\n");
                    fileStream.Write(fileContents, 0, fileContents.Length);
                }
            }
            // zips file just created
            string zipPath = @"C:\Users\curti\OneDrive\Desktop\log -"
                + DateTime.UtcNow.ToString("dd-MM-yyyy")
                + ".zip";

            using (ZipArchive archive = ZipFile.Open(zipPath, ZipArchiveMode.Create))
            {
                archive.CreateEntryFromFile(fileName, Path.GetFileName(fileName));
            }
            File.Delete(fileName);
        }

        public bool CheckList(List<string> listOfLogs)
        {
            return true;
        }
        public void TimerStart()
        {
            _stopWatch.Start();
        }

        public double TimerEnd()
        {
            _stopWatch.Stop();
            return _stopWatch.Elapsed.TotalSeconds;
        }

        public double GetElapsedTime()
        {
            return _timeElapsed;
        }
    }
}
