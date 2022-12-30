﻿namespace RxBim.Application.Menu.Config.Autocad.Sample.Commands
{
    using Autodesk.AutoCAD.ApplicationServices.Core;
    using Command.Autocad;
    using Command.Autocad.Base;
    using Shared;

    /// <inheritdoc />
    [RxBimCommandClass(
        "HelloCmd1Example",
        ToolTip = "Tooltip: I'm run command #1. Push me!",
        Text = "Command\n#1",
        Description = "Description: This is command #1",
        LargeImage = @"img\num1_32.png",
        SmallImage = @"img\num1_16.png",
        SmallImageLight = @"img\num1_16_light.png",
        LargeImageLight = @"img\num1_32_light.png",
        HelpUrl = "https://github.com/ReactiveBIM/RxBim")]
    public class Cmd1 : RxBimCommand
    {
        /// <summary>
        /// Executes the command.
        /// </summary>
        public PluginResult ExecuteCommand()
        {
            Application.ShowAlertDialog("Hello #1");
            return PluginResult.Succeeded;
        }
    }
}