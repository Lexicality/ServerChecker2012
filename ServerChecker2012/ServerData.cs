using System;
using System.Text;
using System.Xml;
using System.Timers;
using System.Diagnostics;

namespace ServerChecker2012
{
	public enum QueryType
	{
		NONE,
		SOURCE,
		//MODULE
	}
	public enum ProcessStatus
	{
		INACTIVE,
		STARTING,
		RUNNING,
		FROZEN
	}
	public class ServerPingUpdateEventArgs : EventArgs
	{
		public long LastQuery;
		public ServerPingUpdateEventArgs(long ping)
		{
			LastQuery = ping;
		}
	}
	public class ServerProcessUpdateEventArgs : EventArgs
	{
		public ProcessStatus Status;
		public Int32 ProcessID;
		public ServerProcessUpdateEventArgs(Int32 PID, ProcessStatus stat)
		{
			Status = stat;
			ProcessID = PID;
		}
	}
	public delegate void ServerPingUpdateEventHandler(object server, ServerPingUpdateEventArgs args);
	public delegate void ServerProcessUpdateEventHandler(object server, ServerProcessUpdateEventArgs args);
	public class ServerData
	{
		readonly ushort id;
		public ushort ID { get { return id; } }

		string name = "No name";
		public string Name
		{
			get
			{
				return name;
			}
			set
			{
				if (value == null)
					throw new ArgumentNullException("Name");
				if (value == name)
					return;
				name = value;
				SendInfoUpdate();
			}
		}

		string ipaddr = "127.0.0.1";
		public string IPAddress
		{
			get
			{
				return ipaddr;
			}
			set
			{
				if (value == null)
					throw new ArgumentNullException("IPAddress");
				if (value == ipaddr)
					return;
				try
				{
					lock (ProcLock)
					{
						var ip = System.Net.IPAddress.Parse(value);
						ipaddr = ip.ToString();
						if (Query == QueryType.SOURCE && SourceQuery != null)
							SourceQuery.UpdateIP(ip);
						SendInfoUpdate();
					}
				}
				catch (Exception e)
				{
					throw new ArgumentException("Invalid IP Address: " + e.Message, "IPAddress", e);
				}

			}
		}

		ushort port = 0;
		public ushort Port
		{
			get
			{
				return port;
			}
			set
			{
				if (value == port)
					return;
				try
				{
					lock (ProcLock)
					{
						if (Query == QueryType.SOURCE && SourceQuery != null)
							SourceQuery.UpdatePort(port);
					}
				}
				catch (Exception e)
				{
					throw new ArgumentException("Invalid port: " + e.Message, "Port", e);
				}
			}
		}

		string executable = "";
		public String Executable
		{
			get { return executable; }
			set
			{
				if (value == null)
					throw new ArgumentNullException("Executable");
				if (value == executable)
					return;
				executable = value;
				if (Process != null)
					Process.StartInfo.FileName = value;
			}
		}
		string parameters = "";
		public string Parameters
		{
			get { return parameters; }
			set
			{
				if (value == null)
					throw new ArgumentNullException("Parameters");
				if (value == parameters)
					return;
				parameters = value;
				if (Process != null)
					Process.StartInfo.Arguments = value;
			}
		}

		//DateTime LastStartup = DateTime.MinValue;
		TimeSpan RestartDelay = TimeSpan.FromSeconds(1);
		System.Threading.Timer RestartTimer;

		QueryType query = QueryType.NONE;
		public QueryType Query
		{
			get { return query; }
			set
			{
				if (value == query)
					return;
				lock (ProcLock)
				{
					// Remove any existing queries
					if (query == QueryType.SOURCE)
						SourceQuery = null;

					// Install the new query
					query = value;
					if (query == QueryType.SOURCE)
						SourceQuery = new SourceQuery(this);
				}
			}
		}
		bool runqueries = true;
		public bool QueriesDisabled
		{
			get { return !runqueries; }
			set
			{
				lock (ProcLock)
				{
					runqueries = !value;
				}
			}
		}

		public SourceQuery SourceQuery { get; private set; }
		//public ModuleQuery ModuleQuery { get; private set; }

		public TimerData TimingData;
		
		IntPtr Affinity = IntPtr.Zero;
		public Int64 ProcessAffinity {
			get
			{
				return Affinity.ToInt64();
			}
			set
			{
				if (Affinity.ToInt64() == value)
					return;
				Affinity = new IntPtr(value);
				if (Affinity == IntPtr.Zero)
					return;
				lock (ProcLock)
				{
					if (ProcRunning())
						Process.ProcessorAffinity = Affinity;
				}
			}
		}

		// Events
		public event ServerPingUpdateEventHandler    PingChanged;
		public event ServerProcessUpdateEventHandler ProcessChanged;
		public event EventHandler                    InfoChanged;            
		
		// Internals
		ProcessStatus InternalStatus = ProcessStatus.INACTIVE;
		Process Process;
		Object ProcLock = new Object();

		// Murder system
		int strikes = 0;
		bool was_frozen_last_time = false;

		Timer Timer;

		public ServerData(ushort id, XmlReader xml)
		{
			this.id = id;
			ReadXML(xml);
			if (TimingData.Parent != this)
				throw new XmlException("Missing required timer tag!");
			setup();
		}
		public ServerData (ushort id,
						  string name,
						  string ipaddr,
						  ushort port,
						  string executable,
						  string parameters,
						  Int64 affinity,
						  QueryType query,
						  TimerData timer)
		{
			this.id = id;
			this.name = name;
			this.ipaddr = ipaddr;
			this.port = port;
			this.executable = executable;
			this.parameters = parameters;
			this.ProcessAffinity = affinity;
			this.query = query;
			timer.Parent = this;
			this.TimingData = timer;
			setup();
		}

		void setup ()
		{
			Timer = new Timer(TimeSpan.FromSeconds((double) TimingData.Interval).TotalMilliseconds);
			Timer.AutoReset = true;
			Timer.Enabled = true;
			Timer.Elapsed += new ElapsedEventHandler(TimerInterval);

			Process = new Process();
			Process.StartInfo.FileName = executable;
			Process.StartInfo.Arguments = parameters;
			Process.EnableRaisingEvents = true;
			Process.Exited += new EventHandler(ProcessExited);
			InternalStatus = ProcessStatus.INACTIVE;

			RestartTimer = new System.Threading.Timer((Object _) => {
				if (InternalStatus != ProcessStatus.INACTIVE)
					Start();
			});

			// Queries
			if (Query == QueryType.SOURCE)
			{
				SourceQuery = new SourceQuery(this);
			}
		}
		// Talky things
		public void Start()
		{
			lock (ProcLock)
			{
				try
				{
					if (ProcRunning())
						Process.Kill();
					Process.Start();
					if (Affinity != IntPtr.Zero)
						Process.ProcessorAffinity = Affinity;
					Timer.Start();
					InternalStatus = ProcessStatus.STARTING;
					SendProcUpdate();
					strikes = 0;
					var t = new System.Threading.Thread(ProcessStartupThread);
					t.IsBackground = true;
					t.Start();
				}
				catch (Exception e)
				{
					PushError("Could not start up", e.Message);
					Stop();
				}
			}
		}
		public void Stop()
		{
			lock (ProcLock)
			{
				Timer.Stop();
				bool shutdown = ProcRunning();
				InternalStatus = ProcessStatus.INACTIVE;
				SendPingUpdate(-2);
				SendProcUpdate();
				if (shutdown)
				{
					if (Process.Responding)
					{
						// If the process is running normally, politely ask it to shut down
						Process.CloseMainWindow();
						// Then make sure it actually closes
						var t = new System.Threading.Thread(ProcessKillThread);
						t.IsBackground = true;
						t.Start();
					}
				}
			}
		}
		// Event Handlers
		void TimerInterval(Object timer, ElapsedEventArgs args)
		{
			lock (ProcLock)
			{
				if (!ProcRunning())
					return;
				long ping = -2;
				if (runqueries)
				{
					try
					{
						if (Query == QueryType.SOURCE)
						{
							ping = SourceQuery.Ping();
						}
					}
					catch (Exception e)
					{
						PushError("Could not run query", e.Message);
						Stop();
						return;
					}
				}
				// Ping checking
				bool killed = false;
				if (ping == -1)
				{
					++strikes;
					if (strikes > TimingData.Strikes)
					{
						Process.Kill();
						killed = true;
					}
				}
				else if (strikes > 0)
				{
					strikes = 0;
				}
				// Process checking
				if (!Process.Responding)
				{
					if (!killed && was_frozen_last_time)
						Process.Kill();
					else
						was_frozen_last_time = true;
				}
				else
				{
					was_frozen_last_time = false;
				}
				SendPingUpdate(ping);
				SendProcUpdate();
			}
		}
		void ProcessExited(Object proc, EventArgs args)
		{
			lock (ProcLock)
			{
				if (InternalStatus != ProcessStatus.INACTIVE)
				{
					// Force a delay between restarts
					InternalStatus = ProcessStatus.STARTING;
					SendProcUpdate();
					/*
					// Slow down crash loops
					TimeSpan LongEnough = TimeSpan.FromMinutes(5);
					if (DateTime.Now.Subtract(LastStartup) > LongEnough)
					{
						RestartDelay = TimeSpan.FromSeconds(1);
					}
					else
					{
						RestartDelay += TimeSpan.FromSeconds(1);
					}
					*/
					// Retrigger the timer
					RestartTimer.Change(RestartDelay, TimeSpan.FromMilliseconds(-1));
				}
				else
				{
					Stop();
				}
			}
		}
		void ProcessStartupThread()
		{
			try
			{
				bool res = true;
				if (Process.MainWindowHandle != IntPtr.Zero)
					res = Process.WaitForInputIdle((int) TimeSpan.FromSeconds((double) TimingData.Startup).TotalMilliseconds);
				lock (ProcLock)
				{
					if (!ProcRunning())
					{
						Stop();
						return;
					}
					if (res)
					{
						strikes = 0;
						//LastStartup = DateTime.Now;
						InternalStatus = ProcessStatus.RUNNING;
						SendProcUpdate();
						// Get an immediate ping
						// TimerInterval(null, null);
						PushInfo("Started up");
					}
					else
					{
						++strikes;
						if (strikes > 3)
						{
							Stop();
							PushInfo("Failed to start up in time 3 times in a row.");
						}
						else
						{
							Process.Kill();
							PushInfo("Failed to start up in time.");
						}
					}
				}
			}
			catch (Exception e)
			{
				PushError("Could not start up", e.Message);
				Stop();
			}
		}
		void ProcessKillThread()
		{
			try
			{
				bool res = Process.WaitForExit((int) TimeSpan.FromSeconds(TimingData.Shutdown).TotalMilliseconds);
				if (!res)
				{
					Process.Kill();
				}
			}
			catch (Exception e)
			{
				PushError("Could not shut down", e.Message);
			}
		}
		// Event dispatcher
		void SendPingUpdate(long ping)
		{
			ServerPingUpdateEventHandler Event = PingChanged;
			if (Event == null)
				return;
			Event(this, new ServerPingUpdateEventArgs(ping));
		}
		void SendProcUpdate()
		{
			ServerProcessUpdateEventHandler Event = ProcessChanged;
			if (Event == null)
				return;
			ProcessStatus Status;
			Int32 PID = -1;
			lock (ProcLock)
			{
				if (InternalStatus != ProcessStatus.RUNNING)
					Status = InternalStatus;
				else if (ProcRunning())
					Status = Process.Responding ? ProcessStatus.RUNNING : ProcessStatus.FROZEN;
				else // wtf?
					Status = ProcessStatus.INACTIVE;
				if (Status != ProcessStatus.INACTIVE)
					PID = Process.Id;
			}
			Event(this, new ServerProcessUpdateEventArgs(PID, Status));
		}
		void SendInfoUpdate()
		{
			EventHandler Event = InfoChanged;
			if (Event != null)
				Event(this, EventArgs.Empty);
		}
		// Misc
		bool ProcRunning()
		{
			return InternalStatus != ProcessStatus.INACTIVE && !Process.HasExited;
		}
		void PushError(string what, string why)
		{
			Program.PushError(string.Format("Server #{0} {1}: {2}", ID, what, why), "Server");
		}
		void PushInfo(string what)
		{
			Program.PushInfo(String.Format("Server #{0} {1}", ID, what), "Server");
		}
		// XML related things
		void ReadXML(XmlReader xml)
		{
			xml.ReadStartElement("server");
			while (xml.Read())
			{
				if (xml.NodeType != XmlNodeType.Element)
					continue;
				switch (xml.Name)
				{
					case "name":
						name = xml.ReadElementContentAsString();
						break;
					case "executable":
						executable = xml.ReadElementContentAsString();
						break;
					case "parameters":
						parameters = xml.ReadElementContentAsString();
						break;
					case "ip":
						ipaddr = xml.ReadElementContentAsString();
						break;
					case "port":
						port = (ushort) xml.ReadElementContentAsInt();
						break;
					case "affinity":
						ProcessAffinity = xml.ReadElementContentAsLong();
						break;
					case "query":
						ReadQuery(xml);
						break;
					case "timer":
						TimingData = new TimerData(xml.ReadSubtree(), this);
						break;
				}
			}
		}
		void ReadQuery(XmlReader xml)
		{
			if (!xml.MoveToAttribute("type"))
				throw new XmlException("No type attribute for query!");
			switch (xml.Value)
			{
				case "source":
					query = QueryType.SOURCE;
					break;
				/*
			case "module":
				query = QueryType.MODULE;
				ModuleQuery = new ModuleQuery(this);
				break;
				*/
			}
		}
		public void WriteXML(XmlWriter xml)
		{
			xml.WriteStartElement("server");
			xml.WriteComment(id.ToString(" ID: #0 "));
			xml.WriteElementString("name", name);
			xml.WriteElementString("executable", executable);
			xml.WriteElementString("parameters", parameters);
			xml.WriteElementString("affinity", ProcessAffinity.ToString());
			xml.WriteElementString("ip", ipaddr);
			xml.WriteElementString("port", port.ToString());
			WriteQuery(xml);
			TimingData.WriteXML(xml);
			xml.WriteEndElement();
		}
		void WriteQuery(XmlWriter xml)
		{
			xml.WriteStartElement("query");
			xml.WriteStartAttribute("type");
			switch (query)
			{
				case QueryType.SOURCE:
					xml.WriteString("source");
					break;
					/*
				case QueryType.MODULE:
					xml.WriteString("module");
					break;
					*/
				case QueryType.NONE:
					xml.WriteString("none");
					break;
			}
			xml.WriteEndAttribute();
			xml.WriteEndElement();
		}
		public struct TimerData
		{
			ushort strikes;
			public ushort Strikes
			{
				get { return strikes; }
				set
				{
					if (value == 0)
						throw new ArgumentException("Argument cannot be 0!", "Strikes");
					strikes = value;
				}
			}
			ushort interval;
			public ushort Interval
			{
				get { return interval; }
				set
				{
					if (value == 0)
						throw new ArgumentException("Argument cannot be 0!", "Interval");
					if (value == interval)
						return;
					interval = value;
					if (parent != null && parent.ProcLock != null && parent.Timer != null)
						lock (parent.ProcLock)
							parent.Timer.Interval = value;
				}
			}
			ushort startup;
			public ushort Startup
			{
				get { return startup; }
				set
				{
					if (value == 0)
						throw new ArgumentException("Argument cannot be 0!", "Startup");
					startup = value;
				}
			}
			ushort shutdown;
			public ushort Shutdown
			{
				get { return shutdown; }
				set { shutdown = value; }
			}
			ServerData parent;
			public ServerData Parent
			{
				get
				{
					return parent;
				}
				internal set
				{
					parent = value;
				}
			}
			public TimerData(XmlReader xml, ServerData parent)
			{
				this.parent = parent;
				int? strikes = null, interval = null, startup = null, shutdown = null;
				while (xml.Read())
				{
					if (xml.NodeType != XmlNodeType.Element)
						continue;
					switch (xml.Name)
					{
						case "strikes":
							strikes = xml.ReadElementContentAsInt();
							break;
						case "interval":
							interval = xml.ReadElementContentAsInt();
							break;
						case "startup":
							startup = xml.ReadElementContentAsInt();
							break;
						case "shutdown":
							shutdown = xml.ReadElementContentAsInt();
							break;
					}
				}

				not_null(strikes, "strikes");
				not_null(startup, "startup");
				not_null(interval, "interval");
				not_null(shutdown, "shutdown");

				validate_value(ref strikes);
				validate_value(ref startup);
				validate_value(ref interval);
				if (shutdown < 0)
					shutdown = 0;

				this.strikes = (ushort) strikes;
				this.startup = (ushort) startup;
				this.interval = (ushort) interval;
				this.shutdown = (ushort) shutdown;
			}
			internal void WriteXML(XmlWriter xml)
			{
				xml.WriteStartElement("timer");
					xml.WriteElementString("startup", startup.ToString());
					xml.WriteElementString("strikes", strikes.ToString());
					xml.WriteElementString("interval", interval.ToString());
					xml.WriteElementString("shutdown", shutdown.ToString());
				xml.WriteEndElement();
			}
			private static void validate_value(ref int? v)
			{
				if (v <= 0)
					v = 1;
			}
			private static void not_null(int? thing, string what)
			{
				if (thing == null)
					throw new XmlException("Missing '" + what + "' field in <timer> tag!");
			}
		}
	}
}