using common;

namespace wServer.networking.packets.outgoing
{
    public class LootNotify : OutgoingMessage
    {
        public byte BagType { get; set; }

        public override PacketId ID => PacketId.LOOT_NOTIFY;
        public override Packet CreateInstance() { return new LootNotify(); }

        protected override void Read(NReader rdr)
        {
            BagType = rdr.ReadByte();
        }

        protected override void Write(NWriter wtr)
        {
            wtr.Write(BagType);
        }
    }
}
