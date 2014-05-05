using log4net;
using log4net.Repository.Hierarchy;

namespace DynamicLog4net
{
    internal sealed class DynamicLogger : Logger
    {
        internal DynamicLogger(string name)
            : base(name)
        {
            base.Hierarchy = (log4net.Repository.Hierarchy.Hierarchy)LogManager.GetRepository();
        }
    }
}
