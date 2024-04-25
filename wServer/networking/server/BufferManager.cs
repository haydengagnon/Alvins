using System.Collections.Generic;
using System.Net.Sockets;

namespace wServer.networking.server
{
    /// <summary>
    /// This class creates a single large buffer which can be divided up
    /// and assigned to SocketAsyncEventArgs objects for use with each
    /// socket I/O operation.
    ///
    /// This enables buffers to be easily reused and guards against
    /// fragmenting heap memory.
    /// </summary>
    public sealed class BufferManager
    {
        /// <summary>
        /// The total number of bytes controlled by the buffer pool.
        /// </summary>
        private readonly int totalBytesInBufferBlock;

        private readonly Stack<int> freeIndexPool;

        /// <summary>
        /// This buffer is a byte array which the Windows TCP buffer can copy its data to.
        /// </summary>
        private readonly int bufferBytesAllocatedForEachSaea;

        /// <summary>
        /// Byte array maintained by the Buffer Manager.
        /// </summary>
        private byte[] bufferBlock;

        private int currentIndex;

        public BufferManager(int totalBytes, int totalBufferBytesInEachSaeaObject)
        {
            totalBytesInBufferBlock = totalBytes;
            currentIndex = 0;
            bufferBytesAllocatedForEachSaea = totalBufferBytesInEachSaeaObject;
            freeIndexPool = new Stack<int>();
        }

        /// <summary>
        /// Allocates buffer space used by the buffer pool, creating one large
        /// buffer block.
        /// </summary>
        public void InitBuffer() => bufferBlock = new byte[totalBytesInBufferBlock];

        /// <summary>
        /// Divide that one large buffer block out to each SocketAsyncEventArg object.
        /// Assign a buffer space from the buffer block to the specified
        /// SocketAsyncEventArgs object.
        /// </summary>
        /// <param name="args"></param>
        /// <returns>Returns true if the buffer was successfully set.</returns>
        public bool SetBuffer(SocketAsyncEventArgs args)
        {
            // This if-statement is only true if you have called the FreeBuffer
            // method previously, which would put an offset for a buffer space
            // back into this stack.
            if (freeIndexPool.Count > 0)
                args.SetBuffer(bufferBlock, freeIndexPool.Pop(), bufferBytesAllocatedForEachSaea);
            else
            {
                // Inside this else-statement is the code that is used to set the
                // buffer for each SAEA object when the pool of SAEA objects is built
                // in the Init method.
                if ((totalBytesInBufferBlock - bufferBytesAllocatedForEachSaea) < currentIndex)
                    return false;

                args.SetBuffer(bufferBlock, currentIndex, bufferBytesAllocatedForEachSaea);
                currentIndex += bufferBytesAllocatedForEachSaea;
            }

            return true;
        }

        /// <summary>
        /// Removes the buffer from a SocketAsyncEventArg object. This frees the
        /// buffer back to the buffer pool. Try NOT to use the FreeBuffer method,
        /// unless you need to destroy the SAEA object, or maybe in the case
        /// of some exception handling. Instead, on the server
        /// keep the same buffer space assigned to one SAEA object for the duration of
        /// this app's running.
        /// </summary>
        /// <param name="args"></param>
        public void FreeBuffer(SocketAsyncEventArgs args)
        {
            freeIndexPool.Push(args.Offset);
            args.SetBuffer(null, 0, 0);
        }
    }
}
