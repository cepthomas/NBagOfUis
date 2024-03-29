﻿using System;
using System.Windows.Forms;


namespace Ephemera.NBagOfUis
{
    /// <summary>Class that provides a better wait cursor. Clients should use it with using (new WaitCursor()) { slow code }</summary>
    public sealed class WaitCursor : IDisposable
    {
        /// <summary>Restore original cursor</summary>
        readonly Cursor? m_cursorOld = null;

       /// <summary>Constructor</summary>
        public WaitCursor()
        {
            m_cursorOld = Cursor.Current;
            Cursor.Current = Cursors.WaitCursor;
        }

        /// <summary>Dispose</summary>
        public void Dispose()
        {
            Cursor.Current = m_cursorOld;
        }
    }
}
