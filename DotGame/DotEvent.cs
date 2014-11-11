using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotGame
{
    class DotEvent: EventArgs
    {
    	private string EventInfo;
    	public DotEvent(string Text)
    	{
    		EventInfo = Text;
    	}
    	public string GetInfo()
    	{
    		return EventInfo;
    	}
    }

    class DotExplode: EventArgs
    {
        private string EventInfo;
    	public DotExplode(string Text)
    	{
    		EventInfo = Text;
    	}
    	public string GetInfo()
    	{
    		return EventInfo;
    	}
    }
}
