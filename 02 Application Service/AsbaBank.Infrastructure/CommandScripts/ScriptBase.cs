

namespace AsbaBank.Infrastructure.CommandScripts
{
    public abstract class ScriptBase
    {
        protected readonly AsbaBank.Infrastructure. JsonSerializer Serializer = new JsonSerializer();
        protected const string ScriptExtension = ".script";
    }
}