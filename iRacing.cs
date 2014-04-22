// This file is part of iRacingSDK.
//
// Copyright 2014 Dean Netherton
// https://github.com/vipoo/iRacingSDK.Net
//
// iRacingSDK is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// iRacingSDK is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with iRacingSDK.  If not, see <http://www.gnu.org/licenses/>.

using System;
using System.Collections.Generic;
using System.Threading;
using Win32.Synchronization;
using System.Runtime.InteropServices;
using System.IO.MemoryMappedFiles;
using System.Diagnostics;

namespace iRacingSDK
{
	public class iRacing
	{
		public static IEnumerable<DataSample> GetDataFeed()
		{
			foreach(var notConnectedSample in WaitForInitialConnection())
				yield return notConnectedSample;

            foreach (var sample in AllSamples())
                yield return sample;
		}

		static IEnumerable<DataSample> WaitForInitialConnection()
		{
            Trace.WriteLine("Waiting to connect to iRacing application");
			while(!iRacingConnection.IsConnected())
			{
				yield return DataSample.YetToConnected;
				Thread.Sleep(10);
			}
            Trace.WriteLine("Connected to iRacing application");
		}

        static DataFeed dataFeed = null;

		static IEnumerable<DataSample> AllSamples()
		{
            if( dataFeed == null )
			    dataFeed = new DataFeed(iRacingConnection.Accessor);

			while(true)
			{
                if (!iRacingConnection.WaitForData())
                    yield return DataSample.YetToConnected;

				var data = dataFeed.GetNextDataSample();
                if (data != null)
                    yield return data;
			}
		}

        public static readonly Replay Replay = new Replay();
	}

}
