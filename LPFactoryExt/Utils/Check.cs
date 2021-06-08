using LPFactory.Models;

namespace LPFactory.Utils
{
    public static class Check
    {
        public static void NotNull(object o, string name)
        {
            if (o == null)
            {
                throw new AweArgumentNullException(name);
            }
        }
    }
}