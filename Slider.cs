using System;
using System.Drawing;
using System.Windows.Forms;
using NBagOfTricks;


namespace NBagOfUis
{
    /// <summary>
    /// Slider control.
    /// </summary>
    public partial class Slider : UserControl
    {
        #region Fields
        /// <summary> </summary>
        double _value = 0.0;

        /// <summary> </summary>
        double _resetVal = 0.0;

        /// <summary>The brush.</summary>
        readonly SolidBrush _brush = new SolidBrush(Color.White);

        /// <summary>For drawing text.</summary>
        StringFormat _format = new StringFormat() { LineAlignment = StringAlignment.Center, Alignment = StringAlignment.Center };
        #endregion

        #region Properties
        /// <summary>Optional label.</summary>
        public string Label { get; set; } = "";

        /// <summary>For styling.</summary>
        public Color DrawColor { get { return _brush.Color; } set { _brush.Color = value; } }

        /// <summary>Number of decimal places to display.</summary>
        public int DecPlaces { get; set; } = 1;

        /// <summary>Fader orientation</summary>
        public Orientation Orientation { get; set; } = Orientation.Horizontal;

        /// <summary>Maximum value of this slider.</summary>
        public double Maximum { get; set; } = 1.0;

        /// <summary>Minimum value of this slider.</summary>
        public double Minimum { get; set; } = 0.0;

        /// <summary>Reset value of this slider.</summary>
        public double ResetValue
        {
            get { return _resetVal; }
            set { _resetVal = Math.Round(MathUtils.Constrain(value, Minimum, Maximum), DecPlaces); }
        }

        /// <summary>The value for this slider.</summary>
        public double Value
        {
            get { return _value; }
            set
            {
                _value = Math.Round(MathUtils.Constrain(value, Minimum, Maximum), DecPlaces);
                ValueChanged?.Invoke(this, EventArgs.Empty);
                Invalidate();
            }
        }
        #endregion

        #region Events
        /// <summary>
        /// Slider value changed event.
        /// </summary>
        public event EventHandler ValueChanged;
        #endregion

        #region Lifecycle
        /// <summary>
        /// Creates a new Slider control.
        /// </summary>
        public Slider()
        {
            SetStyle(ControlStyles.DoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
            Load += Slider_Load;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Slider_Load(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _brush.Dispose();
                _format.Dispose();
            }
            base.Dispose(disposing);
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

            // Draw the bar.
            if (Orientation == Orientation.Horizontal)
            {
                double x = (_value - Minimum) / (Maximum - Minimum);
                pe.Graphics.FillRectangle(_brush, ClientRectangle.Left, ClientRectangle.Top, ClientRectangle.Width * (float)x, ClientRectangle.Height);
            }
            else
            {
                double y = 1.0 - (_value - Minimum) / (Maximum - Minimum);
                pe.Graphics.FillRectangle(_brush, ClientRectangle.Left, ClientRectangle.Height * (float)y, ClientRectangle.Width, ClientRectangle.Bottom);
            }

            // Text.
            string sval = _value.ToString("#0." + new string('0', DecPlaces));
            if (Label != "")
            {
                Rectangle r = new Rectangle(ClientRectangle.X, ClientRectangle.Y, ClientRectangle.Width, ClientRectangle.Height / 2);
                pe.Graphics.DrawString(Label, Font, Brushes.Black, r, _format);

                r = new Rectangle(ClientRectangle.X, ClientRectangle.Height / 2, ClientRectangle.Width, ClientRectangle.Height / 2);
                pe.Graphics.DrawString(sval, Font, Brushes.Black, r, _format);
            }
            else
            {
                pe.Graphics.DrawString(sval, Font, Brushes.Black, ClientRectangle, _format);
            }
        }
        #endregion

        #region Mouse events
        /// <summary>
        /// Handle dragging.
        /// </summary>
        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                SetValueFromMouse(e);
            }
            base.OnMouseMove(e);
        }

        /// <summary>
        /// Handle dragging.
        /// </summary>
        protected override void OnMouseDown(MouseEventArgs e)
        {
            switch(e.Button)
            {
                case MouseButtons.Left:
                    SetValueFromMouse(e);
                    break;

                case MouseButtons.Right:
                    Value = ResetValue;
                    break;
            }

            base.OnMouseDown(e);
        }

        /// <summary>
        /// ommon updater.
        /// </summary>
        /// <param name="e"></param>
        private void SetValueFromMouse(MouseEventArgs e)
        {
            double newval = Orientation == Orientation.Horizontal ?
                Minimum + e.X * (Maximum - Minimum) / Width :
                Minimum + (Height - e.Y) * (Maximum - Minimum) / Height;

            Value = newval;
        }

        /// <summary>
        /// Handle the nudge key.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.Control)
            {
                double incr = Math.Pow(10, -DecPlaces);

                if (e.KeyCode == Keys.Down)
                {
                    Value -= incr;
                }
                else if (e.KeyCode == Keys.Up)
                {
                    Value += incr;
                }
            }

            base.OnKeyDown(e);
        }
        #endregion
    }
}
