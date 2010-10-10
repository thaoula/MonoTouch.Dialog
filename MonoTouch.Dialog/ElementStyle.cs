using System;
using MonoTouch.UIKit;
using System.Collections.Generic;

namespace MonoTouch.Dialog
{
	public class ElementStyle
	{
		#region Font
		
		public UIFont TextFont;
		public UIFont DetailFont;
				
		#endregion
	
		#region TextAlignment
		
		public UITextAlignment? TextAlignment;
		public UITextAlignment? DetailAlignment;
		
		#endregion
		
		#region "Color"
		
		public UIColor BackgroundColor;
		public UIColor TextColor;
		public UIColor TextBackgroundColor;
		public UIColor TextHighlightColor;
		public UIColor DetailColor;
		public UIColor DetailBackgroundColor;
		public UIColor DetailHighlightColor;
		
		#endregion
		
		#region "General"
		
		public UITableViewCellAccessory? Accessory;
		
		#endregion
	
		public ElementStyle Inherit(ElementStyle inherited)
		{
			var style = new ElementStyle();
			
			style.TextFont = this.TextFont ?? inherited.TextFont;
			style.DetailFont = this.DetailFont ?? inherited.DetailFont;
			
			style.BackgroundColor = this.BackgroundColor ?? inherited.BackgroundColor;
			style.TextBackgroundColor = this.TextBackgroundColor ?? inherited.TextBackgroundColor;
			style.DetailBackgroundColor = this.DetailBackgroundColor ?? inherited.DetailBackgroundColor;
			style.DetailColor = this.DetailColor ?? inherited.DetailColor;
			style.TextColor = this.TextColor ?? inherited.TextColor;
			style.TextHighlightColor = this.TextHighlightColor ?? inherited.TextHighlightColor;
			style.DetailHighlightColor = this.DetailHighlightColor ?? inherited.DetailHighlightColor;
			style.TextFont = this.TextFont ?? inherited.TextFont;
			

			style.DetailAlignment = this.DetailAlignment.HasValue ? this.DetailAlignment.Value : inherited.DetailAlignment;
			style.TextAlignment = this.TextAlignment.HasValue ? this.TextAlignment.Value : inherited.TextAlignment;
			style.Accessory = this.Accessory.HasValue ? this.Accessory.Value : inherited.Accessory;
			
			return style;
		}
		
		public void Apply(UITableViewCell cell)
		{
			if (TextFont != null) cell.TextLabel.Font = TextFont;
			if (TextColor != null) cell.TextLabel.TextColor = TextColor;
			if (TextAlignment.HasValue) cell.TextLabel.TextAlignment = TextAlignment.Value;
			if (TextBackgroundColor != null) cell.TextLabel.BackgroundColor = TextBackgroundColor;
			if (TextHighlightColor != null) cell.TextLabel.HighlightedTextColor = TextHighlightColor;
			
			if (cell.DetailTextLabel != null) 
			{
				if (DetailFont != null) cell.DetailTextLabel.Font = DetailFont;
				if (DetailColor != null) cell.DetailTextLabel.TextColor = DetailColor;
				if (DetailAlignment.HasValue) cell.DetailTextLabel.TextAlignment = DetailAlignment.Value;
				if (DetailBackgroundColor != null) cell.DetailTextLabel.BackgroundColor = DetailBackgroundColor;
				if (DetailHighlightColor != null) cell.DetailTextLabel.HighlightedTextColor = DetailHighlightColor;
			}
			
			if (BackgroundColor != null)
				cell.BackgroundColor = BackgroundColor;
			
			if (Accessory.HasValue)
				cell.Accessory = Accessory.Value;

		}
	}
	
	public class StyleSheet: Dictionary<string, ElementStyle>
	{
		public StyleSheet () : base(StringComparer.InvariantCultureIgnoreCase)
		{
		}
		
		private static StyleSheet _defaultSheet = new StyleSheet();
		
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

