using System;
using MonoTouch.UIKit;
using System.Collections.Generic;

namespace MonoTouch.Dialog
{
	public class Style
	{
		public Style () {}
		
		public Style (UIFont font, UIColor background, UIColor textColor, UIColor highlight)
		{
			Font = font;
			BackgroundColor = background;
			TextColor = textColor;
			HighlightColor = highlight;
		}
		
		#region Font
		
		public UIFont Font { get; set; }
				
		#endregion
	
		#region TextAlignment
		
		public UITextAlignment? TextAlignment { get; set; }
		
		#endregion
		
		#region "Color"
		
		public UIColor BackgroundColor { get; set; }
		public UIColor TextColor { get; set; }
		public UIColor HighlightColor { get; set; }
		
		#endregion
		
		#region "General"
		
		public UITableViewCellAccessory? Accessory { get; set; }
		
		#endregion
		
		public virtual Style Inherit(Style inherited)
		{
			return new Style()
			{
				BackgroundColor = this.BackgroundColor ?? inherited.BackgroundColor,
				HighlightColor = this.HighlightColor ?? inherited.HighlightColor,
				TextColor = this.TextColor ?? inherited.TextColor,
				TextAlignment = this.TextAlignment.HasValue ? this.TextAlignment.Value : inherited.TextAlignment,
				Font = this.Font ?? inherited.Font,
				Accessory = this.Accessory.HasValue ? this.Accessory.Value : inherited.Accessory
			};
		}
	}
	
	public class ElementStyle
	{
		public ElementStyle ()
		{
			TextLabel = new Style();
			DetailLabel = new Style();
			Cell = new Style();
			AccessoryCheckmark = new Style();
			DetailedAccessoryElipse = new Style();
		}
		
		public Style TextLabel
		{
			get;
			set;
		}
		
		public Style DetailLabel
		{
			get;
			set;
		}
		
		public Style Cell
		{
			get;
			set;
		}
		
		public Style AccessoryCheckmark 
		{
			get;
			set;
		}
		
		public Style DetailedAccessoryElipse {
			get;
			set;
		}
		
		public virtual ElementStyle Inherit(ElementStyle inherited)
		{
			var style = new ElementStyle();
			style.Cell = Cell.Inherit(inherited.Cell);
			style.DetailLabel = DetailLabel.Inherit(inherited.DetailLabel);
			style.TextLabel = TextLabel.Inherit(inherited.TextLabel);
			style.AccessoryCheckmark = AccessoryCheckmark.Inherit(inherited.AccessoryCheckmark);
			style.DetailedAccessoryElipse = DetailedAccessoryElipse.Inherit(inherited.DetailedAccessoryElipse);
			return style;
		}
		
		public virtual void Apply(UITableViewCell cell)
		{
			if (TextLabel != null)
			{
				if (TextLabel.Font != null) cell.TextLabel.Font = TextLabel.Font;
				if (TextLabel.TextColor != null) cell.TextLabel.TextColor = TextLabel.TextColor;
				if (TextLabel.TextAlignment.HasValue) cell.TextLabel.TextAlignment = TextLabel.TextAlignment.Value;
				if (TextLabel.BackgroundColor != null) cell.TextLabel.BackgroundColor = TextLabel.BackgroundColor;
				if (TextLabel.HighlightColor != null) cell.TextLabel.HighlightedTextColor = TextLabel.HighlightColor;
			}
			
			if (DetailLabel != null && cell.DetailTextLabel != null) 
			{
				if (DetailLabel.Font != null) cell.DetailTextLabel.Font = DetailLabel.Font;
				if (DetailLabel.TextColor != null) cell.DetailTextLabel.TextColor = DetailLabel.TextColor;
				if (DetailLabel.TextAlignment.HasValue) cell.DetailTextLabel.TextAlignment = DetailLabel.TextAlignment.Value;
				if (DetailLabel.BackgroundColor != null) cell.DetailTextLabel.BackgroundColor = DetailLabel.BackgroundColor;
				if (DetailLabel.HighlightColor != null) cell.DetailTextLabel.HighlightedTextColor = DetailLabel.HighlightColor;
			}
			
			if (Cell != null)
			{
				if (Cell.BackgroundColor != null) cell.BackgroundColor = Cell.BackgroundColor;
				if (Cell.Accessory.HasValue) cell.Accessory = Cell.Accessory.Value;
			}
			
			var styledCell = cell as StyleElementCell;
			if (styledCell == null) return;
			
			if (DetailLabel != null && cell.DetailTextLabel != null) 
				if (DetailLabel.BackgroundColor != null) styledCell.StyledDetailBackgroundColor = DetailLabel.BackgroundColor;
			
			if (TextLabel.BackgroundColor != null) styledCell.StyledTextBackgroundColor = TextLabel.BackgroundColor;
			
			if (styledCell.AccessoryView == null) return;
			if (styledCell.Accessory == UITableViewCellAccessory.None || !styledCell.UseStyledAccessory) return;
			
			var disclosure = styledCell.AccessoryView as CellDisclosureAccessory;
			var detailDisclosure = styledCell.AccessoryView as CellDetailDisclosureAccessory;
			
			if (disclosure != null)
			{
				if (AccessoryCheckmark.HighlightColor != null) disclosure.HighlightColor = this.AccessoryCheckmark.HighlightColor;
				if (AccessoryCheckmark.TextColor != null) disclosure.Color = this.AccessoryCheckmark.TextColor;
			}
			
			if (detailDisclosure != null)
			{
				if (AccessoryCheckmark.HighlightColor != null) detailDisclosure.HighlightColor = this.AccessoryCheckmark.HighlightColor;
				if (AccessoryCheckmark.TextColor != null) detailDisclosure.Color = this.AccessoryCheckmark.TextColor;
				if (DetailedAccessoryElipse.HighlightColor != null) detailDisclosure.ElipseHighlightColor = this.DetailedAccessoryElipse.HighlightColor;
				if (DetailedAccessoryElipse.BackgroundColor != null) detailDisclosure.ElipseColor = this.DetailedAccessoryElipse.BackgroundColor;
			}
			
		}
		
	}

	public class StyleSheet: Dictionary<string, ElementStyle>
	{
		private static StyleSheet _defaultSheet = new StyleSheet();
		
		public StyleSheet () : base(StringComparer.InvariantCultureIgnoreCase) { }
	
		public static StyleSheet Default
		{
			get { return _defaultSheet; }
		}
		
		public ElementStyle Get(string key)
		{
			if (string.IsNullOrEmpty(key)) return null;
			
			ElementStyle style;
			TryGetValue(key, out style);
			return style;
		}
	}
}

