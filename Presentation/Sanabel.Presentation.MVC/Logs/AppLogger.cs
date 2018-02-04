using BusinessSolutions.Common.Infra.Log;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sanabel.Presentation.MVC.Logs
{
    public class AppLogger : BusinessSolutions.Common.Infra.Log.ILogger
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        public AppLogger()
        {

        }

        public void Error(Exception exception)
        {
            logger.Error(exception);
        }

        public void Error(string error)
        {
            logger.Error(error);
        }

        public void Info(string message)
        {
            logger.Info(message);
        }
    }
}