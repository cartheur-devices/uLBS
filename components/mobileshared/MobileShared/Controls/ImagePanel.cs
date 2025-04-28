using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using System;

namespace MobileShared.Controls
{
    /// <summary>
    /// Summary description for ImagePanel.
    /// </summary>
    public class ImagePanel : Panel
    {
        private Color _ColorOverlay;
        public Color ColorOverlay
        {
            set { _ColorOverlay = value; }
            get { return _ColorOverlay; }
        }

        private Bitmap _Offscreen;
        private Color _TransParentColor;

        private Image image;
        public Image Image
        {
            set { image = value; }
            get { return image; }
        }

        public ImagePanel()
        {
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            //BuildOffscreen();

            try
            {
                Rectangle dest = new Rectangle(0, 0, this.Width, this.Height);
                Rectangle source = new Rectangle(0, 0, image.Width, image.Height);
                //Draw from the memory bitmap
                e.Graphics.DrawImage(image, dest, source, GraphicsUnit.Pixel);
                DoColorOverlay(Color.White); 
            }
            catch (Exception ex)
            {   
                
            }
        }

        protected override void OnResize(System.EventArgs e)
        {
            base.OnResize(e);
            this.Invalidate();
//#if POCKETPC
//            this.Invalidate();
//#endif
           // BuildOffscreen();
        }

        private void BuildOffscreen()
        {
            Graphics gxOff;	   // Offscreen graphics
            Rectangle destRect;

            if (_Offscreen == null) //Bitmap for doublebuffering
            {
                _Offscreen = new Bitmap(ClientSize.Width, ClientSize.Height);
                gxOff = Graphics.FromImage(_Offscreen);

                if (Parent is ImagePanel)
                {
                    ((ImagePanel)Parent).PaintControl(this, gxOff);
                }
                else if (image != null)
                {
                    if (_TransParentColor.IsEmpty)
                        _TransParentColor = BackgroundImageColor(image);

                    ImageAttributes imageAttr = new ImageAttributes();
                    imageAttr.SetColorKey(_TransParentColor, _TransParentColor);

                    //Draw image
                    gxOff.Clear(_TransParentColor);
                    destRect = new Rectangle(0, this.Height - image.Height, image.Width, image.Height);
                    gxOff.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, imageAttr);

                    // Color Overlay
                    if (!_ColorOverlay.IsEmpty)
                    {
                        DoColorOverlay(_ColorOverlay);
                    }
                }
            }
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
           // BuildOffscreen();

            //Draw from the memory bitmap
            //e.Graphics.DrawImage(_Offscreen, 0, 0);
        }

        /// <summary>
        /// Clear buffer bitmap and redraw control.
        /// </summary>
        public void ReInitialisePaint()
        {
            _Offscreen = null;
            BuildOffscreen();
        }

        public void PaintControl(Control ctrl, Graphics g)
        {
            Rectangle destRect;
            destRect = new Rectangle(0, 0, ctrl.Width, ctrl.Height);

            BuildOffscreen();

            ImageAttributes imageAttr = new ImageAttributes();
            if (_Offscreen != null)
                g.DrawImage(_Offscreen, destRect, ctrl.Bounds.X, ctrl.Bounds.Y, ctrl.Bounds.Width, ctrl.Bounds.Height, GraphicsUnit.Pixel, imageAttr);
        }

        private void DoColorOverlay(Color cover)
        {
            byte R, G, B;
            //int cin, cshift = cover.ToArgb();	//(int)0x0000FF;			
            int width = ClientSize.Width;
            int height = ClientSize.Height;
            int x, y = 0;
            Color temp = Color.Red;

            R = cover.R;
            G = cover.G;
            B = cover.B;

            for (y = 0; y < height; y += 2)
                for (x = (y & 1); x < width; x += 2)
                //for( x = 0; x < width; ++x )
                {
                    temp = _Offscreen.GetPixel(x, y);
                    _Offscreen.SetPixel(x, y,
                        Color.FromArgb(
                            (temp.R + R) >> 1,
                            (temp.G + G) >> 1,
                            (temp.B + B) >> 1));
                }
        }

        private Color BackgroundImageColor(Image image)
        {
            Bitmap bmp = new Bitmap(image);
            return bmp.GetPixel(0, 0);
        }
    }
}
