
using LongHorn.ArrowNav.DAL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Text;

namespace LongHorn.ArrowNav.Archiving
{
    public class FileArchiveService : IArchiveService
    {
        Stopwatch _stopWatch = new Stopwatch();
        private double _timeElapsed;


        public string Archive() // could change later to Predicate<T>
        {
            LoggingRepository archiveRepository = new LoggingRepository();
            var listOfLogs = archiveRepository.ReadAllBasedOnRangeOf(DateTime.UtcNow.Date.AddYears(-10), DateTime.UtcNow.Date.AddDays(-30));
            //Offload(listOfLogs);
            CheckList(listOfLogs);
            var repsonse = archiveRepository.DeleteBasedOnRangeOf(DateTime.UtcNow.Date.AddYears(-10),DateTime.UtcNow.Date.AddDays(-30));
            return repsonse;
        }

        //public void Offload(List<string> result)
        //{
        //    string fileName = ConfigurationManager.AppSettings.Get("ArchiveTxtPath")
        //         + DateTime.UtcNow.ToString("dd-MM-yyyy")
        //        + ConfigurationManager.AppSettings.Get("TxtFileExtension");
        //    using (FileStream fileStream = File.Create(fileName))
        //    {
        //        for (int counter = 0; counter < result.Count; counter++)
        //        {
        //            Byte[] fileContents = new UTF8Encoding(true).GetBytes(result[counter] + "\n");
        //            fileStream.Write(fileContents, 0, fileContents.Length);
        //        }
        //    }
        //    // zips file just created
        //    try
        //    {
        //        string zipPath = ConfigurationManager.AppSettings.Get("ArchvieZipPath")
        //                        + DateTime.UtcNow.ToString("dd-MM-yyyy")
        //                        + ConfigurationManager.AppSettings.Get("ZipFileExtension");

        //        using (ZipArchive archive = ZipFile.Open(zipPath, ZipArchiveMode.Create))
        //        {
        //            archive.CreateEntryFromFile(fileName, Path.GetFileName(fileName));
        //        }
        //    }
        //    catch(System.IO.IOException e)
        //    {
        //        Console.WriteLine("Archive already exists");
        //    }
        //    File.Delete(fileName);
        //}

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
