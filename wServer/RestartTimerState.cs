namespace wServer
{
	public class RestartTimerState
	{

		public RestartTimerTypes Type;
		public int SecondsBeforeRestart;

		public string[] Tokens { get; set; } = { "0" };
		public string[] Hashs { get; set; } = { "0" };

		public RestartTimerState(RestartTimerTypes type, int secondsBeforeRestart)
		{
			Type = type;
			SecondsBeforeRestart = secondsBeforeRestart;
		}
	}

    public enum RestartTimerTypes
	{
        Restart,
		Warning
	}
}