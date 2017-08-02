﻿using System.Threading.Tasks;
using Template10.Common;
using Template10.StartArgs;

namespace Template10.Strategies
{
    public class DefaultExtendedSessionStrategy : ExtendedSessionStrategyBase
    {
        public DefaultExtendedSessionStrategy() : base()
        {
            // empty
        }

        public async override Task StartupAsync(ITemplate10StartArgs e)
        {
            if (Settings.AutoExtendExecutionSession)
            {
                await (this as IExtendedSessionStrategyInternal).StartUnspecifiedAsync();
            }
        }

        public async override Task SuspendingAsync()
        {
            if (Settings.AutoExtendExecutionSession)
            {
                await (this as IExtendedSessionStrategyInternal).StartSaveDataAsync();
            }
            else
            {
                await Task.CompletedTask;
            }
        }
    }
}
