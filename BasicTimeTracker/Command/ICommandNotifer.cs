using System;
using System.Windows.Input;

namespace BasicTimeTracker.Command
{
    public interface ICommandNotifer : ICommand
    {
        EventHandler NotifyExecuteWasRaised { get; set; }
    }
}