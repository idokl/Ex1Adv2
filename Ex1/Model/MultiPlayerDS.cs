using System;
using System.Net.Sockets;
using MazeLib;

//credit to the example from: https://msdn.microsoft.com/en-us/library/aa645739(v=vs.71).aspx

namespace Ex1.Model
{
    // A delegate type for hooking up change notifications.
    public delegate void ChangedEventHandler(object sender, EventArgs e);

    public delegate void HostDirectionChanged(TcpClient client, Direction direction);

    public delegate void GuestDirectionChanged(TcpClient client, Direction direction);

    internal class MultiPlayerDS
    {
        private Direction guestCurrentDirection;
        private Direction hostCurrentDirection;

        public MultiPlayerDS(TcpClient startGameClient, string nameOfGame, Maze maze)
        {
            HostClient = startGameClient;
            NameOfGame = nameOfGame;
            GuestClient = null;
            MazeInit = maze;
            IsAvailable = true;
            Closed = false;
        }


        public TcpClient HostClient { get; set; }
        public TcpClient GuestClient { get; set; }
        public string NameOfGame { get; set; }
        public Maze MazeInit { get; set; }

        public Direction HostCurrentDirection
        {
            get { return hostCurrentDirection; }
            set
            {
                hostCurrentDirection = value;
                OnChangeOfHostDirection(GuestClient, HostCurrentDirection);
            }
        }

        public Direction GuestCurrentDirection
        {
            get { return guestCurrentDirection; }
            set
            {
                guestCurrentDirection = value;
                OnChangeOfGuestDirection(HostClient, GuestCurrentDirection);
            }
        }

        public bool IsAvailable { get; set; }
        public bool Closed { get; private set; }

        // An event that clients can use to be notified whenever the MultiPlayerDS.IsAvilble change.
        public event ChangedEventHandler IsAvailableChanged;

        // An event that clients can use to be notified whenever the MultiPlayerDS.Closed change.
        public event ChangedEventHandler SomebodyClosedTheGame;

        public event HostDirectionChanged HostPlayActionOccurd;
        public event GuestDirectionChanged GuestPlayActionOccurd;

        // Invoke the Changed event; called whenever list changes
        private void OnChangedOfIsAvailable(EventArgs e)
        {
            IsAvailableChanged?.Invoke(this, e);
        }

        private void OnChangedOfClosed(EventArgs e)
        {
            SomebodyClosedTheGame?.Invoke(this, e);
        }

        private void OnChangeOfGuestDirection(TcpClient host, Direction direction)
        {
            GuestPlayActionOccurd?.Invoke(host, direction);
        }

        private void OnChangeOfHostDirection(TcpClient guest, Direction direction)
        {
            HostPlayActionOccurd?.Invoke(guest, direction);
        }

        public void Close()
        {
            OnChangedOfClosed(EventArgs.Empty);
            Closed = true;
        }
    }
}


/*
namespace MyCollections
{
    using System.Collections;

    // A delegate type for hooking up change notifications.
    public delegate void ChangedEventHandler(object sender, EventArgs e);

    // A class that works just like ArrayList, but sends event
    // notifications whenever the list changes.
    public class ListWithChangedEvent : ArrayList
    {
        // An event that clients can use to be notified whenever the
        // elements of the list change.
        public event ChangedEventHandler Changed;

        // Invoke the Changed event; called whenever list changes
        protected virtual void OnChanged(EventArgs e)
        {
            if (Changed != null)
                Changed(this, e);
        }

        // Override some of the methods that can change the list;
        // invoke event after each
        public override int Add(object value)
        {
            int i = base.Add(value);
            OnChanged(EventArgs.Empty);
            return i;
        }

        public override void Clear()
        {
            base.Clear();
            OnChanged(EventArgs.Empty);
        }

        public override object this[int index]
        {
            set
            {
                base[index] = value;
                OnChanged(EventArgs.Empty);
            }
        }
    }
}

namespace TestEvents
{
    using MyCollections;

    class EventListener
    {
        private ListWithChangedEvent List;

        public EventListener(ListWithChangedEvent list)
        {
            List = list;
            // Add "ListChanged" to the Changed event on "List".
            List.Changed += new ChangedEventHandler(ListChanged);
        }

        // This will be called whenever the list changes.
        private void ListChanged(object sender, EventArgs e)
        {
            Console.WriteLine("This is called when the event fires.");
        }

        public void Detach()
        {
            // Detach the event and delete the list
            List.Changed -= new ChangedEventHandler(ListChanged);
            List = null;
        }
    }

    class Test
    {
        // Test the ListWithChangedEvent class.
        public static void Main()
        {
            // Create a new list.
            ListWithChangedEvent list = new ListWithChangedEvent();

            // Create a class that listens to the list's change event.
            EventListener listener = new EventListener(list);

            // Add and remove items from the list.
            list.Add("item 1");
            list.Clear();
            listener.Detach();
        }
    }
}
*/