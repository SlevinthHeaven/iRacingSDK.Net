﻿##DataSample

This type is the container for all data retrieved from iRacing.  iRacing exposes two main data sets: SessionData and Telemetry data. 

###SessionData
The session data provides a highly structured set of read only fields that detail all kinds of interesting bits of information regarding the games current running sessions.  This data does not change very often - unless, for example, the user loads a different track.

This data is extracted from an underlying YAML payload provided by game.  This structure may be changed by iRacing in future builds (new fields added).  You can use the GenerateDataModels project to update the structures to match the latest YAML payload.

Most fields are self explanatory.  The SessionData type is coded as a partial class - and this allows a bunch of extra helper methods to simplify common data requests.

###Telemetry
The Telemetry accessor of the DataSample, provides access to the realtime data set generated by the iRacing game.  

The Telemetry type is a dictionary keyed by a string name, and an object value (number or string).

The type also exposes the known key values in handy accessors.  The GenerateDataModels project can be used to update the known key value pairs.

Most of the keys of the Telemetry type should generally be self-explanatory.

In addition to the dictionary of key values provided by the game's data stream, there are other composite and aggregrate mixins to the Telemetry type

**Telemetry - float[] CarIdxDistance** 

`CarIdxLap[index] + CarIdxDistancePct[index]`

An array of floats for each car (as per SessionData.DriverInfo.Drivers array), representing the sum of the car's lap counter and its lap percentage completion value 

**Telemetry - int[] Positions**

An array of integers representing each car's running order as per the CarIdxDistance figure.

**Telemetry - TimeSpan SessionTimeSpan**

`TimeSpan.FromSeconds(SessionTime)`

Return the a TimeSpan of the current duration of the active session.

**Telemetry - Car[] Cars**

Returns an array of Car objects for each of the active drivers.

The Car type contains fields such as:  Lap, DistancePercentage, TotalDistance, Driver, Postiton, UserName, CarNumber, HasSeenCheckeredFlag, IsPaceCar, HasData, TrackSurface.

**Telementry - Car CamCar**

`Cars[CamCarIdx]`

Returns a Car instance, representing the car currently being viewed by the active camera.

