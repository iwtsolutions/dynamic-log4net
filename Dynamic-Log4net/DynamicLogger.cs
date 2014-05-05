using log4net;
using log4net.Repository.Hierarchy;

namespace Dynamic_Log4net
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
