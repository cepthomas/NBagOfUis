using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using Ephemera.NBagOfTricks;


namespace Ephemera.NBagOfUis
{
    /// <summary>
    /// Slider control.
    /// </summary>
    public class Slider : UserControl
    {
        // TODO log/db for volume use.

        #region Fields
        /// <summary>If user resets. This is the first value assigned to Value.</summary>
        double _resetVal = double.NaN;

        /// <summary>The brush.</summary>
        readonly SolidBrush _brush = new(Color.White);

        /// <summary>For drawing text.</summary>
        readonly StringFormat _format = new() { LineAlignment = StringAlignment.Center, Alignment = StringAlignment.Center };
        #endregion

        #region Backing fields
        double _resolution = 0.1;
        double _minimum = 0.0;
        double _maximum = 10.0;
        double _value = 5.0;
        #endregion

        #region Properties
        /// <summary>Optional label.</summary>
        public string Label { get; set; } = "";

        /// <summary>For styling.</summary>
        public Color DrawColor { get { return _brush.Color; } set { _brush.Color = value; } }

        /// <summary>Fader orientation</summary>
        public Orientation Orientation { get; set; } = Orientation.Horizontal;

        /// <summary>Per step resolution of this slider.</summary>
        public double Resolution
        {
            get { return _resolution; }
            set { _resolution = value; Rescale(); }
        }

        /// <summary>Minimum Value of the slider.</summary>
        public double Minimum
        {
            get { return _minimum; }
            set { _minimum = value; Rescale(); }
        }

        /// <summary>Maximum Value of the slider.</summary>
        public double Maximum
        {
            get { return _maximum; }
            set { _maximum = value; Rescale(); }
        }

        /// <summary>The current value of the slider.</summary>
        public double Value
        {
            get { return _value; }
            set { _value = MathUtils.Constrain(value, _minimum, _maximum, _resolution); if (double.IsNaN(_resetVal)) _resetVal = value; Invalidate(); }
        }
        #endregion

        #region Events
        /// <summary>Slider value changed event.</summary>
        public event EventHandler? ValueChanged;
        #endregion

        #region Lifecycle
        /// <summary>
        /// Creates a new Slider control.
        /// </summary>
        public Slider()
        {
            SetStyle(ControlStyles.DoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
            InitializeComponent();
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

        /// <summary>
        /// From Designer.
        /// </summary>
        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // Slider
            // 
            this.Name = "Slider";
            this.Size = new System.Drawing.Size(150, 30);
            this.ResumeLayout(false);
        }
        #endregion

        #region Calcs
        /// <summary>
        /// If min or max or resolution changed by client.
        /// </summary>
        void Rescale()
        {
            _minimum = MathUtils.Constrain(_minimum, _minimum, _maximum, _resolution);
            _maximum = MathUtils.Constrain(_maximum, _minimum, _maximum, _resolution);
            _value = MathUtils.Constrain(_value, _minimum, _maximum, _resolution);
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
            string sval = _value.ToString("#0." + new string('0', MathUtils.DecPlaces(_resolution)));
            if (Label != "")
            {
                Rectangle r = new(ClientRectangle.X, ClientRectangle.Y, ClientRectangle.Width, ClientRectangle.Height / 2);
                pe.Graphics.DrawString(Label, Font, Brushes.Black, r, _format);

                r = new(ClientRectangle.X, ClientRectangle.Height / 2, ClientRectangle.Width, ClientRectangle.Height / 2);
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
                    Value = _resetVal;
                    break;
            }

            base.OnMouseDown(e);
        }

        /// <summary>
        /// ommon updater.
        /// </summary>
        /// <param name="e"></param>
        void SetValueFromMouse(MouseEventArgs e)
        {
            double oldval = Value;
            // Calculate the new value.
            double newval = Orientation == Orientation.Horizontal ?
                Minimum + e.X * (Maximum - Minimum) / Width :
                Minimum + (Height - e.Y) * (Maximum - Minimum) / Height;

            // This factors in the resolution.
            Value = newval;
            if(oldval != Value)
            {
                ValueChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Handle the nudge key.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.Control)
            {
                if (e.KeyCode == Keys.Down)
                {
                    Value -= _resolution;
                    ValueChanged?.Invoke(this, EventArgs.Empty);
                }
                else if (e.KeyCode == Keys.Up)
                {
                    Value += _resolution;
                    ValueChanged?.Invoke(this, EventArgs.Empty);
                }
            }

            base.OnKeyDown(e);
        }
        #endregion
    }


    /// <summary>Simple toolstrip container for the slider.</summary>
    [ToolStripItemDesignerAvailability(ToolStripItemDesignerAvailability.ToolStrip | ToolStripItemDesignerAvailability.StatusStrip)]
    public class ToolStripSlider : ToolStripControlHost
    {
        #region Fields
        /// <summary>Contained control.</summary>
        readonly Slider _slider;
        #endregion

        #region Properties mapped to contained control
        /// <summary>Optional label.</summary>
        public string Label { get { return _slider.Label; } set { _slider.Label = value; } }

        /// <summary>For styling.</summary>
        public Color DrawColor { get { return _slider.DrawColor; } set { _slider.DrawColor = value; } }

        /// <summary>Per step resolution of this slider.</summary>
        public double Resolution { get { return _slider.Resolution; } set { _slider.Resolution = value; } }

        /// <summary>Minimum Value of the slider.</summary>
        public double Minimum { get { return _slider.Minimum; } set { _slider.Minimum = value; } }

        /// <summary>Maximum Value of the slider.</summary>
        public double Maximum { get { return _slider.Maximum; } set { _slider.Maximum = value; } }

        /// <summary>The current value of the slider.</summary>
        public double Value { get { return _slider.Value; } set { _slider.Value = value; } }

        /// <summary>Optional border.</summary>
        public BorderStyle BorderStyle { get { return _slider.BorderStyle; } set { _slider.BorderStyle = value; } }
        #endregion

        #region Events
        /// <summary>Slider value changed event.</summary>
        public event EventHandler? ValueChanged;
        #endregion

        /// <summary>
        /// Make one.
        /// </summary>
        public ToolStripSlider() : base(new Slider())
        {
            _slider = (Slider)Control;
            AutoSize = false;
            Width = _slider.Width;
            _slider.Orientation = Orientation.Horizontal;
            _slider.ValueChanged += (_, __) => ValueChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
