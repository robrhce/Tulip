using DNP3.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tulip.Lib
{
    public class ChannelWrapper
    {
        public IChannel Channel;
        public Channel Model;
        public StringBuilder Log = new StringBuilder();

        public ChannelState state;

        public Action OnStateChanged;

        public ChannelWrapper(Channel Model, IChannel Channel)
        {
            this.Model = Model;
            this.Channel = Channel;
        }

        public void StateChanged(ChannelState state)
        {
            this.state = state;

            if (OnStateChanged != null)
                OnStateChanged();
        }
    }
}
