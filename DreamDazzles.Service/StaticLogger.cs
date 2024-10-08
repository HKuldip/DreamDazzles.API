﻿using Serilog;

namespace DreamDazzles.Service
{
    public static class StaticLogger
    {
        /// <summary>
        /// Ensures that the logger is initialized.
        /// </summary>
        public static void EnsureInitialized()
        {
            if (Log.Logger is not Serilog.Core.Logger)
            {
                Log.Logger = new LoggerConfiguration()
                    .Enrich.FromLogContext()
                    .WriteTo.Console()
                    .CreateLogger();
            }
        }
    }
}
