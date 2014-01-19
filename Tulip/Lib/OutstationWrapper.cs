using DNP3.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tulip.Lib
{
    public class OutstationWrapper : IMeasurementHandler
    {
        public IMaster Master;
        public Outstation Model;
        public StringBuilder Log = new StringBuilder();
        public StackState state;

        public Action OnStateChanged;
        public Action<OutstationWrapper, IMeasurementUpdate> OnMeasurementsReceived;

        // TODO: poor implementation
        //public List<IndexedValue<Analog>> NewAnalogs = new List<IndexedValue<Analog>>();
        //public List<IndexedValue<Binary>> NewBinaries = new List<IndexedValue<Binary>>();

        public OutstationWrapper(Outstation Model, IMaster Master)
        {
            this.Model = Model;
            this.Master = Master;
        }

        public OutstationWrapper(Outstation Model)
        {
            this.Model = Model;
        }

        public void StateChanged(StackState state)
        {
            this.state = state;

            if (OnStateChanged != null)
                OnStateChanged();
        }

        public void Load(IMeasurementUpdate update)
        {
            /*
            foreach (IndexedValue<Binary> v in update.BinaryUpdates)
            {
                NewBinaries.Add(v);
                //Console.WriteLine("value: " + v.value.value + " index: " + v.index);
            }
            foreach (IndexedValue<Analog> v in update.AnalogUpdates)
            {
                NewAnalogs.Add(v);
                //Console.WriteLine("value: " + v.value.value + " index: " + v.index);               
            }*/
            
            if (OnMeasurementsReceived != null)
                OnMeasurementsReceived(this, update);
            /*
             foreach (var v in update.CounterUpdates) Console.WriteLine("value: " + v.value.value + " index: " + v.index);
             foreach (var v in update.ControlStatusUpdates) Console.WriteLine("value: " + v.value.value + " index: " + v.index);
             foreach (var v in update.SetpointStatusUpdates) Console.WriteLine("value: " + v.value.value + " index: " + v.index);
             foreach (var v in update.OctetStringUpdates) Console.WriteLine("value: " + v.value.AsString() + " index: " + v.index);
             */
        }
    }
}
