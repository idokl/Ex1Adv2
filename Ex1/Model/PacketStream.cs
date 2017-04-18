namespace Ex1.Model
{
    class PacketStream
    {
        public bool MultiPlayer { get; set; } = false;
        public MultiPlayerDS MultiPlayerDs { get; set; }
        public string StringStream { get; set; }
    }
}
