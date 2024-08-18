namespace OpenSBS.Engine.Commands;

public class ServerCommand(string type, object payload) : Command(type, payload);
