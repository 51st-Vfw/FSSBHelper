// ********************************************************************************************
//
// Program.cs: FSSBHelper Top Level
//
// Copyright (c) 2021 pch07 / Rage
// Copyright (C) 2021-22 twillis / ilominar / Raven
//
// This program is free software: you can redistribute it and/or modify it under the terms of
// the GNU General Public License as published by the Free Software Foundation, either version
// 3 of the License, or (at your option) any later version.
//
// This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY;
// without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.
// See the GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License along with this program.
// If not, see <https://www.gnu.org/licenses/>.
//
// ********************************************************************************************

using System;
using System.Windows.Forms;

namespace FSSBHelper
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            using (var monitor = new JoystickMonitor())
                Application.Run(new PrefsUI(monitor));
        }
    }
}
