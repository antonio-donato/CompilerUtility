using System.Windows.Forms;

namespace Studiofarma
{
    public static class Extensions
    {
        /// <summary>
        /// Extension method that allows for automatic anonymous method invocation.
        /// </summary>
        public static void Invoke(this Control c, MethodInvoker mi)
        {
            c.Invoke(mi);
        }
    }
}
