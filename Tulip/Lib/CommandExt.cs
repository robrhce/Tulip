﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DNP3.Interface;

namespace Tulip.Lib
{
    public static class CommandExt
    {
        public static AnalogOutputFloat32 GetAnalogOutput(this Command command)
        {
            if (command.Point.Type == POINT_TYPE.ANALOG_CONTROL && command.AnalogValue.HasValue)
            {
                AnalogOutputFloat32 aof = new AnalogOutputFloat32(Convert.ToSingle(command.AnalogValue));
                return aof;
            }
            else
            {
                throw new ArgumentException("Invalid argument: Point.Type must be ANALOG_CONTROL and AnalogValue must be != null");
            }
        }

        public static ControlRelayOutputBlock GetCROB(this Command command)
        {
            if (command.Point.Type == POINT_TYPE.DIGITAL_CONTROL && command.DigitalControl.HasValue)
            {
                ControlRelayOutputBlock crob = new ControlRelayOutputBlock(command.DigitalControl.Value, Convert.ToByte(command.DigitalCount), Convert.ToUInt16(command.DigitalOnTime), Convert.ToUInt16(command.DigitalOffTime));
                return crob;
            }
            else
            {
                throw new ArgumentException("Invalid argument: Point.Type must be DIGITAL_CONTROL and DigitalValue must be != null");
            }
        }
    }
}