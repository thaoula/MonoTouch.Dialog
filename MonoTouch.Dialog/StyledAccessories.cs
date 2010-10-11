using System;
using MonoTouch.UIKit;
using MonoTouch.CoreGraphics;
using System.Drawing;

namespace MonoTouch.Dialog
{
	public enum AccessoryType
	{
		Disclosure,
		DetailDisclosure
	}
	
	public abstract class CellAccessoryBase: UIControl
	{
		private UIColor _highlightColor;
		private UIColor _color;
		
		public CellAccessoryBase ()
		{
			Frame = new RectangleF(0,0, 16, 16);
			this.BackgroundColor = UIColor.Clear;
		}
		
		public UIColor Color {
			get 
			{ 
				return _color; 
			}
			set
			{
				if (value == null)
					value = UIColor.Clear;
				
				_color = value;
				SetNeedsDisplay();
			}
		}
		
		public UIColor HighlightColor {
			get { return _highlightColor; }
			set
			{
				if (value == null)
					value = UIColor.Clear;
				
				_highlightColor = value;
				
				SetNeedsDisplay();
			}
		}
		
		public override bool Highlighted
		{
			get
			{
				return base.Highlighted;
			}
			set
			{
				base.Highlighted = value;
				SetNeedsDisplay();
			}
		}
		
	}
	
	public class CellDisclosureAccessory: CellAccessoryBase
	{
		public CellDisclosureAccessory ()
		{
			Frame = new RectangleF(0,0, 16, 16);
			BackgroundColor = UIColor.Clear;
			
			Color = UIColor.Gray;
			HighlightColor = UIColor.White;
		}
		
		public override void Draw (RectangleF rect)
		{
			var x = this.Bounds.Right - 5.0f;
			var y = this.Bounds.Height / 2;
			
			const float R = 4.5f;
			
			var ctx = UIGraphics.GetCurrentContext();
			if (ctx == null) return;
			
			UIColor accessoryColor = Highlighted ? this.HighlightColor : this.Color;
			
			ctx.MoveTo(x-R, y-R);
			ctx.AddLineToPoint(x,y);
			ctx.AddLineToPoint(x-R, y+R);
			
			ctx.SetLineCap(CGLineCap.Square);
			ctx.SetLineJoin(CGLineJoin.Miter);
			ctx.SetLineWidth(3);
			
			accessoryColor.SetStroke();
 			ctx.StrokePath();
		}
	}
	
	public class CellDetailDisclosureAccessory: CellDisclosureAccessory
	{
		private UIColor _elipseHighlightColor;
		private UIColor _elipseColor;
		
		public event EventHandler Clicked;
		
		public CellDetailDisclosureAccessory ()
		{
			Frame = new RectangleF(0,0, 16, 16);
			BackgroundColor = UIColor.Clear;
			
			Color = UIColor.Gray;
			HighlightColor = UIColor.Gray;
			
			ElipseColor = UIColor.White;
			ElipseHighlightColor = UIColor.White;
			
			AccessoryBlendMode = CGBlendMode.XOR;
		}
		
		public UIColor ElipseColor {
			get 
			{ 
				return _elipseColor; 
			}
			set
			{
				if (value == null)
					value = UIColor.Clear;
				
				_elipseColor = value;
				
				SetNeedsDisplay();
			}
		}
		
		public UIColor ElipseHighlightColor {
			get { return _elipseHighlightColor; }
			set
			{
				if (value == null)
					value = UIColor.Clear;
				
				_elipseHighlightColor = value;
				
				
				SetNeedsDisplay();
			}
		}
		
		public CGBlendMode AccessoryBlendMode {
			get;
			set;
		}
		
		public override void Draw (RectangleF rect)
		{
			var ctx = UIGraphics.GetCurrentContext();
			if (ctx == null) return;
			
			var elipseColor = Highlighted ? this.ElipseHighlightColor : this.ElipseColor;
			
			ctx.AddEllipseInRect(this.Bounds);
			
			elipseColor.SetFill();
			ctx.FillPath();
			
			//ctx.SetBlendMode(AccessoryBlendMode);

			base.Draw (rect);
		}
		
		public override void TouchesBegan (Foundation.NSSet touches, UIEvent evt)
		{
			base.TouchesBegan (touches, evt);
			RaiseOnClick();
		}
		
		protected void RaiseOnClick()
		{
			if (Clicked != null)
				Clicked(this, EventArgs.Empty);
		}
		
	}
}

