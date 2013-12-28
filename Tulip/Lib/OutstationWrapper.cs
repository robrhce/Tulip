using DNP3.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tulip.Lib
{
    public class OutstationWrapper
    {
        public IMaster Master;
        public Outstation Model;
        public StringBuilder Log = new StringBuilder();
        public StackState state;

        public Action OnStateChanged;

        public OutstationWrapper(Outstation Model, IMaster Master)
        {
            this.Model = Model;
            this.Master = Master;
        }

        public void StateChanged(StackState state)
        {
            this.state = state;

            if (OnStateChanged != null)
                OnStateChanged();
        }
    }
}
