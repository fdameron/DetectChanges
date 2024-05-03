using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OCEntity.OnBoard;

namespace DetectChanges
{
    class Program
    {
        static void Main(string[] args)
        {
            var log = File.AppendText("c:\\logs\\DetectChanges.txt");
            var s = new Stopwatch();
            s.Start();
            using(var ctxOB = new OnboardEntities())
            {
                var myfd = ctxOB.imagesync_filedatas.ToList();
                foreach(var item in myfd)
                {
                    item.LastISSRenamingEnabled = false;
                    //ctxOB.SaveChanges();
                }
                ctxOB.SaveChanges();
            }
            s.Stop();
            Console.WriteLine("AutoDetectChangesEnabled = true  Total Seconds:" + s.Elapsed.TotalSeconds);
            log.WriteLine("AutoDetectChangesEnabled = true  Total Seconds:" + s.Elapsed.TotalSeconds);
            log.Flush();
            s.Reset();
            s.Start();
            using(var ctxOB = new OnboardEntities())
            {
                ctxOB.Configuration.AutoDetectChangesEnabled = false;
                var myfd = ctxOB.imagesync_filedatas.ToList();
                foreach(var item in myfd)
                {
                    item.LastISSRenamingEnabled = true;
                    //ctxOB.ChangeTracker.DetectChanges();
                    //ctxOB.SaveChanges();
                }
                ctxOB.ChangeTracker.DetectChanges();
                ctxOB.SaveChanges();
            }
            s.Stop();
            Console.WriteLine("AutoDetectChangesEnabled = false  Total Seconds:" + s.Elapsed.TotalSeconds);
            log.WriteLine("AutoDetectChangesEnabled = false  Total Seconds:" + s.Elapsed.TotalSeconds);
            log.Flush();
            log.Close();
        }
    }
}
