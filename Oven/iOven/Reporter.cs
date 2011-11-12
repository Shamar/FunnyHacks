using System;
using Oven;
using System.Collections.Generic;

namespace iOven
{
    [Serializable]
    public sealed class Reporter : TimerBase, IReporter
    {
        private static Uri GetReportUri(Uri cookUri)
        {
            if(null == cookUri)
                return null;
            return cookUri.MakeRelativeUri(new Uri("report/"));
        }
  
        private readonly Oven.Reporting.IOven _oven;
        private readonly List<Report> _reports;
        
        public Reporter (Uri cookUri, Oven.Reporting.IOven oven)
            : base(GetReportUri(cookUri))
        {
            if(null == oven)
                throw new ArgumentNullException("oven");
            _oven = oven;
            _reports = new List<Report>();
        }
        
        private void BuildNewReport()
        {
            Report report = new Report(Time.Now, _oven.CurrentState);
            _reports.Add(report);
            EventHandler<InfoEventArgs<Report>> handler = NewReportCreated;
            if(null != handler)
                handler(this, new InfoEventArgs<Report>(report));
        }

        #region IReporter implementation
        public event EventHandler<InfoEventArgs<Report>> NewReportCreated;

        public void SetReportInterval (Minute interval)
        {
            base.EmptySchedules();
            base.AddPeriodicTask(interval, BuildNewReport);
        }

        public IEnumerable<Report> Reports {
            get {
                return _reports;
            }
        }
        #endregion


    }
}

