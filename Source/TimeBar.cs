﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;


namespace NBagOfUis
{
    /// <summary>The control.</summary>
    public partial class TimeBar : UserControl
    {
        #region Fields
        /// <summary>Total length.</summary>
        TimeSpan _length = new();

        /// <summary>Current time/position.</summary>
        TimeSpan _current = new();

        /// <summary>One marker.</summary>
        TimeSpan _start = new();

        /// <summary>Other marker.</summary>
        TimeSpan _end = new();

        /// <summary>For tracking mouse moves.</summary>
        int _lastXPos = 0;

        /// <summary>Tooltip for mousing.</summary>
        readonly ToolTip _toolTip = new();

        /// <summary>The brush.</summary>
        readonly SolidBrush _brush = new(Color.White);

        /// <summary>The pen.</summary>
        readonly Pen _penMarker = new(Color.Black, 1);

        /// <summary>For drawing text.</summary>
        readonly StringFormat _format = new() { LineAlignment = StringAlignment.Center, Alignment = StringAlignment.Center };

        /// <summary>Constant.</summary>
        static readonly int LARGE_CHANGE = 1000;

        /// <summary>Constant.</summary>
        static readonly int SMALL_CHANGE = 100;

        /// <summary>For viewing purposes.</summary>
        const string TS_FORMAT = @"mm\:ss\.fff";
        #endregion

        #region Properties
        /// <summary>Where we be now.</summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public TimeSpan Current { get { return _current; } set { _current = value; Invalidate(); } }

        /// <summary>Total length.</summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public TimeSpan Length { get { return _length; } set { _length = value; Invalidate(); } }

        /// <summary>One marker.</summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public TimeSpan Start { get { return _start; } set { _start = value; Invalidate(); } }

        /// <summary>Other marker.</summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public TimeSpan End { get { return _end; } set { _end = value; Invalidate(); } }

        /// <summary>Snap to this increment value.</summary>
        public int SnapMsec { get; set; } = 0;

        /// <summary>For styling.</summary>
        public Color ProgressColor { get { return _brush.Color; } set { _brush.Color = value; } }

        /// <summary>For styling.</summary>
        public Color MarkerColor { get { return _penMarker.Color; } set { _penMarker.Color = value; } }

        /// <summary>Big font.</summary>
        public Font FontLarge { get; set; } = new("Microsoft Sans Serif", 20, FontStyle.Regular, GraphicsUnit.Point, 0);

        /// <summary>Baby font.</summary>
        public Font FontSmall { get; set; } = new("Microsoft Sans Serif", 10, FontStyle.Regular, GraphicsUnit.Point, 0);
        #endregion

        #region Events
        /// <summary>Value changed by user.</summary>
        public event EventHandler? CurrentTimeChanged;
        #endregion

        #region Lifecycle
        /// <summary>
        /// Constructor.
        /// </summary>
        public TimeBar()
        {
            SetStyle(ControlStyles.DoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
        }

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _toolTip.Dispose();
                _penMarker.Dispose();
                _brush.Dispose();
                _format.Dispose();
            }
            base.Dispose(disposing);
        }
        #endregion

        #region Public functions
        /// <summary>
        /// Offset current by msec.
        /// </summary>
        /// <param name="msec"></param>
        public void IncrementCurrent(int msec)
        {
            int smsec = DoSnap(msec);
            _current = (smsec > 0) ? _current.Add(new TimeSpan(0, 0, 0, 0, smsec)) : _current.Subtract(new TimeSpan(0, 0, 0, 0, -smsec));

            if (_current > _length)
            {
                _current = _length;
            }

            if (_current < TimeSpan.Zero)
            {
                _current = TimeSpan.Zero;
            }
            else if (_current >= _length)
            {
                _current = _length;
            }
            else if (_end != TimeSpan.Zero && _current >= _end)
            {
                _current = _end;
            }

            Invalidate();
        }
        #endregion

        #region Drawing
        /// <summary>
        /// Draw the slider.
        /// </summary>
        protected override void OnPaint(PaintEventArgs pe)
        {
            // Setup.
            pe.Graphics.Clear(BackColor);

            // Validate times.
            _start = Constrain(_start, TimeSpan.Zero, _length);
            _start = Constrain(_start, TimeSpan.Zero, _end);
            _end = Constrain(_end, TimeSpan.Zero, _length);
            _end = Constrain(_end, _start, _length);
            _current = Constrain(_current, _start, _end);

            if (_end == TimeSpan.Zero && _length != TimeSpan.Zero)
            {
                _end = _length;
            }

            // Draw the bar.
            if (_current < _length)
            {
                int dstart = Scale(_start);
                int dend = _current > _end ? Scale(_end) : Scale(_current);
                pe.Graphics.FillRectangle(_brush, dstart, 0, dend - dstart, Height);
            }

            // Draw start/end markers.
            if (_start != TimeSpan.Zero || _end != _length)
            {
                int mstart = Scale(_start);
                int mend = Scale(_end);
                pe.Graphics.DrawLine(_penMarker, mstart, 0, mstart, Height);
                pe.Graphics.DrawLine(_penMarker, mend, 0, mend, Height);
            }

            // Text.
            _format.Alignment = StringAlignment.Center;
            pe.Graphics.DrawString(_current.ToString(TS_FORMAT), FontLarge, Brushes.Black, ClientRectangle, _format);
            _format.Alignment = StringAlignment.Near;
            pe.Graphics.DrawString(_start.ToString(TS_FORMAT), FontSmall, Brushes.Black, ClientRectangle, _format);
            _format.Alignment = StringAlignment.Far;
            pe.Graphics.DrawString(_end.ToString(TS_FORMAT), FontSmall, Brushes.Black, ClientRectangle, _format);
        }
        #endregion

        #region UI handlers
        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnKeyDown(KeyEventArgs e)
        {
            switch (e.KeyData)
            {
                case Keys.Add:
                case Keys.Up:
                    IncrementCurrent(e.Shift ? SMALL_CHANGE : LARGE_CHANGE);
                    e.Handled = true;
                    break;

                case Keys.Subtract:
                case Keys.Down:
                    IncrementCurrent(e.Shift ? -SMALL_CHANGE : -LARGE_CHANGE);
                    e.Handled = true;
                    break;

                case Keys.Escape:
                    // Reset.
                    _start = TimeSpan.Zero;
                    _end = _length;
                    e.Handled = true;
                    Invalidate();
                    break;
            }

            if(e.Handled)
            {
                Invalidate();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPreviewKeyDown(PreviewKeyDownEventArgs e)
        {
            switch (e.KeyData)
            {
                case Keys.Up:
                case Keys.Down:
                    e.IsInputKey = true;
                    break;
            }
        }

        /// <summary>
        /// Handle mouse position changes.
        /// </summary>
        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                _current = GetTimeFromMouse(e.X);
                CurrentTimeChanged?.Invoke(this, new EventArgs());
            }
            else
            {
                if (e.X != _lastXPos)
                {
                    TimeSpan ts = GetTimeFromMouse(e.X);
                    _toolTip.SetToolTip(this, ts.ToString(TS_FORMAT));
                    _lastXPos = e.X;
                }
            }

            Invalidate();
            base.OnMouseMove(e);
        }

        /// <summary>
        /// Handle dragging.
        /// </summary>
        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (ModifierKeys.HasFlag(Keys.Control))
            {
                _start = GetTimeFromMouse(e.X);
            }
            else if (ModifierKeys.HasFlag(Keys.Alt))
            {
                _end = GetTimeFromMouse(e.X);
            }
            else
            {
                _current = GetTimeFromMouse(e.X);
            }

            CurrentTimeChanged?.Invoke(this, new EventArgs());
            Invalidate();
            base.OnMouseDown(e);
        }
        #endregion

        #region Private functions
        /// <summary>
        /// Convert x pos to TimeSpan.
        /// </summary>
        /// <param name="x"></param>
        TimeSpan GetTimeFromMouse(int x)
        {
            int msec = 0;

            if(_current.TotalMilliseconds < _length.TotalMilliseconds)
            {
                msec = x * (int)_length.TotalMilliseconds / Width;
                msec = InternalHelpers.Constrain(msec, 0, (int)_length.TotalMilliseconds);
                msec = DoSnap(msec);
            }
            return new TimeSpan(0, 0, 0, 0, msec);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="msec"></param>
        /// <returns></returns>
        int DoSnap(int msec)
        {
            int smsec = 0;
            if (SnapMsec > 0)
            {
                smsec = (msec / SnapMsec) * SnapMsec;
                if(SnapMsec > (msec % SnapMsec) / 2)
                {
                    smsec += SnapMsec;
                }
            }

            return smsec;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="val"></param>
        /// <param name="lower"></param>
        /// <param name="upper"></param>
        /// <returns></returns>
        TimeSpan Constrain(TimeSpan val, TimeSpan lower, TimeSpan upper)
        {
            return TimeSpan.FromMilliseconds(InternalHelpers.Constrain(val.TotalMilliseconds, lower.TotalMilliseconds, upper.TotalMilliseconds));
        }

        /// <summary>
        /// Map from time to UI pixels.
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public int Scale(TimeSpan val)
        {
            return (int)(val.TotalMilliseconds * Width / _length.TotalMilliseconds);
        }
        #endregion
    }
}
