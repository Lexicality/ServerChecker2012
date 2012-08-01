using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Xml;
using System.IO;

namespace ServerChecker2012
{
	static class Program
	{
		public static MainWindow MainWindow;
		// For adding servers later
		public static ushort LastServerID = 0;
		static string settingsfile;
		static List<ServerData> servers = new List<ServerData>();
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);

			MainWindow = new MainWindow();

			/*
			 * This should by all rights be AppData, but from experience I have found that
			 *  server hosters prefer things to be easy rather than correct, and it's
			 *  less fuss to go to Documents than it is to go to AppData/Roaming/ServerChecker2012.
			 * Thus, we store the settings file in My Documents so it can be backed up / edited
			 *  without dealing with hidden folders.
			 */
			settingsfile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "ServerChecker.xml");

			ushort id = 0;

			if (File.Exists(settingsfile))
			{
				using (FileStream file = new FileStream(settingsfile, FileMode.Open))
				using (XmlReader xml = XmlReader.Create(file))
				{
					// TODO: Some kind of DTD might be nice.
					xml.ReadStartElement("servers");
					while (xml.Read())
					{
						if (xml.NodeType != XmlNodeType.Element)
							continue;
						try
						{
							if (xml.Name != "server")
							{
								PushError("Corrupted settings file! Unexpected tag '" + xml.Name + "' on line " + ((IXmlLineInfo) xml).LineNumber + ".", "Settings File");
								Application.Exit();
								return;
							}
							++id;
							servers.Add(new ServerData(id, xml.ReadSubtree()));
						}
						catch (Exception e)
						{
							// TODO: Do something to prevent this server being removed the next save
							PushError("Could not decode server #" + id + ": " + e.Message, "Settings file");
							// Carry on anyway!
						}
					}
				}
			}
			LastServerID = id;
			MainWindow.LoadServers(ref servers);

			Application.Run(MainWindow);
		}
		public static void SaveData()
		{
			XmlWriterSettings xmlsettings = new XmlWriterSettings();
			// Preferably, I would like this to be human readable.
			xmlsettings.Indent = true;
			using (FileStream file = new FileStream(settingsfile, FileMode.Create))
			using (XmlWriter xml = XmlWriter.Create(file, xmlsettings))
			{
				xml.WriteStartDocument(true);
				xml.WriteStartElement("servers");
				foreach (ServerData server in servers)
				{
					server.WriteXML(xml);
				}
				xml.WriteEndElement();
				xml.WriteEndDocument();
				}

		}
		public static void PushError(string error, string from)
		{
			MessageBox.Show(MainWindow, error, from + " Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
			// TODO: Logging
		}
		public static void PushInfo(string info, string from)
		{
			// TODO: Logging
		}
	}
}
