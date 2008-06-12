using System.Windows.Forms;

namespace DidjImp
{
	public class InvokeUtil
	{
		public delegate void VoidDelegate();

		public static void InvokeIfRequired(Control c, VoidDelegate d)
		{
			if (c.InvokeRequired)
				c.Invoke(d);
			else
				d.Invoke();
		}
	}
}
