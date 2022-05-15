using System;
using System.Windows.Forms;


namespace NBagOfUis
{
    /// <summary>Class that provides a better wait cursor. Clients should use it with using (new WaitCursor()) { slow code }</summary>
    public class WaitCursor : IDisposable
    {
        /// <summary>Restore original cursor</summary>
        readonly Cursor? m_cursorOld = null;

        /// <summary>For metrics</summary>
        readonly DateTime _start;

        /// <summary>Constructor</summary>
        public WaitCursor()
        {
            m_cursorOld = Cursor.Current;
            Cursor.Current = Cursors.WaitCursor;
            _start = DateTime.Now;
        }

        /// <summary>Dispose</summary>
        public void Dispose()
        {
            Cursor.Current = m_cursorOld;
            TimeSpan dur = DateTime.Now - _start;
        }
    }
}
