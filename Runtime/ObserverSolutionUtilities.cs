using System;
using System.Collections.Generic;
using System.Linq;

public static class ObserverSolutionUtilities
{
    public static IEnumerable<Type> GetDerivedChannelGroupTypes()
    {
        var baseChannelGroupType = typeof(BaseChannelGroup);
        return AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(assembly => assembly.GetTypes())
            .Where(type => type.IsClass && type.IsSubclassOf(baseChannelGroupType));
    }

    public static IEnumerable<Type> GetDerivedMessageTypes()
    {
        var interfaceMessageType = typeof(IMessage);
        return AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(assembly => assembly.GetTypes())
            .Where(type => !type.IsClass && !type.IsInterface && interfaceMessageType.IsAssignableFrom(type));
    }

    public static IEnumerable<Type> MakeGenericChannelTypes(Type channelType)
    {
        var messageTypes = GetDerivedMessageTypes();
        foreach (var messageType in messageTypes)
        {
            yield return channelType.MakeGenericType(messageType);
        }
    }
}