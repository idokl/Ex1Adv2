using System;
using System.Net.Sockets;
using MazeLib;

//credit to the example from: https://msdn.microsoft.com/en-us/library/aa645739(v=vs.71).aspx

namespace Ex1.Model
{
    // A delegate type for hooking up change notifications.
    public delegate void ChangedEventHandler(object sender, EventArgs e);
    public delegate void ClienetPlayedEventHandler(/*TcpClient client,*/ Direction direction);
    //public delegate void HostDirectionChanged(TcpClient client, Direction direction);
    
    internal class MultiPlayerDS
    {
        public string NameOfGame { get; }
        public bool AvailableToJoin { get; set; }
        public bool Closed { get; private set; }
        public TcpClient HostClient { get; }
        public TcpClient GuestClient { get; set; }
        public Maze MazeInit;//{ get; set; }
        private Direction guestCurrentDirection;
        private Direction hostCurrentDirection;

        public Direction HostCurrentDirection
        {
            get { return hostCurrentDirection; }
            set
            {
                hostCurrentDirection = value;
                HostPlayActionOccurd?.Invoke(/*guest,*/ hostCurrentDirection);
            }
        }

        public Direction GuestCurrentDirection
        {
            get { return guestCurrentDirection; }
            set
            {
                guestCurrentDirection = value;
                GuestPlayedEvent?.Invoke(/*host,*/ guestCurrentDirection);
            }
        }
        
        // An events that clients can use to be notified whenever the MultiPlayerDS.Closed change.
        public event ChangedEventHandler SomebodyClosedTheGameEvent;
        public event ClienetPlayedEventHandler GuestPlayedEvent;
        public event ClienetPlayedEventHandler HostPlayActionOccurd;

        public MultiPlayerDS(TcpClient hostClient, string nameOfGame, Maze maze)
        {
            HostClient = hostClient;
            NameOfGame = nameOfGame;
            GuestClient = null;
            MazeInit = maze;
            AvailableToJoin = true;
            Closed = false;
        }

        //private void OnChangeOfClientDirection(/*TcpClient host,*/ Direction direction)
        //{
            //ClientPlayedEvent?.Invoke(/*host,*/ direction);
        //}

        
        //private void OnChangeOfHostDirection(/*TcpClient guest,*/Direction direction)
        //{
            //HostPlayActionOccurd?.Invoke(/*guest,*/ direction);
        //}
        
        public void Close()
        {
            Closed = true;
            SomebodyClosedTheGameEvent?.Invoke(this, EventArgs.Empty);
        }
    }
}